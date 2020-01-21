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
using HappyPets_v1._1.DTOs;
using HappyPets_v1._1.Models;


namespace HappyPets_v1._1.Controllers
{
    public class CaresController : ApiController
    {
        private TestEntitiesnew db = new TestEntitiesnew();

        // GET: api/Cares
        public IQueryable<Care> GetCares()
        {
            return db.Cares;
        }

        // GET: api/Cares/5
        /*
        [ResponseType(typeof(Care))]
        public IHttpActionResult GetCare(int id)
        {
            Care care = db.Cares.Find(id);
            if (care == null)
            {
                return NotFound();
            }

            return Ok(care);
        }
        */
        public List<Care>Get()
        {
            return db.Cares.ToList();
        }
        [ResponseType(typeof(CareDTO))]
        public CareDTO Get(int PetId)
        {
            return db.Cares.Where(c => c.PetId == PetId).Select(c => new CareDTO
            {
                Id = c.Id,
                Petid = c.PetId,
                Date = c.Date,
                CareType = c.CareType
            }).LastOrDefault();
        }
        
        // PUT: api/Cares/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCare(int id, Care care)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != care.Id)
            {
                return BadRequest();
            }

            db.Entry(care).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CareExists(id))
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

        // POST: api/Cares
        [ResponseType(typeof(Care))]
        public IHttpActionResult PostCare(Care care)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Cares.Add(care);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (CareExists(care.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = care.Id }, care);
        }

        // DELETE: api/Cares/5
        [ResponseType(typeof(Care))]
        public IHttpActionResult DeleteCare(int id)
        {
            Care care = db.Cares.Find(id);
            if (care == null)
            {
                return NotFound();
            }

            db.Cares.Remove(care);
            db.SaveChanges();

            return Ok(care);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CareExists(int id)
        {
            return db.Cares.Count(e => e.Id == id) > 0;
        }
    }
}