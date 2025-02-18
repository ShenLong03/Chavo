﻿namespace Chavo.ECommerce.Helpers
{
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Mail;
    using System.Threading.Tasks;
    using System.Web.Configuration;

    public class MailHelper
    {
        public static async Task SendMail(string to, string subject, string body)
        {
            try
            {
                var message = new MailMessage();
                message.To.Add(new MailAddress(to));
                message.From = new MailAddress(WebConfigurationManager.AppSettings["AdminMail"]);
                message.Subject = subject;
                message.Body = body;
                message.IsBodyHtml = true;

                using (var smtp = new SmtpClient())
                {
                    var credential = new NetworkCredential
                    {
                        UserName = WebConfigurationManager.AppSettings["AdminMail"],
                        Password = WebConfigurationManager.AppSettings["AdminPassWord"]
                    };

                    smtp.Credentials = credential;
                    smtp.Host = WebConfigurationManager.AppSettings["SMTPName"];
                    smtp.Port = int.Parse(WebConfigurationManager.AppSettings["SMTPPort"]);
                    smtp.EnableSsl = true;
                    await smtp.SendMailAsync(message);
                }
            }
            catch (System.Exception ex)
            {
            }
        }

        public static async Task SendMail(List<string> mails, string subject, string body)
        {
            var message = new MailMessage();

            foreach (var to in mails)
            {
                message.To.Add(new MailAddress(to));
            }

            message.From = new MailAddress(WebConfigurationManager.AppSettings["AdminMail"]);
            message.Subject = subject;
            message.Body = body;
            message.IsBodyHtml = true;

            using (var smtp = new SmtpClient())
            {
                var credential = new NetworkCredential
                {
                    UserName = WebConfigurationManager.AppSettings["AdminMail"],
                    Password = WebConfigurationManager.AppSettings["AdminPassWord"]
                };

                smtp.Credentials = credential;
                smtp.Host = WebConfigurationManager.AppSettings["SMTPName"];
                smtp.Port = int.Parse(WebConfigurationManager.AppSettings["SMTPPort"]);
                smtp.EnableSsl = false;
                await smtp.SendMailAsync(message);
            }
        }

    }
}