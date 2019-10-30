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
    public class MatchesController : ApiController
    {
        private HalfPuggContext db = new HalfPuggContext();

        // GET: api/Matches
        public IQueryable<Match> GetMatches()
        {
            return db.Matches;
        }

        // GET: api/Matches/5
        [ResponseType(typeof(Match))]
        public async Task<IHttpActionResult> GetMatch(int id)
        {
            Player procurando = await db.Gamers.FindAsync(id);

            List<Match> match = db.Matches
                                    .Where(x => x.IdPlayer1 == id || x.IdPlayer2 == id)
                                    .AsEnumerable().ToList();
            if (match != null)
            {
                return Ok(match);
            }

            return NotFound();
        }

        [Route("api/Matches/ByGamer")]
        [HttpGet]
        public async Task<IHttpActionResult> GetMatchByGamer(int gamerid)
        {
            Player procurando = await db.Gamers.FindAsync(gamerid);

            List<Match> match = db.Matches
                                    .Where(x => x.IdPlayer1 == gamerid || x.IdPlayer2 == gamerid)
                                    .AsEnumerable().ToList();
            if(match != null)
            {
                return Ok(match);
            }

            return NotFound();
        }

        // PUT: api/Matches/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutMatch(int id, Match match)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != match.ID)
            {
                return BadRequest();
            }

            db.Entry(match).State = EntityState.Modified;
                
            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MatchExists(id))
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

        // POST: api/Matches
        [ResponseType(typeof(Match))]
        public async Task<IHttpActionResult> PostMatch(Match match)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            //Match hasMatch = db.Matches.FirstOrDefault(ma => 
            //((ma.Player1.ID == match.Player1.ID && ma.Player2.ID == match.Player2.ID) ||
            //(ma.Player2.ID == match.Player1.ID && ma.Player1.ID == match.Player2.ID)) && ma.Status == false);

            //if (hasMatch != null)
            //{
            //    return BadRequest();
            //}

            match.Status = false;
            db.Matches.Add(match);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = match.ID }, match);
        }

        // DELETE: api/Matches/5
        [ResponseType(typeof(Match))]
        public async Task<IHttpActionResult> DeleteMatch(int id)
        {
            Match match = await db.Matches.FindAsync(id);
            if (match == null)
            {
                return NotFound();
            }

            db.Matches.Remove(match);
            await db.SaveChangesAsync();

            return Ok(match);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MatchExists(int id)
        {
            return db.Matches.Count(e => e.ID == id) > 0;
        }
    }
}