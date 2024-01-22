using CleanOracleDB.Classes;
using CleanOracleDB.DAL;
using log4net;
using System.Reflection;

namespace CleanOracleDB
{
    public partial class MainForm : Form
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name);

        public MainForm()
        {
            InitializeComponent();
        }

        public void InitDatagrid()
        {
            dgvList.Columns.Clear();
            DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn()
            {
                Name = Settings.COL_CHECKED,
                FalseValue = 0,
                TrueValue = 1,
                IndeterminateValue = 0,
                Visible = true,
                HeaderText = "Select"
            };
            dgvList.Columns.Add(checkBoxColumn);

            DataGridViewTextBoxColumn column = new DataGridViewTextBoxColumn
            {
                Name = Settings.COL_TABLENAME,
                Visible = true,
                HeaderText = "Table name",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                ReadOnly = true,
                DataPropertyName = "Table_Name"
            };
            dgvList.Columns.Add(column);

            column = new DataGridViewTextBoxColumn
            {
                Name = Settings.COL_ROWCOUNT,
                Visible = true,
                HeaderText = "Row count",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                ReadOnly = true,
                DataPropertyName = "RowCount",
                DefaultCellStyle = new DataGridViewCellStyle()
                {
                    Format = "#,##0"
                }
            };
            dgvList.Columns.Add(column);

            column = new DataGridViewTextBoxColumn
            {
                Name = Settings.COL_SQL,
                Visible = true,
                HeaderText = "SQL",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                ReadOnly = true,
                DataPropertyName = "dropSQL"
            };
            dgvList.Columns.Add(column);

        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.Text += $" - v{Assembly.GetExecutingAssembly().GetName().Version}";
            log.Debug(this.Text);
            InitDatagrid();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            RefreshList();
        }

        private void RefreshList()
        {
            try
            {
                Settings.TNSNames = txtTNS.Text;
                Settings.DBUserName = txtDBUser.Text;
                Settings.DBPassword = txtDBPassword.Text;

                lblProgess.Text = "";
                progressBar1.Value = 0;
                btnCalcDBSizeBefore.Enabled = false;
                btnCalcDBSizeAfter.Enabled = false;
                btnCleanDB.Enabled = false;
                Cursor.Current = Cursors.WaitCursor;
                List<Modele.Tables> tablesList = Data.GetUnusedColumns();
                FillDataGridView(tablesList);
                btnCleanDB.Enabled = true;
                btnCalcDBSizeBefore.Enabled = true;
                btnCalcDBSizeAfter.Enabled = true;
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                log.Error(ex);
                Program.DisplayErrorMessage(ex);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void FillDataGridView(List<Modele.Tables> tablesList)
        {
            dgvList.DataSource = tablesList;
        }

        private void btnCleanDB_Click(object sender, EventArgs e)
        {
            int nbChecked = dgvList.Rows.Cast<DataGridViewRow>()
                               .Where(c => Convert.ToBoolean(c.Cells[Settings.COL_CHECKED].Value).Equals(true)).ToList().Count;
            progressBar1.Maximum = nbChecked;
            progressBar1.Value = 0;
            progressBar1.Refresh();
            foreach (DataGridViewRow row in dgvList.Rows)
            {
                if (row.Cells[Settings.COL_CHECKED].Value != null && (int)row.Cells[Settings.COL_CHECKED].Value == 1)
                {
                    lblProgess.Text = $"Delete columns in {row.Cells[Settings.COL_TABLENAME].Value.ToString()}";
                    log.Debug(SQL.GetDropColumnSQL(row.Cells[Settings.COL_TABLENAME].Value.ToString()));
                    //*** Check row number
                    if (((long)row.Cells[Settings.COL_ROWCOUNT].Value) >= Properties.Settings.Default.MAXLINES_ALERT)
                    {
                        string message = $"The table {row.Cells[Settings.COL_TABLENAME].Value} has more than {Properties.Settings.Default.MAXLINES_ALERT} lines." + Environment.NewLine
                        + "Do you want to drop unused columns from this table? (this may take some time).";
                        if (MessageBox.Show(message, System.AppDomain.CurrentDomain.FriendlyName, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                        {
                            break;
                        }
                    }
                    string sql = row.Cells[Settings.COL_SQL].Value.ToString();
                    try
                    {
                        log.Info($"Drop column for table {row.Cells[Settings.COL_TABLENAME].Value} with {row.Cells[Settings.COL_ROWCOUNT].Value.ToString()} rows started");
                        Data.DropColumn(sql);
                        log.Info($"Drop column for table {row.Cells[Settings.COL_TABLENAME].Value} finished");
                    }
                    catch (Exception ex)
                    {
                        log.Error(ex);
                        Program.DisplayErrorMessage(ex);
                        throw;
                    }
                    progressBar1.Value += 1;
                    progressBar1.Refresh();
                    System.Threading.Thread.Sleep(250);
                }
            }
            RefreshList();
            lblProgess.Text = "Columns deleted";
            progressBar1.Value = nbChecked;
        }

        private void btnTestConnection_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                ConnectionManager.TestConnection();
                MessageBox.Show("Connection successful", System.AppDomain.CurrentDomain.FriendlyName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                Program.DisplayErrorMessage(ex.Message);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void txtTNS_TextChanged(object sender, EventArgs e)
        {
            Settings.TNSNames = txtTNS.Text;
        }

        private void txtDBUser_TextChanged(object sender, EventArgs e)
        {
            Settings.DBUserName = txtDBUser.Text;
        }

        private void txtDBPassword_TextChanged(object sender, EventArgs e)
        {
            Settings.DBPassword = txtDBPassword.Text;
        }

        private void btnCalcDBSizeBefore_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                string result = Data.GetOracleDatabaseSize("Calculate database size before");
                MessageBox.Show(result, System.AppDomain.CurrentDomain.FriendlyName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                Program.DisplayErrorMessage(ex.Message);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void btnCalcDBSizeAfter_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                string result = Data.GetOracleDatabaseSize("Calculate database size after");
                MessageBox.Show(result, System.AppDomain.CurrentDomain.FriendlyName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                Program.DisplayErrorMessage(ex.Message);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

    }
}