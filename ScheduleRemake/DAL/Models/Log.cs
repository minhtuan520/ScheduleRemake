using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class Log
    {
        public int Id { get; set; }
        public string Hieuluc { get; set; }
        public string Gv { get; set; }
        public string Mh { get; set; }
        public string L { get; set; }
        public int Tiet { get; set; }
    }
}
