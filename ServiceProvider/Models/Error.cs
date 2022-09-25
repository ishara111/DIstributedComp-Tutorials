/* Name: Ishara Gomes
 * ID: 20534521
 * 
 * Description: Error when not validated
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServiceProvider.Models
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