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
    public class RangesController : ApiController
    {
        private HalfPuggContext db = new HalfPuggContext();

        // GET: api/Ranges
        public IQueryable<Range> GetRanges()
        {
            return db.Ranges;
        }

        // GET: api/Ranges/5
        [ResponseType(typeof(Range))]
        public async Task<IHttpActionResult> GetRange(int id)
        {
            Range range = await db.Ranges.FindAsync(id);
            if (range == null)
            {
                return NotFound();
            }

            return Ok(range);
        }

        // PUT: api/Ranges/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutRange(int id, Range range)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != range.ID)
            {
                return BadRequest();
            }

            db.Entry(range).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RangeExists(id))
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

        // POST: api/Ranges
        [ResponseType(typeof(Range))]
        public async Task<IHttpActionResult> PostRange(Range range)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Ranges.Add(range);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = range.ID }, range);
        }

        // DELETE: api/Ranges/5
        [ResponseType(typeof(Range))]
        public async Task<IHttpActionResult> DeleteRange(int id)
        {
            Range range = await db.Ranges.FindAsync(id);
            if (range == null)
            {
                return NotFound();
            }

            db.Ranges.Remove(range);
            await db.SaveChangesAsync();

            return Ok(range);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RangeExists(int id)
        {
            return db.Ranges.Count(e => e.ID == id) > 0;
        }
    }
}