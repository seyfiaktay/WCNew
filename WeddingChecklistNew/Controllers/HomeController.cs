using log4net;
using System.Web.Mvc;
using WeddingChecklistNew.Controllers.APIController;

namespace WeddingChecklistNew.Controllers
{
    public class HomeController : Controller
    {
        private AccountController mAccountController = new AccountController();
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";
            return View();
        }
        public PartialViewResult GetSlider()
        {
            return PartialView("_GetSlider");
        }
        public PartialViewResult GetWidget()
        {
            var service = new APIChecklistsController();
            var model = new Models.WidgetModel();
            model.Checklists = service.GetRecentlyCheckLists();
            model.ChecklistMain= service.GetRecentlyCheckListMains();
            return PartialView("_GetWidget",model);
        }
    }
}
