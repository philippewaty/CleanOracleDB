using CleanOracleDB.Classes;
using Dapper;
using log4net;

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
                    log.Debug(sql);
                    connection.Execute(sql);
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
    }
}
