using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyProject.Models;

namespace MyProject.Controllers
{
    public class DelegationController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: /Delegation/
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            return View(db.MyDelegation.ToList());
        }

        // GET: /Delegation/Details/5
        [Authorize(Roles = "Admin")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Delegation delegation = db.MyDelegation.Find(id);
            if (delegation == null)
            {
                return HttpNotFound();
            }
            return View(delegation);
        }

        // GET: /Delegation/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Delegation/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Create([Bind(Include="DelegationId,DelegationType")] Delegation delegation)
        {
            if (ModelState.IsValid)
            {
                db.MyDelegation.Add(delegation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(delegation);
        }

        // GET: /Delegation/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Delegation delegation = db.MyDelegation.Find(id);
            if (delegation == null)
            {
                return HttpNotFound();
            }
            return View(delegation);
        }

        // POST: /Delegation/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit([Bind(Include="DelegationId,DelegationType")] Delegation delegation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(delegation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(delegation);
        }

        // GET: /Delegation/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Delegation delegation = db.MyDelegation.Find(id);
            if (delegation == null)
            {
                return HttpNotFound();
            }
            return View(delegation);
        }

        // POST: /Delegation/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            Delegation delegation = db.MyDelegation.Find(id);
            db.MyDelegation.Remove(delegation);
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
