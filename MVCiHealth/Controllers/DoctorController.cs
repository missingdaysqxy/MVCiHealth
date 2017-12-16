using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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
            var DOCTOR_ID = Global.CurrentUserID;//change
            var p = db.DOCTOR.Find(DOCTOR_ID);
            return View(p);
        }
        [ActionName("_Reservation")]
        public PartialViewResult Reservation()
        {
            return PartialView();
        }
        public ActionResult Edit()
        {

            //var userid = Global.CurrentUserID;
            //var DOCTOR_ID=userid;
            var DOCTOR_ID = Global.CurrentUserID;//change
            var p = db.DOCTOR.Find(DOCTOR_ID);
            return View(p);
        }


    }
}