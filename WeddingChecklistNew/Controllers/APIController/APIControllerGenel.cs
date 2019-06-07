using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WeddingChecklistNew.Models;

namespace WeddingChecklistNew.Controllers.APIController
{
    public class APIControllerGenel : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }


        // GET: api/APIChecklists
        public IQueryable<Currency> GetCurrencies()
        {
            return db.Currencies.Where(x => x.code == "TRY" || x.code =="USD" || x.code == "EUR");
        }

    }



}
