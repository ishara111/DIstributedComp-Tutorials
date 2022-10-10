using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    public class NextDateController : ApiController
    {
        private DatabaseEntities1 db = new DatabaseEntities1();
        // GET: api/NextDate
        public IHttpActionResult Get(int id)
        {
            List<Booking> list = new List<Booking>(db.Bookings);
            ControllerMethods conmethods = new ControllerMethods(id, list);
            DateTime nextdate = conmethods.GetNextAvailableDate();
            if (nextdate!= new DateTime(1, 1, 1, 1, 1, 1))
            {
                return Ok(nextdate);
            }
            else
            {
                return NotFound();
            }
        }
    }
}
