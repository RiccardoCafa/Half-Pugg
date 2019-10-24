using System.Collections.Generic;
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
        public IQueryable<Gamer> GetGamers()
        {
            return db.Gamers;
        }

        // GET: api/Gamers/5
        [ResponseType(typeof(Gamer))]
        public IHttpActionResult GetGamer(int id)
        {
            Gamer gamer = db.Gamers.Find(id);
            if (gamer == null)
            {
                return NotFound();
            }

            return Ok(gamer);
        }

        [Route("api/GamersMatch")]
        [HttpGet]
        public IHttpActionResult GetGamerMatch()
        {
            Gamer gamerL = LoginController.GamerLogado;

            if (gamerL == null)
            {
                return null;
            }
            List<Gamer> gamers = new List<Gamer>();
            List<Match> matches = db.Matches
                                    .Where(ma => ma.IdPlayer1 == gamerL.ID || ma.IdPlayer2 == gamerL.ID)
                                    .AsEnumerable().ToList();
            List<RequestedMatch> reqMatches = db.RequestedMatchs
                                                .Where(ma => ma.IdPlayer == gamerL.ID || ma.IdPlayer2 == gamerL.ID)
                                                .AsEnumerable().ToList();
            foreach (Gamer gMatch in db.Gamers)
            {
                if (gMatch.ID != gamerL.ID)
                {
                    Match found = matches.FirstOrDefault(x =>
                     x.IdPlayer2 == gMatch.ID || x.IdPlayer1 == gMatch.ID);

                    RequestedMatch Requested = reqMatches.FirstOrDefault(x =>
                    x.IdPlayer2 == gMatch.ID || x.IdPlayer == gMatch.ID);

                    if (found == null && Requested == null)
                    {
                        gamers.Add(new Gamer()
                        {
                            ID = gMatch.ID,
                            Nickname = gMatch.Nickname,
                            Name = gMatch.Name,
                            LastName = gMatch.LastName,
                            Email = gMatch.Email,
                            ImagePath = gMatch.ImagePath,
                        });

                    }
                }
            }

            return Json(gamers);
        }


        // PUT: api/Gamers/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutGamer(int id, Gamer gamer)
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
        [ResponseType(typeof(Gamer))]
        public IHttpActionResult PostGamer(Gamer gamer)
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
        [ResponseType(typeof(Gamer))]
        public IHttpActionResult DeleteGamer(int id)
        {
            Gamer gamer = db.Gamers.Find(id);
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