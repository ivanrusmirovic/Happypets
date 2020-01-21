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
using HappyPets_v1._1.DTOs.TreatmentTypes;
using HappyPets_v1._1.Models;

namespace HappyPets_v1._1.Controllers
{
    public class TreatmentTypesController : ApiController
    {
        private TestEntitiesnew db = new TestEntitiesnew();

        // GET: api/TreatmentTypes
      /*  public IQueryable<TreatmentType> GetTreatmentTypes()
        {
            return db.TreatmentTypes;
        }
        */
        public List<TreatmentType> Get()
        {
            return db.TreatmentTypes.ToList();
        }



        // GET: api/TreatmentTypes/5
        [ResponseType(typeof(TreatmentType))]
        public IHttpActionResult GetTreatmentType(int id)
        {
            TreatmentType treatmentType = db.TreatmentTypes.Find(id);
            if (treatmentType == null)
            {
                return NotFound();
            }

            return Ok(treatmentType);
        }

        // PUT: api/TreatmentTypes/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTreatmentType(int id, TreatmentType treatmentType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != treatmentType.Id)
            {
                return BadRequest();
            }

            db.Entry(treatmentType).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TreatmentTypeExists(id))
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

        // POST: api/TreatmentTypes
        [ResponseType(typeof(TreatmentType))]
        public IHttpActionResult PostTreatmentType(TreatmentType treatmentType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.TreatmentTypes.Add(treatmentType);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (TreatmentTypeExists(treatmentType.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = treatmentType.Id }, treatmentType);
        }

        // DELETE: api/TreatmentTypes/5
        [ResponseType(typeof(TreatmentType))]
        public IHttpActionResult DeleteTreatmentType(int id)
        {
            TreatmentType treatmentType = db.TreatmentTypes.Find(id);
            if (treatmentType == null)
            {
                return NotFound();
            }

            db.TreatmentTypes.Remove(treatmentType);
            db.SaveChanges();

            return Ok(treatmentType);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TreatmentTypeExists(int id)
        {
            return db.TreatmentTypes.Count(e => e.Id == id) > 0;
        }
    }
}