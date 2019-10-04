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
using HalfPugg.Models;

namespace HalfPugg.Controllers
{
    public class Classification_GamerController : ApiController
    {
        private HalfPuggContext db = new HalfPuggContext();

        // GET: api/Classification_Gamer
        public IQueryable<Classification_Gamer> GetClassification_Gamer()
        {
            return db.Classification_Gamer;
        }

        // GET: api/Classification_Gamer/5
        [ResponseType(typeof(Classification_Gamer))]
        public IHttpActionResult GetClassification_Gamer(int id)
        {
            Classification_Gamer classification_Gamer = db.Classification_Gamer.Find(id);
            if (classification_Gamer == null)
            {
                return NotFound();
            }

            return Ok(classification_Gamer);
        }

        // PUT: api/Classification_Gamer/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutClassification_Gamer(int id, Classification_Gamer classification_Gamer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != classification_Gamer.ID_Classification)
            {
                return BadRequest();
            }

            db.Entry(classification_Gamer).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Classification_GamerExists(id))
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

        // POST: api/Classification_Gamer
        [ResponseType(typeof(Classification_Gamer))]
        public IHttpActionResult PostClassification_Gamer(Classification_Gamer classification_Gamer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Classification_Gamer.Add(classification_Gamer);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = classification_Gamer.ID_Classification }, classification_Gamer);
        }

        // DELETE: api/Classification_Gamer/5
        [ResponseType(typeof(Classification_Gamer))]
        public IHttpActionResult DeleteClassification_Gamer(int id)
        {
            Classification_Gamer classification_Gamer = db.Classification_Gamer.Find(id);
            if (classification_Gamer == null)
            {
                return NotFound();
            }

            db.Classification_Gamer.Remove(classification_Gamer);
            db.SaveChanges();

            return Ok(classification_Gamer);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Classification_GamerExists(int id)
        {
            return db.Classification_Gamer.Count(e => e.ID_Classification == id) > 0;
        }
    }
}