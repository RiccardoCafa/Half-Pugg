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
    public class PlayerGroupsController : ApiController
    {
        private HalfPuggContext db = new HalfPuggContext();

        // GET: api/PlayerGroups
        public IQueryable<PlayerGroup> GetPlayerGroups()
        {
            return db.PlayerGroups;
        }

        // GET: api/PlayerGroups/5
        [ResponseType(typeof(PlayerGroup))]
        public async Task<IHttpActionResult> GetPlayerGroup(int id)
        {
            PlayerGroup playerGroup = await db.PlayerGroups.FindAsync(id);
            if (playerGroup == null)
            {
                return NotFound();
            }
         
            return Ok(playerGroup);
        }

        
        [ResponseType(typeof(PlayerGroup))]
        [Route("api/PlayerGroups")]
        [HttpGet]
        public IHttpActionResult GetPlayerGroup(int playerID, int groupID)
        {
            PlayerGroup playerGroup =  db.PlayerGroups.Where(x => x.IdPlayer == playerID && x.IdGroup == groupID).FirstOrDefault();
            if (playerGroup == null)
            {
                return NotFound();
            }

            return Ok(playerGroup);
        }

        [ResponseType(typeof(ICollection<int>))]
        [Route("api/PlayerGroups/GetGroups")]
        [HttpGet]
        public IHttpActionResult GetPlayerGroups(int playerID)
        {
            var groups = db.PlayerGroups.Where(x => x.IdPlayer == playerID).Select(x=>x.IdGroup);
            
            if (groups.Count() == 0)
            {
                return NotFound();
            }

            return Ok(groups);
        }

        [ResponseType(typeof(ICollection<int>))]
        [Route("api/PlayerGroups/GetGroups/complete")]
        [HttpGet]
        public IHttpActionResult GetPlayerGroupsComplete(int playerID)
        {
            Group[] groups = db.PlayerGroups.Where(x => x.IdPlayer == playerID).Select(x => x.Group).Where(y => y.IdAdmin == playerID).AsEnumerable().ToArray();

            return Ok(groups);
        }

        // PUT: api/PlayerGroups/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutPlayerGroup(int id, PlayerGroup playerGroup)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != playerGroup.ID)
            {
                return BadRequest();
            }

            db.Entry(playerGroup).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlayerGroupExists(id))
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

        // POST: api/PlayerGroups
        [ResponseType(typeof(PlayerGroup))]
        public async Task<IHttpActionResult> PostPlayerGroup(PlayerGroup playerGroup)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.PlayerGroups.Add(playerGroup);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = playerGroup.ID }, playerGroup);
        }

        // DELETE: api/PlayerGroups/5
        [ResponseType(typeof(PlayerGroup))]
        public async Task<IHttpActionResult> DeletePlayerGroup(int id)
        {
            PlayerGroup playerGroup = await db.PlayerGroups.FindAsync(id);
            if (playerGroup == null)
            {
                return NotFound();
            }

            db.PlayerGroups.Remove(playerGroup);
            await db.SaveChangesAsync();

            return Ok(playerGroup);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PlayerGroupExists(int id)
        {
            return db.PlayerGroups.Count(e => e.ID == id) > 0;
        }

        [Route("api/GroupIntegrants")]
        [ResponseType(typeof(ICollection<Player>))]
        [HttpGet]
        public async Task<IHttpActionResult> GetGroupIntegrants(int IdGroup)
        {
            Group group = await db.Groups.FindAsync(IdGroup);
            if (group == null)
            {
                return NotFound();
            }
            var query = db.PlayerGroups.Where(x => x.IdGroup == IdGroup).Select(x => x.Player);
            if (query == null) return BadRequest();
            //var p = db.PlayerGroups.Where(x => x.IdGroup == IdGroup).Select(x => x.Player);
            return Ok(query);
        }

        
    }

}