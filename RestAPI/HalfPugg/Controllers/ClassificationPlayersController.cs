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
using System.Web.Http.Results;
using HalfPugg.Models;

namespace HalfPugg.Controllers
{
    public class ClassificationPlayersController : ApiController
    {
        private HalfPuggContext db = new HalfPuggContext();

        // GET: api/ClassificationPlayers
        public IQueryable<ClassificationPlayer> GetClassification_Players()
        {
            return db.Classification_Players;
        }

        // GET: api/ClassificationPlayers/5
        [ResponseType(typeof(ClassificationPlayer))]
        public async Task<IHttpActionResult> GetClassificationPlayer(int id)
        {
            ClassificationPlayer classificationPlayer = await db.Classification_Players.FindAsync(id);
            if (classificationPlayer == null)
            {
                return NotFound();
            }

            return Ok(classificationPlayer);
        }

        [ResponseType(typeof(ClassificationPlayer))]
        [HttpGet]
        [Route("api/classificationPlayers/Match")]
        public IHttpActionResult GetClassificationMatch(int idJudge, int idJudger)
        {
            ClassificationPlayer classfPlayer = null;
            try
            {
                 classfPlayer = db.Classification_Players
                                                 .Where(clfp => clfp.IdJudgePlayer == idJudge && clfp.IdPlayer == idJudger)
                                                 .FirstOrDefault();
            }
            catch
            {

            }
            if(classfPlayer == null)
            {
                classfPlayer = new ClassificationPlayer()
                {
                    IdJudgePlayer = idJudge,
                    IdPlayer = idJudger,
                    Points = 0,
                    IdClassification = 0
                };
            }
            return Ok(classfPlayer);
            
        }

        // PUT: api/ClassificationPlayers/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutClassificationPlayer(int id, ClassificationPlayer classificationPlayer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != classificationPlayer.ID)
            {
                return BadRequest();
            }

            db.Entry(classificationPlayer).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClassificationPlayerExists(id))
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

        // POST: api/ClassificationPlayers
        [ResponseType(typeof(ClassificationPlayer))]
        public async Task<IHttpActionResult> PostClassificationPlayer(ClassificationPlayer classificationPlayer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ClassificationPlayer clfp = db.Classification_Players.Where(clf => clf.IdJudgePlayer == classificationPlayer.IdJudgePlayer && clf.IdPlayer == classificationPlayer.IdPlayer).AsEnumerable().FirstOrDefault();

            if(clfp != null)
            {
                return BadRequest();
            }

            db.Classification_Players.Add(classificationPlayer);
            try
            {
                await db.SaveChangesAsync();
            } catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return Ok(classificationPlayer);
        }

        // DELETE: api/ClassificationPlayers/5
        [ResponseType(typeof(ClassificationPlayer))]
        public async Task<IHttpActionResult> DeleteClassificationPlayer(int id)
        {
            ClassificationPlayer classificationPlayer = await db.Classification_Players.FindAsync(id);
            if (classificationPlayer == null)
            {
                return NotFound();
            }

            db.Classification_Players.Remove(classificationPlayer);
            await db.SaveChangesAsync();

            return Ok(classificationPlayer);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ClassificationPlayerExists(int id)
        {
            return db.Classification_Players.Count(e => e.ID == id) > 0;
        }
    }
}