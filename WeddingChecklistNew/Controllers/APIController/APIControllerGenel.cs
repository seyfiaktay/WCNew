using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using System;
using System.Collections.Generic;
using System.IO;
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

        public byte[] ConvertHtmlToPdfAsBytes(string HtmlData)
        {
            // do some additional cleansing to handle some scenarios that are out of control with the html data  
            HtmlData = HtmlData.Replace("<br>", "<br />");

            // create a stream that we can write to, in this case a MemoryStream  
            using (var stream = new MemoryStream())
            {
                // create an iTextSharp Document which is an abstraction of a PDF but **NOT** a PDF  
                using (var document = new Document())
                {
                    // create a writer that's bound to our PDF abstraction and our stream  
                    using (var writer = PdfWriter.GetInstance(document, stream))
                    {
                        // open the document for writing  
                        document.Open();
                        document.NewPage();

                        // read html data to StringReader  
                        using (var html = new StringReader(HtmlData))
                        {
                            XMLWorkerHelper.GetInstance().ParseXHtml(writer, document, html);
                        }

                        // close document  
                        document.Close();
                    }
                }

                // get bytes from stream  
                return stream.ToArray();
            }
        }
    }



}
