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
    public class TreatmentsController : ApiController
    {
        private TestEntitiesnew db = new TestEntitiesnew();

        // GET: api/Treatments
        public IQueryable<Treatment> GetTreatments()
        {
            return db.Treatment;
        }

        // GET: api/Treatments/5
        [ResponseType(typeof(Treatment))]
        public IHttpActionResult GetTreatment(int id)
        {
            Treatment treatment = db.Treatment.Find(id);
            if (treatment == null)
            {
                return NotFound();
            }

            return Ok(treatment);
        }
        
        // PUT: api/Treatments/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTreatment(int id,[FromBody] Treatment treatment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != treatment.Id)
            {
                return BadRequest();
            }

            db.Entry(treatment).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TreatmentExists(id))
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

        // POST: api/Treatments
        [ResponseType(typeof(Treatment))]
        public IHttpActionResult PostTreatment(Treatment treatment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Treatment.Add(treatment);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (TreatmentExists(treatment.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = treatment.Id }, treatment);
        }

        // DELETE: api/Treatments/5
        [ResponseType(typeof(Treatment))]
        public IHttpActionResult DeleteTreatment(int id)
        {
            Treatment treatment = db.Treatment.Find(id);
            if (treatment == null)
            {
                return NotFound();
            }

            db.Treatment.Remove(treatment);
            db.SaveChanges();

            return Ok(treatment);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TreatmentExists(int id)
        {
            return db.Treatment.Count(e => e.Id == id) > 0;
        }
    }
}