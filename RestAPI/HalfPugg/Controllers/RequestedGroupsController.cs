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

            // Checar se já há uma requisição para esse grupo -> player
            RequestedGroup reqG = db.RequestedGroups.Where(x => x.IdPlayer == requestedGroup.IdPlayer && x.Status == "A").AsEnumerable().FirstOrDefault();

            if(reqG != null)
            {
                return BadRequest(message: "O jogador já possui uma solicitação para esse grupo em aberto");
            }
            
            Group group = db.Groups.Find(requestedGroup.IdGroup);
            if(group == null)
            {
                return NotFound();
            }

            var integrantes = db.PlayerGroups.Where(x => x.IdGroup == requestedGroup.IdGroup).Select(x => x.Player).AsEnumerable().ToArray();

            // Checar se o grupo está em sua capacidade máxima
            if(integrantes.Length >= group.Capacity)
            {
                return BadRequest(message: "O grupo está em sua capacidade máxima");
            }

            // Checar se o player já está naquele grupo
            Player p1 = integrantes.FirstOrDefault(x => x.ID == requestedGroup.IdPlayer);
            if(p1 != null)
            {
                return BadRequest(message: "O jogador já se encontra nesse grupo.");
            }

            db.RequestedGroups.Add(requestedGroup);
            await db.SaveChangesAsync();

            return Ok(requestedGroup);
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