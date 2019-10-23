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
    public class MessageHallsController : ApiController
    {
        private HalfPuggContext db = new HalfPuggContext();

        // GET: api/MessageHalls
        public IQueryable<MessageHall> GetMessageHalls()
        {
            return db.MessageHalls;
        }

        // GET: api/MessageHalls/5
        [ResponseType(typeof(MessageHall))]
        public async Task<IHttpActionResult> GetMessageHall(int id)
        {
            MessageHall messageHall = await db.MessageHalls.FindAsync(id);
            if (messageHall == null)
            {
                return NotFound();
            }

            return Ok(messageHall);
        }

        // PUT: api/MessageHalls/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutMessageHall(int id, MessageHall messageHall)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != messageHall.ID)
            {
                return BadRequest();
            }

            db.Entry(messageHall).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MessageHallExists(id))
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

        // POST: api/MessageHalls
        [ResponseType(typeof(MessageHall))]
        public async Task<IHttpActionResult> PostMessageHall(MessageHall messageHall)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.MessageHalls.Add(messageHall);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = messageHall.ID }, messageHall);
        }

        // DELETE: api/MessageHalls/5
        [ResponseType(typeof(MessageHall))]
        public async Task<IHttpActionResult> DeleteMessageHall(int id)
        {
            MessageHall messageHall = await db.MessageHalls.FindAsync(id);
            if (messageHall == null)
            {
                return NotFound();
            }

            db.MessageHalls.Remove(messageHall);
            await db.SaveChangesAsync();

            return Ok(messageHall);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MessageHallExists(int id)
        {
            return db.MessageHalls.Count(e => e.ID == id) > 0;
        }
    }
}