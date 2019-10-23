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
        [Route("api/GetPlayerOw")]
        [HttpGet]
        public IHttpActionResult GetPlayerOw(int GameID)
        {
            List<string> names = new List<string>();
            List<region> regions = new List<region>();

            foreach(PlayerGame pg in db.PlayerGames.Where(x=>x.IDGame == GameID))
            {
                names.Add(pg.IdAPI);
                regions.Add(region.us);
            }

            return Json(OwAPI.GetPlayer(names, regions));
        }

    }
}
