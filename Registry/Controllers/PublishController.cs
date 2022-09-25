/* Name: Ishara Gomes
 * ID: 20534521
 * 
 * Description: publish controller
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
    public class PublishController : ApiController
    {
        private static Authenticate auth = new Authenticate();
        private FileExists fe = new FileExists("/Services.txt");
        private List<Service> list = new List<Service>();
        private string folder = HttpContext.Current.Server.MapPath("~/App_Data");

        public IHttpActionResult Post([FromUri] int token, [FromBody] Service service)
        {
            ServiceMethods services = new ServiceMethods(token,service,folder,list,auth);
            Task<object> task = new Task<object>(services.Publish);//calls publish asynchronously
            task.Start();
            return Ok(task.Result);
        }

    }
}
