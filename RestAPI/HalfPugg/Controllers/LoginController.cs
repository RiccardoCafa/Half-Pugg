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

        public LoginController()
        {
            db = new HalfPuggContext();
        }

        // GET: api/Login
        public IHttpActionResult Get()
        {
            if (HttpContext.Current.Session["ID"] != null)
            {
                var gamer = db.Gamers
                                .FirstOrDefault(g => 
                                    g.ID == int.Parse(HttpContext.Current.Session["ID"].ToString()));
                if (gamer != null)
                    return Ok<Gamer>(gamer);
                else return BadRequest();
            }
            else return NotFound();
        }

        // POST: api/Login
        public IHttpActionResult Post(Gamer gamer)
        {
            var gamerLogged = db.Gamers.FirstOrDefault(g => g.ID == gamer.ID && g.HashPassword == gamer.HashPassword);

            if(gamerLogged != null)
            {
                HttpContext.Current.Session["ID"] = gamer.ID;
                return Json<Gamer>(gamer);
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
