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
            //return db.CheckLists;
            return db.CheckLists.Include("ChecklistMain");
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

            using (DbContextTransaction transaction = db.Database.BeginTransaction())
            {
                try
                {
                    //DELETE ALL IMAGES BEFORE
                    db.CheckListImages.RemoveRange(db.CheckListImages.Where(x => x.CheckListId == checklist.Id));
                    db.Entry(checklist).State = EntityState.Modified;
                    foreach (ChecklistImage item in checklist.CheckListImage) 
                    {
                        db.CheckListImages.Add(item);
                    }
                    db.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
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
            using (DbContextTransaction transaction = db.Database.BeginTransaction())
            {
                try
                {
                    db.CheckLists.Add(checklist);
                    foreach (ChecklistImage item in checklist.CheckListImage)
                    {
                        db.CheckListImages.Add(item);
                    }
                    db.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                }
            }
            
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