using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using MoodPocket.WebUI.Utilities.Abstract;
using System.Net.Mail;
using System.Configuration;
using System.Web.Mvc;

namespace MoodPocket.WebUI.Utilities.Concrete
{
    public class EmailService : IEmailSender
    {
        public string CoreEmail { get; private set; }

        private IStringHasher stringHashService;

        public EmailService()
        {
            CoreEmail = ConfigurationManager.AppSettings["CoreEmail"];
            stringHashService = new StringHashService();
        }

        public void SendVerificationLink(string name, string to)
        {
            UrlHelper urlHelper = new UrlHelper(HttpContext.Current.Request.RequestContext);
            string randSalt = stringHashService.GetRandomSalt();
            string activationCode = stringHashService.HashString(name, randSalt);
            string verifyUrl = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) +
                urlHelper.Action("VerifyAccount", "Account")+"/"+name+"/"+ randSalt +"/"+ activationCode;

            using(SmtpClient client = new SmtpClient())
            {
                client.Host = "smtp.gmail.com";
                using (MailMessage message = new MailMessage(CoreEmail, to))
                {
                    message.Subject = "Please verify your account!";
                    message.Body = "<html><head><meta content=\"text/html; charset = utf - 8\" /></head>" +
                        "<body><p>Dear " + name + ", </p><p>To verify your account, please click the following link:</p>"
                    + "<p><a href=\"" + verifyUrl + "\" >" + verifyUrl + "</a>" +
                    "</p><div>Best regards,</div><div>Your Master</div><p>Do not forward this email. The verify link is private.</p></body></html>";

                    message.IsBodyHtml = true;
                    client.EnableSsl = true;

                    client.Send(message);
                }
            }
        }
    }

}