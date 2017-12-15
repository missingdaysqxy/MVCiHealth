using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCiHealth.Models;

namespace MVCiHealth.Controllers
{
    public class DoctorsController : Controller
    {
        iHealthEntities db = new iHealthEntities();
        public ActionResult Index()
        {
            //var userid = Global.CurrentUserID;
            //var DOCTOR_ID=userid;
            var DOCTOR_ID = "123";//change
            var p = db.DOCTOR.Find(int.Parse(DOCTOR_ID));
            return View(p);
        }
        [ActionName("_Reservation")]
        public PartialViewResult Reservation()
        {
            return PartialView();
        }
        public ActionResult Setinfo()
        {

            //var userid = Global.CurrentUserID;
            //var DOCTOR_ID=userid;
            var DOCTOR_ID = "123";//change
            var p = db.DOCTOR.Find(int.Parse(DOCTOR_ID));
            return View(p);
        }


    }
}