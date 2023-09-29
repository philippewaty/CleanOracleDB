namespace CleanOracleDB.Classes
{
    internal static class SQL
    {
        /// <summary>
        /// Returns the unused columns SQL
        /// </summary>
        /// <returns></returns>
        public static string GetUnusedColumns()
        {
            string sql = "SELECT table_name"
            + " FROM user_unused_col_tabs"
            + " WHERE table_name not like 'BIN$%==$0'" //We don't take tables from RECYLCE BIN
            + " ORDER BY table_name";

            return sql;
        }

        /// <summary>
        /// Returns the table's rowcount SQL
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static string GetRowCount(string tableName)
        {
            string sql = $"SELECT COUNT(*) FROM {tableName}";

            return sql;
        }

        /// <summary>
        /// Returns the drop column SQL
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static string GetDropColumnSQL(string tableName)
        {
            string sql = $"ALTER TABLE {tableName} DROP UNUSED COLUMNS";

            return sql;
        }

    }
}
