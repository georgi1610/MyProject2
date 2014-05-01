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
            
            ViewBag.Sup = new SelectList(db.MyEmployee, "EmployeeId", "FirstName");
            ViewBag.RoleId = new SelectList(db.Roles, "Id", "Name");
            
         //   ViewBag.Rol = new SelectList(db.Roles, "Id", "Name");
    
            return View();
        }

        // POST: /Employee/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="EmployeeId,FirstName,MiddleName,LastName,EMail,PersonalID,HireDate,Position,Department")] Employee employee, int? Sup, int? RoleId)
        {
            if (ModelState.IsValid)
            {
//                db.MyEmployee.Add(employee);
//                db.SaveChanges();

                ViewBag.Sup = new SelectList(db.MyEmployee, "EmployeeId", "FirstName");
              //  ViewBag.RoleId = new SelectList(db.Roles, "Id", "Name");
            
                //var role = ViewBag.Rol;
                
                using (var trans = db.Database.BeginTransaction())
                {
                    try
                    {
                        //create employee
                        if (Sup != null)
                        {
                            Employee sup = db.MyEmployee.Find(Sup);
                            employee.SuperiorEmployee = sup;
                        }
                        db.MyEmployee.Add(employee);
                        db.SaveChanges();

                        //create user for employee
                        var user = new ApplicationUser();
                        user.UserName = employee.FirstName + employee.LastName;
                        user.MyEmployee = employee;
                        var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
                        var adminresult = UserManager.Create(user, "password");
                        db.SaveChanges();
                                               
                       
                        //Add User to Role 'Employee'
                        if (adminresult.Succeeded)
                        {
                            
                            //var result = UserManager.AddToRole(user.Id, "Employee");
                            //ViewBag.RoleId = new SelectList(db.Roles, "Id", "Name");
                            //var r = db.Roles.Find(RoleId).Name;
                
                            var result = UserManager.AddToRole(user.Id, "Employee");
                            
                            db.SaveChanges();
                        }
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
            return View(employee);
        }

        // POST: /Employee/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="EmployeeId,FirstName,MiddleName,LastName,PersonalID,HireDate,Position,Department")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employee).State = EntityState.Modified;
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
            db.MyEmployee.Remove(employee);
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
