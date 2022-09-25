﻿/* Name: Ishara Gomes
 * ID: 20534521
 * 
 * Description: mul2nums controller
 */
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
    public class MulTwoNumbersController : ApiController
    {
        static Authenticate auth = new Authenticate();
        public IHttpActionResult Get(int token,int num1,int num2) 
        {
            ServiceMethods service = new ServiceMethods(token, num1, num2, 0, auth);//calls multwonumbers asynchronously
            Task<object> task = new Task<object>(service.MulTwoNumbers);
            task.Start();
            return Ok(task.Result);

        }
    }
}
