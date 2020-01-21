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
    public class CaracteristicsController : ApiController
    {
        private TestEntitiesnew db = new TestEntitiesnew();

        // GET: api/Caracteristics
        public IQueryable<Caracteristic> GetCaracteristics()
        {
            return db.Caracteristics;
        }

        // GET: api/Caracteristics/5
        [ResponseType(typeof(Caracteristic))]
        public IHttpActionResult GetCaracteristic(int id)
        {
            Caracteristic caracteristic = db.Caracteristics.Find(id);
            if (caracteristic == null)
            {
                return NotFound();
            }

            return Ok(caracteristic);
        }
        [ResponseType(typeof (CaracteristicDTO))]
        public CaracteristicDTO Get(int Id)
        {
            return db.Caracteristics.Where(c => c.Id == Id).Select(c => new CaracteristicDTO
            {
                Id = c.Id,
                Weight = c.Weight,
                Height = c.Height,
                PetId = c.PetId
            }).FirstOrDefault();

        }

        // PUT: api/Caracteristics/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCaracteristic(int id, Caracteristic caracteristic)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != caracteristic.Id)
            {
                return BadRequest();
            }

            db.Entry(caracteristic).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CaracteristicExists(id))
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

        // POST: api/Caracteristics
        [ResponseType(typeof(Caracteristic))]
        public IHttpActionResult PostCaracteristic(Caracteristic caracteristic)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Caracteristics.Add(caracteristic);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (CaracteristicExists(caracteristic.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = caracteristic.Id }, caracteristic);
        }

        // DELETE: api/Caracteristics/5
        [ResponseType(typeof(Caracteristic))]
        public IHttpActionResult DeleteCaracteristic(int id)
        {
            Caracteristic caracteristic = db.Caracteristics.Find(id);
            if (caracteristic == null)
            {
                return NotFound();
            }

            db.Caracteristics.Remove(caracteristic);
            db.SaveChanges();

            return Ok(caracteristic);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CaracteristicExists(int id)
        {
            return db.Caracteristics.Count(e => e.Id == id) > 0;
        }
    }
}