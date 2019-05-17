using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http.Results;
using System.Web.Mvc;
using WeddingChecklistNew.Controllers.APIController;
using WeddingChecklistNew.Models;

namespace WeddingChecklistNew.Controllers
{
    public class ChecklistsController : Controller
    {
        private APIChecklistsController mAPIChecklistController = new APIChecklistsController();
        private APIChecklistMainsController mAPIChecklistMainController = new APIChecklistMainsController();

        // GET: Checklists
        public ActionResult Index()
        {
            return View(mAPIChecklistController.GetCheckLists());
        }

        // GET: Checklists/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Checklist checklist = GetChecklist(id);
            /*Checklist checklist = db.CheckLists.Find(id);*/
            if (checklist == null)
            {
                return HttpNotFound();
            }
            return View(checklist);
        }

        // GET: Checklists/Create
        public ActionResult Create()
        {
            ViewData["listChecklistMain"] =  new SelectList(mAPIChecklistMainController.GetChecklistMains().Select(m=>m.Name));
            return View();
        }

        // POST: Checklists/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Url,Price,Priority")] Checklist checklist)
        {
            if (ModelState.IsValid)
            {
                mAPIChecklistController.PostChecklist(checklist);
                return RedirectToAction("Index");
            }

            return View(checklist);
        }

        // GET: Checklists/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Checklist checklist = GetChecklist(id);
            if (checklist == null)
            {
                return HttpNotFound();
            }
            return View(checklist);
        }

        // POST: Checklists/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Url,Price,Priority")] Checklist checklist)
        {
            if (ModelState.IsValid)
            {
                mAPIChecklistController.PutChecklist(checklist.Id,checklist);
                return RedirectToAction("Index");
            }
            return View(checklist);
        }

        // GET: Checklists/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Checklist checklist = GetChecklist(id);
            if (checklist == null)
            {
                return HttpNotFound();
            }
            return View(checklist);
        }

        // POST: Checklists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Checklist checklist = GetChecklist(id);
            mAPIChecklistController.DeleteChecklist(id);
            return RedirectToAction("Index");
        }


        private Checklist GetChecklist(int? id)
        {
            var actionResult = mAPIChecklistController.GetChecklist(id);
            var contentResult = actionResult as OkNegotiatedContentResult<Checklist>;
            var checklist = contentResult.Content;
            return checklist;
        }
    }
}
