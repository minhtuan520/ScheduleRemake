using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class Tkb
    {
        public int Id { get; set; }
        public string Hieuluc { get; set; }
        public string Gv { get; set; }
        public string Mh { get; set; }
        public string L { get; set; }
        public short Tiet { get; set; }
        public short Thu { get; set; }
    }
}
