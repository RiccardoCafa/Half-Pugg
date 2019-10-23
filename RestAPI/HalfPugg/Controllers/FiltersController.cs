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
    public class FiltersController : ApiController
    {
        private HalfPuggContext db = new HalfPuggContext();

        // GET: api/Filters
        public IQueryable<Filter> GetFilters()
        {
            return db.Filters;
        }

        // GET: api/Filters/5
        [ResponseType(typeof(Filter))]
        public async Task<IHttpActionResult> GetFilter(int id)
        {
            Filter filter = await db.Filters.FindAsync(id);
            if (filter == null)
            {
                return NotFound();
            }

            return Ok(filter);
        }

        // PUT: api/Filters/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutFilter(int id, Filter filter)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != filter.ID_Filter)
            {
                return BadRequest();
            }

            db.Entry(filter).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FilterExists(id))
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

        // POST: api/Filters
        [ResponseType(typeof(Filter))]
        public async Task<IHttpActionResult> PostFilter(Filter filter)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Filters.Add(filter);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = filter.ID_Filter }, filter);
        }

        // DELETE: api/Filters/5
        [ResponseType(typeof(Filter))]
        public async Task<IHttpActionResult> DeleteFilter(int id)
        {
            Filter filter = await db.Filters.FindAsync(id);
            if (filter == null)
            {
                return NotFound();
            }

            db.Filters.Remove(filter);
            await db.SaveChangesAsync();

            return Ok(filter);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool FilterExists(int id)
        {
            return db.Filters.Count(e => e.ID_Filter == id) > 0;
        }
    }
}