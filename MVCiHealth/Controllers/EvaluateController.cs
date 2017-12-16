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
        private iHealthEntities db = new iHealthEntities();
        // GET: editEvaluate
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
            var doctor = db.DOCTOR.Find(doctor_id);
            return View(doctor);
        }

        [HttpPost]
        public ActionResult editEvaluate(DOCTOR_EVALUATION e)
        {

            return View();
        }

        //GET: detailEvaluate
        public ActionResult detailEvaluate_Doctor(int? doctor_id)
        {
            if(doctor_id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var doctor_evaluate = db.DOCTOR_EVALUATION.Where(m=> m.DOCTOR_ID == doctor_id).ToList();
            if (doctor_evaluate == null)
            {
                return HttpNotFound();
            }
            return View(doctor_evaluate);
        }

        [HttpPost]
        public ActionResult detailEvaluate_Doctor()
        {
            
            return View();
        }

        //GET: detailEvaluate
        public ActionResult detailEvaluate_Patient(int? doctor_id)
        {
            if (doctor_id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var doctor_evaluate = db.DOCTOR_EVALUATION.Where(m => m.DOCTOR_ID == doctor_id).ToList();
            if (doctor_evaluate == null)
            {
                return HttpNotFound();
            }
            var d = db.DOCTOR.Find(doctor_id);
            ViewBag.DOCTOR_NM = d.DOCTOR_NM;
            ViewBag.LEVEL = d.LEVEL;
            return View(doctor_evaluate);
        }

        [HttpPost]
        public ActionResult detailEvaluate_Patient(DOCTOR_EVALUATION e)
        {
            var d = db.DOCTOR.Find(e.DOCTOR_ID);
            ViewBag.DOCTOR_NM = d.DOCTOR_NM;
            ViewBag.LEVEL = d.LEVEL;

            return View();
        }
    }
}