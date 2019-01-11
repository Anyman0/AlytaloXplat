using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlytaloXplat.Models
{
    public class SaunaDBModel
    {
        [PrimaryKey, AutoIncrement]
        public int SaunaId { get; set; }

        public string SaunanNimi { get; set; }

        public bool SaunanTila { get; set; }

        public int ToivottuTemp { get; set; }
    }
}
