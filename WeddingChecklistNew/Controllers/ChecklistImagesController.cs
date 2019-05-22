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
    public class ChecklistImagesController : Controller
    {
        private APIChecklistImagesController mAPICheckListImageController = new APIChecklistImagesController();

        // GET: ChecklistImages
        public ActionResult Index()
        {
            var checkListImages = mAPICheckListImageController.GetCheckListImages();
            return View(checkListImages.ToList());
        }

        // GET: ChecklistImages/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChecklistImage checklistImage = GetChecklistMain(id);
            if (checklistImage == null)
            {
                return HttpNotFound();
            }
            return View(checklistImage);
        }

        // GET: ChecklistImages/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ChecklistImages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Path,CheckListId,Type")] ChecklistImage checklistImage)
        {
            if (ModelState.IsValid)
            {
                mAPICheckListImageController.PostChecklistImage(checklistImage);
                return RedirectToAction("Index");
            }
            return View(checklistImage);
        }

        // GET: ChecklistImages/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChecklistImage checklistImage = GetChecklistMain(id);
            if (checklistImage == null)
            {
                return HttpNotFound();
            }
            return View(checklistImage);
        }

        // POST: ChecklistImages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Path,CheckListId,Type")] ChecklistImage checklistImage)
        {
            if (ModelState.IsValid)
            {
                mAPICheckListImageController.PutChecklistImage(checklistImage.Id, checklistImage);
                return RedirectToAction("Index");
            }
            return View(checklistImage);
        }

        // GET: ChecklistImages/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChecklistImage checklistImage = GetChecklistMain(id);
            if (checklistImage == null)
            {
                return HttpNotFound();
            }
            return View(checklistImage);
        }

        // POST: ChecklistImages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ChecklistImage checklistImage = GetChecklistMain(id);
            mAPICheckListImageController.DeleteChecklistImage(id);
            return RedirectToAction("Index");
        }

        private ChecklistImage GetChecklistMain(int? id)
        {
            var actionResult = mAPICheckListImageController.GetChecklistImage(id);
            var contentResult = actionResult as OkNegotiatedContentResult<ChecklistImage>;
            var checklistimage = contentResult.Content;
            return checklistimage;
        }
    }
}
