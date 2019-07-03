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
using WeddingChecklistNew.Controllers.Class;
using WeddingChecklistNew.Models;
using WeddingChecklistNew.Models.Domain;

namespace WeddingChecklistNew.Controllers.APIController
{
    public class APIChecklistsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/APIChecklists
        public IQueryable<Checklist> GetCheckLists()
        {
            //return db.CheckLists;
            return db.CheckLists.Include("ChecklistMain").Include("Currency");
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
                    if (checklist.CheckListImage != null)
                    {
                        foreach (ChecklistImage item in checklist.CheckListImage)
                        {
                            db.CheckListImages.Add(item);
                        }
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
                    if (checklist.CheckListImage != null)
                    {
                        foreach (ChecklistImage item in checklist.CheckListImage)
                        {
                            db.CheckListImages.Add(item);
                        }
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

            return CreatedAtRoute("DefaultApi", new { id = checklist.Id }, checklist);
        }

        // DELETE: api/APIChecklists/5
        [ResponseType(typeof(Checklist))]
        public IHttpActionResult DeleteChecklist(int id)
        {
            Checklist checklist = db.CheckLists.Find(id);
            using (DbContextTransaction transaction = db.Database.BeginTransaction())
            {
                try
                {
                    if (checklist == null)
                    {
                        throw new Exception("item not found");
                    }
                    db.CheckListImages.RemoveRange(db.CheckListImages.Where(x => x.CheckListId == id));
                    db.CheckLists.Remove(checklist);
                    db.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                }
            }
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



        // GET: api/GetRecentlyCheckLists
        [Route("api/APIChecklists/GetRecentlyCheckLists")]
        public List<RecentlyChecklist> GetRecentlyCheckLists()
        {
            var list = from checklist in db.CheckLists
                       join checklistmain in db.ChecklistMains on checklist.ChecklistMainId equals checklistmain.Id
                       join currency in db.Currencies on checklist.CurrencyId equals currency.Id into currencylj
                       from item in currencylj.DefaultIfEmpty()
                       where checklistmain.Private == false
                       orderby checklist.LogDate descending
                       select new RecentlyChecklist
                       {
                           Name = checklist.Name,
                           Url = checklist.Url,
                           Price = checklist.Price,
                           CurrencyName = item.code,
                           Priority = checklist.Priority,
                           ImagePath = (from cp in db.CheckListImages
                                        where cp.CheckListId == checklist.Id
                                        orderby cp.Id descending
                                        select cp.Path).FirstOrDefault(),
                           Link = clsGenel.cnstWebsiteURL + "/Checklists/Details/" + checklist.Id,
                           UserId = checklist.UserId
                       };
            return list.Take(10).ToList();
        }


        // GET: api/GetRecentlyCheckLists
        [Route("api/APIChecklists/GetRecentlyCheckListMains")]
        public List<RecentlyChecklist> GetRecentlyCheckListMains()
        {
            var list = from checklistmain in db.ChecklistMains
                       where checklistmain.Private == false
                       orderby checklistmain.LogDate descending
                       select new RecentlyChecklist
                       {
                           MainName = checklistmain.Name,
                           DueDate = checklistmain.DueDate,
                           Description = checklistmain.Definition,
                           Link = clsGenel.cnstWebsiteURL + "/Checklists/Index/" + checklistmain.Id,
                           UserId = checklistmain.UserId
                       };
            return list.Take(10).ToList();
        }
    }
}