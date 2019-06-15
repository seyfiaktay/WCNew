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
    [Authorize]
    public class ChecklistMainsController : Controller
    {
        private APIChecklistMainsController mAPIChecklistMainController = new APIChecklistMainsController();
        private AccountController mAccountController = new AccountController();
        private APICommentsController mAPICommentsController = new APICommentsController();
        // GET: ChecklistMains
        public ActionResult Index()
        {
            string username = HttpContext.User.Identity.Name;
            return View(mAPIChecklistMainController.GetChecklistMains().Where(x => x.UserId == username));
        }

        // GET: ChecklistMains/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChecklistMain checklistMain = GetChecklistMain(id);
            if (checklistMain == null)
            {
                return HttpNotFound();
            }
            return View(checklistMain);
        }

        // GET: ChecklistMains/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ChecklistMains/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,LogDate,UserId,DueDate,Private,Definition")] ChecklistMain checklistMain)
        {
            checklistMain.LogDate = DateTime.Now;
            checklistMain.UserId = HttpContext.User.Identity.Name;
            checklistMain.checklists = new List<Checklist>();
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (ModelState.IsValid)
            {
                mAPIChecklistMainController.PostChecklistMain(checklistMain);
                TempData["message"] = "success";
                return RedirectToAction("Index");
            }
            return View(checklistMain);
        }

        // GET: ChecklistMains/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChecklistMain checklistMain = GetChecklistMain(id);
            if (checklistMain == null)
            {
                return HttpNotFound();
            }
            return View(checklistMain);
        }

        // POST: ChecklistMains/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,LogDate,UserId,DueDate,Private,Definition")] ChecklistMain checklistMain)
        {
            checklistMain.LogDate = DateTime.Now;
            checklistMain.UserId = HttpContext.User.Identity.Name;
            if (ModelState.IsValid)
            {
                mAPIChecklistMainController.PutChecklistMain(checklistMain.Id, checklistMain);
                TempData["message"] = "success";
                return RedirectToAction("Index");
            }
            return View(checklistMain);
        }

        // GET: ChecklistMains/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChecklistMain checklistMain = GetChecklistMain(id);
            if (checklistMain == null)
            {
                return HttpNotFound();
            }
            return View(checklistMain);
        }

        // POST: ChecklistMains/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ChecklistMain checklistMain = GetChecklistMain(id);
            mAPIChecklistMainController.DeleteChecklistMain(id);
            TempData["message"] = "success";
            return RedirectToAction("Index");
        }


        public PartialViewResult CommentView(int id)
        {
            ViewBag.UserName = HttpContext.User.Identity.Name;
            ViewBag.ChecklistMainId = id; 
            List<Comment> commentList;
            commentList = mAPICommentsController.GetComments().Where(x => x.ChecklistMainId == id).ToList();
            return PartialView("_CommentView", commentList);
        }

        private ChecklistMain GetChecklistMain(int? id)
        {
            var actionResult = mAPIChecklistMainController.GetChecklistMain(id);
            var contentResult = actionResult as OkNegotiatedContentResult<ChecklistMain>;
            var checklistmain = contentResult.Content;
            return checklistmain;
        }


    }
}
