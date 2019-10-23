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
    public class Classification_MatchController : ApiController
    {
        private HalfPuggContext db = new HalfPuggContext();

        // GET: api/Classification_Match
        public IQueryable<Classification_Match> GetClassification_Matchs()
        {
            return db.Classification_Matchs;
        }

        // GET: api/Classification_Match/5
        [ResponseType(typeof(Classification_Match))]
        public async Task<IHttpActionResult> GetClassification_Match(int id)
        {
            Classification_Match classification_Match = await db.Classification_Matchs.FindAsync(id);
            if (classification_Match == null)
            {
                return NotFound();
            }

            return Ok(classification_Match);
        }

        // PUT: api/Classification_Match/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutClassification_Match(int id, Classification_Match classification_Match)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != classification_Match.ID)
            {
                return BadRequest();
            }

            db.Entry(classification_Match).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Classification_MatchExists(id))
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

        // POST: api/Classification_Match
        [ResponseType(typeof(Classification_Match))]
        public async Task<IHttpActionResult> PostClassification_Match(Classification_Match classification_Match)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Classification_Matchs.Add(classification_Match);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = classification_Match.ID }, classification_Match);
        }

        // DELETE: api/Classification_Match/5
        [ResponseType(typeof(Classification_Match))]
        public async Task<IHttpActionResult> DeleteClassification_Match(int id)
        {
            Classification_Match classification_Match = await db.Classification_Matchs.FindAsync(id);
            if (classification_Match == null)
            {
                return NotFound();
            }

            db.Classification_Matchs.Remove(classification_Match);
            await db.SaveChangesAsync();

            return Ok(classification_Match);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Classification_MatchExists(int id)
        {
            return db.Classification_Matchs.Count(e => e.ID == id) > 0;
        }
    }
}