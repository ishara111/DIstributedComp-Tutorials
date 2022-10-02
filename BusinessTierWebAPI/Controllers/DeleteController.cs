using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BusinessTierWebAPI.Controllers
{
    public class DeleteController : ApiController
    {
        // GET: api/Delete
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Delete/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Delete
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Delete/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Delete/5
        public void Delete(int id)
        {
        }
    }
}
