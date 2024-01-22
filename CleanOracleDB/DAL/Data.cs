using CleanOracleDB.Classes;
using Dapper;
using log4net;
using System.Text;

namespace CleanOracleDB.DAL
{
    internal class Data
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name);

        /// <summary>
        /// Return a list containing all unused columns in the database's schema
        /// </summary>
        /// <returns></returns>
        public static List<Modele.Tables> GetUnusedColumns()
        {
            List<Modele.Tables> tablesList;
            try
            {
                using (var connection = ConnectionManager.GetConnection())
                {
                    log.Debug(SQL.GetUnusedColumns());
                    tablesList = connection.Query<Modele.Tables>(SQL.GetUnusedColumns()).ToList<Modele.Tables>();
                    foreach (var column in tablesList)
                    {
                        log.Debug(SQL.GetRowCount(column.Table_Name));
                        column.RowCount = connection.ExecuteScalar<long>(SQL.GetRowCount(column.Table_Name));
                        column.DropSQL = SQL.GetDropColumnSQL(column.Table_Name);
                    }
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw;
            }

            return tablesList;
        }

        /// <summary>
        /// Drop the table column
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static bool DropColumn(string sql)
        {
            try
            {
                using (var connection = ConnectionManager.GetConnection())
                {
                    var watch = System.Diagnostics.Stopwatch.StartNew();
                    log.Info($"Start drop column");
                    log.Debug(sql);
                    connection.Execute(sql);
                    watch.Stop();
                    log.Info($"End drop column : {watch.Elapsed.ToString(@"m\:ss\.fff")}");
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw;
            }
            return true;
        }

        public static string GetOracleDatabaseSize(string action)
        {
            StringBuilder builder = new StringBuilder();
            log.Info(action);
            builder.AppendLine(action);
            using (var connection = ConnectionManager.GetConnection())
            {
                string sql = SQL.GetOracleDatabaseSize();
                var watch = System.Diagnostics.Stopwatch.StartNew();
                log.Debug(sql);
                List<DatabaseSize> databaseSizes = connection.Query<DatabaseSize>(sql).ToList();
                foreach (var databaseSize in databaseSizes) 
                {
                    string value = $"TableSpace : {databaseSize.TableSpace}, Total space : {databaseSize.Total_Space_MB}, User space : {databaseSize.Used_Space_MB}, Free space : {databaseSize.Free_Space_MB}, Pourcentage free : {databaseSize.Pct_Free}";
                    log.Info(value);
                    builder.AppendLine(value);
                }
                connection.Close();
            }
            return builder.ToString();
        }
    }
}
