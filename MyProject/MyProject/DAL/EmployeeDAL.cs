using MyProject.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Web;
using PdfFileWriter;
using System.Drawing;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace MyProject.DAL
{
    public class EmployeeDAL
    {
        public void writePDF()
        {
            PdfDocument document = new PdfDocument(PaperType.A4, false, UnitOfMeasure.cm);
          
            // Step 3: Add new empty page
            PdfPage Page = new PdfPage(document);

            // Step 4: Add contents object to the page object
            PdfContents Contents = new PdfContents(Page);

            // Step 5: add graphics and text contents to the contents object
            TextBox Box = new TextBox(10, 5);

            // add text to the text box

            PdfFont font = new PdfFont(document, "Arial", FontStyle.Bold, true);
            Box.AddText(font,12.0,
            "This area is an example of displaying text that is too long to fit within a " +
            "fixed width area. The text is displayed justified to right edge. You define a " +
            "text box with the required width and first line indent. You add text to this " +
            "box. The box will divide the text into lines. Each line is made of segments " +
            "of text. For each segment, you define font, font size, drawing style and color. " +
            "After loading all the text, the program will draw the formatted text.\n");
            Double PosY = 1.4;
            Contents.DrawText(0.0, ref PosY, 0.0, 0, 0.015, 0.05, true, Box);

            // Step 6: create pdf file
            // argument: PDF file name
            string FileName = @"D:\MyPDFFile.pdf";
            document.CreateFile(FileName);

        }
        public void WriteToWordFile()
        {
            // Any folder 
            string path = @"D:\MyWordFile.doc";

            string text = "scrieeeeeeeee";
            // Put it in try-catch finally :) 
            FileStream fs = File.Create(path);
            fs.Close();
            StreamWriter sw = new StreamWriter(path);
            sw.Write(text);
            sw.Close();

        }

        public int sendEmail(Employee dest, Employee sender, string subject, string body)
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
                return 0;
            }
            catch(Exception e)
            {
                //send email failed
                return -1;
            }
        }

        private ApplicationDbContext db = new ApplicationDbContext();

        public int GetEmployeeIdByLoggedUser(string loggedUserName)
        {
            var id = (from u in db.Users
                      where u.UserName.Equals(loggedUserName)
                      select u.EmployeeId).First();
            return Convert.ToInt32(id);
       }

        public List<Request> GetRequestsListByEmployeeId(Int32 employeeId)
        {
            var req = (from r in db.MyRequest
                       where r.Applicant.EmployeeId == employeeId
                       select r).ToList();
            return req;
        }

        public string GetStatusNameById(int id)
        {
            string s = (from st in db.MyStatus
                        where st.StatusId.Equals(id)
                        select st.StatusName).First().ToString();
            return s;
        }
        public Employee GetEmployeeById(int id)
        {
            var emp = db.MyEmployee.Find(Convert.ToInt32(id));//obtin obiectul employee cu id-ul obtinut mai sus
            return emp;            
        }
        public void CreateUserAndAddToRole(Employee employee)
        {
            //create user for employee
            var user = new ApplicationUser();
            user.UserName = employee.FirstName + employee.LastName;
            user.MyEmployee = employee;
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var adminresult = UserManager.Create(user, "password");
            //db.SaveChanges();
            SaveChanges();

            //Add User to Role 'Employee'
            if (adminresult.Succeeded)
            {
                var result = UserManager.AddToRole(user.Id, "Employee");
                //db.SaveChanges();
                SaveChanges();
            }
        }
        public Employee GetHREmployee(string dep)
        {
            var hr = (from p in db.MyEmployee
                      where p.Department.Equals(dep)
                      select p).First();
            return hr;
        }
        public Address GetAddressById(int id)
        {
            var dep = (from d in db.MyAddress
                       where d.AddressId.Equals(id)
                       select d).First();
            return dep;
        }
        public Employee GetStandInEmployeeById(int id)
        {
            var stdin = (from e in db.MyEmployee
                         where e.EmployeeId == id
                         select e).First();
            return stdin;
        }
        public TransportCompany GetTransportCompanyById(int id)
        {
            var trans = (from t in db.TransportCompanies
                         where t.TransportCompanyId == id
                         select t).First();
            return trans;
        }
        public void AddRequestAndSaveChanges(Request request)
        {
            db.MyRequest.Add(request);
            db.SaveChanges();
        }
        public void AddTransportAndSaveChanges(Transport transport)
        {
            db.MyTransport.Add(transport);
            db.SaveChanges();
        }
        public void AddEmployeeAndSaveChanges(Employee employee)
        {
            db.MyEmployee.Add(employee);
            db.SaveChanges();
        }
        public void SaveChanges()
        {
            db.SaveChanges();
        }
    }
}