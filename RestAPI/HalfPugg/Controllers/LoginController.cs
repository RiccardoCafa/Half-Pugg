using HalfPugg.Models;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Security.Claims;
using System.Collections.Generic;
using System.IdentityModel.Tokens;

namespace HalfPugg.Controllers
{
    public class LoginController : ApiController
    {
        HalfPuggContext db;
        public static int IDLoggado = -1;

        public LoginController()
        {
            db = new HalfPuggContext();
        }

        // GET: api/Login
        [HttpGet]
        public IHttpActionResult Get()
        {
            var gamerLogged = db.Gamers.FirstOrDefault(g => g.ID == IDLoggado);
            
            if(gamerLogged != null)
            {
                return Json<Gamer>(gamerLogged);
            }

            return NotFound();
        }

        // POST: api/Login
        public IHttpActionResult Post(Gamer gamer)
        {
            var gamerLogged = db.Gamers.FirstOrDefault(g => g.Email == gamer.Email && g.HashPassword == gamer.HashPassword);

            if (gamerLogged != null)
            {
                IDLoggado = gamerLogged.ID;
                return Ok();
            }
            return NotFound();
        }
    }
}