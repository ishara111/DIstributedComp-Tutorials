/* Name: Ishara Gomes
 * ID: 20534521
 * 
 * Description: check if service file exist of not creates one
 */
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Registry.Models
{
    public class FileExists
    {
        private string folder = HttpContext.Current.Server.MapPath("~/App_Data");
        public FileExists(string filename)
        {
            if (!File.Exists(folder+filename))
            {
                using (StreamWriter w = File.CreateText(folder+filename)) { }
            }
        }
    }
}