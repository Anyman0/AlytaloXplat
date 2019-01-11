using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlytaloXplat.Models
{
    public class TempDBModel
    {

        [PrimaryKey, AutoIncrement]
        public int LämpötilaId { get; set; }

        public int TalonLämpötila { get; set; }

        public int Kosteusprosentti { get; set; }

        public DateTime Päivämäärä { get; set; }

    }
}
 