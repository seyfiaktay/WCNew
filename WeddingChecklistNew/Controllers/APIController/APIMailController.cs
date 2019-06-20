using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Web.Http;
using WeddingChecklistNew.Models;

namespace WeddingChecklistNew.Controllers.APIController
{
    public class APIMailController : ApiController
    {
        [Route("api/Mail/SendMail")]
        [HttpPost]
        public IHttpActionResult SendMail(Mail mail)
        {
            try
            {
                using (SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com"))
                {
                    MailMessage mailMessage = new MailMessage();
                    SmtpServer.Port = 587;
                    SmtpServer.UseDefaultCredentials = false;
                    SmtpServer.Credentials = new System.Net.NetworkCredential("weddingchecklst@gmail.com", "sa1306070009");
                    SmtpServer.EnableSsl = true;
                    SmtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;
                    mailMessage.From = new MailAddress("weddingchecklst@gmail.com");
                    mailMessage.To.Add(mail.ToAddress);
                    mailMessage.Subject = mail.Subject;
                    mailMessage.Body = mail.Body;
                    SmtpServer.Send(mailMessage);
                }
                return Ok();
            }
            catch(Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
