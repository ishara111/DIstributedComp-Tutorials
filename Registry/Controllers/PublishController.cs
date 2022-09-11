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
    public class PublishController : ApiController
    {
        private static Authenticate auth = new Authenticate();
        private static List<Service> list = new List<Service>();
        private string folder = HttpContext.Current.Server.MapPath("~/App_Data");

        public object Post([FromUri] int token, [FromBody] Service service)
        {
            if (auth.authenticate.Validate(token).Equals("Validated"))
            {
                list.Add(service);
                string json = JsonConvert.SerializeObject(list, Formatting.Indented);
                File.WriteAllText(folder + "/Services.txt", json);

                return "Successfully published";
            }
            else
            {
                Error error = new Error();
                return error;
            }
        }

    }
}
