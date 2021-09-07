using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CarRentalApp.Models;
using System.Data.Entity.SqlServer;
using System.Data.Entity;

namespace CarRentalApp.Controllers
{
    public class ReturnCarController : Controller
    {

        SuperCarEntities db = new SuperCarEntities();
        // GET: ReturnCar
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Save(returncar recar) 
        {
            if (ModelState.IsValid)
            {
                db.returncars.Add(recar);
                var car = db.CarRegs.SingleOrDefault(e => e.carno == recar.carno);
                if (car == null)
                {
                    return HttpNotFound("CarNo not found");
                }

                car.available = "yes";
                db.Entry(car).State =EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(recar);
        }







        [HttpPost]
        public ActionResult Getid(string carno)
        {
            var carn = (from s in db.rentails where s.carid == carno
                        select new
                        {
                            StartDate=s.sdate,
                            EndDate=s.edate,
                            Custid=s.custid,
                            CarNo=s.carid,
                            Fee=s.fee,
                            ElapseDays = SqlFunctions.DateDiff("day",s.edate,DateTime.Now)

                        }).ToArray();



            return Json(carn,JsonRequestBehavior.AllowGet);
        }
    }
}