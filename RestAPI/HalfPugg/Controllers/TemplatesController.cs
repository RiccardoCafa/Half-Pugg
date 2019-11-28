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
    public class TemplatesController : ApiController
    {
        private HalfPuggContext db = new HalfPuggContext();

        // GET: api/Templates
        public IQueryable<Template> GetTemplates()
        {
            return db.Templates;
        }

        // GET: api/Templates/5
        [ResponseType(typeof(Template))]
        public async Task<IHttpActionResult> GetTemplate(int id)
        {
            Template template = await db.Templates.FindAsync(id);
            if (template == null)
            {
                return NotFound();
            }

            return Ok(template);
        }

        // PUT: api/Templates/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutTemplate(int id, Template template)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != template.ID_Template)
            {
                return BadRequest();
            }

            db.Entry(template).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TemplateExists(id))
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

        // POST: api/Templates
        [ResponseType(typeof(Template))]
        public async Task<IHttpActionResult> PostTemplate(Template template)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Templates.Add(template);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = template.ID_Template }, template);
        }

        // DELETE: api/Templates/5
        [ResponseType(typeof(Template))]
        public async Task<IHttpActionResult> DeleteTemplate(int id)
        {
            Template template = await db.Templates.FindAsync(id);
            if (template == null)
            {
                return NotFound();
            }

            db.Templates.Remove(template);
            await db.SaveChangesAsync();

            return Ok(template);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TemplateExists(int id)
        {
            return db.Templates.Count(e => e.ID_Template == id) > 0;
        }
    }
}