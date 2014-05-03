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

        private ApplicationDbContext db = new ApplicationDbContext();
/*
        public string getEmployeeIdByLoggedUser(AspNetUsers loggedUser)
        {
            var id = (from u in db.Users
                      where u.UserName.Equals(loggedUser.Name)
                      select u.EmployeeId).First();//iau id-ul angajatului cu nume user = loggedUser.Name
            return id.ToString();
        }
*/
        public string getStatusNameById(int id)
        {
            string s = (from st in db.MyStatus
                        where st.StatusId.Equals(id)
                        select st.StatusName).First().ToString();
            return s;
        }
        public Employee getEmployeeById(int id)
        {
            var emp = db.MyEmployee.Find(Convert.ToInt32(id));//obtin obiectul employee cu id-ul obtinut mai sus
            return emp;            
        }
        public Employee getHREmployee(string dep)
        {
            var hr = (from p in db.MyEmployee
                      where p.Department.Equals(dep)
                      select p).First();
            return hr;
        }
        public Address getAddressById(int id)
        {
            var dep = (from d in db.MyAddress
                       where d.AddressId.Equals(id)
                       select d).First();
            return dep;
        }
        public Employee getStandInEmployeeById(int id)
        {
            var stdin = (from e in db.MyEmployee
                         where e.EmployeeId == id
                         select e).First();
            return stdin;
        }
        public void addRequestAndSaveChanges(Request request)
        {
            db.MyRequest.Add(request);
            db.SaveChanges();
        }
        public void saveChanges()
        {
            db.SaveChanges();
        }
    }
}