using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using WebServer.Models;

namespace WebServer.Controllers
{
    public class JobstateController : ApiController
    {
        private DatabaseEntities1 db = new DatabaseEntities1();

        // GET: api/Jobstate
        public List<Jobstate> GetJobstates()
        {
            var list = new List<Jobstate>(db.Jobstates);
            return list;
        }

        // GET: api/Jobstate/5
        [ResponseType(typeof(Jobstate))]
        public IHttpActionResult GetJobstate(int id)
        {
            Jobstate jobstate = db.Jobstates.Find(id);
            if (jobstate == null)
            {
                return NotFound();
            }

            return Ok(jobstate);
        }

        // PUT: api/Jobstate/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutJobstate(int id, Jobstate jobstate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != jobstate.Id)
            {
                return BadRequest();
            }

            db.Entry(jobstate).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JobstateExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Jobstate
        [ResponseType(typeof(Jobstate))]
        public IHttpActionResult PostJobstate(Jobstate jobstate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Jobstates.Add(jobstate);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (JobstateExists(jobstate.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = jobstate.Id }, jobstate);
        }

        // DELETE: api/Jobstate/5
        [ResponseType(typeof(Jobstate))]
        public IHttpActionResult DeleteJobstate(int id)
        {
            Jobstate jobstate = db.Jobstates.Find(id);
            if (jobstate == null)
            {
                return NotFound();
            }

            db.Jobstates.Remove(jobstate);
            db.SaveChanges();

            return Ok(jobstate);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool JobstateExists(int id)
        {
            return db.Jobstates.Count(e => e.Id == id) > 0;
        }
    }
}