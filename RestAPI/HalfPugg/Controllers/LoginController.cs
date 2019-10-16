using HalfPugg.Models;
using System.Linq;
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
                return Json<Gamer>(HttpContext.Current.Session["ID"] as Gamer);
            }
            else return NotFound();
        }

        // POST: api/Login
        public IHttpActionResult Post(Gamer gamer)
        {
            var gamerLogged = db.Gamers.FirstOrDefault(g => g.Email == gamer.Email && g.HashPassword == gamer.HashPassword);

            if(gamerLogged != null)
            {
                HttpContext.Current.Session["ID"] = gamerLogged;
                return Json<Gamer>(gamerLogged);
            }
            return NotFound();
        }
    }
}