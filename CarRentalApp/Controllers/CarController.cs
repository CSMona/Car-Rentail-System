using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CarRentalApp.Models;

namespace CarRentalApp.Controllers
{
    public class CarController : Controller
    {
        private SuperCarEntities db = new SuperCarEntities();

        // GET: Car
        public ActionResult Index()
        {
            return View(db.CarRegs.ToList());
        }

        // GET: Car/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CarReg carReg = db.CarRegs.Find(id);
            if (carReg == null)
            {
                return HttpNotFound();
            }
            return View(carReg);
        }

        // GET: Car/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Car/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,carno,make,model,available")] CarReg carReg)
        {
            if (ModelState.IsValid)
            {
                db.CarRegs.Add(carReg);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(carReg);
        }

        // GET: Car/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CarReg carReg = db.CarRegs.Find(id);
            if (carReg == null)
            {
                return HttpNotFound();
            }
            return View(carReg);
        }

        // POST: Car/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,carno,make,model,available")] CarReg carReg)
        {
            if (ModelState.IsValid)
            {
                db.Entry(carReg).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(carReg);
        }

        // GET: Car/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CarReg carReg = db.CarRegs.Find(id);
            if (carReg == null)
            {
                return HttpNotFound();
            }
            return View(carReg);
        }

        // POST: Car/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CarReg carReg = db.CarRegs.Find(id);
            db.CarRegs.Remove(carReg);
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
