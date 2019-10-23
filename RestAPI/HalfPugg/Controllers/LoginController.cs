using HalfPugg.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace HalfPugg.Controllers
{
    public class LoginController : ApiController
    {
        HalfPuggContext db;
        public static Gamer GamerLogado = null;

        public LoginController()
        {
            db = new HalfPuggContext();
        }

        // GET: api/Login
        public IHttpActionResult Get()
        {
            //var gamerLogged = db.Gamers.FirstOrDefault(g => g.ID == GamerLogado.ID);

            if (GamerLogado != null)
            {
                return Json<Gamer>(GamerLogado);
            }

            return NotFound();
        }

        // POST: api/Login
        public IHttpActionResult Post(Gamer gamer)
        {
            var gamerLogged = db.Gamers.FirstOrDefault(g => g.Email == gamer.Email && g.HashPassword == gamer.HashPassword);

            if (gamerLogged != null)
            {
                GamerLogado = gamerLogged;
                return Ok();
            }
            return NotFound();
        }

        [HttpDelete]
        [Route("api/logoff")]
        public IHttpActionResult DeleteLogin(Gamer gamer)
        {
            GamerLogado = null;
            return Ok();
        }
    }
}
