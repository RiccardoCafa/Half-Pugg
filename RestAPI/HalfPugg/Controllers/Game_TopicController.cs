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
    public class Game_TopicController : ApiController
    {
        private HalfPuggContext db = new HalfPuggContext();

        // GET: api/Game_Topic
        public IQueryable<Game_Topic> GetGame_Topic()
        {
            return db.Game_Topic;
        }

        // GET: api/Game_Topic/5
        [ResponseType(typeof(Game_Topic))]
        public IHttpActionResult GetGame_Topic(int id)
        {
            Game_Topic game_Topic = db.Game_Topic.Find(id);
            if (game_Topic == null)
            {
                return NotFound();
            }

            return Ok(game_Topic);
        }

        // PUT: api/Game_Topic/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutGame_Topic(int id, Game_Topic game_Topic)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != game_Topic.ID_Gamer_Topic)
            {
                return BadRequest();
            }

            db.Entry(game_Topic).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Game_TopicExists(id))
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

        // POST: api/Game_Topic
        [ResponseType(typeof(Game_Topic))]
        public IHttpActionResult PostGame_Topic(Game_Topic game_Topic)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Game_Topic.Add(game_Topic);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = game_Topic.ID_Gamer_Topic }, game_Topic);
        }

        // DELETE: api/Game_Topic/5
        [ResponseType(typeof(Game_Topic))]
        public IHttpActionResult DeleteGame_Topic(int id)
        {
            Game_Topic game_Topic = db.Game_Topic.Find(id);
            if (game_Topic == null)
            {
                return NotFound();
            }

            db.Game_Topic.Remove(game_Topic);
            db.SaveChanges();

            return Ok(game_Topic);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Game_TopicExists(int id)
        {
            return db.Game_Topic.Count(e => e.ID_Gamer_Topic == id) > 0;
        }
    }
}