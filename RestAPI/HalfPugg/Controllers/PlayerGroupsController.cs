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
    }
}