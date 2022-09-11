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
    public class SearchController : ApiController
    {
        private static Authenticate auth = new Authenticate();
        private FileExists fe = new FileExists("/Services.txt");
        private string folder = HttpContext.Current.Server.MapPath("~/App_Data");
        private List<Service> foundServices = new List<Service>();

        public object Get(int token,string search)
        {
            if (auth.authenticate.Validate(token).Equals("Validated"))
            {
                bool found = false;
                var text = File.ReadAllText(folder + "/Services.txt");
                List<Service> allServices = JsonConvert.DeserializeObject<List<Service>>(text);
                if (allServices == null)
                {
                    return "No services published";
                }
                foreach (Service s in allServices)
                {
                    if (s.name.ToLower().Contains(search) || s.description.ToLower().Contains(search) || s.APIEndpoint.ToLower().Contains(search) || s.NoOfOperands.ToString().Equals(search) || s.operandType.ToLower().Contains(search))
                    {
                        foundServices.Add(s);
                        found = true;
                    }
                }
                if (found)
                {
                    return foundServices;
                }
                return "not found";
            }
            else
            {
                Error error = new Error();
                return error;
            }
        }
    }
}
