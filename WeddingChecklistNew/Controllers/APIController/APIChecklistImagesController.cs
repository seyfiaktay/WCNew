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
    public class APIChecklistImagesController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/APIChecklistImages
        public IQueryable<ChecklistImage> GetCheckListImages()
        {
            return db.CheckListImages;
        }

        // GET: api/APIChecklistImages/5
        [ResponseType(typeof(ChecklistImage))]
        public IHttpActionResult GetChecklistImage(int? id)
        {
            ChecklistImage checklistImage = db.CheckListImages.Find(id);
            if (checklistImage == null)
            {
                return NotFound();
            }

            return Ok(checklistImage);
        }

        // PUT: api/APIChecklistImages/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutChecklistImage(int id, ChecklistImage checklistImage)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != checklistImage.Id)
            {
                return BadRequest();
            }

            db.Entry(checklistImage).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ChecklistImageExists(id))
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

        // POST: api/APIChecklistImages
        [ResponseType(typeof(ChecklistImage))]
        public IHttpActionResult PostChecklistImage(ChecklistImage checklistImage)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.CheckListImages.Add(checklistImage);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = checklistImage.Id }, checklistImage);
        }

        // DELETE: api/APIChecklistImages/5
        [ResponseType(typeof(ChecklistImage))]
        public IHttpActionResult DeleteChecklistImage(int id)
        {
            ChecklistImage checklistImage = db.CheckListImages.Find(id);
            if (checklistImage == null)
            {
                return NotFound();
            }

            db.CheckListImages.Remove(checklistImage);
            db.SaveChanges();

            return Ok(checklistImage);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ChecklistImageExists(int id)
        {
            return db.CheckListImages.Count(e => e.Id == id) > 0;
        }
    }
}