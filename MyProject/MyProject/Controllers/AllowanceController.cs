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
    public class AllowanceController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: /Allowance/
        public ActionResult Index()
        {
            return View(db.Allowances.ToList());
        }

        // GET: /Allowance/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Allowance allowance = db.Allowances.Find(id);
            if (allowance == null)
            {
                return HttpNotFound();
            }
            return View(allowance);
        }

        // GET: /Allowance/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Allowance/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="AllowanceId,Type,Amount,Currency")] Allowance allowance)
        {
            if (ModelState.IsValid)
            {
                db.Allowances.Add(allowance);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(allowance);
        }

        // GET: /Allowance/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Allowance allowance = db.Allowances.Find(id);
            if (allowance == null)
            {
                return HttpNotFound();
            }
            return View(allowance);
        }

        // POST: /Allowance/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="AllowanceId,Type,Amount,Currency")] Allowance allowance)
        {
            if (ModelState.IsValid)
            {
                db.Entry(allowance).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(allowance);
        }

        // GET: /Allowance/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Allowance allowance = db.Allowances.Find(id);
            if (allowance == null)
            {
                return HttpNotFound();
            }
            return View(allowance);
        }

        // POST: /Allowance/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Allowance allowance = db.Allowances.Find(id);
            db.Allowances.Remove(allowance);
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
