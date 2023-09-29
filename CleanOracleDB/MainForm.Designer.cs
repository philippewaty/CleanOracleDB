namespace CleanOracleDB
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            btnRefresh = new Button();
            dgvList = new DataGridView();
            btnCleanDB = new Button();
            toolTip1 = new ToolTip(components);
            btnTestConnection = new Button();
            grpDatabase = new GroupBox();
            txtDBPassword = new TextBox();
            lblDBPassword = new Label();
            txtDBUser = new TextBox();
            lblDBUser = new Label();
            txtTNS = new TextBox();
            lblTNS = new Label();
            lblProgess = new Label();
            progressBar1 = new ProgressBar();
            ((System.ComponentModel.ISupportInitialize)dgvList).BeginInit();
            grpDatabase.SuspendLayout();
            SuspendLayout();
            // 
            // btnRefresh
            // 
            btnRefresh.Image = Properties.Resources.view_refresh;
            btnRefresh.Location = new Point(12, 100);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(42, 42);
            btnRefresh.TabIndex = 1;
            btnRefresh.UseVisualStyleBackColor = true;
            btnRefresh.Click += btnRefresh_Click;
            // 
            // dgvList
            // 
            dgvList.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvList.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvList.Location = new Point(12, 148);
            dgvList.Name = "dgvList";
            dgvList.RowTemplate.Height = 25;
            dgvList.Size = new Size(969, 272);
            dgvList.TabIndex = 3;
            // 
            // btnCleanDB
            // 
            btnCleanDB.Enabled = false;
            btnCleanDB.Image = Properties.Resources.db_remove;
            btnCleanDB.Location = new Point(60, 100);
            btnCleanDB.Name = "btnCleanDB";
            btnCleanDB.Size = new Size(42, 42);
            btnCleanDB.TabIndex = 2;
            toolTip1.SetToolTip(btnCleanDB, "Supprimer les colonnes sélectionnées");
            btnCleanDB.UseVisualStyleBackColor = true;
            btnCleanDB.Click += btnCleanDB_Click;
            // 
            // btnTestConnection
            // 
            btnTestConnection.Image = Properties.Resources.database_server;
            btnTestConnection.Location = new Point(379, 27);
            btnTestConnection.Name = "btnTestConnection";
            btnTestConnection.Size = new Size(42, 42);
            btnTestConnection.TabIndex = 6;
            toolTip1.SetToolTip(btnTestConnection, "Tester la connexion");
            btnTestConnection.UseVisualStyleBackColor = true;
            btnTestConnection.Click += btnTestConnection_Click;
            // 
            // grpDatabase
            // 
            grpDatabase.Controls.Add(btnTestConnection);
            grpDatabase.Controls.Add(txtDBPassword);
            grpDatabase.Controls.Add(lblDBPassword);
            grpDatabase.Controls.Add(txtDBUser);
            grpDatabase.Controls.Add(lblDBUser);
            grpDatabase.Controls.Add(txtTNS);
            grpDatabase.Controls.Add(lblTNS);
            grpDatabase.Location = new Point(12, 12);
            grpDatabase.Name = "grpDatabase";
            grpDatabase.Size = new Size(434, 82);
            grpDatabase.TabIndex = 0;
            grpDatabase.TabStop = false;
            grpDatabase.Text = "Base de données";
            // 
            // txtDBPassword
            // 
            txtDBPassword.Location = new Point(273, 46);
            txtDBPassword.Name = "txtDBPassword";
            txtDBPassword.Size = new Size(100, 23);
            txtDBPassword.TabIndex = 5;
            txtDBPassword.TextChanged += txtDBPassword_TextChanged;
            // 
            // lblDBPassword
            // 
            lblDBPassword.AutoSize = true;
            lblDBPassword.Location = new Point(169, 49);
            lblDBPassword.Name = "lblDBPassword";
            lblDBPassword.Size = new Size(98, 15);
            lblDBPassword.TabIndex = 4;
            lblDBPassword.Text = "Mot de passe DB:";
            // 
            // txtDBUser
            // 
            txtDBUser.Location = new Point(63, 46);
            txtDBUser.Name = "txtDBUser";
            txtDBUser.Size = new Size(100, 23);
            txtDBUser.TabIndex = 3;
            txtDBUser.TextChanged += txtDBUser_TextChanged;
            // 
            // lblDBUser
            // 
            lblDBUser.AutoSize = true;
            lblDBUser.Location = new Point(6, 49);
            lblDBUser.Name = "lblDBUser";
            lblDBUser.Size = new Size(51, 15);
            lblDBUser.TabIndex = 2;
            lblDBUser.Text = "User DB:";
            // 
            // txtTNS
            // 
            txtTNS.Location = new Point(63, 18);
            txtTNS.Name = "txtTNS";
            txtTNS.Size = new Size(100, 23);
            txtTNS.TabIndex = 1;
            txtTNS.TextChanged += txtTNS_TextChanged;
            // 
            // lblTNS
            // 
            lblTNS.AutoSize = true;
            lblTNS.Location = new Point(6, 21);
            lblTNS.Name = "lblTNS";
            lblTNS.Size = new Size(31, 15);
            lblTNS.TabIndex = 0;
            lblTNS.Text = "TNS:";
            // 
            // lblProgess
            // 
            lblProgess.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            lblProgess.AutoSize = true;
            lblProgess.Location = new Point(11, 452);
            lblProgess.Name = "lblProgess";
            lblProgess.Size = new Size(38, 15);
            lblProgess.TabIndex = 4;
            lblProgess.Text = "label1";
            // 
            // progressBar1
            // 
            progressBar1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            progressBar1.Location = new Point(11, 426);
            progressBar1.Name = "progressBar1";
            progressBar1.Size = new Size(970, 23);
            progressBar1.TabIndex = 5;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(991, 476);
            Controls.Add(progressBar1);
            Controls.Add(lblProgess);
            Controls.Add(grpDatabase);
            Controls.Add(btnCleanDB);
            Controls.Add(dgvList);
            Controls.Add(btnRefresh);
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Clean Oracle DB";
            Load += MainForm_Load;
            ((System.ComponentModel.ISupportInitialize)dgvList).EndInit();
            grpDatabase.ResumeLayout(false);
            grpDatabase.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnRefresh;
        private DataGridView dgvList;
        private Button btnCleanDB;
        private ToolTip toolTip1;
        private GroupBox grpDatabase;
        private TextBox txtDBPassword;
        private Label lblDBPassword;
        private TextBox txtDBUser;
        private Label lblDBUser;
        private TextBox txtTNS;
        private Label lblTNS;
        private Button btnTestConnection;
        private Label lblProgess;
        private ProgressBar progressBar1;
    }
}