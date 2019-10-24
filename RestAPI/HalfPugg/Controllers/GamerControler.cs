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
    public class GamersController : ApiController
    {
        private HalfPuggContext db = new HalfPuggContext();

        // GET: api/Gamers
        public IQueryable<Player> GetGamers()
        {
            return db.Gamers;
        }

        // GET: api/Gamers/5
        [ResponseType(typeof(Player))]
        public IHttpActionResult GetGamer(int id)
        {
            Player gamer = db.Gamers.Find(id);
            if (gamer == null)
            {
                return NotFound();
            }

            return Ok(gamer);
        }

        // PUT: api/Gamers/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutGamer(int id, Player gamer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != gamer.ID)
            {
                return BadRequest();
            }

            db.Entry(gamer).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GamerExists(id))
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

        // POST: api/Gamers
        [ResponseType(typeof(Player))]
        public IHttpActionResult PostGamer(Player gamer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Gamers.Add(gamer);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = gamer.ID }, gamer);
        }

        // DELETE: api/Gamers/5
        [ResponseType(typeof(Player))]
        public IHttpActionResult DeleteGamer(int id)
        {
            Player gamer = db.Gamers.Find(id);
            if (gamer == null)
            {
                return NotFound();
            }

            db.Gamers.Remove(gamer);
            db.SaveChanges();

            return Ok(gamer);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool GamerExists(int id)
        {
            return db.Gamers.Count(e => e.ID == id) > 0;
        }
    }
}