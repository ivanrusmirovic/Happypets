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
using HappyPets_v1._1.Models;

namespace HappyPets_v1._1.Controllers
{
    public class FeedingsController : ApiController
    {
        private TestEntitiesnew db = new TestEntitiesnew();

        // GET: api/Feedings
        public IQueryable<Feeding> GetFeedings()
        {
            return db.Feedings;
        }

        // GET: api/Feedings/5
        [ResponseType(typeof(Feeding))]
        public IHttpActionResult GetFeeding(int id)
        {
            Feeding feeding = db.Feedings.Find(id);
            if (feeding == null)
            {
                return NotFound();
            }

            return Ok(feeding);
        }

        // PUT: api/Feedings/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutFeeding(int id, Feeding feeding)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != feeding.Id)
            {
                return BadRequest();
            }

            db.Entry(feeding).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FeedingExists(id))
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

        // POST: api/Feedings
        [ResponseType(typeof(Feeding))]
        public IHttpActionResult PostFeeding(Feeding feeding)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Feedings.Add(feeding);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (FeedingExists(feeding.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = feeding.Id }, feeding);
        }

        // DELETE: api/Feedings/5
        [ResponseType(typeof(Feeding))]
        public IHttpActionResult DeleteFeeding(int id)
        {
            Feeding feeding = db.Feedings.Find(id);
            if (feeding == null)
            {
                return NotFound();
            }

            db.Feedings.Remove(feeding);
            db.SaveChanges();

            return Ok(feeding);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool FeedingExists(int id)
        {
            return db.Feedings.Count(e => e.Id == id) > 0;
        }
    }
}