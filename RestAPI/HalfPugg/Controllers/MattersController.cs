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
    public class MattersController : ApiController
    {
        private HalfPuggContext db = new HalfPuggContext();

        // GET: api/Matters
        public IQueryable<Matter> GetMatters()
        {
            return db.Matters;
        }

        // GET: api/Matters/5
        [ResponseType(typeof(Matter))]
        public IHttpActionResult GetMatter(int id)
        {
            Matter matter = db.Matters.Find(id);
            if (matter == null)
            {
                return NotFound();
            }

            return Ok(matter);
        }

        // PUT: api/Matters/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutMatter(int id, Matter matter)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != matter.ID_Matter)
            {
                return BadRequest();
            }

            db.Entry(matter).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MatterExists(id))
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

        // POST: api/Matters
        [ResponseType(typeof(Matter))]
        public IHttpActionResult PostMatter(Matter matter)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Matters.Add(matter);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = matter.ID_Matter }, matter);
        }

        // DELETE: api/Matters/5
        [ResponseType(typeof(Matter))]
        public IHttpActionResult DeleteMatter(int id)
        {
            Matter matter = db.Matters.Find(id);
            if (matter == null)
            {
                return NotFound();
            }

            db.Matters.Remove(matter);
            db.SaveChanges();

            return Ok(matter);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MatterExists(int id)
        {
            return db.Matters.Count(e => e.ID_Matter == id) > 0;
        }
    }
}