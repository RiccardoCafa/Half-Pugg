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

namespace HalfPugg.Controllers
{
    public class User_GameController : ApiController
    {
        private HalfPuggContext db = new HalfPuggContext();

        // GET: api/User_Game
        public IQueryable<User_Game> GetUser_Game()
        {
            return db.User_Game;
        }

        // GET: api/User_Game/5
        [ResponseType(typeof(User_Game))]
        public IHttpActionResult GetUser_Game(int id)
        {
            User_Game user_Game = db.User_Game.Find(id);
            if (user_Game == null)
            {
                return NotFound();
            }

            return Ok(user_Game);
        }

        // PUT: api/User_Game/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutUser_Game(int id, User_Game user_Game)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != user_Game.ID_Game_Gamer)
            {
                return BadRequest();
            }

            db.Entry(user_Game).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!User_GameExists(id))
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

        // POST: api/User_Game
        [ResponseType(typeof(User_Game))]
        public IHttpActionResult PostUser_Game(User_Game user_Game)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.User_Game.Add(user_Game);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = user_Game.ID_Game_Gamer }, user_Game);
        }

        // DELETE: api/User_Game/5
        [ResponseType(typeof(User_Game))]
        public IHttpActionResult DeleteUser_Game(int id)
        {
            User_Game user_Game = db.User_Game.Find(id);
            if (user_Game == null)
            {
                return NotFound();
            }

            db.User_Game.Remove(user_Game);
            db.SaveChanges();

            return Ok(user_Game);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool User_GameExists(int id)
        {
            return db.User_Game.Count(e => e.ID_Game_Gamer == id) > 0;
        }
    }
}