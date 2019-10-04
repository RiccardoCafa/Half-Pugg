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
    public class Classification_UserController : ApiController
    {
        private HalfPuggContext db = new HalfPuggContext();

        // GET: api/Classification_User
        public IQueryable<Classification_User> GetClassification_User()
        {
            return db.Classification_User;
        }

        // GET: api/Classification_User/5
        [ResponseType(typeof(Classification_User))]
        public IHttpActionResult GetClassification_User(int id)
        {
            Classification_User classification_User = db.Classification_User.Find(id);
            if (classification_User == null)
            {
                return NotFound();
            }

            return Ok(classification_User);
        }

        // PUT: api/Classification_User/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutClassification_User(int id, Classification_User classification_User)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != classification_User.ID_Classification)
            {
                return BadRequest();
            }

            db.Entry(classification_User).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Classification_UserExists(id))
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

        // POST: api/Classification_User
        [ResponseType(typeof(Classification_User))]
        public IHttpActionResult PostClassification_User(Classification_User classification_User)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Classification_User.Add(classification_User);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = classification_User.ID_Classification }, classification_User);
        }

        // DELETE: api/Classification_User/5
        [ResponseType(typeof(Classification_User))]
        public IHttpActionResult DeleteClassification_User(int id)
        {
            Classification_User classification_User = db.Classification_User.Find(id);
            if (classification_User == null)
            {
                return NotFound();
            }

            db.Classification_User.Remove(classification_User);
            db.SaveChanges();

            return Ok(classification_User);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Classification_UserExists(int id)
        {
            return db.Classification_User.Count(e => e.ID_Classification == id) > 0;
        }
    }
}