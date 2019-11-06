using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using HalfPugg.Models;

namespace HalfPugg.Controllers
{
    public class NumberedsController : ApiController
    {
        private HalfPuggContext db = new HalfPuggContext();

        // GET: api/Numbereds
        public IQueryable<Numbered> GetNumbereds()
        {
            return db.Numbereds;
        }

        // GET: api/Numbereds/5
        [ResponseType(typeof(Numbered))]
        public async Task<IHttpActionResult> GetNumbered(int id)
        {
            Numbered numbered = await db.Numbereds.FindAsync(id);
            if (numbered == null)
            {
                return NotFound();
            }

            return Ok(numbered);
        }

        // PUT: api/Numbereds/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutNumbered(int id, Numbered numbered)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != numbered.ID)
            {
                return BadRequest();
            }

            db.Entry(numbered).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NumberedExists(id))
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

        // POST: api/Numbereds
        [ResponseType(typeof(Numbered))]
        public async Task<IHttpActionResult> PostNumbered(Numbered numbered)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Numbereds.Add(numbered);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = numbered.ID }, numbered);
        }

        // DELETE: api/Numbereds/5
        [ResponseType(typeof(Numbered))]
        public async Task<IHttpActionResult> DeleteNumbered(int id)
        {
            Numbered numbered = await db.Numbereds.FindAsync(id);
            if (numbered == null)
            {
                return NotFound();
            }

            db.Numbereds.Remove(numbered);
            await db.SaveChangesAsync();

            return Ok(numbered);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool NumberedExists(int id)
        {
            return db.Numbereds.Count(e => e.ID == id) > 0;
        }
    }
}