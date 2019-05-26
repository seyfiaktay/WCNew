using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using WeddingChecklistNew.Models;

namespace WeddingChecklistNew.Controllers.APIController
{
    public class APIChecklistMainsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/APIChecklistMains
        public IQueryable<ChecklistMain> GetChecklistMains()
        {
            return db.ChecklistMains;
        }

        // GET: api/APIChecklistMains/5
        [ResponseType(typeof(ChecklistMain))]
        public IHttpActionResult GetChecklistMain(int? id)
        {
            ChecklistMain checklistMain = db.ChecklistMains.Find(id);
            if (checklistMain == null)
            {
                return NotFound();
            }

            return Ok(checklistMain);
        }

        // PUT: api/APIChecklistMains/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutChecklistMain(int id, ChecklistMain checklistMain)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != checklistMain.Id)
            {
                return BadRequest();
            }

            db.Entry(checklistMain).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ChecklistMainExists(id))
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

        // POST: api/APIChecklistMains
        [ResponseType(typeof(ChecklistMain))]
        public IHttpActionResult PostChecklistMain(ChecklistMain checklistMain)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            db.ChecklistMains.Add(checklistMain);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = checklistMain.Id }, checklistMain);
        }

        // DELETE: api/APIChecklistMains/5
        [ResponseType(typeof(ChecklistMain))]
        public IHttpActionResult DeleteChecklistMain(int id)
        {
            ChecklistMain checklistMain = db.ChecklistMains.Find(id);
            if (checklistMain == null)
            {
                return NotFound();
            }

            db.ChecklistMains.Remove(checklistMain);
            db.SaveChanges();

            return Ok(checklistMain);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ChecklistMainExists(int id)
        {
            return db.ChecklistMains.Count(e => e.Id == id) > 0;
        }
    }
}