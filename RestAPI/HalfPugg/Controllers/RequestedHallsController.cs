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
    public class RequestedHallsController : ApiController
    {
        private HalfPuggContext db = new HalfPuggContext();

        // GET: api/RequestedHalls
        public IQueryable<RequestedHall> GetRequestedHalls()
        {
            return db.RequestedHalls;
        }

        // GET: api/RequestedHalls/5
        [ResponseType(typeof(RequestedHall))]
        public async Task<IHttpActionResult> GetRequestedHall(int id)
        {
            RequestedHall requestedHall = await db.RequestedHalls.FindAsync(id);
            if (requestedHall == null)
            {
                return NotFound();
            }

            return Ok(requestedHall);
        }

        // PUT: api/RequestedHalls/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutRequestedHall(int id, RequestedHall requestedHall)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != requestedHall.ID)
            {
                return BadRequest();
            }

            db.Entry(requestedHall).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RequestedHallExists(id))
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

        // POST: api/RequestedHalls
        [ResponseType(typeof(RequestedHall))]
        public async Task<IHttpActionResult> PostRequestedHall(RequestedHall requestedHall)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.RequestedHalls.Add(requestedHall);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = requestedHall.ID }, requestedHall);
        }

        // DELETE: api/RequestedHalls/5
        [ResponseType(typeof(RequestedHall))]
        public async Task<IHttpActionResult> DeleteRequestedHall(int id)
        {
            RequestedHall requestedHall = await db.RequestedHalls.FindAsync(id);
            if (requestedHall == null)
            {
                return NotFound();
            }

            db.RequestedHalls.Remove(requestedHall);
            await db.SaveChangesAsync();

            return Ok(requestedHall);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RequestedHallExists(int id)
        {
            return db.RequestedHalls.Count(e => e.ID == id) > 0;
        }
    }
}