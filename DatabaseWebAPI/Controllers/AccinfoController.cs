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
using DatabaseWebAPI.Models;

namespace DatabaseWebAPI.Controllers
{
    public class AccinfoController : ApiController
    {
        private DatabaseEntitiesNew db = new DatabaseEntitiesNew();

        // GET: api/Accinfoes
        public IQueryable<Accinfo> GetAccinfoes()
        {
            return db.Accinfoes;
        }

        // GET: api/Accinfoes/5
        [ResponseType(typeof(Accinfo))]
        public IHttpActionResult GetAccinfo(int id)
        {
            Accinfo accinfo = db.Accinfoes.Find(id);
            if (accinfo == null)
            {
                return NotFound();
            }

            return Ok(accinfo);
        }

        // PUT: api/Accinfoes/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAccinfo(int id, Accinfo accinfo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != accinfo.Id)
            {
                return BadRequest();
            }

            db.Entry(accinfo).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AccinfoExists(id))
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

        // POST: api/Accinfoes
        [ResponseType(typeof(Accinfo))]
        public IHttpActionResult PostAccinfo(Accinfo accinfo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Accinfoes.Add(accinfo);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (AccinfoExists(accinfo.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = accinfo.Id }, accinfo);
        }

        // DELETE: api/Accinfoes/5
        [ResponseType(typeof(Accinfo))]
        public IHttpActionResult DeleteAccinfo(int id)
        {
            Accinfo accinfo = db.Accinfoes.Find(id);
            if (accinfo == null)
            {
                return NotFound();
            }

            db.Accinfoes.Remove(accinfo);
            db.SaveChanges();

            return Ok(accinfo);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AccinfoExists(int id)
        {
            return db.Accinfoes.Count(e => e.Id == id) > 0;
        }
    }
}