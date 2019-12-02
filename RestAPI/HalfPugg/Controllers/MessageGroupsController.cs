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
    public class MessageGroupsController : ApiController
    {
        private HalfPuggContext db = new HalfPuggContext();

        // GET: api/MessageGroups
        public IQueryable<MessageGroup> GetMessageGroups()
        {
            return db.MessageGroups;
        }

        // GET: api/MessageGroups/5
        [ResponseType(typeof(MessageGroup))]
        public async Task<IHttpActionResult> GetMessageGroup(int id)
        {
            MessageGroup messageGroup = await db.MessageGroups.FindAsync(id);
            if (messageGroup == null)
            {
                return NotFound();
            }

            return Ok(messageGroup);
        }

        // PUT: api/MessageGroups/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutMessageGroup(int id, MessageGroup messageGroup)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != messageGroup.ID)
            {
                return BadRequest();
            }

            db.Entry(messageGroup).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MessageGroupExists(id))
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

        // POST: api/MessageGroups
        [ResponseType(typeof(MessageGroup))]
        public async Task<IHttpActionResult> PostMessageGroup(MessageGroup messageGroup)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.MessageGroups.Add(messageGroup);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = messageGroup.ID }, messageGroup);
        }

        // DELETE: api/MessageGroups/5
        [ResponseType(typeof(MessageGroup))]
        public async Task<IHttpActionResult> DeleteMessageGroup(int id)
        {
            MessageGroup messageGroup = await db.MessageGroups.FindAsync(id);
            if (messageGroup == null)
            {
                return NotFound();
            }

            db.MessageGroups.Remove(messageGroup);
            await db.SaveChangesAsync();

            return Ok(messageGroup);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MessageGroupExists(int id)
        {
            return db.MessageGroups.Count(e => e.ID == id) > 0;
        }

        [Route("api/GroupMenssages")]
        [ResponseType(typeof(ICollection<MessageGroup>))]
        [HttpGet]
        public async Task<IHttpActionResult> GetGroupMessages(int IdGroup)
        {
            Group group = await db.Groups.FindAsync(IdGroup);
            if (group == null)
            {
                return NotFound();
            }
            var query = db.PlayerGroups.Where(x => x.IdGroup == IdGroup).FirstOrDefault();
            if (query == null) return BadRequest();
            var query2 = db.MessageGroups.Where(x => x.ID_Relation == query.ID).ToList();
            
            return Ok(query2);
        }
    }
}