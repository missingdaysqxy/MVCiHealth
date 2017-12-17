using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using MVCiHealth.Models;

namespace MVCiHealth.Controllers
{
    public class DoctorController : Controller
    {
        iHealthEntities db = new iHealthEntities();
        public ActionResult Index()
        {

            //var userid = Global.CurrentUserID;
            //var DOCTOR_ID=userid;
            var DOCTOR_ID = "123";//change
            ViewBag.DoctorName = db.DOCTOR.Find(int.Parse(DOCTOR_ID)).DOCTOR_NM;
            
            //return View(list);
            return View();
        }
        [ActionName("_Reservation")]
        public PartialViewResult Reservation()
        {
            var list = db.V_RESERVATION.Take(5).ToList();
            //return View(list);
            return PartialView(list);
        }
        public ActionResult Edit()
        {

            //var userid = Global.CurrentUserID;
            //var DOCTOR_ID=userid;
            var DOCTOR_ID = "123";//change
            var p = db.DOCTOR.Find(int.Parse(DOCTOR_ID));
            return View(p);
        }
        
        [HttpPost]
        public ActionResult Edit( DOCTOR DOCTOR)
        {

            if (ModelState.IsValid)
            {
                /*
                db.Entry(DOCTOR).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("index");
                */
                //var userid = global.currentuserid;
                //var doctor_id=userid;
                var DOCTOR_ID = "123";//change
                var p = db.DOCTOR.Find(int.Parse(DOCTOR_ID));
                p.DOCTOR_NM=DOCTOR.DOCTOR_NM;
                p.GENDER = DOCTOR.GENDER;
                p.AGE = DOCTOR.AGE;
                p.TEL = DOCTOR.TEL;
                p.SECTION_ID = DOCTOR.SECTION_ID;
                p.DISEASE_ID = DOCTOR.DISEASE_ID;
                p.INTRODUCTION = DOCTOR.INTRODUCTION;
                p.PHOTO_URL = DOCTOR.PHOTO_URL;
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

    }
}