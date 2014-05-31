using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyProject.Models;
using System.Net.Mail;
using MyProject.DAL;
using System.Data.Entity.Core;
using System.Data.Entity.Core.Objects;

namespace MyProject.Controllers
{
    public class RequestController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: /Request/Home
        public ActionResult Home()
        {
            var myrequest = db.MyRequest.Include(r => r.Delegation).Include(r => r.Status);
            return View(myrequest.ToList());
        }

        // GET: /Request/
        [Authorize(Roles="Employee")]
        public ActionResult Index()
        {
            return View();
        }

        // GET: /Request/IndexSup
        [Authorize(Roles="Employee")]
        public ActionResult IndexSup()//superior - cererile gestionate de el - sa dea approve/deny
        {
            var myrequest = db.MyRequest.Include(r => r.Delegation).Include(r => r.Status);

            var loggedUser = User.Identity;
            var id = (from u in db.Users
                      where u.UserName.Equals(loggedUser.Name)
                      select u.EmployeeId).First();//iau id-ul angajatului logat cu nume user = loggedUser.Name
            var req = (from r in db.MyRequest
                       where r.Approver.EmployeeId == id
                       select r).ToList();
            return View(req.ToList());
        }

        // GET: /Request/IndexMyReq
        [Authorize(Roles="Employee")]
        public ActionResult IndexMyReq()//cererile user-ului logat
        {
            var myrequest = db.MyRequest.Include(r => r.Delegation).Include(r => r.Status);

            var loggedUser = User.Identity;
            var id = (from u in db.Users
                      where u.UserName.Equals(loggedUser.Name)
                      select u.EmployeeId).First();//iau id-ul angajatului logat cu nume user = loggedUser.Name
            var req = (from r in db.MyRequest
                       where r.Applicant.EmployeeId == id
                       select r).ToList();
            return View(req.ToList());
        }

        // GET: /Request/IndexHR
        [Authorize(Roles = "Employee")]
        public ActionResult IndexHR()//cererile user-ului logat
        {
            var myrequest = db.MyRequest.Include(r => r.Delegation).Include(r => r.Status);

            var loggedUser = User.Identity;
            var id = (from u in db.Users
                      where u.UserName.Equals(loggedUser.Name)
                      select u.EmployeeId).First();//iau id-ul angajatului logat cu nume user = loggedUser.Name
            var req = (from r in db.MyRequest//iau doar cererile aprobate, la care user-ul HR logat este setat
                       where r.HREmployee.EmployeeId == id && r.HREmployee.Department.Equals("Human Resources") 
                             && (r.Status.StatusName.Equals("Approved") || r.Status.StatusName.Equals("Denied"))
                       select r).ToList();
            return View(req.ToList());
        }

        // GET: /Request/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Request request = db.MyRequest.Find(id);
            if (request == null)
            {
                return HttpNotFound();
            }
            if (Request.IsAjaxRequest())
            {
                Request myReq = new Request();

                myReq.DepartureAddress = request.DepartureAddress;
                myReq.ReturnAddress = request.ReturnAddress;
                myReq.DepartureDate = request.DepartureDate;
                myReq.ReturnDate = request.ReturnDate;
                myReq.Status.StatusName = request.Status.StatusName;
                myReq.Applicant = request.Applicant;
                myReq.Approver = request.Approver;
                myReq.StandIn = request.StandIn;
                myReq.HREmployee = request.HREmployee;

                return PartialView("_Details", myReq);
            }
            else
            return View(request);
        }

        // GET: /Request/Create
        public ActionResult Create()
        {
            ViewBag.DelegationId = new SelectList(db.MyDelegation, "DelegationId", "DelegationType");
            ViewBag.StatusId = new SelectList(db.MyStatus, "StatusId", "StatusName");
            ViewBag.DepartureAddressId = new SelectList(db.MyAddress, "AddressId", "CompanyName");
            ViewBag.ReturnAddressId = new SelectList(db.MyAddress, "AddressId", "CompanyName");
            ViewBag.StandInId = new SelectList(db.MyEmployee, "EmployeeId", "FullName");

            return View();
        }

        // POST: /Request/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RequestId,DepartureDate,ReturnDate,StatusId,Description,DelegationId,DepartureAddressId,ReturnAddressId")] Request request,int? StandInId,int? TransportId)
        {
            if (ModelState.IsValid)
            {
                using (var trans = db.Database.BeginTransaction())
                {
                    try
                    {
                        EmployeeDAL ed = new EmployeeDAL();

                        request.SubmitDate = DateTime.Now;//set submit date to current date

                        var loggedUser = User.Identity;//user logat
                        var id = (from u in db.Users
                                  where u.UserName.Equals(loggedUser.Name)
                                  select u.EmployeeId).First();//iau id-ul angajatului cu nume user = loggedUser.Name

                        var emp = ed.getEmployeeById(Convert.ToInt32(id)); //obtin obiectul employee cu id-ul obtinut mai sus
                        if(emp!=null)
                            request.Applicant = emp;//setez applicant la request in cerere

                        var sup = emp.SuperiorEmployee;
                        if (sup != null)//are superior, seteaza si id superior in cerere
                            request.Approver = sup;
                        

                        var hr = ed.getHREmployee("Human Resources");
                        if (hr != null)
                            request.HREmployee = hr;

                        var dep = ed.getAddressById(request.DepartureAddressId);
                        if (dep != null)
                            request.DepartureAddress = dep;

                        var ret = ed.getAddressById(request.ReturnAddressId);
                        if (ret != null)
                            request.ReturnAddress = ret;

                        var stdin = ed.getStandInEmployeeById(StandInId.Value);
                        if (stdin != null)
                            request.StandIn = stdin;

                        ed.addRequestAndSaveChanges(request);
                                                
                        string subject = "delegation request";
                        string body = "please approve my request by entering this link http://localhost:5281/Request/IndexSup";

                        ed.sendEmail(request.Approver, request.Applicant, subject, body);

                        trans.Commit();
                    }
                    catch (Exception e)
                    {
                        trans.Rollback();
                    }
                }
               
                return RedirectToAction("IndexMyReq");
            }

            ViewBag.DelegationId = new SelectList(db.MyDelegation, "DelegationId", "DelegationType", request.DelegationId);
            ViewBag.StatusId = new SelectList(db.MyStatus, "StatusId", "StatusName", request.StatusId);
            ViewBag.DepartureAddressId = new SelectList(db.MyAddress, "AddressId", "CompanyName", request.DepartureAddressId);
            ViewBag.ReturnAddressId = new SelectList(db.MyAddress, "AddressId", "CompanyName", request.ReturnAddressId);
            ViewBag.StandInId = new SelectList(db.MyEmployee, "EmployeeId", "FullName");//, request.StandIn.EmployeeId);
  

            return View(request);
        }

        // GET: /Request/EditEmp/5
        public ActionResult EditEmp(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Request request = db.MyRequest.Find(id);
            if (request == null)
            {
                return HttpNotFound();
            }
            ViewBag.DelegationId = new SelectList(db.MyDelegation, "DelegationId", "DelegationType", request.DelegationId);
            ViewBag.StatusId = new SelectList(db.MyStatus, "StatusId", "StatusName", request.StatusId);

            return View(request);
        }

        // POST: /Request/EditEmp/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        /*
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditEmp([Bind(Include = "RequestId,SubmitDate,DepartureDate,ReturnDate,StatusId,Motivation,Description,DelegationId")] Request request)
        {
            if (ModelState.IsValid)
            {
                using (var trans = db.Database.BeginTransaction())
                {
                    try
                    {
                        EmployeeDAL ed = new EmployeeDAL();

                        db.Entry(request).State = EntityState.Modified;
                        object o;
                        bool b;
                        b = TempData.TryGetValue("subDate", out o);
                        request.SubmitDate = (System.DateTime)o;
                        b = TempData.TryGetValue("depDate", out o);
                        request.DepartureDate = (System.DateTime)o;
                        b = TempData.TryGetValue("retDate", out o);
                        request.ReturnDate = (System.DateTime)o;
                        b = TempData.TryGetValue("delID", out o);
                        request.DelegationId = (Int32)o;

                        //b = TempData.TryGetValue("status", out o);
                        //request.StatusId = (Int32)o;

                        //db.SaveChanges();
                        ed.saveChanges();

                        b = TempData.TryGetValue("applicant", out o);
                        request.Applicant = (Employee)o;
                        b = TempData.TryGetValue("approver", out o);
                        request.Approver = (Employee)o;
                        b = TempData.TryGetValue("HR", out o);
                        request.HREmployee = (Employee)o;

                        //b = TempData.TryGetValue("status", out o);
                        //request.StatusId = (Int32)o;
                        string s = ed.getStatusNameById(request.StatusId);


                        //send email to applicant
                        string body = "";
                        string subject = "";
                        if (s.Equals("Approved"))
                        {
                            subject = "delegation request approved";
                            body = "your request has been approved, view here your request status http://localhost:5281/Request/IndexMyReq";
                        }
                        if (s.Equals("Denied"))
                        {
                            subject = "delegation request denied";
                            body = "your request has been denied, view here your request status http://localhost:5281/Request/IndexMyReq";
                        }
                        ed.sendEmail(request.Applicant, request.Approver, subject, body);

                        //send email to HR
                        if (s.Equals("Approved"))
                        {
                            subject = "delegation request approved";
                            body = "request has been approved, add transport details http://localhost:5281/Request/IndexHR";
                        }
                        if (s.Equals("Denied"))
                        {
                            subject = "delegation request denied";
                            body = "request has been denied";
                        }
                        ed.sendEmail(request.HREmployee, request.Approver, subject, body);

                        TempData.Clear();

                        trans.Commit();
                    }
                    catch (Exception e)
                    {
                        trans.Rollback();
                    }
                }

                return RedirectToAction("IndexSup");
            }
            ViewBag.DepartureDate = new SelectList(db.MyRequest, "RequestId", "DepartureDate", request.DepartureDate);
            ViewBag.ReturnDate = new SelectList(db.MyRequest, "RequestId", "ReturnDate", request.ReturnDate);
            ViewBag.Description = new SelectList(db.MyRequest, "RequestId", "DepartureDate", request.DepartureDate);

            ViewBag.DelegationId = new SelectList(db.MyDelegation, "DelegationId", "DelegationType", request.DelegationId);
            ViewBag.StatusId = new SelectList(db.MyStatus, "StatusId", "StatusName", request.StatusId);
            return View(request);
        }
        */

        // GET: /Request/EditHR/5
        public ActionResult EditHR(int? id)//, string returnUrl)
        {
           // ViewBag.ReturnUrl = returnUrl;//geo-returnurl??
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Request request = db.MyRequest.Find(id);
            if (request == null)
            {
                return HttpNotFound();
            }
            TempData.Clear();
            TempData.Add("subDate", request.SubmitDate);
            TempData.Add("depDate", request.DepartureDate);
            TempData.Add("retDate", request.ReturnDate);
            TempData.Add("delID", request.DelegationId);
            TempData.Add("applicant", request.Applicant);
            TempData.Add("approver", request.Approver);
            TempData.Add("HR", request.HREmployee);
            TempData.Add("status", request.StatusId);

            ViewBag.DelegationId = new SelectList(db.MyDelegation, "DelegationId", "DelegationType");//, request.DelegationId);
            ViewBag.StatusId = new SelectList(db.MyStatus, "StatusId", "StatusName");//, request.StatusId);

            //ViewBag.TransCompId = new SelectList(db.TransportCompanies, "TransportCompanyId", "CompanyName");
            //ViewBag.DrvId = new SelectList(db.MyEmployee, "EmployeeId", "FullName");
            ViewBag.driver = new SelectList(db.MyEmployee.Where(x => x.Position.Equals("Driver")), "EmployeeId", "FullName");
            ViewBag.transcomp = new SelectList(db.TransportCompanies, "TransportCompanyId", "CompanyName");
            ViewBag.AllowanceId = new SelectList(db.Allowances, "AllowanceId", "Amount");
            
            ViewBag.Transports = db.MyTransport.ToList();

            return View(request);
        }
        // POST: /Request/EditHR/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditHR([Bind(Include = "RequestId,StatusId,TransportCompId,AllowanceId")] Request request)
        {
            EmployeeDAL ed = new EmployeeDAL();
            object o;
            bool b;
            int id = Convert.ToInt32(Session["tId"]);
            string s = ViewBag.dep;
            if (ModelState.IsValid)
            {
                using (var trans = db.Database.BeginTransaction())
                {
                    try
                    {
                        db.Entry(request).State = EntityState.Modified;
                        
                        b = TempData.TryGetValue("subDate", out o);
                        request.SubmitDate = (System.DateTime)o;
                        b = TempData.TryGetValue("depDate", out o);
                        request.DepartureDate = (System.DateTime)o;
                        b = TempData.TryGetValue("retDate", out o);
                        request.ReturnDate = (System.DateTime)o;
                        b = TempData.TryGetValue("delID", out o);
                        request.DelegationId = (Int32)o;

                        if (id != 0)//daca a fost creat un nou transport
                        {
                            request.TransportId = id;
                            request.Transport = db.MyTransport.Find(id);
                        }

                        db.SaveChanges();

                        b = TempData.TryGetValue("applicant", out o);
                        request.Applicant = (Employee)o;
                        b = TempData.TryGetValue("approver", out o);
                        request.Approver = (Employee)o;
                        b = TempData.TryGetValue("HR", out o);
                        request.HREmployee = (Employee)o;
                        
                        s = ed.getStatusNameById(request.StatusId);

                        //send email to applicant
                        string subject = "";
                        string body = "";
                        if (s.Equals("Closed"))
                        {
                            subject = "delegation request updated";
                            body = "transport details have been added, view here your request status http://localhost:5281/Request/IndexMyReq";
                            ed.sendEmail(request.Applicant, request.HREmployee, subject, body);
                        }

                        //send email to approver
                        if (s.Equals("Closed"))
                        {
                            subject = "delegation request updated";
                            body = "transport details have been added for employee" + request.Applicant.FullName;
                            ed.sendEmail(request.Approver, request.HREmployee, subject, body);
                        }
                        TempData.Clear();
                        trans.Commit();
                    }
                    catch(OptimisticConcurrencyException)
                    {
                        
                        //db.Refresh(RefreshMode.ClientWins, db.MyTransport);
                        db.SaveChanges();
                    }
                    catch (Exception e)
                    {
                        trans.Rollback();
                    }
                }

                return RedirectToAction("IndexHR");
            }
            ViewBag.DelegationId = new SelectList(db.MyDelegation, "DelegationId", "DelegationType", request.DelegationId);
            ViewBag.StatusId = new SelectList(db.MyStatus, "StatusId", "StatusName");
            ViewBag.TransCompId = new SelectList(db.TransportCompanies, "TransportCompanyId", "CompanyName");
            ViewBag.AllowanceId = new SelectList(db.Allowances, "AllowanceId", "Amount");


            return View(request);
        }

        // GET: /Request/EditSup/5
        public ActionResult EditSup(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Request request = db.MyRequest.Find(id);
            if (request == null)
            {
                return HttpNotFound();
            }
            TempData.Clear();
            TempData.Add("subDate", request.SubmitDate);
            TempData.Add("depDate", request.DepartureDate);
            TempData.Add("retDate", request.ReturnDate);
            TempData.Add("delID", request.DelegationId);
            TempData.Add("applicant", request.Applicant);
            TempData.Add("approver", request.Approver);
            TempData.Add("HR", request.HREmployee);
            TempData.Add("status", request.StatusId);
            TempData.Add("desc", request.Description);

            ViewBag.DepartureDate = new SelectList(db.MyRequest, "RequestId", "DepartureDate", request.DepartureDate);
            ViewBag.ReturnDate = new SelectList(db.MyRequest, "RequestId", "ReturnDate", request.ReturnDate);
            ViewBag.Description = new SelectList(db.MyRequest, "RequestId", "Description", request.Description);

            ViewBag.DelegationId = new SelectList(db.MyDelegation, "DelegationId", "DelegationType", request.DelegationId);
            ViewBag.StatusId = new SelectList(db.MyStatus, "StatusId", "StatusName", request.StatusId);

            return View(request);
        }

        // POST: /Request/EditSup/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditSup([Bind(Include="RequestId,DepartureDate,ReturnDate,StatusId,Motivation,Description")] Request request)
        {
            EmployeeDAL ed = new EmployeeDAL();
      
            if (ModelState.IsValid)
            {
                using (var trans = db.Database.BeginTransaction())
                {
                    try
                    {
                        
                        db.Entry(request).State = EntityState.Modified;
                        object o;
                        bool b;
                        b = TempData.TryGetValue("subDate", out o);
                        request.SubmitDate = (System.DateTime)o;
                        b = TempData.TryGetValue("depDate", out o);
                        request.DepartureDate = (System.DateTime)o;
                        b = TempData.TryGetValue("retDate", out o);
                        request.ReturnDate = (System.DateTime)o;
                        b = TempData.TryGetValue("delID", out o);
                        request.DelegationId = (Int32)o;
                        b = TempData.TryGetValue("desc", out o);
                        request.Description = (string)o;
                        
                        //b = TempData.TryGetValue("status", out o);
                        //request.StatusId = (Int32)o;

                        db.SaveChanges();
                        
                        b = TempData.TryGetValue("applicant", out o);
                        request.Applicant = (Employee)o;
                        b = TempData.TryGetValue("approver", out o);
                        request.Approver = (Employee)o;
                        b = TempData.TryGetValue("HR", out o);
                        request.HREmployee = (Employee)o;

                        //b = TempData.TryGetValue("status", out o);
                        //request.StatusId = (Int32)o;
                        string s = ed.getStatusNameById(request.StatusId);


                        //send email to applicant
                        string body = "";
                        string subject = "";
                        if (s.Equals("Approved"))
                        {
                            subject = "delegation request approved";
                            body = "your request has been approved, view here your request status http://localhost:5281/Request/IndexMyReq";
                        }
                        if (s.Equals("Denied"))
                        {
                            subject = "delegation request denied";
                            body = "your request has been denied, view here your request status http://localhost:5281/Request/IndexMyReq";
                        }
                        ed.sendEmail(request.Applicant, request.Approver, subject, body);

                        //send email to HR
                        if (s.Equals("Approved"))
                        {
                            subject = "delegation request approved";
                            body = "request has been approved, add transport details http://localhost:5281/Request/IndexHR";
                        }
                        if (s.Equals("Denied"))
                        {
                            subject = "delegation request denied";
                            body = "request has been denied";
                        }
                        ed.sendEmail(request.HREmployee, request.Approver, subject, body);

                        TempData.Clear();

                        trans.Commit();
                    }
                    catch (Exception e)
                    { 
                        trans.Rollback(); 
                    }
                }
           
                return RedirectToAction("IndexSup");
            }
            ViewBag.DepartureDate = new SelectList(db.MyRequest, "RequestId", "DepartureDate", request.DepartureDate);
            ViewBag.ReturnDate = new SelectList(db.MyRequest, "RequestId", "ReturnDate", request.ReturnDate);
            ViewBag.Description = new SelectList(db.MyRequest, "RequestId", "Description", request.Description);

            ViewBag.DelegationId = new SelectList(db.MyDelegation, "DelegationId", "DelegationType", request.DelegationId);
            ViewBag.StatusId = new SelectList(db.MyStatus, "StatusId", "StatusName", request.StatusId);
            return View(request);
        }

        // GET: /Request/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Request request = db.MyRequest.Find(id);
            if (request == null)
            {
                return HttpNotFound();
            }
            return View(request);
        }

        // POST: /Request/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Request request = db.MyRequest.Find(id);
            db.MyRequest.Remove(request);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
