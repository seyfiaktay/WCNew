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
    public class APIChecklistsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/APIChecklists
        public IQueryable<Checklist> GetCheckLists()
        {
            return db.CheckLists;
        }

        // GET: api/APIChecklists/5
        [ResponseType(typeof(Checklist))]
        public IHttpActionResult GetChecklist(int? id)
        {
            Checklist checklist = db.CheckLists.Find(id);
            if (checklist == null)
            {
                return NotFound();
            }

            return Ok(checklist);
        }

        // PUT: api/APIChecklists/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutChecklist(int id, Checklist checklist)
        {
            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != checklist.Id)
            {
                return BadRequest();
            }

            db.Entry(checklist).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ChecklistExists(id))
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

        // POST: api/APIChecklists
        [ResponseType(typeof(Checklist))]
        public IHttpActionResult PostChecklist(Checklist checklist)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.CheckLists.Add(checklist);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = checklist.Id }, checklist);
        }

        // DELETE: api/APIChecklists/5
        [ResponseType(typeof(Checklist))]
        public IHttpActionResult DeleteChecklist(int id)
        {
            Checklist checklist = db.CheckLists.Find(id);
            if (checklist == null)
            {
                return NotFound();
            }

            db.CheckLists.Remove(checklist);
            db.SaveChanges();

            return Ok(checklist);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ChecklistExists(int id)
        {
            return db.CheckLists.Count(e => e.Id == id) > 0;
        }
    }
}