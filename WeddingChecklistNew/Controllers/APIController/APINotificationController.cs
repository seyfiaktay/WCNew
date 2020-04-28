using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Web.Http;
using WeddingChecklistNew.Models;

namespace WeddingChecklistNew.Controllers.APIController
{
    [RoutePrefix("api/Notification")]
    public class APINotificationController : ApiController
    {
        public APINotificationController()
        {

        }
        [HttpPost]
        [Route("SendEmail")]
        public IHttpActionResult SendEmail(Mail mailAddress)
        {
            try
            {
                // Send an email with this link
                string smtpServer = ConfigurationSettings.AppSettings.Get("smtpServer");
                string emailUserName = ConfigurationSettings.AppSettings.Get("emailUserName");
                string emailPassword = ConfigurationSettings.AppSettings.Get("emailPassword");
                string fromEmailAddress = ConfigurationSettings.AppSettings.Get("fromEmailAddress");
                int port = Convert.ToInt32(ConfigurationSettings.AppSettings.Get("port"));
                using (SmtpClient SmtpServer = new SmtpClient(smtpServer))
                {
                    MailMessage mail = new MailMessage();
                    SmtpServer.Port = port;
                    SmtpServer.UseDefaultCredentials = false;
                    SmtpServer.Credentials = new System.Net.NetworkCredential(emailUserName, emailPassword);
                    SmtpServer.EnableSsl = true;
                    SmtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;
                    mail.From = new MailAddress(fromEmailAddress);
                    mail.To.Add(mailAddress.ToAddress);
                    mail.Subject = mailAddress.Subject;
                    mail.Body = mailAddress.Body;
                    SmtpServer.Send(mail);
                }
                return Ok();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occured when sending email. Details:" + ex.Message);
            }
        }
    }
}
