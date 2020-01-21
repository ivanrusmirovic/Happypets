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
    public class PetsController : ApiController
    {
        private TestEntitiesnew db = new TestEntitiesnew();

        //GET: api/Pets
       [Route("api/Pets)")]
        public IQueryable<Pet> GetPets()
        {
            return db.Pets;
        }

        //// GET: api/Pets/5
        //[Route("api/Pets/{Id})")]
        //[ResponseType(typeof(Pet))]
        //public IHttpActionResult GetPet(int id)
        //{
        //    Pet pet = db.Pets.Find(id);
        //    if (pet == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(pet);
        //}





        //public List<Pet> Get()
        //{
        //    return db.Pets.ToList();
        //}
        [Route ("api/Pets/{Name}")]
       [ResponseType(typeof(PetsDTO))]
       public PetsDTO Get(string Name)
       {
           return db.Pets.Where(a => a.Name == Name).Select(a => new PetsDTO
           {
               Id = a.Id,
               Name = a.Name,
               DateOfBirth = a.DateOfBirth,
               Gender = a.Gender,
               BreedId = a.BreedId,
               Breed = a.Breed.Breed1
           }).FirstOrDefault();
       }



        [Route("api/Pets/{Id})")]
        [ResponseType(typeof(PetsDTO))]

        public PetsDTO Get(int Id)
        {
            return db.Pets.Where(p => p.Id == Id).Select(p => new PetsDTO
            {
                Id = p.Id,
                Name = p.Name,
                DateOfBirth = p.DateOfBirth,
                Gender = p.Gender,
                BreedId = p.BreedId,
                Breed = p.Breed.Breed1
            }).FirstOrDefault();
        }

        // PUT: api/Pets/5
        [Route("api/Pet/{Id}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPet(int id,[FromBody] Pet pet)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != pet.Id)
            {
                return BadRequest();
            }

            db.Entry(pet).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PetExists(id))
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

        // POST: api/Pets
        [ResponseType(typeof(Pet))]
        public IHttpActionResult PostPet(Pet pet)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Pets.Add(pet);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (PetExists(pet.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = pet.Id }, pet);
        }
        // DELETE: api/Pets/5
        [ResponseType(typeof(Pet))]
        public IHttpActionResult DeletePet(int id)
        {
            Pet pet = db.Pets.Find(id);
            if (pet == null)
            {
                return NotFound();
            }

            db.Pets.Remove(pet);
            db.SaveChanges();

            return Ok(pet);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PetExists(int id)
        {
            return db.Pets.Count(e => e.Id == id) > 0;
        }
        
    }
}