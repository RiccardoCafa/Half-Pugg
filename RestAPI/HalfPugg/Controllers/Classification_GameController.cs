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
    public class Classification_GameController : ApiController
    {
        private HalfPuggContext db = new HalfPuggContext();

        // GET: api/Classification_Game
        public IQueryable<Classification_Player> GetClassification_Games()
        {
            return db.Classification_Games;
        }

        // GET: api/Classification_Game/5
        [ResponseType(typeof(Classification_Player))]
        public async Task<IHttpActionResult> GetClassification_Game(int id)
        {
            Classification_Player classification_Game = await db.Classification_Games.FindAsync(id);
            if (classification_Game == null)
            {
                return NotFound();
            }

            return Ok(classification_Game);
        }

        // PUT: api/Classification_Game/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutClassification_Game(int id, Classification_Player classification_Game)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != classification_Game.ID)
            {
                return BadRequest();
            }

            db.Entry(classification_Game).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Classification_GameExists(id))
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

        // POST: api/Classification_Game
        [ResponseType(typeof(Classification_Player))]
        public async Task<IHttpActionResult> PostClassification_Game(Classification_Player classification_Game)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Classification_Games.Add(classification_Game);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = classification_Game.ID }, classification_Game);
        }

        // DELETE: api/Classification_Game/5
        [ResponseType(typeof(Classification_Player))]
        public async Task<IHttpActionResult> DeleteClassification_Game(int id)
        {
            Classification_Player classification_Game = await db.Classification_Games.FindAsync(id);
            if (classification_Game == null)
            {
                return NotFound();
            }

            db.Classification_Games.Remove(classification_Game);
            await db.SaveChangesAsync();

            return Ok(classification_Game);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Classification_GameExists(int id)
        {
            return db.Classification_Games.Count(e => e.ID == id) > 0;
        }
    }
}