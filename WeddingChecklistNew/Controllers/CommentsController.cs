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
    public class CommentsController : Controller
    {

        private APICommentsController mAPICommentController = new APICommentsController();
        // GET: Comments
        public ActionResult Index()
        {
            var comments = mAPICommentController.GetComments().Include(c => c.CheckListMain);
            return View(comments.ToList());
        }

        // GET: Comments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = GetComment(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        // GET: Comments/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Comments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create([Bind(Include = "Id,Text,ChecklistMainId,Type,LogDate,UserId")] Comment comment)
        {
            comment.UserId = HttpContext.User.Identity.Name;
            comment.LogDate = DateTime.Now;
            if (ModelState.IsValid)
            {
                mAPICommentController.PostComment(comment);
                //return RedirectToAction("Details", "ChecklistMains", new { id = comment.ChecklistMainId });
                return Json("Done");
            }
            return View(comment);
        }

        // GET: Comments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = GetComment(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        // POST: Comments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Text,ChecklistMainId,Type,LogDate,UserId")] Comment comment)
        {
            comment.UserId = HttpContext.User.Identity.Name;
            comment.LogDate = DateTime.Now;
            if (ModelState.IsValid)
            {
                mAPICommentController.PutComment(comment.Id,comment);
                return RedirectToAction("Details", "ChecklistMains", new { id = comment.ChecklistMainId });
            }
            return View(comment);
        }

        // GET: Comments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = GetComment(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        // POST: Comments/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Comment comment = GetComment(id);
            if (comment.UserId != User.Identity.Name)
            {
                return Json(new { returntype = 0, message = "you can not do that transaction" });
            }
            mAPICommentController.DeleteComment(id);
            //return RedirectToAction("Details", "ChecklistMains", new { id = comment.ChecklistMainId });
            return Json(new { returntype=1,message="done" });
        }


        private Comment GetComment(int? id)
        {
            var actionResult = mAPICommentController.GetComment(id);
            var contentResult = actionResult as OkNegotiatedContentResult<Comment>;
            var comment = contentResult.Content;
            return comment;
        }

        [AllowAnonymous]
        public PartialViewResult CommentViewTop()
        {
            List<Comment> commentList;
            commentList = mAPICommentController.GetComments().ToList().OrderByDescending(x => x.LogDate).Take(10).ToList();
            ViewBag.Transaction = false;
            return PartialView("~/Views/ChecklistMains/_CommentView.cshtml", commentList);
        }
    }
}
