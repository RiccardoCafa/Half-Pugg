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
    public class PlayerGamesController : ApiController
    {
        private HalfPuggContext db = new HalfPuggContext();

        // GET: api/PlayerGames
        public IQueryable<PlayerGame> GetPlayerGames()
        {
            return db.PlayerGames;
        }

        // GET: api/PlayerGames/5
        [ResponseType(typeof(PlayerGame))]
        public async Task<IHttpActionResult> GetPlayerGame(int id)
        {
            PlayerGame playerGame = await db.PlayerGames.FindAsync(id);
            if (playerGame == null)
            {
                return NotFound();
            }

            return Ok(playerGame);
        }

        // PUT: api/PlayerGames/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutPlayerGame(int id, PlayerGame playerGame)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != playerGame.ID)
            {
                return BadRequest();
            }

            db.Entry(playerGame).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlayerGameExists(id))
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

        // POST: api/PlayerGames
        [ResponseType(typeof(PlayerGame))]
        public async Task<IHttpActionResult> PostPlayerGame(PlayerGame playerGame)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.PlayerGames.Add(playerGame);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = playerGame.ID }, playerGame);
        }

        // DELETE: api/PlayerGames/5
        [ResponseType(typeof(PlayerGame))]
        public async Task<IHttpActionResult> DeletePlayerGame(int id)
        {
            PlayerGame playerGame = await db.PlayerGames.FindAsync(id);
            if (playerGame == null)
            {
                return NotFound();
            }

            db.PlayerGames.Remove(playerGame);
            await db.SaveChangesAsync();

            return Ok(playerGame);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PlayerGameExists(int id)
        {
            return db.PlayerGames.Count(e => e.ID == id) > 0;
        }
    }
}