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
        public ActionResult EditEvaluate(int? doctor_id)
        {
            if (doctor_id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var doctor = db.DOCTOR.Find(doctor_id);
            return View(doctor);
        }

        //POST: editEvaluate
        [HttpPost]
        public ActionResult EditEvaluate(DOCTOR_EVALUATION d_e)
        {
            if(d_e == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            db.DOCTOR_EVALUATION.Add(d_e);
            db.SaveChanges();
            var doctor = db.DOCTOR.Find(d_e.DOCTOR_ID);
            if(doctor == null)
            {
                return HttpNotFound();
            }
            var list = db.DOCTOR_EVALUATION.Where(m => m.DOCTOR_ID == d_e.DOCTOR_ID).ToList();
            if (list != null)
            {
                doctor.LEVEL = (doctor.LEVEL + d_e.RATE) / list.Count();
            }
            else
            {
                doctor.LEVEL = d_e.RATE;
            }
            var state = db.Entry(doctor);
            state.State = System.Data.Entity.EntityState.Unchanged;
            state.Property(m => m.LEVEL).IsModified = true;
            db.SaveChanges();
            //TODO 
            //修改之后页面跳转至原页面（患者评价）
            return View(d_e);
        }

        //GET: detailEvaluate_doctor
        public ActionResult DetailEvaluate_Doctor(int? doctor_id)
        {
            if(doctor_id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var doctor_evaluateslist = db.V_EVALUATION.Where(m=> m.DOCTOR_ID == doctor_id).ToList();
            if (doctor_evaluateslist == null)
            {
                return HttpNotFound();
            }
            return View(doctor_evaluateslist);
        }

        //POST: detailEvaluate_doctor
        [HttpPost]
        public ActionResult DetailEvaluate_Doctor()
        {
            //TODO 
            //修改之后页面跳转至原页面（医生界面评价）
            return View();
        }

        //GET: detailEvaluate_patientOne
        public ActionResult DetailEvaluate_PatientOne(int? doctor_id)
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

        //POST: detailEvaluateOne
        [HttpPost]
        public ActionResult DetailEvaluate_PatientOne(DOCTOR_EVALUATION e)
        {
            var d = db.DOCTOR.Find(e.DOCTOR_ID);
            ViewBag.DOCTOR_NM = d.DOCTOR_NM;
            ViewBag.LEVEL = d.LEVEL;

            return View();
        }
    }
}