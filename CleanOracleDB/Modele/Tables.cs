namespace CleanOracleDB.Modele
{
    internal class Tables
    {
        public string Table_Name { get; set; }
        public long RowCount { get; set; } = 0;
        public string DropSQL { get; set; }
    }
}
