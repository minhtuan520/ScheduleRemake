using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class Change
    {
        public string User { get; set; }
        public string Action { get; set; }
        public string Target { get; set; }
        public string Time { get; set; }
    }
}
