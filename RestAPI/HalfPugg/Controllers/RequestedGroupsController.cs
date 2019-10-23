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
    public class RequestedGroupsController : ApiController
    {
        private HalfPuggContext db = new HalfPuggContext();

        // GET: api/RequestedGroups
        public IQueryable<RequestedGroup> GetRequestedGroups()
        {
            return db.RequestedGroups;
        }

        // GET: api/RequestedGroups/5
        [ResponseType(typeof(RequestedGroup))]
        public async Task<IHttpActionResult> GetRequestedGroup(int id)
        {
            RequestedGroup requestedGroup = await db.RequestedGroups.FindAsync(id);
            if (requestedGroup == null)
            {
                return NotFound();
            }

            return Ok(requestedGroup);
        }

        // PUT: api/RequestedGroups/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutRequestedGroup(int id, RequestedGroup requestedGroup)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != requestedGroup.ID)
            {
                return BadRequest();
            }

            db.Entry(requestedGroup).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RequestedGroupExists(id))
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

        // POST: api/RequestedGroups
        [ResponseType(typeof(RequestedGroup))]
        public async Task<IHttpActionResult> PostRequestedGroup(RequestedGroup requestedGroup)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.RequestedGroups.Add(requestedGroup);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = requestedGroup.ID }, requestedGroup);
        }

        // DELETE: api/RequestedGroups/5
        [ResponseType(typeof(RequestedGroup))]
        public async Task<IHttpActionResult> DeleteRequestedGroup(int id)
        {
            RequestedGroup requestedGroup = await db.RequestedGroups.FindAsync(id);
            if (requestedGroup == null)
            {
                return NotFound();
            }

            db.RequestedGroups.Remove(requestedGroup);
            await db.SaveChangesAsync();

            return Ok(requestedGroup);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RequestedGroupExists(int id)
        {
            return db.RequestedGroups.Count(e => e.ID == id) > 0;
        }
    }
}