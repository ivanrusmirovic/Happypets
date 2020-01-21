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
    public class CareTypesController : ApiController
    {
        private TestEntitiesnew db = new TestEntitiesnew();

        // GET: api/CareTypes
        public IQueryable<CareType> GetCareTypes()
        {
            return db.CareTypes;
        }

        // GET: api/CareTypes/5
        [ResponseType(typeof(CareType))]
        public IHttpActionResult GetCareType(int id)
        {
            CareType careType = db.CareTypes.Find(id);
            if (careType == null)
            {
                return NotFound();
            }

            return Ok(careType);
        }

        // PUT: api/CareTypes/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCareType(int id, CareType careType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != careType.Id)
            {
                return BadRequest();
            }

            db.Entry(careType).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CareTypeExists(id))
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

        // POST: api/CareTypes
        [ResponseType(typeof(CareType))]
        public IHttpActionResult PostCareType(CareType careType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.CareTypes.Add(careType);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (CareTypeExists(careType.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = careType.Id }, careType);
        }

        // DELETE: api/CareTypes/5
        [ResponseType(typeof(CareType))]
        public IHttpActionResult DeleteCareType(int id)
        {
            CareType careType = db.CareTypes.Find(id);
            if (careType == null)
            {
                return NotFound();
            }

            db.CareTypes.Remove(careType);
            db.SaveChanges();

            return Ok(careType);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CareTypeExists(int id)
        {
            return db.CareTypes.Count(e => e.Id == id) > 0;
        }
    }
}