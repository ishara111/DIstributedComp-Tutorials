using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    public class CentreBookingsController : ApiController
    {
        private DatabaseEntities1 db= new DatabaseEntities1();


        // GET: api/CentreBookings/5
        public IHttpActionResult Get(int id)
        {
            List<Booking> list = new List<Booking>(db.Bookings);
            ControllerMethods conmethods = new ControllerMethods(id, list);
            List<Booking> clist = conmethods.GetBookingsByCentre();
            if (clist.Count>0)
            {
                return Ok(clist);
            }
            else
            {
                return NotFound();
            }
        }
    }
}
