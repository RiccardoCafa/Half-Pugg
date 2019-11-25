using HalfPugg.Models;
using HalfPugg.TokenJWT;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;

namespace HalfPugg.Controllers
{

    public class TokenData
    {
        public int ID;
        public float Exp;
    }

    public class LoginController : ApiController
    {
        HalfPuggContext db;
        public static Player GamerLogado = null;

        public LoginController()
        {
            db = new HalfPuggContext();
        }

        [Route("api/ValidateToken")]
        [HttpGet]
        public IHttpActionResult ValidateToken()
        {
            var headers = Request.Headers;
            if (headers.Contains("token-jwt"))
            {
                string token = headers.GetValues("token-jwt").First();
                TokenValidation validation = new TokenValidation();
                string userValidated = validation.ValidateToken(token);
                if (userValidated != null)
                {
                    return Ok();
                }
            }
            return BadRequest();
        }

        // GET: api/Login
        public IHttpActionResult Get()
        {
            var headers = Request.Headers;
         
            if (headers.Contains("token-jwt"))
            {
                string token = headers.GetValues("token-jwt").First();
                TokenValidation validation = new TokenValidation();
                string userValidated = validation.ValidateToken(token);
                if (userValidated != null)
                {
                    TokenData data = JsonConvert.DeserializeObject<TokenData>(userValidated);
                    Player g2 = db.Gamers.FirstOrDefault(g => g.ID == data.ID);
                    return Ok(g2);
                }
                else
                {
                    return BadRequest();
                }
            }
            else
            {
                return BadRequest();
            }
        }

        // POST: api/Login
        public IHttpActionResult Post(Player gamer)
        {
            var gamerLogged = db.Gamers.FirstOrDefault(g => g.Email == gamer.Email && g.HashPassword == gamer.HashPassword);

            if (gamerLogged != null)
            {
                TokenMaker token = new TokenMaker();
                Dictionary<string, object> payload = new Dictionary<string, object>()
                {
                    {"ID", gamerLogged.ID }
                };
                var generatedToken = token.MakeToken(payload, 3 * 60 * 60);
                return Ok(generatedToken);
            }
            return NotFound();
        }

        [HttpDelete]
        [Route("api/logoff")]
        public IHttpActionResult DeleteLogin(Player gamer)
        {
            return Ok();
        }
    }
}