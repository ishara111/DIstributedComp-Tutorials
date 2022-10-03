using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessTierWebAPI.Models
{
    public partial class Accinfo
    {
        public int Id { get; set; }
        public string fname { get; set; }
        public string lname { get; set; }
        public int accno { get; set; }
        public int pin { get; set; }
        public decimal balance { get; set; }
        public string imageurl { get; set; }
    }
}