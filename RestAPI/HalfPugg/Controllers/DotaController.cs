﻿using HalfPugg._3rd;
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
            DotaPlayer a = DotaAPI.GetPlayer(pg.IdAPI, pg.IDGamer).Result;
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
            var a = DotaAPI.GetPlayers(pg.Select(x=>x.IdAPI).ToList(), pg.Select(x=>x.IDGamer).ToList());
            return Ok(a);
        }

        [Route("api/Dota/PostPlayerInDota")]
        [HttpPost]
        public async Task<IHttpActionResult> PostPlayerInDota([FromBody]PlayerGame playerGame)
        {

            if (DotaAPI.GetPlayer(playerGame.IdAPI, playerGame.IDGamer) == null)
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

        [Route("api/Dota/FilterPlayerRec")]
        [HttpPost]
        public IHttpActionResult GetPlayerFilterRec(int PlayerID, [FromUri]DotaFilter filter)
        {
            var Ows = GetDotaMatchFilter(PlayerID, filter);
            List<PlayerRecomendation> recom = new List<PlayerRecomendation>();
            List<Match> playerMatches = db.Matches.Where(x => x.IdPlayer1 == PlayerID || x.IdPlayer2 == PlayerID).AsEnumerable().ToList();
            if (Ows == null)
            {
                return BadRequest("Jogador que requisitou o match não consta no banco");
            }
            else
            {
                foreach (DotaPlayer ow in Ows)
                {
                    if (playerMatches.Find(pm => pm.IdPlayer1 == ow.idHalf || pm.IdPlayer2 == ow.idHalf) != null)
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

        [ResponseType(typeof(IEnumerable<DotaPlayer>))]
        [Route("api/Dota/GetNearPlayer")]
        [HttpGet]
        public async Task<IHttpActionResult> GetNearPlayer(int PlayerID, float percent)
        {
            Graph<Player, Match, string> graph = new Graph<Player, Match, string>((Match e) => { return e.Weight != 0 ? 1 / e.Weight : float.MaxValue; });

            PlayerGame currentPlayer = db.PlayerGames.Where(x => x.IDGamer == PlayerID).FirstOrDefault();

            foreach (var m in db.Matches.ToArray())
            {
                var p1 = db.PlayerGames.Where(x => x.IDGamer == m.IdPlayer1 && x.IDGame == (int)Games.Overwatch).FirstOrDefault();
                var p2 = db.PlayerGames.Where(x => x.IDGamer == m.IdPlayer2 && x.IDGame == (int)Games.Overwatch).FirstOrDefault();

                if (p1 != null && p2 != null)
                {
                    graph.AddVertice(m.Player1, p1.IdAPI);
                    graph.AddVertice(m.Player2, p2.IdAPI);
                    graph.AddAresta(p1.IdAPI, p2.IdAPI, m);
                }

            }

            DotaPlayer dotaPlayer = await DotaAPI.GetPlayer(currentPlayer.IdAPI,  PlayerID);
            PlayerStats career = dotaPlayer.stats;
            float perc = percent / 100.0f;

            DotaFilter filter = new DotaFilter
            {
                mmr_estimate = new int[] { (int)(dotaPlayer.mmr_estimate.estimate * (1.0f - perc)), (int)(dotaPlayer.mmr_estimate.estimate * (1.0f + perc)) },
                kills = new int[] { (int)(career.kills * (1.0f - perc)), (int)(career.kills * (1.0f + perc)) },
                xp_per_min = new int[] { (int)(career.xp_per_min * (1.0f - perc)), (int)(career.xp_per_min * (1.0f + perc)) },
                hero_damage = new int[] { (int)(career.hero_damage * (1.0f - perc)), (int)(career.hero_damage * (1.0f + perc)) },
                gold_per_min = new int[] { (int)(career.gold_per_min * (1.0f - perc)), (int)(career.gold_per_min * (1.0f + perc)) },
            };

            var sorted = graph.ShortPath(currentPlayer.IdAPI).OrderBy(x => x.Value).Skip(1).Take(graph.vertCount);
            List<DotaPlayer> players = new List<DotaPlayer>(sorted.Count());
            foreach (var v in sorted)
            {
                DotaPlayer pp =await DotaAPI.GetPlayer(v.Key,0);
                if (filterDota(pp, filter))
                    players.Add(pp);

            }

            return Ok(players);

        }

        public IEnumerable<DotaPlayer> GetDotaMatchFilter(int PlayerID, [FromUri]DotaFilter filter)
        {
            Player p = db.Gamers.Find(PlayerID);

            if (p == null)
            {
                return null;//BadRequest("Jogador que requisitou o match não consta no banco");
            }

            List<string> dotaIDs = new List<string>();
            List<int> halfIDS = new List<int>();
            List<DotaPlayer> ret = new List<DotaPlayer>();

            foreach (var pg in db.PlayerGames.Where(x => x.IDGamer != PlayerID && x.IDGame == (int)Games.Dota))
            {
                halfIDS.Add(pg.IDGamer);
                dotaIDs.Add(pg.IdAPI);
            }

            var dp = DotaAPI.GetPlayers(dotaIDs, halfIDS).Where(x => x != null && filterDota(x, filter));

            if (dp == null) NotFound();

            return dp;

        }

        [ResponseType(typeof(IEnumerable<DotaPlayer>))]
        [Route("api/Dota/GetFilterPlayer")]
        [HttpGet]

        public IHttpActionResult GetOwMatchFilter(int PlayerID, [FromUri]DotaFilter filter)
        {
            Player p = db.Gamers.Find(PlayerID);

            if (p == null)
            {
                return BadRequest("Jogador que requisitou o match não consta no banco");
            }

            List<string> dotaIDs = new List<string>();
            List<int> halfIDS = new List<int>();
            List<DotaPlayer> ret = new List<DotaPlayer>();

            foreach(var pg in db.PlayerGames.Where(x=>x.IDGamer!=PlayerID && x.IDGame == (int)Games.Dota))
            {
                halfIDS.Add(pg.IDGamer);
                dotaIDs.Add(pg.IdAPI);
            }

            var dp = DotaAPI.GetPlayers(dotaIDs, halfIDS).Where(x => x != null && filterDota(x,filter));

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