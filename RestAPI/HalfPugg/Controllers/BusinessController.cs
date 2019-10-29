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
        
            foreach(PlayerGame pg in db.PlayerGames.Where(x=>x.IDGame == 1))
            {
                names.Add(pg.IdAPI);
                regions.Add(region.us);
                Console.WriteLine(pg.IdAPI);
            }
            var a = OwAPI.GetPlayer(names, regions);
            return Json(a);
        }

        [ResponseType(typeof(IEnumerable<player>))]
        [Route("api/GetOwerwatchMacth")]
        [HttpGet]
        public IHttpActionResult GetOwMatch(int PlayerID)
        {
            List<string> names = new List<string>();
            List<region> regions = new List<region>();
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
                }
            }
            if (p == null) return null;
           
            var player = OwAPI.GetPlayer(p.IdAPI, region.us);
            var a = OwAPI.GetPlayer(names, regions).Where
                (
                m=> 
                m.profile.rating > 1600
                );
           
            return Json(a);
        }

       
        
        [ResponseType(typeof(IEnumerable<player>))]
        [Route("api/GetOwerwatchMacth")]
        [HttpGet]
        public IHttpActionResult GetOwMatchFilter(int PlayerID,owFilter filter)
        {
            List<string> names = new List<string>();
            List<region> regions = new List<region>();
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
                }
            }
            if (p == null) return null;

            var player = OwAPI.GetPlayer(p.IdAPI, region.us);
            var a = OwAPI.GetPlayer(names, regions);

            if (filter.role == 1) a = a.Where(x => x.profile.tank_rating>0);
            else if (filter.role == 2) a = a.Where(x => x.profile.damage_rating > 0);
            else if (filter.role == 4) a = a.Where(x => x.profile.support_rating > 0);

            a = a.Where(x => filterPlayer(x,filter));

            return Json(a);
        }

        bool filterPlayer(player p, owFilter filter)
        {
            bool ret = true;
            
           //rating
            if (filter.rating.Length > 0)
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
            //damage quick
            if (filter.damage.Length > 0)
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
            //level 
            if (filter.level.Length > 0)
            {
                if (filter.level.Length > 1)
                {
                    if (filter.level[1] == -1) ret = p.profile.level > filter.level[0];
                    else if (filter.level[1] > -1 && filter.level[0] > -1) ret = (p.profile.level > filter.level[0] && p.profile.level < filter.level[1]);
                    else if (filter.level[0] == -1) ret = p.profile.level < filter.level[1];
                }
                else
                {
                    ret = p.profile.level == filter.level[0];
                }
            }

            if (!ret) return false;
            //elimination quick
            if (filter.elimination.Length > 0)
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
            if (filter.healing.Length > 0)
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
            if (filter.objTime.Length > 0)
            {
                if (filter.objTime.Length > 1)
                {
                    if (filter.objTime[1] == null) ret = DateTime.Compare(p.quickCareer.objectiveTime , filter.objTime[0]) > 0;
                    else if (filter.objTime[1] !=null && filter.objTime[0] !=null) ret = (DateTime.Compare(p.quickCareer.objectiveTime, filter.objTime[0]) > 0 && DateTime.Compare(p.quickCareer.objectiveTime, filter.objTime[1]) < 0);
                    else if (filter.objTime[0] !=null) ret = DateTime.Compare(p.quickCareer.objectiveTime, filter.objTime[1]) < 0;
                }
                else
                {
                    ret = DateTime.Compare(p.quickCareer.objectiveTime, filter.objTime[0]) == 0;
                }
            }

            if (!ret) return false;
            //onfire quick
            if (filter.onfire.Length > 0)
            {
                if (filter.onfire.Length > 1)
                {
                    if (filter.onfire[1] == null) ret = DateTime.Compare(p.quickCareer.timeSpentOnFire, filter.onfire[0]) > 0;
                    else if (filter.onfire[1] != null && filter.onfire[0] != null) ret = (DateTime.Compare(p.quickCareer.timeSpentOnFire, filter.onfire[0]) > 0 && DateTime.Compare(p.quickCareer.timeSpentOnFire, filter.onfire[1]) < 0);
                    else if (filter.onfire[0] != null) ret = DateTime.Compare(p.quickCareer.timeSpentOnFire, filter.onfire[1]) < 0;
                }
                else
                {
                    ret = DateTime.Compare(p.quickCareer.timeSpentOnFire, filter.onfire[0]) == 0;
                }
            }

            return ret;
        }

    }
}
