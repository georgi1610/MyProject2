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
    public class Request2Controller : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: /Request2/
        public ActionResult Index()
        {
            var myrequest = db.MyRequest.Include(r => r.Delegation).Include(r => r.Status);
            return View(myrequest.ToList());
        }

        // GET: /Request2/Details/5
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
            return View(request);
        }

        // GET: /Request2/Create
        public ActionResult Create()
        {
            ViewBag.DelegationId = new SelectList(db.MyDelegation, "DelegationId", "DelegationType");
            ViewBag.StatusId = new SelectList(db.MyStatus, "StatusId", "StatusName");
            return View();
        }

        // POST: /Request2/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="RequestId,SubmitDate,DepartureDate,ReturnDate,DelegationId,StatusId,Description,DepartureAddressId,ReturnAddressId")] Request request)
        {
            if (ModelState.IsValid)
            {
                db.MyRequest.Add(request);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DelegationId = new SelectList(db.MyDelegation, "DelegationId", "DelegationType", request.DelegationId);
            ViewBag.StatusId = new SelectList(db.MyStatus, "StatusId", "StatusName", request.StatusId);
            return View(request);
        }

        // GET: /Request2/Edit/5
        public ActionResult Edit(int? id)
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

        // POST: /Request2/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="RequestId,SubmitDate,DepartureDate,ReturnDate,DelegationId,StatusId,Description,DepartureAddressId,ReturnAddressId")] Request request)
        {
            if (ModelState.IsValid)
            {
                db.Entry(request).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DelegationId = new SelectList(db.MyDelegation, "DelegationId", "DelegationType", request.DelegationId);
            ViewBag.StatusId = new SelectList(db.MyStatus, "StatusId", "StatusName", request.StatusId);
            return View(request);
        }

        // GET: /Request2/Delete/5
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

        // POST: /Request2/Delete/5
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
