using ServiceProvider.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace ServiceProvider.Controllers
{
    public class MulThreeNumbersController : ApiController
    {
        static Authenticate auth = new Authenticate();
        public IHttpActionResult Get(int token,int num1,int num2,int num3)
        {
            ServiceMethods service = new ServiceMethods(token, num1, num2, num3, auth);
            Task<object> task = new Task<object>(service.MulThreeNumbers);
            task.Start();
            return Ok(task.Result);
        }
    }
}
