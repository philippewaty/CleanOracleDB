using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanOracleDB.Classes
{
    internal class DatabaseSize
    {
        public string TableSpace { get; set; }
        public int Total_Space_MB { get; set; }
        public int Used_Space_MB { get; set; }
        public int Free_Space_MB { get; set; }
        /// <summary>
        /// Pourcentage free
        /// </summary>
        public double Pct_Free { get; set; }
    }
}
