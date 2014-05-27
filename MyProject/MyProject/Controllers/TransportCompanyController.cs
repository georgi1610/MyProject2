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
    public class TransportCompanyController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: /TransportCompany/
        public ActionResult Index()
        {
            return View(db.TransportCompanies.ToList());
        }

        // GET: /TransportCompany/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TransportCompany transportcompany = db.TransportCompanies.Find(id);
            if (transportcompany == null)
            {
                return HttpNotFound();
            }
            return View(transportcompany);
        }

        // GET: /TransportCompany/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /TransportCompany/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="TransportCompanyId,CompanyName")] TransportCompany transportcompany)
        {
            if (ModelState.IsValid)
            {
                db.TransportCompanies.Add(transportcompany);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(transportcompany);
        }

        // GET: /TransportCompany/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TransportCompany transportcompany = db.TransportCompanies.Find(id);
            if (transportcompany == null)
            {
                return HttpNotFound();
            }
            return View(transportcompany);
        }

        // POST: /TransportCompany/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="TransportCompanyId,CompanyName")] TransportCompany transportcompany)
        {
            if (ModelState.IsValid)
            {
                db.Entry(transportcompany).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(transportcompany);
        }

        // GET: /TransportCompany/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TransportCompany transportcompany = db.TransportCompanies.Find(id);
            if (transportcompany == null)
            {
                return HttpNotFound();
            }
            return View(transportcompany);
        }

        // POST: /TransportCompany/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TransportCompany transportcompany = db.TransportCompanies.Find(id);
            db.TransportCompanies.Remove(transportcompany);
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
