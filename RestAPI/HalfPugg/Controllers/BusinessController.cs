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

namespace HalfPugg.Controllers
{
    public class BusinessController : ApiController
    {
        private HalfPuggContext db = new HalfPuggContext();

       
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

            foreach (PlayerGame pg in db.PlayerGames.Where(x => x.IDGame == 1))
            {
                names.Add(pg.IdAPI);
                regions.Add(region.us);
                Console.WriteLine(pg.IdAPI);
            }
            var a = OwAPI.GetPlayer(names, regions);
            return Json(a);
        }

    }
}
