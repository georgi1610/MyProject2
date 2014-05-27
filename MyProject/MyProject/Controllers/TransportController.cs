﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyProject.Models;
using MyProject.DAL;
using System.IO;

namespace MyProject.Controllers
{
    public class TransportController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: /Transport/
        public ActionResult Index()
        {
            return View(db.MyTransport.ToList());
        }

        // GET: /Transport/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transport transport = db.MyTransport.Find(id);
            if (transport == null)
            {
                return HttpNotFound();
            }
            return View(transport);
        }

        // GET: /Transport/Create
        public ActionResult Create()
        {
            ViewBag.DepartureAddressId = new SelectList(db.MyAddress, "AddressId", "CompanyName");
            ViewBag.ArrivalAddressId = new SelectList(db.MyAddress, "AddressId", "CompanyName");
            ViewBag.DriverId = new SelectList(db.MyEmployee, "EmployeeId", "FullName");
            ViewBag.TransportCompId = new SelectList(db.TransportCompanies, "TransportCompanyId", "CompanyName");

            return View();
        }

        // POST: /Transport/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="TransportId,DepartureDateTime,DepartureTime,ArrivalDateTime,ArrivalTime,DepartureAddress,ArrivalAddress,DriverId,TransportCompId")] Transport transport)
        {
            if (ModelState.IsValid)
            {
                EmployeeDAL ed = new EmployeeDAL();
                /*
                var dep = ed.getAddressById(transport.DepartureAddressId); 
                if (dep != null)
                    transport.DepartureAddress = dep;

                var arr = ed.getAddressById(transport.ArrivalAddressId);
                if (arr != null)
                    transport.ArrivalAddress = arr;
                */
                var drv = ed.getEmployeeById(transport.DriverId);
                if (drv != null)
                    transport.Driver = drv;

                var trans = ed.getTransportCompanyById(transport.TransportCompId);
                if (trans != null)
                    transport.TransportComp = trans;

                ed.addTransportAndSaveChanges(transport);
                //db.MyTransport.Add(transport);
                //db.SaveChanges();
                //return RedirectToAction("../Request/EditHR/59");//sa duca inapoi la ../Request/EditHR/59????

                string path = Path.Combine (AppDomain.CurrentDomain.BaseDirectory, "Images");
                string photoPath = null;
                HttpPostedFileBase file = null;
                if (Request.Files.Count > 0)
                {// preluam fisierul din lista de fisiere
                    file = Request.Files[0];
                    // construim path relativ la poza
                    photoPath = Path.Combine(path, file.FileName);
                    // generam numele fisierului de pe disk unde vom salva poza
                    string filePath = transport.TransportId + Path.GetExtension(file.FileName);//path - ul care va fi folosit pentru afisarea imaginii
                    //c:\...\images\1.jpg
                    string newPhotoPath = Path.Combine(path, transport.TransportId + Path.GetExtension(file.FileName));
                    //salvam fisierul in images
                    file.SaveAs(newPhotoPath);
                    //salvam calea catre poza in baza de date
                    transport.PlaneTicketPath = filePath;
                    //salvam modificarile
                    ed.saveChanges();
                }
                return RedirectToAction("Index");
            }

            ViewBag.DepartureAddressId = new SelectList(db.MyAddress, "AddressId", "DepartureAddress", transport.DepartureAddress);
            ViewBag.ArrivalAddressId = new SelectList(db.MyAddress, "AddressId", "ArrivalAddress", transport.ArrivalAddress);
            ViewBag.DriverId = new SelectList(db.MyEmployee, "EmployeeId", "FullName", transport.DriverId);
            ViewBag.TransportCompId = new SelectList(db.TransportCompanies, "TransportCompanyId", "CompanyName",transport.TransportCompId);

            
            return View(transport);
        }

        // GET: /Transport/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transport transport = db.MyTransport.Find(id);
            if (transport == null)
            {
                return HttpNotFound();
            }

            ViewBag.DriverId = new SelectList(db.MyEmployee, "EmployeeId", "FullName");
            ViewBag.TransportCompId = new SelectList(db.TransportCompanies, "TransportCompanyId", "CompanyName");

            return View(transport);
        }

        // POST: /Transport/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = @"TransportId,DepartureDateTime,DepartureTime,ArrivalDateTime,
            ArrivalTime,DepartureAddress,ArrivalAddress,DriverId,TransportCompId")] Transport transport)
        {
            if (ModelState.IsValid)
            {
                using (var trans = db.Database.BeginTransaction())
                {
                    try
                    {

                        db.Entry(transport).State = EntityState.Modified;

                        //driver
                        //transcomp
                        EmployeeDAL ed = new EmployeeDAL();

                        var drv = ed.getEmployeeById(transport.DriverId);
                        if (drv != null)
                            transport.Driver = drv;

                        var tr = ed.getTransportCompanyById(transport.TransportCompId);
                        if (tr != null)
                            transport.TransportComp = tr;
                        
                        ed.saveChanges();
                        //db.SaveChanges();
                        trans.Commit();
                    }
                    catch (Exception e)
                    {
                        trans.Rollback();
                    }
                }
                return RedirectToAction("Index");
            }

            ViewBag.DriverId = new SelectList(db.MyEmployee, "EmployeeId", "FullName", transport.Driver);
            ViewBag.TransportCompId = new SelectList(db.TransportCompanies, "TransportCompanyId", "CompanyName", transport.TransportComp);

            return View(transport);
        }

        // GET: /Transport/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transport transport = db.MyTransport.Find(id);
            if (transport == null)
            {
                return HttpNotFound();
            }
            return View(transport);
        }

        // POST: /Transport/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Transport transport = db.MyTransport.Find(id);
            db.MyTransport.Remove(transport);
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