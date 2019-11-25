using System;
using System.Collections.Generic;
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
using System.Security.Cryptography;

namespace HalfPugg.Controllers
{
    public class PlayerRecomendation
    {
        public Player playerFound;
        public string PlayerRecName;
        public float aproximity;
    }

    public class GamersController : ApiController
    {
        private HalfPuggContext db = new HalfPuggContext();

        private Player GetPlayerFromToken(string token)
        {
            Player gamerL = null;
            TokenValidation validation = new TokenValidation();
            string userValidated = validation.ValidateToken(token);
            if (userValidated != null)
            {
                TokenData data = JsonConvert.DeserializeObject<TokenData>(userValidated);
                gamerL = db.Gamers.FirstOrDefault(g => g.ID == data.ID);
            }
            return gamerL;
        }

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
            gamer.Groups = db.PlayerGroups.Where(x => x.IdPlayer == id).ToList();
            gamer.Halls = db.PlayerHalls.Where(x => x.IdPlayer == id).ToList();
          
            return Ok(gamer);
        }

        [Route("api/GamersMatch")]
        [HttpGet]
        public IHttpActionResult GetGamerMatch(int recType)
        {
            Player gamerL = null; // para testes, use> db.Gamers.Find(1); (comentar o token)
            var headers = Request.Headers;
            if (headers.Contains(WebApiConfig.tokenHeader))
            {
                string token = headers.GetValues(WebApiConfig.tokenHeader).First();
                gamerL = GetPlayerFromToken(token);
            }
            if (gamerL == null) return NotFound();

            switch (recType)
            {
                case 1:
                    // Por conhecido de conhecidos
                    // TODO tratar a aproximidade
                    return Ok(adjPlayersConnections(gamerL));
            }
            return BadRequest();
        }

        private List<PlayerRecomendation> adjPlayersConnections(Player logado)
        {
            List<Match> logadoMatches = db.Matches
                                    .Where(x => x.IdPlayer1 == logado.ID || x.IdPlayer2 == logado.ID)
                                    .AsEnumerable().ToList();
            List<PlayerRecomendation> players = new List<PlayerRecomendation>();
            foreach (Match m in logadoMatches)
            {
                Player found = m.IdPlayer1 == logado.ID ? m.Player2 : m.Player1;//await db.Gamers.FindAsync(idToFind);
                PlayerRecomendation recomendation = new PlayerRecomendation()
                { PlayerRecName = found.Nickname, aproximity = 0 };
                if (found != null)
                {
                    List<Match> matchesAdj = db.Matches
                                               .Where(y => (y.IdPlayer1 != logado.ID && y.IdPlayer2 != logado.ID) &&
                                                            (y.IdPlayer1 == found.ID || y.IdPlayer2 == found.ID))
                                               .AsEnumerable().ToList();
                    foreach(Match m2 in matchesAdj)
                    {
                        Player adj = m2.IdPlayer1 == logado.ID ? m2.Player2 : m2.Player1;
                        if (adj != null)
                        {
                            recomendation.playerFound = adj;
                            players.Add(recomendation);
                        }
                    }
                }
            }

            return players;
        }

        [Route("api/GamersMatch")]
        [HttpGet]
        public IHttpActionResult GetGamerMatch()
        {
            Player gamerL = null;

            var headers = Request.Headers;

            if (headers.Contains("token-jwt"))
            {
                string token = headers.GetValues("token-jwt").First();
                gamerL = GetPlayerFromToken(token);
            }

            if (gamerL == null)
            {
                return NotFound();
            }
            List<PlayerRecomendation> gamers = new List<PlayerRecomendation>();
            List<Match> matches = db.Matches
                                    .Where(ma => ma.IdPlayer1 == gamerL.ID || ma.IdPlayer2 == gamerL.ID)
                                    .AsEnumerable().ToList();
            List<RequestedMatch> reqMatches = db.RequestedMatchs
                                                .Where(ma => ma.IdPlayer1 == gamerL.ID || ma.IdPlayer2 == gamerL.ID)
                                                .AsEnumerable().ToList();
            foreach (Player gMatch in db.Gamers)
            {
                if (gMatch.ID != gamerL.ID)
                {
                    Match found = matches.FirstOrDefault(x =>
                     x.IdPlayer2 == gMatch.ID || x.IdPlayer1 == gMatch.ID);

                    RequestedMatch Requested = reqMatches.FirstOrDefault(x =>
                    x.IdPlayer2 == gMatch.ID || x.IdPlayer1 == gMatch.ID);

                    if (found == null && Requested == null)
                    {
                        gamers.Add(new PlayerRecomendation()
                        {
                            playerFound = new Player()
                            {
                                ID = gMatch.ID,
                                Nickname = gMatch.Nickname,
                                Name = gMatch.Name,
                                LastName = gMatch.LastName,
                                Email = gMatch.Email,
                                ImagePath = gMatch.ImagePath,
                                Bio = gMatch.Bio,
                                Slogan = gMatch.Slogan,
                                Sex = gMatch.Sex,
                            },
                            PlayerRecName = "pessoas aleatórias.",
                            aproximity = 0f,
                        });

                    }
                }
            }

            return Json(gamers);
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
            try
            {
                //vf;v  rvar md5 = MD5.Create();l4 l,1,r
                
                db.Gamers.Add(gamer);
                db.SaveChanges();
            } catch(Exception)
            {
                return BadRequest();
            }

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

        [System.Web.Mvc.HttpPost]
        [System.Web.Mvc.AcceptVerbs(HttpVerbs.Post)]
        public ActionResult UploadFoto(HttpPostedFileBase file)
        {
     
            if (file == null)
            {
                file = this.Request.Files[0];
            }

            return null;
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