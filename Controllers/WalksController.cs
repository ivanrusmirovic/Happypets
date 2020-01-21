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
    public class WalksController : ApiController
    {
        private TestEntitiesnew db = new TestEntitiesnew();

        // GET: api/Walks
        public IQueryable<Walk> GetWalks()
        {
            return db.Walks;
        }

        // GET: api/Walks/5
        [ResponseType(typeof(Walk))]
        public IHttpActionResult GetWalk(int id)
        {
            Walk walk = db.Walks.Find(id);
            if (walk == null)
            {
                return NotFound();
            }

            return Ok(walk);
        }

        // PUT: api/Walks/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutWalk(int id, Walk walk)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != walk.Id)
            {
                return BadRequest();
            }

            db.Entry(walk).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WalkExists(id))
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

        // POST: api/Walks
        [ResponseType(typeof(Walk))]
        public IHttpActionResult PostWalk(Walk walk)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Walks.Add(walk);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (WalkExists(walk.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = walk.Id }, walk);
        }

        // DELETE: api/Walks/5
        [ResponseType(typeof(Walk))]
        public IHttpActionResult DeleteWalk(int id)
        {
            Walk walk = db.Walks.Find(id);
            if (walk == null)
            {
                return NotFound();
            }

            db.Walks.Remove(walk);
            db.SaveChanges();

            return Ok(walk);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool WalkExists(int id)
        {
            return db.Walks.Count(e => e.Id == id) > 0;
        }
    }
}