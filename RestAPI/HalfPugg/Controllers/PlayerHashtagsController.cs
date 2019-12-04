using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using HalfPugg.Models;

namespace HalfPugg.Controllers
{
    public class PlayerHashtagsController : ApiController
    {
        private HalfPuggContext db = new HalfPuggContext();

        // GET: api/PlayerHashtags
        public IQueryable<PlayerHashtag> GetPlayerHashtags()
        {
            return db.PlayerHashtags;
        }

        // GET: api/PlayerHashtags/5
        [ResponseType(typeof(PlayerHashtag))]
        public async Task<IHttpActionResult> GetPlayerHashtag(int id)
        {
            PlayerHashtag playerHashtag = await db.PlayerHashtags.FindAsync(id);
            if (playerHashtag == null)
            {
                return NotFound();
            }

            return Ok(playerHashtag);
        }

        // PUT: api/PlayerHashtags/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutPlayerHashtag(int id, PlayerHashtag playerHashtag)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != playerHashtag.ID)
            {
                return BadRequest();
            }

            db.Entry(playerHashtag).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlayerHashtagExists(id))
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

        // POST: api/PlayerHashtags
        [ResponseType(typeof(PlayerHashtag))]
        public async Task<IHttpActionResult> PostPlayerHashtag(PlayerHashtag playerHashtag)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.PlayerHashtags.Add(playerHashtag);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = playerHashtag.ID }, playerHashtag);
        }

        // DELETE: api/PlayerHashtags/5
        [ResponseType(typeof(PlayerHashtag))]
        public async Task<IHttpActionResult> DeletePlayerHashtag(int id)
        {
            PlayerHashtag playerHashtag = await db.PlayerHashtags.FindAsync(id);
            if (playerHashtag == null)
            {
                return NotFound();
            }

            db.PlayerHashtags.Remove(playerHashtag);
            await db.SaveChangesAsync();

            return Ok(playerHashtag);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PlayerHashtagExists(int id)
        {
            return db.PlayerHashtags.Count(e => e.ID == id) > 0;
        }
        [Route("api/HashPlayer")]
        [ResponseType(typeof(PlayerHashtag))]
        [HttpGet]
        public async Task<IHttpActionResult> GetGroupIntegrants(int IdHash, int IdPlayer)
        {
            HashTag h = await db.HashTags.FindAsync(IdHash);
            Player p = await db.Gamers.FindAsync(IdPlayer);
            if (h== null || p== null)
            {
                return NotFound();
            }
            var query = db.PlayerHashtags.Where(x => x.IdHash == IdHash && x.IdPlayer == IdPlayer).FirstOrDefault();
            if (query == null) return NotFound();
            return Ok(query);
        }
    }
}