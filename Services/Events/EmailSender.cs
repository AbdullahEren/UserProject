using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Services.Events
{
    static class EmailSender
    {
        public static void SendEmail(string email, string content)
        {
            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Host = "smtp-mail.outlook.com";
            smtpClient.Port = 587;
            smtpClient.EnableSsl = true;

            NetworkCredential credential = new System.Net.NetworkCredential("yourSenderEmail@outlook.com", "yourPassword");
            smtpClient.Credentials = credential;

            MailAddress sender = new MailAddress("yourSenderEmail@outlook.com", "User Project");
            MailAddress receiver = new MailAddress(email);

            MailMessage mail = new MailMessage(sender,receiver);
            mail.Subject = "User Project";
            mail.Body = content;
            smtpClient.Send(mail);
        }
    }
}
