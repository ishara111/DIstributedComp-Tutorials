using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebAPI.Controllers
{
    public class LoginController : ApiController
    {

        // POST: api/Login
        public IHttpActionResult Post(string username,string password)
        {
            if (username.Equals("admin") && password.Equals("adminpass"))
            {
                return Ok("logged in");
            }
            else
            {
                return Ok("incorrect username or password");
            }
        }

    }
}
