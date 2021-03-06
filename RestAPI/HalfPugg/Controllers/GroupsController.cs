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
    public class GroupsController : ApiController
    {
        private HalfPuggContext db = new HalfPuggContext();

        // GET: api/Groups
        public IQueryable<Group> GetGroups()
        {
            return db.Groups;
        }

        // GET: api/Groups/5
        [ResponseType(typeof(Group))]
        public async Task<IHttpActionResult> GetGroup(int id)
        {   
            Group group = await db.Groups.FindAsync(id);
            if (group == null)
            {
                return NotFound();
            }
         
            return Ok(group);
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

        //[ResponseType(typeof(void))]
        //[Route("api/Groups/JoinGroup")]
        //[HttpPut]
        //public async Task<IHttpActionResult> JoinInGroup(int GroupID, int PlayerID)
        //{
        //    Group g = await db.Groups.FindAsync(GroupID);
        //    if (g == null) return NotFound();
        //    Player p = await db.Gamers.FindAsync(PlayerID);
        //    if (p == null) return NotFound();

        //    if (!g.Integrants.Contains(p))
        //    {
        //        p.Groups.Add(g);
        //        g.Integrants.Add(p);
        //        await db.SaveChangesAsync();
        //    }

        //    return Ok();

        //}
        //[ResponseType(typeof(void))]
        //[Route("api/Groups/RemoveFromGroup")]
        //[HttpPut]
        //public async Task<IHttpActionResult> RemoveFromGroup(int GroupID, int PlayerID)
        //{
        //    Group g = await db.Groups.FindAsync(GroupID);
        //    if (g == null) return NotFound();
        //    Player p = await db.Gamers.FindAsync(PlayerID);
        //    if (p == null) return NotFound();

        //    if (g.Integrants.Contains(p))
        //    {
        //        p.Groups.Remove(g);
        //        g.Integrants.Remove(p);
        //        await db.SaveChangesAsync();
        //    }

        //    return Ok();

        //}

        // PUT: api/Groups/5


        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutGroup(int id, Group group)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != group.ID)
            {
                return BadRequest();
            }

            db.Entry(group).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GroupExists(id))
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

        // POST: api/Groups
        [ResponseType(typeof(Group))]
        public async Task<IHttpActionResult> PostGroup(Group group)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Group groups = db.Groups.FirstOrDefault(x => x.Name == group.Name);
            
            if(groups != null)
            {
                return BadRequest(message: "Um grupo com o mesmo nome já existe!");
            }

            if(group.Capacity >= 16)
            {
                return BadRequest(message: "Seu grupo não pode ter mais de 15 pessoas!");
            }

            group.CreateAt = DateTime.UtcNow;
            group.TotalComponentes = 1;
            db.Groups.Add(group);
            try
            {
                await db.SaveChangesAsync();
            } catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return BadRequest(message: "Algo inesperado ocorreu, tente recarregar a página, se voltar o erro contate o suporte!");
            }

            return Ok(group);
        }

        // DELETE: api/Groups/5
        [ResponseType(typeof(Group))]
        public async Task<IHttpActionResult> DeleteGroup(int id)
        {
            Group group = await db.Groups.FindAsync(id);
            if (group == null)
            {
                return NotFound();
            }

            db.Groups.Remove(group);
            await db.SaveChangesAsync();

            return Ok(group);   
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool GroupExists(int id)
        {
            return db.Groups.Count(e => e.ID == id) > 0;
        }



    }

}