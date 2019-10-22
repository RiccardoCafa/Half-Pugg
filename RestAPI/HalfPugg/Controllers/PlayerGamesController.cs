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
using Newtonsoft.Json.Linq;

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

        [Route("api/GetGamersInGame")]
        [ResponseType(typeof(PlayerGame))]
        [HttpGet]
        public IQueryable<PlayerGame> GetGamersInGame(int GameID)
        {
            return db.PlayerGames.Where(x=> x.IDGame == GameID);
        }

        [Route("api/GetGamesInGamers")]
        [ResponseType(typeof(PlayerGame))]
        [HttpGet]
        public IQueryable<PlayerGame> GetGamesInGamers(int GamerID)
        {
            return db.PlayerGames.Where(x => x.IDGamer == GamerID);
        }

        // GET: api/PlayerGames/5
        [ResponseType(typeof(PlayerGame))]
        public IHttpActionResult GetPlayerGame(int id)
        {
            PlayerGame playerGame = db.PlayerGames.Find(id);
            if (playerGame == null)
            {
                return NotFound();
            }

            return Ok(playerGame);
        }

        // PUT: api/PlayerGames/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPlayerGame(int id, PlayerGame playerGame)
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
                db.SaveChanges();
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
        public IHttpActionResult PostPlayerGame(PlayerGame playerGame)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            playerGame.Game = db.Games.Find(playerGame.IDGame);
            playerGame.Gamer = db.Gamers.Find(playerGame.IDGamer);
            db.PlayerGames.Add(playerGame);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = playerGame.ID }, playerGame);
        }

        // DELETE: api/PlayerGames/5
        [ResponseType(typeof(PlayerGame))]
        public IHttpActionResult DeletePlayerGame(int id)
        {
            PlayerGame playerGame = db.PlayerGames.Find(id);
            if (playerGame == null)
            {
                return NotFound();
            }

            db.PlayerGames.Remove(playerGame);
            db.SaveChanges();

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