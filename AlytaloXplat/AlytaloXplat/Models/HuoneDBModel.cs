using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlytaloXplat.Models
{

    
   public class HuoneDBModel
    {
        [PrimaryKey, AutoIncrement]

        public int huoneId { get; set; }

        public string Nimi { get; set; }

        public int Valot { get; set; }

        public bool valonTila { get; set; }
    
        public int Himmennin { get; set; }

    }
}
