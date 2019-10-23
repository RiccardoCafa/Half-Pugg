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
    public class ClassificationPlayersController : ApiController
    {
        private HalfPuggContext db = new HalfPuggContext();

        // GET: api/ClassificationPlayers
        public IQueryable<ClassificationPlayer> GetClassification_Players()
        {
            return db.Classification_Players;
        }

        // GET: api/ClassificationPlayers/5
        [ResponseType(typeof(ClassificationPlayer))]
        public async Task<IHttpActionResult> GetClassificationPlayer(int id)
        {
            ClassificationPlayer classificationPlayer = await db.Classification_Players.FindAsync(id);
            if (classificationPlayer == null)
            {
                return NotFound();
            }

            return Ok(classificationPlayer);
        }

        // PUT: api/ClassificationPlayers/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutClassificationPlayer(int id, ClassificationPlayer classificationPlayer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != classificationPlayer.ID)
            {
                return BadRequest();
            }

            db.Entry(classificationPlayer).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClassificationPlayerExists(id))
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

        // POST: api/ClassificationPlayers
        [ResponseType(typeof(ClassificationPlayer))]
        public async Task<IHttpActionResult> PostClassificationPlayer(ClassificationPlayer classificationPlayer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Classification_Players.Add(classificationPlayer);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = classificationPlayer.ID }, classificationPlayer);
        }

        // DELETE: api/ClassificationPlayers/5
        [ResponseType(typeof(ClassificationPlayer))]
        public async Task<IHttpActionResult> DeleteClassificationPlayer(int id)
        {
            ClassificationPlayer classificationPlayer = await db.Classification_Players.FindAsync(id);
            if (classificationPlayer == null)
            {
                return NotFound();
            }

            db.Classification_Players.Remove(classificationPlayer);
            await db.SaveChangesAsync();

            return Ok(classificationPlayer);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ClassificationPlayerExists(int id)
        {
            return db.Classification_Players.Count(e => e.ID == id) > 0;
        }
    }
}