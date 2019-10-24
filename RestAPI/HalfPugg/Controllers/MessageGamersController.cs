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
    public class MessageGamersController : ApiController
    {
        private HalfPuggContext db = new HalfPuggContext();

        // GET: api/MessageGamers
        public IQueryable<MessagePlayer> GetMessageGamers()
        {
            return db.MessageGamers;
        }

        // GET: api/MessageGamers/5
        [ResponseType(typeof(MessagePlayer))]
        public async Task<IHttpActionResult> GetMessageGamer(int id)
        {
            MessagePlayer messageGamer = await db.MessageGamers.FindAsync(id);
            if (messageGamer == null)
            {
                return NotFound();
            }

            return Ok(messageGamer);
        }

        // PUT: api/MessageGamers/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutMessageGamer(int id, MessagePlayer messageGamer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != messageGamer.ID_Message)
            {
                return BadRequest();
            }

            db.Entry(messageGamer).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MessageGamerExists(id))
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

        // POST: api/MessageGamers
        [ResponseType(typeof(MessagePlayer))]
        public async Task<IHttpActionResult> PostMessageGamer(MessagePlayer messageGamer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.MessageGamers.Add(messageGamer);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = messageGamer.ID_Message }, messageGamer);
        }

        // DELETE: api/MessageGamers/5
        [ResponseType(typeof(MessagePlayer))]
        public async Task<IHttpActionResult> DeleteMessageGamer(int id)
        {
            MessagePlayer messageGamer = await db.MessageGamers.FindAsync(id);
            if (messageGamer == null)
            {
                return NotFound();
            }

            db.MessageGamers.Remove(messageGamer);
            await db.SaveChangesAsync();

            return Ok(messageGamer);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MessageGamerExists(int id)
        {
            return db.MessageGamers.Count(e => e.ID_Message == id) > 0;
        }
    }
}