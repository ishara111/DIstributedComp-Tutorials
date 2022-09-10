using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ServiceProvider.Controllers
{
    public class MulTwoNumbersController : ApiController
    {
        public int Get(int num1,int num2)
        {
            return num1*num2;
        }
    }
}
