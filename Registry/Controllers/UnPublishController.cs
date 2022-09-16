using Newtonsoft.Json;
using Registry.Models;
using RegistryClasses;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace Registry.Controllers
{
    public class UnPublishController : ApiController
    {
        private static Authenticate auth = new Authenticate();
        private FileExists fe = new FileExists("/Services.txt");
        private string folder = HttpContext.Current.Server.MapPath("~/App_Data");
        private List<Service> removeServices = new List<Service>();

        public IHttpActionResult Delete(int token, string endpoint)
        {
            ServiceMethods services = new ServiceMethods(token,endpoint,removeServices,folder,auth);
            Task<object> task = new Task<object>(services.UnPublish);
            task.Start();
            return Ok(task.Result);
        }
    }
}
