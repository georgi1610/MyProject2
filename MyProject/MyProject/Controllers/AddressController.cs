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
    public class AddressController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: /Address/
        public ActionResult Index()
        {
            return View(db.MyAddress.ToList());
        }

        // GET: /Address/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Address address = db.MyAddress.Find(id);
            if (address == null)
            {
                return HttpNotFound();
            }
            return View(address);
        }

        // GET: /Address/Create
        public ActionResult Create()
        {
            ViewBag.CountryId = new SelectList(db.MyCountry, "CountryId", "CountryName");
            
            return View();
        }

        // POST: /Address/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="AddressId,CompanyName,StreetName,Number,PostalCode,CountryId")] Address address)
        {
            if (ModelState.IsValid)
            {
                db.MyAddress.Add(address);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CountryId = new SelectList(db.MyCountry, "CountryId", "CountryName", address.CountryId);

            return View(address);
        }

        // GET: /Address/Edit/5
        public ActionResult Edit(int? id)
        {
            ViewBag.CountryId = new SelectList(db.MyCountry, "CountryId", "CountryName");
            
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Address address = db.MyAddress.Find(id);
            if (address == null)
            {
                return HttpNotFound();
            }
            return View(address);
        }

        // POST: /Address/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="AddressId,CompanyName,StreetName,Number,PostalCode,CountryId")] Address address)
        {
            if (ModelState.IsValid)
            {
                db.Entry(address).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CountryId = new SelectList(db.MyCountry, "CountryId", "CountryName", address.CountryId);

            return View(address);
        }

        // GET: /Address/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Address address = db.MyAddress.Find(id);
            if (address == null)
            {
                return HttpNotFound();
            }
            return View(address);
        }

        // POST: /Address/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Address address = db.MyAddress.Find(id);
            db.MyAddress.Remove(address);
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
