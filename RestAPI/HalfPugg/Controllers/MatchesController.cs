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
        public IHttpActionResult GetMatch(int id)
        {
            Match match = db.Matches.Find(id);
            if (match == null)
            {
                return NotFound();
            }

            return Ok(match);
        }

        // PUT: api/Matches/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutMatch(int id, Match match)
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
                db.SaveChanges();
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
        public IHttpActionResult PostMatch(Match match, bool foimatch)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Match hasMatch = db.Matches.FirstOrDefault(ma => 
            ((ma.Player1.ID == match.Player1.ID && ma.Player2.ID == match.Player2.ID) ||
            (ma.Player2.ID == match.Player1.ID && ma.Player1.ID == match.Player2.ID)) && ma.Status == 'A');

            if (hasMatch != null)
            {
                if (foimatch) hasMatch.Status = 'M';
                else hasMatch.Status = 'D';
                hasMatch.Status = match.Status;
                db.Entry(hasMatch).State = EntityState.Modified;
                db.SaveChanges();
                return Ok();
            }
            match.Status = 'A';
            db.Matches.Add(match);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = match.ID }, match);
        }

        // DELETE: api/Matches/5
        [ResponseType(typeof(Match))]
        public IHttpActionResult DeleteMatch(int id)
        {
            Match match = db.Matches.Find(id);
            if (match == null)
            {
                return NotFound();
            }

            db.Matches.Remove(match);
            db.SaveChanges();

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