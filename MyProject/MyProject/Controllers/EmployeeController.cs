using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyProject.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using MyProject.DAL;

namespace MyProject.Controllers
{
    public class EmployeeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: /Employee/
        public ActionResult Index()
        {
            return View(db.MyEmployee.ToList());
        }

        // GET: /Employee/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.MyEmployee.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // GET: /Employee/Create
        public ActionResult Create()
        {

            ViewBag.Sup = new SelectList(db.MyEmployee.Where(x => x.EndDate.Year == 1800)
                .Where(x => x.Position != "Driver"), "EmployeeId", "FullName");
            ViewBag.RoleId = new SelectList(db.Roles, "Id", "Name");
            ViewBag.Hq = new SelectList(db.MyAddress, "AddressId", "CompanyName");

         //   ViewBag.Rol = new SelectList(db.Roles, "Id", "Name");
    
            return View();
        }

        // POST: /Employee/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="EmployeeId,FirstName,MiddleName,LastName,EMail,EMailPassword,PersonalID,HireDate,Position,Department")] Employee employee, int? Sup, int? RoleId, int Hq)
        {
            if (ModelState.IsValid)
            {
                //ViewBag.Sup = new SelectList(db.MyEmployee, "EmployeeId", "FirstName");
                EmployeeDAL ed = new EmployeeDAL();
                        
                using (var trans = db.Database.BeginTransaction())
                {
                    try
                    {
                        //create employee
                        if (Sup != null)
                        {
                            int supId = Sup.Value;
                            Employee sup = ed.getEmployeeById(supId);// db.MyEmployee.Find(Sup);
                            employee.SuperiorEmployee = sup;
                        }
                        if(Hq != 0)
                        {
                            employee.Headquarter = ed.getAddressById(Hq);
                        }
                        employee.EndDate = new DateTime(1800, 1, 1);
                        //db.MyEmployee.Add(employee);
                        //db.SaveChanges();
                        ed.addEmployeeAndSaveChanges(employee);

                        ed.createUserAndAddToRole(employee);
                        /*
                        //create user for employee
                        var user = new ApplicationUser();
                        user.UserName = employee.FirstName + employee.LastName;
                        user.MyEmployee = employee;
                        var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
                        var adminresult = UserManager.Create(user, "password");
                        //db.SaveChanges();
                        ed.saveChanges();                       
                       
                        //Add User to Role 'Employee'
                        if (adminresult.Succeeded)
                        {
                            var result = UserManager.AddToRole(user.Id, "Employee");
                            //db.SaveChanges();
                            ed.saveChanges();
                        }*/
                        trans.Commit();
                    }
                    catch (Exception e)
                    {
                        trans.Rollback();
                    }
                }
                return RedirectToAction("Index");
            }

            return View(employee);
        }

        // GET: /Employee/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.MyEmployee.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }

            ViewBag.Headquarter_AddressId = new SelectList(db.MyAddress, "AddressId", "CompanyName");
            //ViewBag.Sup2 = new SelectList(db.MyEmployee, "EmployeeId", "FullName");
            

            employee.HireDate = DateTime.Now;
            return View(employee);
        }

        // POST: /Employee/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include=@"EmployeeId,FirstName,MiddleName,LastName,EMail,EMailPassword,
                    PersonalID,HireDate,Position,Department,EndDate")] 
                    Employee employee, int Headquarter_AddressId)//, int? Sup2)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employee).State = EntityState.Modified;

                var headq = db.MyAddress.Find(Headquarter_AddressId);
                employee.Headquarter = headq;
                employee.EndDate = new DateTime(1800,1,1);
                
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            
            return View(employee);
        }

        // GET: /Employee/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.MyEmployee.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: /Employee/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Employee employee = db.MyEmployee.Find(id);
            //db.MyEmployee.Remove(employee);
//            employee.EndDate = DateTime.Now;
//            db.SaveChanges();

            //reset password
            var userId = (from u in db.Users
                      where u.UserName.Equals(employee.FirstName + employee.LastName)
                      select u).First();//iau id-ul angajatului cu nume user = employee prenume+nume
            
          //  UserManager<IdentityUser> userManager = new UserManager<IdentityUser>(new UserStore<IdentityUser>());
/*            userManager.RemovePassword(userId.ToString());
            userManager.AddPassword(userId.ToString(), "parola123");
            */
            //geo reset pass admin
            UserManager<IdentityUser> userManager =
                new UserManager<IdentityUser>(new UserStore<IdentityUser>());
            
            var myuser = userManager.FindById(userId.Id.ToString());
            userManager.RemovePassword("587a1dce-7293-4464-8150-0cacba352055");
            userManager.AddPassword("587a1dce-7293-4464-8150-0cacba352055", "parola123");


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
