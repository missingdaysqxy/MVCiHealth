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
        // GET: editEvaluation
        public ActionResult EditEvaluation(int? doctor_id,int? reservation_id)
        {
            if (doctor_id == null||reservation_id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var doctor = db.DOCTOR.Find(doctor_id);
            if(doctor == null)
            {
                return HttpNotFound();
            }
            var v_EVALUATION = new V_EVALUATION();
            if (reservation_id != null)
            {
                v_EVALUATION.DOCTOR_NM = doctor.DOCTOR_NM;
                v_EVALUATION.DOCTOR_ID = doctor.DOCTOR_ID;
                v_EVALUATION.LEVEL = doctor.LEVEL;
                v_EVALUATION.RATE = 0;
                v_EVALUATION.RESERVATION_ID = (int)reservation_id;
            }           
            return View(v_EVALUATION);
        }

        //POST: editEvaluation
        [HttpPost]
        public ActionResult EditEvaluation(V_EVALUATION v_EVALUATION)
        {
            if(v_EVALUATION == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var dOCTOR_EVALUATION = new DOCTOR_EVALUATION()
            {
                EVALUATION_ID = Global.NextEvaluationID(),
                RESERVATION_ID = v_EVALUATION.RESERVATION_ID,
                DOCTOR_ID = v_EVALUATION.DOCTOR_ID,
                PATIENT_ID = Global.CurrentUserID,
                RATE = v_EVALUATION.RATE,
                DETAIL = v_EVALUATION.DETAIL,
                AGREETIMES = 0,
                INSDATE = DateTime.Now
            };
            db.DOCTOR_EVALUATION.Add(dOCTOR_EVALUATION);
            db.SaveChanges();
            var doctor = db.DOCTOR.Find(v_EVALUATION.DOCTOR_ID);
            if(doctor == null)
            {
                return HttpNotFound();
            }
            var list = db.DOCTOR_EVALUATION.Where(m => m.DOCTOR_ID == v_EVALUATION.DOCTOR_ID).ToList();
            if (list != null)
            {
                doctor.LEVEL = (doctor.LEVEL + v_EVALUATION.RATE) / list.Count();
            }
            else
            {
                doctor.LEVEL = v_EVALUATION.RATE;
            }
            var state = db.Entry(doctor);
            state.State = System.Data.Entity.EntityState.Unchanged;
            state.Property(m => m.LEVEL).IsModified = true;
            db.SaveChanges();
            //TODO 
            //修改之后页面跳转至原页面（患者评价）
            return this.RedirectTo("MedicalRecords","PatientController");
        }

        //GET: detailEvaluation_doctor
        public ActionResult DetailEvaluation_Doctor(int? doctor_id)
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

        //POST: detailEvaluation_doctor
        [HttpPost]
        public ActionResult DetailEvaluation_Doctor()
        {
            //TODO 
            //修改之后页面跳转至原页面（医生界面）
            return View();
        }

        //GET: detailEvaluate_patientOne
        public ActionResult DetailEvaluation_PatientOne(int? reservation_id)
        {
            if (reservation_id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var doctor_evaluate = db.DOCTOR_EVALUATION.Find(reservation_id);
            if (doctor_evaluate == null)
            {
                return HttpNotFound();
            }
            var doctor = db.DOCTOR.Find(doctor_evaluate.DOCTOR_ID);
            ViewBag.DOCTOR_NM = doctor.DOCTOR_NM;
            ViewBag.LEVEL = doctor.LEVEL;
            return View(doctor_evaluate);
        }

        //POST: detailEvaluationOne
        [HttpPost]
        public ActionResult DetailEvaluation_PatientOne()
        {
            //TODO 
            //修改之后页面跳转至原页面（病人界面）
            return View();
        }

        //GET: detailEvaluation_patientAll
        public ActionResult DetailEvaluation_PatientAll(int? doctor_id)
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
            var doctor = db.DOCTOR.Find(doctor_id);
            ViewBag.DOCTOR_NM = doctor.DOCTOR_NM;
            ViewBag.LEVEL = doctor.LEVEL;
            return View(doctor_evaluate);
        }

        //POST: detailEvaluationAll
        [HttpPost]
        public ActionResult DetailEvaluation_PatientAll(DOCTOR_EVALUATION e)
        {
            if(e == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            e.AGREETIMES += 1;
            var state = db.Entry(e);
            state.State = System.Data.Entity.EntityState.Unchanged;
            state.Property(m => m.AGREETIMES).IsModified = true;
            db.SaveChanges();
            e = db.DOCTOR_EVALUATION.Find(e.EVALUATION_ID);
            return View(e);
        }
    }
}