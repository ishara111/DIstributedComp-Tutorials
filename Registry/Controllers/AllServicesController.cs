using Newtonsoft.Json;
using Registry.Models;
using RegistryClasses;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace Registry.Controllers
{
    public class AllServicesController : ApiController
    {
        private static Authenticate auth = new Authenticate();
        private FileExists fe = new FileExists("/Services.txt");
        private string folder = HttpContext.Current.Server.MapPath("~/App_Data");

        public object Get(int token)
        {
            if (auth.authenticate.Validate(token).Equals("Validated"))
            {
                var text = File.ReadAllText(folder + "/Services.txt");
                List<Service> list = JsonConvert.DeserializeObject<List<Service>>(text);
                if (list == null)
                {
                    return "No services published";
                }

                return list;
            }
            else
            {
                Error error = new Error();
                return error;
            }
        }
    }
}
