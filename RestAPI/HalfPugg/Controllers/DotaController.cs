using HalfPugg._3rd;
using HalfPugg.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;

namespace HalfPugg.Controllers
{
    public class DotaController: ApiController
    {
        private HalfPuggContext db = new HalfPuggContext();
       
     
        [Route("api/Dota/GetPlayers")]
        [HttpGet]
        public IHttpActionResult GetPlayerDota(int PlayerID)
        {
            PlayerGame pg = db.PlayerGames.Where(x => x.IDGamer == PlayerID && x.IDGame == (int)Games.Dota).FirstOrDefault();
            if (pg == null) return NotFound();
            DotaPlayer a = DotaAPI.GetPlayer(pg.IdAPI).Result;
            return Ok(a);
        }

        [Route("api/Dota/DeletePlayers")]
        [HttpDelete]
        public IHttpActionResult DeletePlayerDota(int PlayerID)
        {
            var a= db.PlayerGames.Where(x => x.IDGamer == PlayerID && x.IDGame == (int)Games.Dota).FirstOrDefault();
            if (a == null) return NotFound();
            db.PlayerGames.Remove(a);
            db.SaveChanges();
            return Ok();
        }

        [Route("api/Dota/GetPlayers")]
        [HttpGet]
        public IHttpActionResult GetPlayers()
        {
            var pg = db.PlayerGames.Where(x=>x.IDGame == (int)Games.Dota);
            if (pg == null) return NotFound();
            var a = DotaAPI.GetPlayers(pg.Select(x=>x.IdAPI).ToList());
            return Ok(a);
        }

        [Route("api/Dota/PostPlayerInDota")]
        [HttpPost]
        public async Task<IHttpActionResult> PostPlayerInDota([FromBody]PlayerGame playerGame)
        {

            if (DotaAPI.GetPlayer(playerGame.IdAPI) == null)
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

        [ResponseType(typeof(IEnumerable<DotaPlayer>))]
        [Route("api/Overwatch/GetFilterPlayer")]
        [HttpGet]

        public IHttpActionResult GetOwMatchFilter(int PlayerID, [FromUri]DotaFilter filter)
        {
            Player p = db.Gamers.Find(PlayerID);

            if (p == null)
            {
                return BadRequest("Jogador que requisitou o match não consta no banco");
            }

            List<string> dotaIDs = new List<string>();
            List<DotaPlayer> ret = new List<DotaPlayer>();

            foreach(var pg in db.PlayerGames.Where(x=>x.IDGamer!=PlayerID && x.IDGame == (int)Games.Dota))
            {
                dotaIDs.Add(pg.IdAPI);
            }

            var dp = DotaAPI.GetPlayers(dotaIDs).Where(x => x != null && filterDota(x,filter));

            if (dp == null) NotFound();
            
            return Ok(dp);
            
        }

        bool filterDota(DotaPlayer p, DotaFilter filter)
            {
             bool ret = true;

            //assists
            if (!ret) return false;
            if (filter.assists != null)
            {
                if (filter.assists.Length > 1)
                {
                    if (filter.assists[1] == -1) ret = p.stats.assists > filter.assists[0];
                    else if (filter.assists[1] > -1 && filter.assists[0] > -1) ret = (p.stats.assists > filter.assists[0] && p.stats.assists < filter.assists[1]);
                    else if (filter.assists[0] == -1) ret = p.stats.assists < filter.assists[1];
                }
                else
                {
                    ret = p.stats.assists == filter.assists[0];
                }
            }

            //competitive_rank
            if (!ret) return false;
            if (filter.competitive_rank != null)
            {
                if (filter.competitive_rank.Length > 1)
                {
                    if (filter.competitive_rank[1] == -1) ret = p.competitive_rank > filter.competitive_rank[0];
                    else if (filter.competitive_rank[1] > -1 && filter.competitive_rank[0] > -1) ret = (p.competitive_rank > filter.competitive_rank[0] && p.competitive_rank < filter.competitive_rank[1]);
                    else if (filter.competitive_rank[0] == -1) ret = p.competitive_rank < filter.competitive_rank[1];
                }
                else
                {
                    ret = p.competitive_rank == filter.competitive_rank[0];
                }
            }

            //deaths
            if (!ret) return false;
            if (filter.deaths != null)
            {
                if (filter.deaths.Length > 1)
                {
                    if (filter.deaths[1] == -1) ret = p.stats.deaths > filter.deaths[0];
                    else if (filter.deaths[1] > -1 && filter.deaths[0] > -1) ret = (p.stats.deaths > filter.deaths[0] && p.stats.deaths < filter.deaths[1]);
                    else if (filter.deaths[0] == -1) ret = p.stats.deaths < filter.deaths[1];
                }
                else
                {
                    ret = p.stats.deaths == filter.deaths[0];
                }
            }

            //gold_per_min
            if (!ret) return false;
            if (filter.gold_per_min != null)
            {
                if (filter.gold_per_min.Length > 1)
                {
                    if (filter.gold_per_min[1] == -1) ret = p.stats.gold_per_min > filter.gold_per_min[0];
                    else if (filter.gold_per_min[1] > -1 && filter.gold_per_min[0] > -1) ret = (p.stats.gold_per_min > filter.gold_per_min[0] && p.stats.gold_per_min < filter.gold_per_min[1]);
                    else if (filter.gold_per_min[0] == -1) ret = p.stats.gold_per_min < filter.gold_per_min[1];
                }
                else
                {
                    ret = p.stats.gold_per_min == filter.gold_per_min[0];
                }
            }

            //hero damage
            if (!ret) return false;
            if (filter.hero_damage != null)
            {
                if (filter.hero_damage.Length > 1)
                {
                    if (filter.hero_damage[1] == -1) ret = p.stats.hero_damage > filter.hero_damage[0];
                    else if (filter.hero_damage[1] > -1 && filter.hero_damage[0] > -1) ret = (p.stats.hero_damage > filter.hero_damage[0] && p.stats.hero_damage < filter.hero_damage[1]);
                    else if (filter.hero_damage[0] == -1) ret = p.stats.hero_damage < filter.hero_damage[1];
                }
                else
                {
                    ret = p.stats.hero_damage == filter.hero_damage[0];
                }
            }

            //hero healing
            if (!ret) return false;
            if (filter.hero_healing != null)
            {
                if (filter.hero_healing.Length > 1)
                {
                    if (filter.hero_healing[1] == -1) ret = p.stats.hero_healing > filter.hero_healing[0];
                    else if (filter.hero_healing[1] > -1 && filter.hero_healing[0] > -1) ret = (p.stats.hero_healing > filter.hero_healing[0] && p.stats.hero_healing < filter.hero_healing[1]);
                    else if (filter.hero_healing[0] == -1) ret = p.stats.hero_healing < filter.hero_healing[1];
                }
                else
                {
                    ret = p.stats.hero_healing == filter.hero_healing[0];
                }
            }

            //kills
            if (!ret) return false;
            if (filter.kills != null)
            {
                if (filter.kills.Length > 1)
                {
                    if (filter.kills[1] == -1) ret = p.stats.kills > filter.kills[0];
                    else if (filter.kills[1] > -1 && filter.kills[0] > -1) ret = (p.stats.kills > filter.kills[0] && p.stats.kills < filter.kills[1]);
                    else if (filter.kills[0] == -1) ret = p.stats.kills < filter.kills[1];
                }
                else
                {
                    ret = p.stats.kills == filter.kills[0];
                }
            }

            //last_hits
            if (!ret) return false;
            if (filter.last_hits != null)
            {
                if (filter.last_hits.Length > 1)
                {
                    if (filter.last_hits[1] == -1) ret = p.stats.last_hits > filter.last_hits[0];
                    else if (filter.last_hits[1] > -1 && filter.last_hits[0] > -1) ret = (p.stats.last_hits > filter.last_hits[0] && p.stats.last_hits < filter.last_hits[1]);
                    else if (filter.last_hits[0] == -1) ret = p.stats.last_hits < filter.last_hits[1];
                }
                else
                {
                    ret = p.stats.last_hits == filter.last_hits[0];
                }
            }

            //mmr_estimate
            if (!ret) return false;
            if (filter.mmr_estimate != null)
            {
                if (filter.mmr_estimate.Length > 1)
                {
                    if (filter.mmr_estimate[1] == -1) ret = p.mmr_estimate.estimate > filter.mmr_estimate[0];
                    else if (filter.mmr_estimate[1] > -1 && filter.mmr_estimate[0] > -1) ret = (p.mmr_estimate.estimate > filter.mmr_estimate[0] && p.mmr_estimate.estimate < filter.mmr_estimate[1]);
                    else if (filter.mmr_estimate[0] == -1) ret = p.mmr_estimate.estimate < filter.mmr_estimate[1];
                }
                else
                {
                    ret = p.mmr_estimate.estimate == filter.mmr_estimate[0];
                }
            }

            //rank_tier
            if (!ret) return false;
            if (filter.rank_tier != null)
            {
                if (filter.rank_tier.Length > 1)
                {
                    if (filter.rank_tier[1] == -1) ret = p.rank_tier > filter.rank_tier[0];
                    else if (filter.rank_tier[1] > -1 && filter.rank_tier[0] > -1) ret = (p.rank_tier > filter.rank_tier[0] && p.rank_tier < filter.rank_tier[1]);
                    else if (filter.rank_tier[0] == -1) ret = p.rank_tier < filter.rank_tier[1];
                }
                else
                {
                    ret = p.rank_tier == filter.rank_tier[0];
                }
            }

            //tower_damage
            if (!ret) return false;
            if (filter.tower_damage != null)
            {
                if (filter.tower_damage.Length > 1)
                {
                    if (filter.tower_damage[1] == -1) ret = p.stats.tower_damage > filter.tower_damage[0];
                    else if (filter.tower_damage[1] > -1 && filter.tower_damage[0] > -1) ret = (p.stats.tower_damage > filter.tower_damage[0] && p.stats.tower_damage < filter.tower_damage[1]);
                    else if (filter.tower_damage[0] == -1) ret = p.stats.tower_damage < filter.tower_damage[1];
                }
                else
                {
                    ret = p.stats.tower_damage == filter.tower_damage[0];
                }
            }

            //xp_per_min
            if (!ret) return false;
            if (filter.xp_per_min != null)
            {
                if (filter.xp_per_min.Length > 1)
                {
                    if (filter.xp_per_min[1] == -1) ret = p.stats.xp_per_min > filter.xp_per_min[0];
                    else if (filter.xp_per_min[1] > -1 && filter.xp_per_min[0] > -1) ret = (p.stats.xp_per_min > filter.xp_per_min[0] && p.stats.xp_per_min < filter.xp_per_min[1]);
                    else if (filter.xp_per_min[0] == -1) ret = p.stats.xp_per_min < filter.xp_per_min[1];
                }
                else
                {
                    ret = p.stats.xp_per_min == filter.xp_per_min[0];
                }
            }

            
            return ret;
            }

        }
}