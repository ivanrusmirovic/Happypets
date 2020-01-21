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
    public class ToiletTypesController : ApiController
    {
        private TestEntitiesnew db = new TestEntitiesnew();

        // GET: api/ToiletTypes
        public IQueryable<ToiletType> GetToiletTypes()
        {
            return db.ToiletTypes;
        }

        // GET: api/ToiletTypes/5
        [ResponseType(typeof(ToiletType))]
        public IHttpActionResult GetToiletType(int id)
        {
            ToiletType toiletType = db.ToiletTypes.Find(id);
            if (toiletType == null)
            {
                return NotFound();
            }

            return Ok(toiletType);
        }

        // PUT: api/ToiletTypes/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutToiletType(int id, ToiletType toiletType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != toiletType.Id)
            {
                return BadRequest();
            }

            db.Entry(toiletType).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ToiletTypeExists(id))
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

        // POST: api/ToiletTypes
        [ResponseType(typeof(ToiletType))]
        public IHttpActionResult PostToiletType(ToiletType toiletType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ToiletTypes.Add(toiletType);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (ToiletTypeExists(toiletType.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = toiletType.Id }, toiletType);
        }

        // DELETE: api/ToiletTypes/5
        [ResponseType(typeof(ToiletType))]
        public IHttpActionResult DeleteToiletType(int id)
        {
            ToiletType toiletType = db.ToiletTypes.Find(id);
            if (toiletType == null)
            {
                return NotFound();
            }

            db.ToiletTypes.Remove(toiletType);
            db.SaveChanges();

            return Ok(toiletType);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ToiletTypeExists(int id)
        {
            return db.ToiletTypes.Count(e => e.Id == id) > 0;
        }
    }
}