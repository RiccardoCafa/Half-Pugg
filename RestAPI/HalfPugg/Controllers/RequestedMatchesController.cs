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
using HalfPugg.TokenJWT;
using Newtonsoft.Json;

namespace HalfPugg.Controllers
{
    public class RequestedMatchesController : ApiController
    {
        private HalfPuggContext db = new HalfPuggContext();

        // GET: api/RequestedMatches
        public IQueryable<RequestedMatch> GetRequestedMatchs()
        {
            return db.RequestedMatchs;
        }

        // GET: api/RequestedMatches/5
        [ResponseType(typeof(RequestedMatch))]
        public async Task<IHttpActionResult> GetRequestedMatch(int id)
        {
            RequestedMatch requestedMatch = await db.RequestedMatchs.FindAsync(id);
            if (requestedMatch == null)
            {
                return NotFound();
            }

            return Ok(requestedMatch);
        }

        [Route("api/RequestedMatchesLoggedGamer")]
        [HttpGet]
        public IHttpActionResult GetRequestedMatchForLogg()
        {
            Player gamerlogado = null;

            var headers = Request.Headers;

            if (headers.Contains("token-jwt"))
            {
                string token = headers.GetValues("token-jwt").First();
                TokenValidation validation = new TokenValidation();
                string userValidated = validation.ValidateToken(token);
                if (userValidated != null)
                {
                    TokenData data = JsonConvert.DeserializeObject<TokenData>(userValidated);
                    gamerlogado = db.Gamers.FirstOrDefault(g => g.ID == data.ID);
                }
            }

            if (gamerlogado == null) return BadRequest();
            List<RequestedMatch> reqMatches = db.RequestedMatchs
                                                     .Where(x => x.IdPlayer2 == gamerlogado.ID)
                                                     .AsEnumerable().ToList();

            List<Player> gamersReq = new List<Player>();
            foreach (RequestedMatch reqMatch in reqMatches)
            {
                if (reqMatch.Status != "A") continue;
                Player findMe = db.Gamers.Find(reqMatch.IdPlayer1);
                if(findMe != null)
                {
                    gamersReq.Add(new Player()
                    {
                        ID = findMe.ID,
                        Nickname =findMe.Nickname,
                        Bio = findMe.Bio,
                        LastName = findMe.LastName,
                        Name = findMe.Name,
                        Sex = findMe.Sex,
                        ImagePath = findMe.ImagePath
                    });
                }
            }
            return Ok(gamersReq);
        }

        // PUT: api/RequestedMatches
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutRequestedMatch(RequestedMatch requestedMatch)
        {
            int id;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var req2 = db.Set<RequestedMatch>().FirstOrDefault(req => req.IdPlayer1 == requestedMatch.IdPlayer1
                                                  && req.IdPlayer2 == requestedMatch.IdPlayer2);
            if (req2 == null) return BadRequest();
            id = req2.ID;
            requestedMatch.ID = id;

            if (id != requestedMatch.ID)
            {
                return BadRequest();
            }

            if(requestedMatch.Status != "A")
            {
                bool deuMatch = requestedMatch.Status == "M" ? true : false;
                Match match = new Match()
                {
                    ID = 0,
                    IdPlayer1 = requestedMatch.IdPlayer1,
                    IdPlayer2 = requestedMatch.IdPlayer2,
                    Status = deuMatch,
                    Weight = 0,
                    CreateAt = DateTime.UtcNow,
                    AlteredAt = DateTime.UtcNow
                };
                db.Matches.Add(match);
            }

            db.Entry(req2).State = EntityState.Detached;
            db.Entry(requestedMatch).State = EntityState.Modified;
            
            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RequestedMatchExists(id))
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

        // POST: api/RequestedMatches
        [ResponseType(typeof(RequestedMatch))]
        public async Task<IHttpActionResult> PostRequestedMatch(RequestedMatch requestedMatch)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                Match hasMatch = db.Matches.FirstOrDefault(ma =>
                ((ma.Player1.ID == requestedMatch.IdPlayer1 && ma.IdPlayer2 == requestedMatch.IdPlayer2) ||
                (ma.Player2.ID == requestedMatch.IdPlayer1 && ma.IdPlayer1 == requestedMatch.IdPlayer2)) && ma.Status == true);

                if (hasMatch != null)
                {
                    return BadRequest();
                }
                db.RequestedMatchs.Add(requestedMatch);
                await db.SaveChangesAsync();
            }catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return BadRequest();
            }
            return Ok(requestedMatch);
        }

        // DELETE: api/RequestedMatches/5
        [ResponseType(typeof(RequestedMatch))]
        public async Task<IHttpActionResult> DeleteRequestedMatch(int id)
        {
            RequestedMatch requestedMatch = await db.RequestedMatchs.FindAsync(id);
            if (requestedMatch == null)
            {
                return NotFound();
            }

            db.RequestedMatchs.Remove(requestedMatch);
            await db.SaveChangesAsync();

            return Ok(requestedMatch);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RequestedMatchExists(int id)
        {
            return db.RequestedMatchs.Count(e => e.ID == id) > 0;
        }
    }
}