using System.Data;

namespace CleanOracleDB.Classes
{
    internal class ConnectionManager
    {
        private static string connString = "Data Source={0};User Id={1};Password={2};";

        private static IDbConnection GetDbConnection(string connectionString)
        {
            return new Oracle.ManagedDataAccess.Client.OracleConnection(connectionString);
        }

        public static IDbConnection GetConnection()
        {
            var conn = GetDbConnection(string.Format(connString, Settings.TNSNames, Settings.DBUserName, Settings.DBPassword));
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            return conn;
        }

        public static void CloseConnection(IDbConnection conn)
        {
            if (conn.State == ConnectionState.Open || conn.State == ConnectionState.Broken)
            {
                conn.Close();
            }
        }

        public static bool TestConnection()
        {
            try
            {
                using (var connection = ConnectionManager.GetConnection()){
                    connection.Close();
                }

            }
            catch (Exception)
            {
                throw;
            }
            return true;
        }

    }
}
