﻿using System;
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
    public class RequestedMatchesController : ApiController
    {
        private HalfPuggContext db = new HalfPuggContext();

        // GET: api/RequestedMatches
        public IQueryable<RequestedMatch> GetRequestedMatchs()
        {
            return db.RequestedMatchs;
        }

        // GET: api/RequestedMatches/5
        [ResponseType(typeof(RequestedMatch))]
        public async Task<IHttpActionResult> GetRequestedMatch(int id)
        {
            RequestedMatch requestedMatch = await db.RequestedMatchs.FindAsync(id);
            if (requestedMatch == null)
            {
                return NotFound();
            }

            return Ok(requestedMatch);
        }

        // PUT: api/RequestedMatches/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutRequestedMatch(int id, RequestedMatch requestedMatch)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != requestedMatch.ID)
            {
                return BadRequest();
            }

            db.Entry(requestedMatch).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RequestedMatchExists(id))
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

        // POST: api/RequestedMatches
        [ResponseType(typeof(RequestedMatch))]
        public async Task<IHttpActionResult> PostRequestedMatch(RequestedMatch requestedMatch)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.RequestedMatchs.Add(requestedMatch);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = requestedMatch.ID }, requestedMatch);
        }

        // DELETE: api/RequestedMatches/5
        [ResponseType(typeof(RequestedMatch))]
        public async Task<IHttpActionResult> DeleteRequestedMatch(int id)
        {
            RequestedMatch requestedMatch = await db.RequestedMatchs.FindAsync(id);
            if (requestedMatch == null)
            {
                return NotFound();
            }

            db.RequestedMatchs.Remove(requestedMatch);
            await db.SaveChangesAsync();

            return Ok(requestedMatch);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RequestedMatchExists(int id)
        {
            return db.RequestedMatchs.Count(e => e.ID == id) > 0;
        }
    }
}