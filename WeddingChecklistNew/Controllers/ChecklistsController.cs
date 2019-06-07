﻿using System;
using System.Collections.Generic;
using System.Configuration;
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
        private APIChecklistImagesController mAPIChecklistImagesController = new APIChecklistImagesController();
        private APIControllerGenel mAPIControllerGenel = new APIControllerGenel();

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
            var list = mAPIChecklistMainController.GetChecklistMains().Select(m=> new {m.Name,m.Id});
            ViewData["listChecklistMain"] =  new SelectList(list,"Id","Name");
            var currencylist = mAPIControllerGenel.GetCurrencies().Select(m => new { m.code, m.Id });
            ViewData["listCurrency"] = new SelectList(currencylist, "Id", "code");
            return View();
        }

        // POST: Checklists/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Url,Price,Priority,ChecklistMainId,CurrencyId")] Checklist checklist)
        {
            var list = mAPIChecklistMainController.GetChecklistMains().Select(m => new { m.Name, m.Id });
            ViewData["listChecklistMain"] = new SelectList(list, "Id", "Name");
            var currencylist = mAPIControllerGenel.GetCurrencies().Select(m => new { m.code, m.Id });
            ViewData["listCurrency"] = new SelectList(currencylist, "Id", "code");
            SetCheckListImages_Upload(checklist);
            if (ModelState.IsValid)
            {
                mAPIChecklistController.PostChecklist(checklist);
                TempData["message"] = "success";
                return RedirectToAction("Index");
            }
            return View(checklist);
        }
       
        // GET: Checklists/Edit/5
        public ActionResult Edit(int? id)
        {
            var list = mAPIChecklistMainController.GetChecklistMains().Select(m => new { m.Name, m.Id });
            ViewData["listChecklistMain"] = new SelectList(list, "Id", "Name");
            var imagelist = mAPIChecklistImagesController.GetCheckListImages().Where(x=> x.CheckListId == id).Select(m => new {m.Path, m.Id });
            ViewData["listChecklistImage"] = new SelectList(imagelist, "Id", "Path");
            var currencylist = mAPIControllerGenel.GetCurrencies().Select(m => new { m.code, m.Id });
            ViewData["listCurrency"] = new SelectList(currencylist, "Id", "code");
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
        public ActionResult Edit([Bind(Include = "Id,Name,Url,Price,Priority,ChecklistMainId,CurrencyId")] Checklist checklist)
        {
            var list = mAPIChecklistMainController.GetChecklistMains().Select(m => new { m.Name, m.Id });
            ViewData["listChecklistMain"] = new SelectList(list, "Id", "Name");
            var imagelist = mAPIChecklistImagesController.GetCheckListImages().Where(x => x.CheckListId == checklist.Id).Select(m => new { m.Path, m.Id });
            ViewData["listChecklistImage"] = new SelectList(imagelist, "Id", "Path");
            var currencylist = mAPIControllerGenel.GetCurrencies().Select(m => new { m.code, m.Id });
            ViewData["listCurrency"] = new SelectList(currencylist, "Id", "code");

            SetCheckListImages_Upload(checklist);
            if (ModelState.IsValid)
            {
                mAPIChecklistController.PutChecklist(checklist.Id,checklist);
                TempData["message"] = "success";
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
            TempData["message"] = "success";
            return RedirectToAction("Index");
        }


        private Checklist GetChecklist(int? id)
        {
            var actionResult = mAPIChecklistController.GetChecklist(id);
            var contentResult = actionResult as OkNegotiatedContentResult<Checklist>;
            var checklist = contentResult.Content;
            return checklist;
        }

        private void SetCheckListImages_Upload(Checklist checklist)
        {
            List<ChecklistImage> lstImages = new List<ChecklistImage>();
            for (int i = 0; i < Request.Files.Count; i++)
            {
                Guid guid = Guid.NewGuid();
                string filename = Request.Files[i].FileName;
                if (filename == "") return;
                string type = filename.Substring(filename.IndexOf("."), filename.Length - filename.IndexOf("."));
                string mapPath = "~/Content/UserFiles/Images/" + guid.ToString() + type;
                string physicalPath = Server.MapPath(mapPath);
                ChecklistImage checklistImage = new ChecklistImage();
                checklistImage.Path = mapPath;
                checklistImage.Type = 1;
                checklistImage.CheckListId = checklist.Id;
                lstImages.Add(checklistImage);
                Request.Files[i].SaveAs(physicalPath);
            }
            checklist.CheckListImage = lstImages;
        }
    }
}
