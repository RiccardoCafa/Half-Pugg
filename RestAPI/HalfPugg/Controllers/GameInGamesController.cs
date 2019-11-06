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
    public class GameInGamesController : ApiController
    {
        private HalfPuggContext db = new HalfPuggContext();

        // GET: api/GameInGames
        public IQueryable<GameInGame> GetGamerInGame()
        {
            return db.GamerInGame;
        }

        // GET: api/GameInGames/5
        [ResponseType(typeof(GameInGame))]
        public async Task<IHttpActionResult> GetGameInGame(int id)
        {
            GameInGame gameInGame = await db.GamerInGame.FindAsync(id);
            if (gameInGame == null)
            {
                return NotFound();
            }

            return Ok(gameInGame);
        }

        // PUT: api/GameInGames/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutGameInGame(int id, GameInGame gameInGame)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != gameInGame.ID)
            {
                return BadRequest();
            }

            db.Entry(gameInGame).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GameInGameExists(id))
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

        // POST: api/GameInGames
        [ResponseType(typeof(GameInGame))]
        public async Task<IHttpActionResult> PostGameInGame(GameInGame gameInGame)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.GamerInGame.Add(gameInGame);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = gameInGame.ID }, gameInGame);
        }

        // DELETE: api/GameInGames/5
        [ResponseType(typeof(GameInGame))]
        public async Task<IHttpActionResult> DeleteGameInGame(int id)
        {
            GameInGame gameInGame = await db.GamerInGame.FindAsync(id);
            if (gameInGame == null)
            {
                return NotFound();
            }

            db.GamerInGame.Remove(gameInGame);
            await db.SaveChangesAsync();

            return Ok(gameInGame);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool GameInGameExists(int id)
        {
            return db.GamerInGame.Count(e => e.ID == id) > 0;
        }
    }
}