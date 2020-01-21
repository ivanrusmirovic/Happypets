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
    public class BreedsController : ApiController
    {
        private TestEntitiesnew db = new TestEntitiesnew();

        // GET: api/Breeds
        [Route("api/GetBreed")]
        public IQueryable<Breed> GetBreeds()
        {
            return db.Breeds;
        }

        public List<Breed> Get()
        {
            return db.Breeds.ToList();
        }
        [ResponseType(typeof(BreedDTO))]
        
        public BreedDTO Get(int Id)
        {
            return db.Breeds.Where(b => b.Id == Id).Select(b => new BreedDTO
            {
                Id = b.Id,
                Breed1 = b.Breed1
            }).FirstOrDefault();
        }



        // GET: api/Breeds/5
        [ResponseType(typeof(Breed))]
        public IHttpActionResult GetBreed(int id)
        {
            Breed breed = db.Breeds.Find(id);
            if (breed == null)
            {
                return NotFound();
            }

            return Ok(breed);
        }

        // PUT: api/Breeds/5
        [ResponseType(typeof(Breed))]
        public IHttpActionResult PutBreed(int id, [FromBody]Breed breed)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != breed.Id)
            {
                return BadRequest();
            }

            db.Entry(breed).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BreedExists(id))
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

        // POST: api/Breeds
        [ResponseType(typeof(Breed))]
        public IHttpActionResult PostBreed(Breed breed)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Breeds.Add(breed);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (BreedExists(breed.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = breed.Id }, breed);
        }

        // DELETE: api/Breeds/5
        [ResponseType(typeof(Breed))]
        public IHttpActionResult DeleteBreed(int id)
        {
            Breed breed = db.Breeds.Find(id);
            if (breed == null)
            {
                return NotFound();
            }

            db.Breeds.Remove(breed);
            db.SaveChanges();

            return Ok(breed);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BreedExists(int id)
        {
            return db.Breeds.Count(e => e.Id == id) > 0;
        }
    }
}