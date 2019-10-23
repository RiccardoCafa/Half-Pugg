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
    public class LabelsController : ApiController
    {
        private HalfPuggContext db = new HalfPuggContext();

        // GET: api/Labels
        public IQueryable<Label> GetLabels()
        {
            return db.Labels;
        }

        // GET: api/Labels/5
        [ResponseType(typeof(Label))]
        public async Task<IHttpActionResult> GetLabel(int id)
        {
            Label label = await db.Labels.FindAsync(id);
            if (label == null)
            {
                return NotFound();
            }

            return Ok(label);
        }

        // PUT: api/Labels/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutLabel(int id, Label label)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != label.ID)
            {
                return BadRequest();
            }

            db.Entry(label).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LabelExists(id))
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

        // POST: api/Labels
        [ResponseType(typeof(Label))]
        public async Task<IHttpActionResult> PostLabel(Label label)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Labels.Add(label);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = label.ID }, label);
        }

        // DELETE: api/Labels/5
        [ResponseType(typeof(Label))]
        public async Task<IHttpActionResult> DeleteLabel(int id)
        {
            Label label = await db.Labels.FindAsync(id);
            if (label == null)
            {
                return NotFound();
            }

            db.Labels.Remove(label);
            await db.SaveChangesAsync();

            return Ok(label);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool LabelExists(int id)
        {
            return db.Labels.Count(e => e.ID == id) > 0;
        }
    }
}