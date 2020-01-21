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
    public class ToiletsController : ApiController
    {
        private TestEntitiesnew db = new TestEntitiesnew();

        // GET: api/Toilets
        public IQueryable<Toilet> GetToilets()
        {
            return db.Toilets;
        }
        public List<Toilet>Get()
        {
            return db.Toilets.ToList();
        }
        [ResponseType(typeof(ToiletDTO))]

        public ToiletDTO Get(int PetId)
        {
            return db.Toilets.Where(t => t.PetId == PetId).Select(t => new ToiletDTO
                {
                    Id = t.Id,
                    PetId = t.PetId,
                    Type = t.Type,
                    ToiletType = t.ToiletType,
                    Pet = t.Pet
                }).FirstOrDefault();
        }


        // GET: api/Toilets/5
        [ResponseType(typeof(Toilet))]
        public IHttpActionResult GetToilet(int id)
        {
            Toilet toilet = db.Toilets.Find(id);
            if (toilet == null)
            {
                return NotFound();
            }

            return Ok(toilet);
        }

        // PUT: api/Toilets/5
        [ResponseType(typeof(void))]
        [Route("api/Toilets/{Id}")]
        public IHttpActionResult PutToilet(int id,[FromBody] Toilet toilet)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != toilet.Id)
            {
                return BadRequest();
            }

            db.Entry(toilet).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ToiletExists(id))
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

        // POST: api/Toilets
        [ResponseType(typeof(Toilet))]
        public IHttpActionResult PostToilet(Toilet toilet)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Toilets.Add(toilet);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (ToiletExists(toilet.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = toilet.Id }, toilet);
        }

        // DELETE: api/Toilets/5
        [ResponseType(typeof(Toilet))]
        public IHttpActionResult DeleteToilet(int id)
        {
            Toilet toilet = db.Toilets.Find(id);
            if (toilet == null)
            {
                return NotFound();
            }

            db.Toilets.Remove(toilet);
            db.SaveChanges();

            return Ok(toilet);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ToiletExists(int id)
        {
            return db.Toilets.Count(e => e.Id == id) > 0;
        }
    }
}