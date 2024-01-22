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

        public static string GetOracleDatabaseSize()
        {
            string sql = "SELECT df.tablespace_name TABLESPACE, df.total_space_mb TOTAL_SPACE_MB,"
                + "(df.total_space_mb - fs.free_space_mb) USED_SPACE_MB,"
                + "fs.free_space_mb FREE_SPACE_MB,"
                + "ROUND(100 * (fs.free_space / df.total_space),2) PCT_FREE"
                + " FROM (SELECT tablespace_name, SUM(bytes) TOTAL_SPACE,"
                + "   ROUND(SUM(bytes) / 1048576) TOTAL_SPACE_MB"
                + "   FROM dba_data_files"
                + "   GROUP BY tablespace_name) df,"
                + "  (SELECT tablespace_name, SUM(bytes) FREE_SPACE,"
                + "    ROUND(SUM(bytes) / 1048576) FREE_SPACE_MB"
                + "    FROM dba_free_space"
                + "    GROUP BY tablespace_name) fs"
                + " WHERE df.tablespace_name = fs.tablespace_name(+)"
                + " ORDER BY fs.tablespace_name";

            return sql;
        }
    }
}
