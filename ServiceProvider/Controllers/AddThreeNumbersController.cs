using ServiceProvider.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ServiceProvider.Controllers
{
    public class AddThreeNumbersController : ApiController
    {
        static Authenticate auth = new Authenticate();
        public object Get(int token,int num1,int num2,int num3)
        {
            if (auth.authenticate.Validate(token).Equals("Validated"))
            {
                return num1 + num2 + num3;
            }
            else
            {
                Error error = new Error();
                return error;
            }
        }

    }
}
