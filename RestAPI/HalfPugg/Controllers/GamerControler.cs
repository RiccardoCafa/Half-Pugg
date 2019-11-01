﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using HalfPugg.Models;
using HalfPugg.TokenJWT;
using Newtonsoft.Json;

namespace HalfPugg.Controllers
{
    public class GamersController : ApiController
    {
        private HalfPuggContext db = new HalfPuggContext();

        // GET: api/Gamers
        public IQueryable<Player> GetGamers()
        {
            return db.Gamers;
        }

        // GET: api/Gamers/5
        [ResponseType(typeof(Player))]
        public IHttpActionResult GetGamer(int id)
        {
            Player gamer = db.Gamers.Find(id);
            if (gamer == null)
            {
                return NotFound();
            }

            return Ok(gamer);
        }

        [Route("api/GamersMatch")]
        [HttpGet]
        public IHttpActionResult GetGamerMatch()
        {
            Player gamerL = null;

            var headers = Request.Headers;

            if (headers.Contains("token-jwt"))
            {
                string token = headers.GetValues("token-jwt").First();
                TokenValidation validation = new TokenValidation();
                string userValidated = validation.ValidateToken(token);
                if (userValidated != null)
                {
                    TokenData data = JsonConvert.DeserializeObject<TokenData>(userValidated);
                    gamerL = db.Gamers.FirstOrDefault(g => g.ID == data.ID);
                }
            }

            if (gamerL == null)
            {
                return NotFound();
            }
            List<Player> gamers = new List<Player>();
            List<Match> matches = db.Matches
                                    .Where(ma => ma.IdPlayer1 == gamerL.ID || ma.IdPlayer2 == gamerL.ID)
                                    .AsEnumerable().ToList();
            List<RequestedMatch> reqMatches = db.RequestedMatchs
                                                .Where(ma => ma.IdPlayer == gamerL.ID || ma.IdPlayer2 == gamerL.ID)
                                                .AsEnumerable().ToList();
            foreach (Player gMatch in db.Gamers)
            {
                if (gMatch.ID != gamerL.ID)
                {
                    Match found = matches.FirstOrDefault(x =>
                     x.IdPlayer2 == gMatch.ID || x.IdPlayer1 == gMatch.ID);

                    RequestedMatch Requested = reqMatches.FirstOrDefault(x =>
                    x.IdPlayer2 == gMatch.ID || x.IdPlayer == gMatch.ID);

                    if (found == null && Requested == null)
                    {
                        gamers.Add(new Player()
                        {
                            ID = gMatch.ID,
                            Nickname = gMatch.Nickname,
                            Name = gMatch.Name,
                            LastName = gMatch.LastName,
                            Email = gMatch.Email,
                            ImagePath = gMatch.ImagePath,
                            Bio = gMatch.Bio,
                        });

                    }
                }
            }

            return Json(gamers);
        }

        // PUT: api/Gamers/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutGamer(int id, Player gamer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != gamer.ID)
            {
                return BadRequest();
            }

            db.Entry(gamer).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GamerExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Gamers
        [ResponseType(typeof(Player))]
        public IHttpActionResult PostGamer(Player gamer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                db.Gamers.Add(gamer);
                db.SaveChanges();
            } catch(Exception)
            {
                return BadRequest();
            }

            return CreatedAtRoute("DefaultApi", new { id = gamer.ID }, gamer);
        }

        // DELETE: api/Gamers/5
        [ResponseType(typeof(Player))]
        public IHttpActionResult DeleteGamer(int id)
        {
            Player gamer = db.Gamers.Find(id);
            if (gamer == null)
            {
                return NotFound();
            }

            db.Gamers.Remove(gamer);
            db.SaveChanges();

            return Ok(gamer);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool GamerExists(int id)
        {
            return db.Gamers.Count(e => e.ID == id) > 0;
        }
    }
}