using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI.Models
{
    public class ControllerMethods
    {
        private int id;
        private Booking booking;
        private List<Booking> list;

        public ControllerMethods(Booking booking, List<Booking> list)
        {
            this.booking = booking;
            this.list = list;
        }

        public ControllerMethods(int id, List<Booking> list)
        {
            this.id= id;
            this.list = list;
        }

        public bool IsBooked()
        {
            bool booked = false;
            foreach (Booking b in list)
            {
                if (b.centreId==booking.centreId)
                {
                    if ((booking.startDate>=b.startDate && booking.startDate<=b.endDate) 
                        || (booking.endDate>=b.startDate && booking.endDate<=b.endDate))
                    {
                        booked = true;
                    }
                }
            }
            return booked;
        }

        public DateTime GetNextAvailableDate()
        {
            DateTime nextAvailableDate = new DateTime(1,1,1,1,1,1);
            DateTime endDate;
            foreach (Booking b in list)
            {
                if (b.centreId==id)
                {
                    endDate = (DateTime)b.endDate;
                    nextAvailableDate = endDate.AddDays(1);
                }
            }
            return nextAvailableDate;
        }

        public List<Booking> GetBookingsByCentre()
        {
            List<Booking> clist = new List<Booking>();
            foreach (Booking b in list)
            {
                if (b.centreId==id)
                {
                    clist.Add(b);
                }
            }
            return clist;
        }
    }
}