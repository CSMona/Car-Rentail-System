using CarRentalApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarRentalApp.Controllers
{
    public class RentailController : Controller
    {
        SuperCarEntities db = new SuperCarEntities();
        // GET: Rentail
        public ActionResult Index()
        {
            var result = (from r in db.rentails
                          join c in db.CarRegs on r.carid equals c.carno
                          select new RentailViewModel
                          {
                              id=r.id,
                              carid=r.carid,
                              custid=r.custid,
                              fee=r.fee,
                              sdate=r.sdate,
                              edate=r.edate,
                              available=c.available 

                          }).ToList();
            return View(result);
        }
        [HttpGet]
        public ActionResult Getcar()
        {
            var car = db.CarRegs.ToList();
            return Json(car, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Getid(int id)
        {
            var customer = (from s in db.Customers where s.id == id select s.custname).ToList();
            return Json(customer, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Getavil(string carno)
        {
            var caravil = (from s in db.CarRegs where s.carno == carno select s.available).FirstOrDefault();
          
            return Json(caravil, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Save(rentail rent)
        {
            if(ModelState.IsValid)
            {
                db.rentails.Add(rent);
                var car = db.CarRegs.SingleOrDefault(e => e.carno == rent.carid);
                if (car == null)
                {
                    return HttpNotFound("CarNo not valid");
                }
                car.available = "no";
                db.Entry(car).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(rent);
        }





    }
}