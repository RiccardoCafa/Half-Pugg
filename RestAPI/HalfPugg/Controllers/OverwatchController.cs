using HalfPugg.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using OverwatchAPI;
using System.Threading.Tasks;
using System.Web.Http.Results;
using System.Web.Http.Description;
using System.Data.Entity;

namespace HalfPugg.Controllers
{
    public class OverwatchController : ApiController
    {
      /// <summary>
      /// PlayerID : id do jogador no half
      /// Region : região do jogador 0-> us, 1-> eu, 2-> asia
      /// </summary>
        private HalfPuggContext db = new HalfPuggContext();


        #region GET
        [ResponseType(typeof(IEnumerable<OwPlayer>))]
        [Route("api/GetPlayersOwerwatch")]
        [HttpGet]
        public IHttpActionResult GetPlayerOw(int PlayerID, region Region)
        {
            PlayerGame pg = db.PlayerGames.Where(x => x.IDGamer == PlayerID && x.IDGame == (int)Games.Overwatch).FirstOrDefault();
            if (pg == null) return NotFound();
            var a = OwAPI.GetPlayer(pg.IdAPI, Region, PlayerID);
            if (a == null) return NotFound();
            return Ok(a);
        }

        [ResponseType(typeof(OwPlayer))]
        [Route("api/GetPlayerOwerwatch")]
        [HttpGet]
        public IHttpActionResult GetPlayerOw(int PlayerID)
        {
            PlayerGame pg = db.PlayerGames.Where(x => x.IDGamer == PlayerID && x.IDGame == (int)Games.Overwatch).FirstOrDefault();
            if (pg == null) return NotFound();
            var a = OwAPI.GetPlayer(pg.IdAPI, region.us, PlayerID);
            if (a == null) return NotFound();
            return Ok(a);
        }

        public OwPlayer GetPlayerOwObject(int PlayerID, region Region)
        {
            PlayerGame pg = db.PlayerGames.Where(x => x.IDGamer == PlayerID).FirstOrDefault();
            if (pg == null) return null;
            var a = OwAPI.GetPlayer(pg.IdAPI, Region, PlayerID);
            if (a == null) return null;
            return a;
        }

        [ResponseType(typeof(IEnumerable<OwPlayer>))]
        [Route("api/GetPlayersOwerwatch")]
        [HttpGet]
        public IHttpActionResult GetPlayerOw()
        {
            List<string> names = new List<string>();
            List<region> regions = new List<region>();
            List<int> ids = new List<int>();
        
            foreach(PlayerGame pg in db.PlayerGames.Where(x=>x.IDGame == 1))
            {
                names.Add(pg.IdAPI);
                regions.Add(region.us);
                ids.Add(pg.IDGamer);
                Console.WriteLine(pg.IdAPI);
            }
            var a = OwAPI.GetPlayer(names, regions, ids);
            return Json(a);
        }

        [ResponseType(typeof(IEnumerable<OwPlayer>))]
        [Route("api/FilterPlayerOverwatch")]
        [HttpGet]
        public IHttpActionResult GetOwMatchFilter(int PlayerID, [FromUri]owFilter filter)
        {
            var ows = OwMatchFilter(PlayerID, filter);
            if(ows == null)
            {
                return BadRequest("Jogador que requisitou o match não consta no banco");
            } else
            {
                return Ok(ows);
            }
        }

        public IEnumerable<OwPlayer> OwMatchFilter(int PlayerID, [FromUri]owFilter filter)
        {
            List<string> names = new List<string>();
            List<region> regions = new List<region>();
            List<int> ids = new List<int>();
            PlayerGame p = null;

            foreach (PlayerGame pg in db.PlayerGames.Where(x => x.IDGame == 1))
            {
                if (pg.IDGamer == PlayerID)
                {
                    p = pg;
                }
                else
                {
                    names.Add(pg.IdAPI);
                    regions.Add(region.us);
                    ids.Add(pg.IDGamer);
                }
            }
            if (p == null) return null;//BadRequest("Jogador que requisitou o match não consta no banco"); //400

            var player = OwAPI.GetPlayer(p.IdAPI, region.us, p.IDGamer);
            var a = OwAPI.GetPlayer(names, regions, ids).Where(x => filterPlayer(x, filter));

            return a;//201
        }

        [ResponseType(typeof(IEnumerable<PlayerRecomendation>))]
        [Route("api/FilterPlayerRecOverwatch")]
        [HttpPost]
        public IHttpActionResult GetOwPlayerRecFilter(int PlayerID, owFilter filter)
        {
            var Ows = OwMatchFilter(PlayerID, filter);
            List<PlayerRecomendation> recom = new List<PlayerRecomendation>();
            List<Match> playerMatches = db.Matches.Where(x => x.IdPlayer1 == PlayerID || x.IdPlayer2 == PlayerID).AsEnumerable().ToList();
            if(Ows == null)
            {
                return BadRequest("Jogador que requisitou o match não consta no banco");
            } else
            {
                foreach(OwPlayer ow in Ows)
                {
                    if(playerMatches.Find(pm => pm.IdPlayer1 == ow.idHalf || pm.IdPlayer2 == ow.idHalf) != null)
                    {
                        continue;
                    }
                    PlayerRecomendation reco = new PlayerRecomendation()
                    {
                        playerFound = db.Gamers.Find(ow.idHalf),
                        PlayerRecName = "filtro Overwatch",
                        aproximity = 0f
                    };
                    recom.Add(reco);
                }
            }

            return Ok(recom);
        }

        #endregion

        #region POST
        [ResponseType(typeof(PlayerGame))]
        [Route("api/PostPlayerInOw", Name = "PostPlayerInOw")]
        [HttpPost]
        public async Task<IHttpActionResult> PostPlayerInOw([FromBody]PlayerGame playerGame,[FromUri]region Region)
        {
            if (OwAPI.GetPlayerProfile(playerGame.IdAPI, Region, playerGame.IDGamer) == null)
            {
                return BadRequest($"{playerGame.IdAPI} não possui conta associada a overwatch ou conta não é publica");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            playerGame.Game = db.Games.Find(playerGame.IDGame);
            playerGame.Gamer = db.Gamers.Find(playerGame.IDGamer);
            db.PlayerGames.Add(playerGame);
            await db.SaveChangesAsync();

            return Ok(playerGame);
        }

        #endregion
       
        bool filterPlayer(OwPlayer p, owFilter filter)
        {
            bool ret = true;

            if (filter.role == 1) ret = p.profile.tank_rating > 0;
            else if (filter.role == 2) ret = p.profile.damage_rating > 0;
            else if (filter.role == 4) ret = p.profile.support_rating > 0;
            else if (filter.role == 3) ret = (p.profile.tank_rating > 0 || p.profile.damage_rating > 0);
            else if (filter.role == 5) ret = (p.profile.tank_rating > 0 || p.profile.support_rating > 0);
            else if (filter.role == 6) ret = (p.profile.damage_rating > 0 || p.profile.support_rating > 0);

            if (!ret) return false;

            //rating
            if (filter.rating != null)
            {
                if (filter.rating.Length > 1)
                {
                    if (filter.rating[1] == -1) ret = p.profile.rating > filter.rating[0];
                    else if (filter.rating[1] > -1 && filter.rating[0] > -1) ret = (p.profile.rating > filter.rating[0] && p.profile.rating < filter.rating[1]);
                    else if (filter.rating[0] == -1) ret = p.profile.rating < filter.rating[1];
                }
                else
                {
                   ret = p.profile.rating == filter.rating[0];
                }
            }

            if (!ret) return false;
            //level 
            if (filter.level != null)
            {
                int l = p.profile.level + p.profile.prestige * 100;
                if (filter.level.Length > 1)
                {
                    if (filter.level[1] == -1) ret = l > filter.level[0];
                    else if (filter.level[1] > -1 && filter.level[0] > -1) ret = (l > filter.level[0] && l < filter.level[1]);
                    else if (filter.level[0] == -1) ret = l < filter.level[1];
                }
                else
                {
                    ret = l == filter.level[0];
                }
            }

            if (filter.competitive)
            {
                if (!ret) return false;
                //damage quick
                if (filter.damage != null )
                {
                    if (filter.damage.Length > 1)
                    {
                        if (filter.damage[1] == -1) ret = p.compCareer.allDamageDone > filter.damage[0];
                        else if (filter.damage[1] > -1 && filter.damage[0] > -1) ret = (p.compCareer.allDamageDone > filter.damage[0] && p.compCareer.allDamageDone < filter.damage[1]);
                        else if (filter.damage[0] == -1) ret = p.compCareer.allDamageDone < filter.damage[1];
                    }
                    else
                    {
                        ret = p.compCareer.allDamageDone == filter.damage[0];
                    }
                }

                if (!ret) return false;
                //elimination quick
                if (filter.elimination != null)
                {
                    if (filter.elimination.Length > 1)
                    {
                        if (filter.elimination[1] == -1) ret = p.compCareer.eliminations > filter.elimination[0];
                        else if (filter.elimination[1] > -1 && filter.elimination[0] > -1) ret = (p.compCareer.eliminations > filter.elimination[0] && p.compCareer.eliminations < filter.elimination[1]);
                        else if (filter.elimination[0] == -1) ret = p.compCareer.eliminations < filter.elimination[1];
                    }
                    else
                    {
                        ret = p.compCareer.eliminations == filter.elimination[0];
                    }
                }

                if (!ret) return false;
                //healing quick
                if (filter.healing != null)
                {
                    if (filter.healing.Length > 1)
                    {
                        if (filter.healing[1] == -1) ret = p.compCareer.healingDone > filter.healing[0];
                        else if (filter.healing[1] > -1 && filter.healing[0] > -1) ret = (p.compCareer.healingDone > filter.healing[0] && p.compCareer.healingDone < filter.healing[1]);
                        else if (filter.healing[0] == -1) ret = p.compCareer.healingDone < filter.healing[1];
                    }
                    else
                    {
                        ret = p.compCareer.healingDone == filter.healing[0];
                    }
                }

                if (!ret) return false;
                //objtime quick
                if (filter.objTime != null)
                {
                    if (filter.objTime.Length > 1)
                    {
                        if (filter.objTime[1] == null) ret = TimeSpan.Compare(p.compCareer.objectiveTime, filter.objTime[0]) > 0;
                        else if (filter.objTime[1] != null && filter.objTime[0] != null) ret = (TimeSpan.Compare(p.compCareer.objectiveTime, filter.objTime[0]) > 0 && TimeSpan.Compare(p.compCareer.objectiveTime, filter.objTime[1]) < 0);
                        else if (filter.objTime[0] != null) ret = TimeSpan.Compare(p.compCareer.objectiveTime, filter.objTime[1]) < 0;
                    }
                    else
                    {
                        ret = TimeSpan.Compare(p.compCareer.objectiveTime, filter.objTime[0]) == 0;
                    }
                }

                if (!ret) return false;
                //onfire quick
                if (filter.onfire != null)
                {
                    if (filter.onfire.Length > 1)
                    {
                        if (filter.onfire[1] == null) ret = TimeSpan.Compare(p.compCareer.timeSpentOnFire, filter.onfire[0]) > 0;
                        else if (filter.onfire[1] != null && filter.onfire[0] != null) ret = (TimeSpan.Compare(p.compCareer.timeSpentOnFire, filter.onfire[0]) > 0 && TimeSpan.Compare(p.compCareer.timeSpentOnFire, filter.onfire[1]) < 0);
                        else if (filter.onfire[0] != null) ret = TimeSpan.Compare(p.compCareer.timeSpentOnFire, filter.onfire[1]) < 0;
                    }
                    else
                    {
                        ret = TimeSpan.Compare(p.compCareer.timeSpentOnFire, filter.onfire[0]) == 0;
                    }
                }
            }
            else
            {
                if (!ret) return false;
                //damage quick
                if (filter.damage != null)
                {
                    if (filter.damage.Length > 1)
                    {
                        if (filter.damage[1] == -1) ret = p.quickCareer.allDamageDone > filter.damage[0];
                        else if (filter.damage[1] > -1 && filter.damage[0] > -1) ret = (p.quickCareer.allDamageDone > filter.damage[0] && p.quickCareer.allDamageDone < filter.damage[1]);
                        else if (filter.damage[0] == -1) ret = p.quickCareer.allDamageDone < filter.damage[1];
                    }
                    else
                    {
                        ret = p.quickCareer.allDamageDone == filter.damage[0];
                    }
                }

                if (!ret) return false;
                //elimination quick
                if (filter.elimination != null)
                {
                    if (filter.elimination.Length > 1)
                    {
                        if (filter.elimination[1] == -1) ret = p.quickCareer.eliminations > filter.elimination[0];
                        else if (filter.elimination[1] > -1 && filter.elimination[0] > -1) ret = (p.quickCareer.eliminations > filter.elimination[0] && p.quickCareer.eliminations < filter.elimination[1]);
                        else if (filter.elimination[0] == -1) ret = p.quickCareer.eliminations < filter.elimination[1];
                    }
                    else
                    {
                        ret = p.quickCareer.eliminations == filter.elimination[0];
                    }
                }

                if (!ret) return false;
                //healing quick
                if (filter.healing != null)
                {
                    if (filter.healing.Length > 1)
                    {
                        if (filter.healing[1] == -1) ret = p.quickCareer.healingDone > filter.healing[0];
                        else if (filter.healing[1] > -1 && filter.healing[0] > -1) ret = (p.quickCareer.healingDone > filter.healing[0] && p.quickCareer.healingDone < filter.healing[1]);
                        else if (filter.healing[0] == -1) ret = p.quickCareer.healingDone < filter.healing[1];
                    }
                    else
                    {
                        ret = p.quickCareer.healingDone == filter.healing[0];
                    }
                }

                if (!ret) return false;
                //objtime quick
                if (filter.objTime != null)
                {
                    if (filter.objTime.Length > 1)
                    {
                        if (filter.objTime[1] == null) ret = TimeSpan.Compare(p.quickCareer.objectiveTime, filter.objTime[0]) > 0;
                        else if (filter.objTime[1] != null && filter.objTime[0] != null) ret = (TimeSpan.Compare(p.quickCareer.objectiveTime, filter.objTime[0]) > 0 && TimeSpan.Compare(p.quickCareer.objectiveTime, filter.objTime[1]) < 0);
                        else if (filter.objTime[0] != null) ret = TimeSpan.Compare(p.quickCareer.objectiveTime, filter.objTime[1]) < 0;
                    }
                    else
                    {
                        ret = TimeSpan.Compare(p.quickCareer.objectiveTime, filter.objTime[0]) == 0;
                    }
                }

                if (!ret) return false;
                //onfire quick
                if (filter.onfire != null)
                {
                    if (filter.onfire.Length > 1)
                    {
                        if (filter.onfire[1] == null) ret = TimeSpan.Compare(p.quickCareer.timeSpentOnFire, filter.onfire[0]) > 0;
                        else if (filter.onfire[1] != null && filter.onfire[0] != null) ret = (TimeSpan.Compare(p.quickCareer.timeSpentOnFire, filter.onfire[0]) > 0 && TimeSpan.Compare(p.quickCareer.timeSpentOnFire, filter.onfire[1]) < 0);
                        else if (filter.onfire[0] != null) ret = TimeSpan.Compare(p.quickCareer.timeSpentOnFire, filter.onfire[1]) < 0;
                    }
                    else
                    {
                        ret = TimeSpan.Compare(p.quickCareer.timeSpentOnFire, filter.onfire[0]) == 0;
                    }
                }
            }

            return ret;
        }

    }
}
