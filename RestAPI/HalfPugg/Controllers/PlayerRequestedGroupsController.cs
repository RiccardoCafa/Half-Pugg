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
    public class PlayerRequestedGroupsController : ApiController
    {
        private HalfPuggContext db = new HalfPuggContext();

        // GET: api/PlayerRequestedGroups
        public IQueryable<PlayerRequestedGroup> GetPlayerRequestedGroup()
        {
            return db.PlayerRequestedGroup;
        }

        // GET: api/PlayerRequestedGroups/5
        [ResponseType(typeof(PlayerRequestedGroup))]
        public async Task<IHttpActionResult> GetPlayerRequestedGroup(int id)
        {
            PlayerRequestedGroup playerRequestedGroup = await db.PlayerRequestedGroup.FindAsync(id);
            if (playerRequestedGroup == null)
            {
                return NotFound();
            }

            return Ok(playerRequestedGroup);
        }

        // PUT: api/PlayerRequestedGroups/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutPlayerRequestedGroup(int id, PlayerRequestedGroup playerRequestedGroup)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != playerRequestedGroup.ID)
            {
                return BadRequest();
            }

            db.Entry(playerRequestedGroup).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlayerRequestedGroupExists(id))
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

        // POST: api/PlayerRequestedGroups
        [ResponseType(typeof(PlayerRequestedGroup))]
        public async Task<IHttpActionResult> PostPlayerRequestedGroup(PlayerRequestedGroup playerRequestedGroup)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.PlayerRequestedGroup.Add(playerRequestedGroup);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = playerRequestedGroup.ID }, playerRequestedGroup);
        }

        // DELETE: api/PlayerRequestedGroups/5
        [ResponseType(typeof(PlayerRequestedGroup))]
        public async Task<IHttpActionResult> DeletePlayerRequestedGroup(int id)
        {
            PlayerRequestedGroup playerRequestedGroup = await db.PlayerRequestedGroup.FindAsync(id);
            if (playerRequestedGroup == null)
            {
                return NotFound();
            }

            db.PlayerRequestedGroup.Remove(playerRequestedGroup);
            await db.SaveChangesAsync();

            return Ok(playerRequestedGroup);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PlayerRequestedGroupExists(int id)
        {
            return db.PlayerRequestedGroup.Count(e => e.ID == id) > 0;
        }
    }
}