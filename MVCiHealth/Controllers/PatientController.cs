using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCiHealth.Models;
using System.Net;
using System.Data.Entity;

namespace MVCiHealth.Controllers
{
    public class PatientController : Controller
    {
        iHealthEntities db = new iHealthEntities();
        // GET: Patient

        public ActionResult Index()
        {
            int userid = Global.CurrentUserID;
            if (userid < 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var list = db.V_RESERVATION.Where(m => m.PATIENT_ID == userid).ToList();
            return View(list);
        }

        public ActionResult Edit()
        {
            int userid = Global.CurrentUserID;
            if (userid < 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PATIENT p = db.PATIENT.Find(userid);
            if (p == null)
            {
                return HttpNotFound();
            }
            return View(p);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Edit(
            [Bind(Include ="PATIENT_ID,INSDATE,PATIENT_NM,AGE,GENDER,TEL,TEL2,EMAIL,ADDRESS,ALLERGIC_HISTORY,GENETIC_HISTORY,CAPITAL_OPERATION,EMERGENCY_NAME,EMERGENCY_TEL,COMMENT")]
            PATIENT info)
        {
            if (string.IsNullOrEmpty(info.PATIENT_NM))
            {
                ModelState.AddModelError("PATIENT_NM", "姓名不能为空");
                return View(info);
            }
            if (info.AGE == null)
            {
                ModelState.AddModelError("AGE", "年龄不能为空");
                return View(info);
            }
            if (string.IsNullOrEmpty(info.TEL) || string.IsNullOrEmpty(info.TEL2))
            {
                ModelState.AddModelError("TEL", "联系方式不能为空");
                return View(info);
            }
            var p = new PATIENT()
            {
                PATIENT_ID = info.PATIENT_ID,
                PATIENT_NM = info.PATIENT_NM,
                AGE = info.AGE,
                GENDER = info.GENDER,
                TEL = info.TEL,
                TEL2 = info.TEL2,
                EMAIL = info.EMAIL,
                ADDRESS = info.ADDRESS,
                ALLERGIC_HISTORY = info.ALLERGIC_HISTORY,
                GENETIC_HISTORY = info.GENETIC_HISTORY,
                CAPITAL_OPERATION = info.CAPITAL_OPERATION,
                EMERGENCY_NAME = info.EMERGENCY_NAME,
                EMERGENCY_TEL = info.EMERGENCY_TEL,
                COMMENT = info.COMMENT,
                INSDATE = DateTime.Now
            };
            if (ModelState.IsValid)
            {
                db.Entry(p).State = EntityState.Modified;
                db.Entry(p).Property(m => m.INSDATE).IsModified = false;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(p);
        }

        public ActionResult Detail(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PATIENT p = db.PATIENT.Find(id);
            if (p == null)
            {
                return HttpNotFound();
            }
            return View(p);
        }

        public ActionResult PersonalInfo()
        {
            int userid = Global.CurrentUserID;
            if (userid < 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PATIENT p = db.PATIENT.Find(userid);
            if (p == null)
            {
                return HttpNotFound();
            }
            return View(p);
        }

    }
}