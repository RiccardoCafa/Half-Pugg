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
    public class HashTagsController : ApiController
    {
        private HalfPuggContext db = new HalfPuggContext();

        // GET: api/HashTags
        public IQueryable<HashTag> GetHashTags()
        {
            return db.HashTags;
        }

        // GET: api/HashTags/5
        [ResponseType(typeof(HashTag))]
        public async Task<IHttpActionResult> GetHashTag(int id)
        {
            HashTag hashTag = await db.HashTags.FindAsync(id);
            if (hashTag == null)
            {
                return NotFound();
            }

            return Ok(hashTag);
        }

        // PUT: api/HashTags/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutHashTag(int id, HashTag hashTag)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != hashTag.ID_Matter)
            {
                return BadRequest();
            }

            db.Entry(hashTag).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HashTagExists(id))
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

        // POST: api/HashTags
        [ResponseType(typeof(HashTag))]
        public async Task<IHttpActionResult> PostHashTag(HashTag hashTag)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.HashTags.Add(hashTag);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = hashTag.ID_Matter }, hashTag);
        }

        // DELETE: api/HashTags/5
        [ResponseType(typeof(HashTag))]
        public async Task<IHttpActionResult> DeleteHashTag(int id)
        {
            HashTag hashTag = await db.HashTags.FindAsync(id);
            if (hashTag == null)
            {
                return NotFound();
            }

            db.HashTags.Remove(hashTag);
            await db.SaveChangesAsync();

            return Ok(hashTag);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool HashTagExists(int id)
        {
            return db.HashTags.Count(e => e.ID_Matter == id) > 0;
        }
    }
}