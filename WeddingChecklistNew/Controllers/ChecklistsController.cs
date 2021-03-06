﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http.Results;
using System.Web.Mvc;
using System.Web.Routing;
using WeddingChecklistNew.Controllers.APIController;
using WeddingChecklistNew.Models;

namespace WeddingChecklistNew.Controllers
{
    [Authorize]
    public class ChecklistsController : Controller
    {
        private APIChecklistsController mAPIChecklistController = new APIChecklistsController();
        private APIChecklistMainsController mAPIChecklistMainController = new APIChecklistMainsController();
        private APIChecklistImagesController mAPIChecklistImagesController = new APIChecklistImagesController();
        private APIControllerGenel mAPIControllerGenel = new APIControllerGenel();
        private AccountController mAccountController = new AccountController();

        // GET: Checklists
        //public ActionResult Index()
        //{
        //    mAccountController.InitializeController(this.Request.RequestContext);
        //    string username = mAccountController.GetLoginUserName();
        //    return View(mAPIChecklistController.GetCheckLists().Where(x=>x.UserId == username));
        //}

        //GET: Checklists/1
        [AllowAnonymous]
        public ActionResult Index(int checklistmainid)
        {
            string username = HttpContext.User.Identity.Name;
            ViewBag.Search = false;
            ViewBag.checklistmainid = checklistmainid;
            return View(mAPIChecklistController.GetCheckLists().Where(x => x.ChecklistMainId == checklistmainid && (x.CheckListMain.Private == false || x.UserId == username)));
        }


        //GET: Checklists/1
        [AllowAnonymous]
        public ActionResult Search(string searchstr)
        {
            string username = HttpContext.User.Identity.Name;
            var searchlist = mAPIChecklistController.GetCheckLists().Where(x => x.Name.Contains(searchstr) || x.Url.Contains(searchstr) || x.CheckListMain.Name.Contains(searchstr) || x.CheckListMain.Definition.Contains(searchstr) && (x.CheckListMain.Private == false || x.UserId == username));
            ViewBag.Search = true;
            ViewBag.checklistmainid = 1;
            return View("Index",searchlist);
        }


        // GET: Checklists/Details/5
        [AllowAnonymous]
        public ActionResult Details(int? id)
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
            var imagelist = mAPIChecklistImagesController.GetCheckListImages().Where(x => x.CheckListId == id).Select(m => new { m.Path, m.Id });
            ViewData["listChecklistImage"] = new SelectList(imagelist, "Id", "Path");
            var currencylist = mAPIControllerGenel.GetCurrencies().Select(m => new { m.code, m.Id });
            ViewData["listCurrency"] = new SelectList(currencylist, "Id", "code");
            return View(checklist);
        }

        // GET: Checklists/Create
        public ActionResult Create(int checklistmainid)
        {
            var currencylist = mAPIControllerGenel.GetCurrencies().Select(m => new { m.code, m.Id });
            ViewData["listCurrency"] = new SelectList(currencylist, "Id", "code");
            Checklistdo checklistdo = new Checklistdo();
            checklistdo.ChecklistMainId = checklistmainid;
            return View(checklistdo);
        }

        // POST: Checklists/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create([Bind(Include = "Id,Name,Url,Price,Priority,ChecklistMainId,CurrencyId,LogDate,UserId,ImageUrl,Done")] int checklistmainid, Checklistdo checklistdo)
        {
            Checklist checklist;
            ChecklistMain checklistMain;
            checklist = GetModel(checklistdo);
            checklistMain = GetChecklistMain(checklistmainid);
            var currencylist = mAPIControllerGenel.GetCurrencies().Select(m => new { m.code, m.Id });
            ViewData["listCurrency"] = new SelectList(currencylist, "Id", "code");
            AddCustomError(checklistMain.UserId);
            checklist.LogDate = DateTime.Now;
            checklist.UserId = GetUserName();
            checklist.ChecklistMainId = checklistmainid;
            if (ModelState.IsValid)
            {
                SetCheckListImages_Upload(checklist, checklistdo.ImageUrl);
                mAPIChecklistController.PostChecklist(checklist);
                TempData["message"] = "success";
                return RedirectToAction("Index");
            }
            
            return View(checklistdo);
        }
       
        // GET: Checklists/Edit/5
        public ActionResult Edit(int checklistmainid, int? id)
        {
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
            Checklistdo checklistdo = GetDomain(checklist);
            return View(checklistdo);
        }

        // POST: Checklists/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Edit([Bind(Include = "Id,Name,Url,Price,Priority,ChecklistMainId,CurrencyId,LogDate,UserId,ImageUrl,Done")] int checklistmainid, Checklistdo checklistdo)
        {
            Checklist checklist;
            checklist = GetModel(checklistdo);
            var imagelist = mAPIChecklistImagesController.GetCheckListImages().Where(x => x.CheckListId == checklist.Id).Select(m => new { m.Path, m.Id });
            ViewData["listChecklistImage"] = new SelectList(imagelist, "Id", "Path");
            var currencylist = mAPIControllerGenel.GetCurrencies().Select(m => new { m.code, m.Id });
            ViewData["listCurrency"] = new SelectList(currencylist, "Id", "code");
            AddCustomError(checklist.UserId);
            checklist.LogDate = DateTime.Now;
            checklist.UserId = GetUserName();
            checklist.ChecklistMainId = checklistmainid;
            if (ModelState.IsValid)
            {
                SetCheckListImages_Upload(checklist, checklistdo.ImageUrl);
                mAPIChecklistController.PutChecklist(checklist.Id,checklist);
                TempData["message"] = "success";
                return RedirectToAction("Index");
            }
            return View(checklistdo);
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
        public ActionResult DeleteConfirmed(int checklistmainid, int id)
        {
            Checklist checklist = GetChecklist(id);
            AddCustomError(checklist.UserId);
            if (ModelState.IsValid)
            {
                mAPIChecklistController.DeleteChecklist(id);
                TempData["message"] = "success";
                return RedirectToAction("Index", new { checklistmainid = checklistmainid });
            }
            return View(checklist);
        }


        private Checklist GetChecklist(int? id)
        {
            var actionResult = mAPIChecklistController.GetChecklist(id);
            var contentResult = actionResult as OkNegotiatedContentResult<Checklist>;
            var checklist = contentResult.Content;
            return checklist;
        }

        private ChecklistMain GetChecklistMain(int? id)
        {
            var actionResult = mAPIChecklistMainController.GetChecklistMain(id);
            var contentResult = actionResult as OkNegotiatedContentResult<ChecklistMain>;
            var checklist = contentResult.Content;
            return checklist;
        }

        private void SetCheckListImages_Upload(Checklist checklist,string ImageURL)
        {
            List<ChecklistImage> lstImages = new List<ChecklistImage>();
            //Local Images
            for (int i = 0; i < Request.Files.Count; i++)
            {
                Guid guid = Guid.NewGuid();
                string filename = Request.Files[i].FileName;
                if (filename == "") break;
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
            //URL Images
            if (ImageURL!=null)
            {
                SaveImageURL(lstImages, ImageURL, checklist.Id);
            }
            checklist.CheckListImage = lstImages;
        }

        private string GetUserName()
        {
            string username;
            mAccountController.InitializeController(this.Request.RequestContext);
            username = mAccountController.GetLoginUserName();
            return username;
        }

        private Checklist GetModel(Checklistdo checklistdo)
        {
            Checklist checklist = new Checklist();
            checklist.CheckListImage = checklistdo.CheckListImage;
            checklist.CheckListMain = checklistdo.CheckListMain;
            checklist.ChecklistMainId = checklistdo.ChecklistMainId;
            checklist.Currency = checklistdo.Currency;
            checklist.CurrencyId = checklistdo.CurrencyId;
            checklist.Id = checklistdo.Id;
            checklist.LogDate = checklistdo.LogDate;
            checklist.Name = checklistdo.Name;
            checklist.Price = checklistdo.Price;
            checklist.Priority = checklistdo.Priority;
            checklist.Url = checklistdo.Url;
            checklist.UserId = checklistdo.UserId;
            checklist.Done = checklistdo.Done;
            return checklist;
        }


        private Checklistdo GetDomain(Checklist checklist)
        {
            Checklistdo checklistdo = new Checklistdo();
            checklistdo.CheckListImage = checklist.CheckListImage;
            checklistdo.CheckListMain = checklist.CheckListMain;
            checklistdo.ChecklistMainId = checklist.ChecklistMainId;
            checklistdo.Currency = checklist.Currency;
            checklistdo.CurrencyId = checklist.CurrencyId;
            checklistdo.Id = checklist.Id;
            checklistdo.LogDate = checklist.LogDate;
            checklistdo.Name = checklist.Name;
            checklistdo.Price = checklist.Price;
            checklistdo.Priority = checklist.Priority;
            checklistdo.Url = checklist.Url;
            checklistdo.UserId = checklist.UserId;
            checklistdo.Done = checklist.Done;
            return checklistdo;
        }

        public void SaveImageURL(List<ChecklistImage> lstImages,string imageURL,int checklistid)
        {
            using (WebClient client = new WebClient())
            {
                Guid guid = Guid.NewGuid();
                string filename = Server.MapPath("~/Content/UserFiles/Images/" + guid.ToString() + ".jpg");
                client.DownloadFile(new Uri(imageURL), filename);
                ChecklistImage checklistImage = new ChecklistImage();
                checklistImage.Path = "~/Content/UserFiles/Images/" + guid.ToString() + ".jpg";
                checklistImage.Type = 1;
                checklistImage.CheckListId = checklistid;
                lstImages.Add(checklistImage);
            }
        }

        private void AddCustomError(string userid)
        {
            if (userid != User.Identity.Name)
            {
                ModelState.AddModelError(string.Empty, "You can not edit someone's list");
            }
        }

    }
}
