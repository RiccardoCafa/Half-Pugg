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
    public class HallsController : ApiController
    {
        private HalfPuggContext db = new HalfPuggContext();

        // GET: api/Halls
        public IQueryable<Hall> GetHalls()
        {
            return db.Halls;
        }

        // GET: api/Halls/5
        [ResponseType(typeof(Hall))]
        public async Task<IHttpActionResult> GetHall(int id)
        {
            Hall hall = await db.Halls.FindAsync(id);
            if (hall == null)
            {
                return NotFound();
            }

            return Ok(hall);
        }

        // PUT: api/Halls/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutHall(int id, Hall hall)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != hall.ID)
            {
                return BadRequest();
            }

            db.Entry(hall).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HallExists(id))
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

        // POST: api/Halls
        [ResponseType(typeof(Hall))]
        public async Task<IHttpActionResult> PostHall(Hall hall)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Halls.Add(hall);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = hall.ID }, hall);
        }

        // DELETE: api/Halls/5
        [ResponseType(typeof(Hall))]
        public async Task<IHttpActionResult> DeleteHall(int id)
        {
            Hall hall = await db.Halls.FindAsync(id);
            if (hall == null)
            {
                return NotFound();
            }

            db.Halls.Remove(hall);
            await db.SaveChangesAsync();

            return Ok(hall);
        }

        [Route("api/HallIntegrants")]
        [ResponseType(typeof(ICollection<Player>))]
        [HttpGet]
        public async Task<IHttpActionResult> GetHallIntegrants(int IdHall)
        {
            Hall hall = await db.Halls.FindAsync(IdHall);
            if (hall == null)
            {
                return NotFound();
            }
           
            var p = db.PlayerHalls.Where(x => x.IdHall == IdHall).Select(x => x.Player);
            return Ok(p);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool HallExists(int id)
        {
            return db.Halls.Count(e => e.ID == id) > 0;
        }
    }
}