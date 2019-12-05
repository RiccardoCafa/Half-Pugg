using HalfPugg.Models;
using OverwatchAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace HalfPugg.Controllers
{
    public class CurriculoInformation
    {
        public Player gamer;
        public int ConnectionsLenght;
        public float Stars;
    }

    public class CurriculoController : ApiController
    {
        private HalfPuggContext db = new HalfPuggContext();

        [HttpGet]
        [Route("api/Curriculo")]
        public IHttpActionResult GetCurriculoInfo(int GamerID)
        {
            CurriculoInformation curriculoInfo = null;
            curriculoInfo = new CurriculoInformation();
            Player p1 = db.Gamers.Find(GamerID);
            curriculoInfo.gamer = db.Gamers.Find(GamerID);
            int conexoes = db.Matches.Where(ma => ma.IdPlayer1 == GamerID || ma.IdPlayer2 == GamerID).Count();
            curriculoInfo.ConnectionsLenght = conexoes;
            curriculoInfo.Stars = p1.MPoints;
            return Ok(curriculoInfo);
        }
    }
}
