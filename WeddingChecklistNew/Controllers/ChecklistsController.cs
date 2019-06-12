using System;
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
        public ActionResult Index(int checklistmainid)
        {
            mAccountController.InitializeController(this.Request.RequestContext);
            string username = mAccountController.GetLoginUserName();
            return View(mAPIChecklistController.GetCheckLists().Where(x => x.UserId == username && x.ChecklistMainId == checklistmainid));
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
        public ActionResult Create(int checklistmainid)
        {
            ViewData["listChecklistMain"] = GetMainList(checklistmainid);
            var currencylist = mAPIControllerGenel.GetCurrencies().Select(m => new { m.code, m.Id });
            ViewData["listCurrency"] = new SelectList(currencylist, "Id", "code");
            return View();
        }

        // POST: Checklists/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Url,Price,Priority,ChecklistMainId,CurrencyId,LogDate,UserId,ImageUrl")] int checklistmainid, Checklistdo checklistdo)
        {
            Checklist checklist;
            checklist = GetModel(checklistdo);
            ViewData["listChecklistMain"] = GetMainList(checklistmainid);
            var currencylist = mAPIControllerGenel.GetCurrencies().Select(m => new { m.code, m.Id });
            ViewData["listCurrency"] = new SelectList(currencylist, "Id", "code");
            checklist.LogDate = DateTime.Now;
            checklist.UserId = GetUserName();
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
            ViewData["listChecklistMain"] = GetMainList(checklistmainid);
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
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Url,Price,Priority,ChecklistMainId,CurrencyId,LogDate,UserId,ImageUrl")] int checklistmainid, Checklistdo checklistdo)
        {
            Checklist checklist;
            checklist = GetModel(checklistdo);
            ViewData["listChecklistMain"] = GetMainList(checklistmainid);
            var imagelist = mAPIChecklistImagesController.GetCheckListImages().Where(x => x.CheckListId == checklist.Id).Select(m => new { m.Path, m.Id });
            ViewData["listChecklistImage"] = new SelectList(imagelist, "Id", "Path");
            var currencylist = mAPIControllerGenel.GetCurrencies().Select(m => new { m.code, m.Id });
            ViewData["listCurrency"] = new SelectList(currencylist, "Id", "code");
            checklist.LogDate = DateTime.Now;
            checklist.UserId = GetUserName();
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


        private SelectList GetMainList(int checklistmainid)
        {
            SelectList selectlists;
            string username = GetUserName();
            var list = mAPIChecklistMainController.GetChecklistMains().Where(x => x.UserId == username && x.Id == checklistmainid).Select(m => new { m.Name, m.Id });
            selectlists = new SelectList(list, "Id", "Name");
            return selectlists;
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
    }
}
