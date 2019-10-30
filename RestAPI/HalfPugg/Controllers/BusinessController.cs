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
   

    

    public class BusinessController : ApiController
    {
      
        
        
        private HalfPuggContext db = new HalfPuggContext();

        /// <summary>
        /// Find all overwatch players
        /// </summary>
        /// <returns>(Json)IEnumerable<player></returns>
        [ResponseType(typeof(IEnumerable<player>))]
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

        [ResponseType(typeof(IEnumerable<player>))]
        [Route("api/GetOwerwatchMacth")]
        [HttpGet]
        public IHttpActionResult GetOwMatch(int PlayerID)
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
            if (p == null) return null;
           
            var player = OwAPI.GetPlayer(p.IdAPI, region.us,p.IDGamer);
            var a = OwAPI.GetPlayer(names, regions, ids).Where
                (
                m=> 
                m.profile.rating > 1600
                );
           
            return Json(a);
        }

        [ResponseType(typeof(IEnumerable<player>))]
        [Route("api/GetOwMatchFilter")]
        [HttpGet]
        public IHttpActionResult GetOwMatchFilter(int PlayerID, owFilter filter)
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
            if (p == null) return null;

            var player = OwAPI.GetPlayer(p.IdAPI, region.us, p.IDGamer);
            var a = OwAPI.GetPlayer(names, regions, ids).Where(x => filterPlayer(x, filter));

          

            return Json(a);
        }

        bool filterPlayer(player p, owFilter filter)
        {
            bool ret = true;

            if (filter.role == 1) ret = p.profile.tank_rating > 0;
            else if (filter.role == 2) ret = p.profile.damage_rating > 0;
            else if (filter.role == 4) ret = p.profile.support_rating > 0;

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
                int l = p.profile.level + p.profile.endorsement * 100;
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
