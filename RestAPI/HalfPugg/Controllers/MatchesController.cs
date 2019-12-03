using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using HalfPugg.Models;
using JWT;

namespace HalfPugg.Controllers
{
    public class PlayerMatchInfo
    {
        public Player matchPlayer;
        public float afinidade;
    }
    public class MatchesController : ApiController
    {
        private HalfPuggContext db = new HalfPuggContext();

        // GET: api/Matches
        public IQueryable<Match> GetMatches()
        {
            return db.Matches;
        }

        // GET: api/Matches/5
        [ResponseType(typeof(List<PlayerMatchInfo>))]
        public async Task<IHttpActionResult> GetMatch(int id)
        {
            List<PlayerMatchInfo> playersInfo = new List<PlayerMatchInfo>();
            Player procurando = await db.Gamers.FindAsync(id);

            List<Match> match = db.Matches
                                    .Where(x => x.Status && (x.IdPlayer1 == id || x.IdPlayer2 == id))
                                    .AsEnumerable().ToList();
            //List<Player> myConnections = new List<Player>();

            foreach(Match m in match)
            {
                Player mp = null;
                if(m.IdPlayer1 != procurando.ID)
                {
                    mp = db.Gamers.Find(m.IdPlayer1);
                } else
                {
                    mp = db.Gamers.Find(m.IdPlayer2);
                }
                playersInfo.Add(new PlayerMatchInfo()
                {
                    matchPlayer = mp,
                    afinidade = m.Weight,
                });
            }
            //.Where(x => x.IdPlayer1 == id || x.IdPlayer2 == id)
            if (match != null)
            {
                return Ok(playersInfo);
            }

            return NotFound();
        }

        [Route("api/Matches/Rejected")]
        [HttpGet]
        public IHttpActionResult GetRejectedMatch(int playerID)
        {
            var matches = db.Matches.Where(x => !x.Status && (x.IdPlayer1 == playerID || x.IdPlayer2 == playerID)).AsEnumerable().ToArray();
            List<dynamic> players = new List<dynamic>();
            foreach(Match match in matches)
            {
                Player rejected = match.IdPlayer1 == playerID ? match.Player2 : match.Player1;
                players.Add(new
                {
                    match,
                    rejected
                });
            }
            return Ok(players);
        }

        [Route("api/Matches/HasMatch")]
        [HttpGet]
        public IHttpActionResult HasMatch(int playerId1, int playerId2)
        {
            Match m = db.Matches.Where(x => (x.IdPlayer1 == playerId1 && x.IdPlayer2 == playerId2) || (x.IdPlayer1 == playerId2 && x.IdPlayer2 == playerId1)).FirstOrDefault();
            return Ok(m != null);
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
            match.CreateAt = DateTime.UtcNow;
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

            return Ok();
        }

        // POST: api/Matches
        [ResponseType(typeof(Match))]
        public async Task<IHttpActionResult> PostMatch(Match match)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            Match hasMatch = db.Matches.FirstOrDefault(ma => 
            ((ma.Player1.ID == match.Player1.ID && ma.Player2.ID == match.Player2.ID) ||
            (ma.Player2.ID == match.Player1.ID && ma.Player1.ID == match.Player2.ID)) && ma.Status == true);

            if (hasMatch != null)
            {
                return BadRequest();
            }
            DateTime now = DateTime.UtcNow;
            match.CreateAt = now;
            match.AlteredAt = now;
            match.Status = false;
            db.Matches.Add(match);
            await db.SaveChangesAsync();

            return Ok(match);
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