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
    public class UnPublishController : ApiController
    {
        private static Authenticate auth = new Authenticate();
        private FileExists fe = new FileExists("/Services.txt");
        private string folder = HttpContext.Current.Server.MapPath("~/App_Data");
        private static List<Service> removeServices = new List<Service>();

        public object Get(int token, string endpoint)
        {
            if (auth.authenticate.Validate(token).Equals("Validated"))
            {
                bool remove = false;
                var text = File.ReadAllText(folder + "/Services.txt");
                List<Service> allServices = JsonConvert.DeserializeObject<List<Service>>(text);
                if (allServices == null)
                {
                    return "No services published";
                }
                foreach (Service s in allServices)
                {
                    if (s.APIEndpoint.ToLower().Equals(endpoint))
                    {
                        removeServices.Add(s);
                        remove = true;
                    }
                }
                foreach (Service s in removeServices)
                {
                    allServices.Remove(s);
                }
                if (remove)
                {
                    string json = JsonConvert.SerializeObject(allServices, Formatting.Indented);
                    File.WriteAllText(folder + "/Services.txt", json);
                }
                else
                {
                    return "endpoint not found";
                }

                return allServices;
            }
            else
            {
                Error error = new Error();
                return error;
            }
        }
    }
}
