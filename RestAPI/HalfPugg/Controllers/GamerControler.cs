using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using HalfPugg.Models;
using HalfPugg.TokenJWT;
using Newtonsoft.Json;


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
                try
                {
                    gamerL = db.Gamers.FirstOrDefault(g => g.ID == data.ID);
                }
                catch(Exception e)
                {

                }

               
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
                      
            return Ok(gamer);
        }

        [HttpGet]
        [Route("api/Player/GetGames")]
        public IHttpActionResult GetGames(int PlayerID)
        {
            Player gamer = db.Gamers.Find(PlayerID);
            if (gamer == null)
            {
                return NotFound();
            }
            var res = db.PlayerGames.Where(x => x.IDGamer == PlayerID).Select(x => x.Game);

            return Ok(res);
        }

        [HttpGet]
        [Route("api/GetGamerByNickname")]
        public IHttpActionResult GetGamerByNickname(string nickname)
        {
            Player player = db.Gamers.FirstOrDefault(x => x.Nickname == nickname);
            if(player == null)
            {
                return NotFound();
            }
            player.HashPassword = null;
            return Ok(player);
        }

        [HttpGet]
        [ResponseType(typeof(List<PlayerRecomendation>))]
        public IHttpActionResult GetGamer(string nickname)
        {
            List<PlayerRecomendation> playerRecomendations = new List<PlayerRecomendation>();
            List<Player> players = db.Gamers.Where(g => g.Nickname.Contains(nickname)).AsEnumerable().ToList();
            foreach(Player p1 in players)
            {
                playerRecomendations.Add(new PlayerRecomendation()
                {
                    aproximity = (nickname.Length / p1.Nickname.Length) * 100,
                    playerFound = p1,
                    PlayerRecName = "busca por nickname"
                });
            }
            players = db.Gamers.Where(g => (g.Name + " " + g.LastName).Contains(nickname)).AsEnumerable().ToList();
            foreach (Player p1 in players)
            {
                if (players.Contains(p1)) continue;
                playerRecomendations.Add(new PlayerRecomendation()
                {
                    aproximity = (nickname.Length / p1.Nickname.Length) * 100,
                    playerFound = p1,
                    PlayerRecName = "busca por nome completo"
                });
            }
            return Ok(playerRecomendations);
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
                    return Ok(adjPlayersConnections(gamerL));
                case 2:
                    return Ok(RandomPlayers(gamerL));
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
                if (found != null)
                {
                    List<Match> matchesAdj = db.Matches
                                               .Where(y => (y.IdPlayer1 != logado.ID && y.IdPlayer2 != logado.ID) &&
                                                            (y.IdPlayer1 == found.ID || y.IdPlayer2 == found.ID))
                                               .AsEnumerable().ToList();
                    foreach(Match m2 in matchesAdj)
                    {
                        PlayerRecomendation recomendation = new PlayerRecomendation()
                        { PlayerRecName = found.Nickname, aproximity = 0 };
                        Player adj = m2.IdPlayer1 == found.ID ? m2.Player2 : m2.Player1;
                        if (adj != null && logadoMatches.FirstOrDefault(x => x.IdPlayer1 == adj.ID || x.IdPlayer2 == adj.ID) == null && players.FirstOrDefault(p => p.playerFound.ID == adj.ID) == null)
                        {
                            recomendation.playerFound = adj;
                            players.Add(recomendation);
                        }
                    }
                }
            }

            return players;
        }

        public List<PlayerRecomendation> RandomPlayers(Player gamerL)
        {
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

            return gamers;
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
        [HttpPost]
        [Route("api/CadastroPlayer")]
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
            } catch(Exception e)
            {
                return BadRequest();
            }

            return Ok(gamer);
        }

        [ResponseType(typeof(List<Player>))]
        [HttpPost]
        [Route("api/ListaGamers")]
        public IHttpActionResult PostListGamers(List<Player> gamers)
        {
            foreach(Player player in gamers)
            {
                if (db.Gamers.Where(g => g.Nickname == player.Nickname).Count() == 0)
                {
                    db.Gamers.Add(player);
                }
            }
            db.SaveChanges();
            return Ok();
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

       
        [ResponseType(typeof(ICollection<Group>))]
        [Route("api/Gamers/GetGroups")]
        [HttpGet]
        public IHttpActionResult GetPlayerGroups(int id)
        {
            Player p = db.Gamers.Find(id);
            if (p == null) return BadRequest($"Player {id} not founded");

            List<dynamic> ret = new List<dynamic>();

            var g = db.PlayerGroups.Where(x => x.IdPlayer == id).Select(x => x.Group).ToArray();
            
            foreach(var x in g)
            {
                x.Admin = db.Gamers.Find(x.IdAdmin);
                x.CreateAt = db.Groups.Find(x.ID).CreateAt;
                x.Game = db.Games.Find(x.IdGame);

                ret.Add(new {
                    ID = x.ID,
                    Name = x.Name,
                    Desc = "Sem descricao",
                    Admin = x.Admin.Name,
                    Capacity = x.Capacity,
                    PlayerCount = 1,
                    CreateAt = x.CreateAt,
                    Game = x.Game.Name,
                    ImagePath = x.SouceImg
                }) ;
               
            }

            return Ok(ret);
        }

        
        [ResponseType(typeof(ICollection<Player>))]
        [Route("api/GetGamersNear")]
        [HttpGet]
        public IHttpActionResult GetGamersDeep(int id,int qnt)
        {
            Graph<Player, Match, int> graph = new Graph<Player, Match, int>((Match e) => { return e.Weight != 0 ? 1 / e.Weight : float.MaxValue; });

            foreach (var m in db.Matches.ToArray())
            {

                graph.AddVertice(m.Player1, m.IdPlayer1);
                graph.AddVertice(m.Player2, m.IdPlayer2);
                graph.AddAresta(m.IdPlayer1, m.IdPlayer2, m);
            }

            var sorted = graph.ShortPath(id).OrderBy(x => x.Value).Skip(1).Take(qnt);
            List<Player> players = new List<Player>(sorted.Count());
            foreach(var v in sorted)
            {
                players.Add(db.Gamers.Find(v.Key));
               
            }
            return Ok(players);

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