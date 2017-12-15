using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Web.Mvc;
using MVCiHealth.Models;

namespace MVCiHealth.Controllers
{
    public class EvaluateController : Controller
    {
        iHealthEntities db = new iHealthEntities();
        // GET: Evaluate
        public ActionResult editEvaluate(int? doctor_id)
        {
            //var d = new DOCTOR()
            //{
            //    DOCTOR_NM = "ADOCTOR"
            //};
            if (doctor_id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var d = db.DOCTOR.Find(doctor_id);
            return View(d);
        }

        [HttpPost]
        public ActionResult editEvaluate(DOCTOR_EVALUATION e)
        {
            
            return
        }
    }
}