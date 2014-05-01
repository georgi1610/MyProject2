using MyProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;


namespace MyProject.DAL
{
    public class EmployeeDAL
    {
        public void sendEmail(Employee dest, Employee sender, string subject, string body)
        {
            try
            {
                MailMessage mail = new MailMessage();
                mail.To.Add(dest.EMail);
                mail.From = new MailAddress(sender.EMail);
                mail.Subject = subject; 
                mail.Body = body;
                mail.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.UseDefaultCredentials = false;
                string userName = sender.EMail.Substring(0, sender.EMail.IndexOf('@'));
                smtp.Credentials = new System.Net.NetworkCredential
                (userName, sender.EMailPassword);// Enter senders User name and password
                smtp.EnableSsl = true;
                smtp.Send(mail);
            }
            catch(Exception e)
            {
                //send email failed
            }
        }
    }
}