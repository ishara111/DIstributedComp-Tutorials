using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Registry.Models
{
    public class Error
    {
        public string status { get; set; }
        public string reason { get; set; }

        public Error()
        {
            status = "Denied";
            reason = "Authentication Error";
        }
    }
}