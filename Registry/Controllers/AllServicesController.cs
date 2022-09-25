/* Name: Ishara Gomes
 * ID: 20534521
 * 
 * Description: allservices controller
 */
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
    public class AllServicesController : ApiController
    {
        private static Authenticate auth = new Authenticate();
        private FileExists fe = new FileExists("/Services.txt");
        private string folder = HttpContext.Current.Server.MapPath("~/App_Data");

        public IHttpActionResult Get(int token)
        {
            ServiceMethods services = new ServiceMethods(token,folder,auth);
            Task<object> task = new Task<object>(services.AllServices);//calls allservices asynchronously
            task.Start();
            return Ok(task.Result);
        }
    }
}
