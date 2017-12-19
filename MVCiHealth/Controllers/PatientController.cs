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
            //var p = new PATIENT()
            //{
            //    PATIENT_NM = "TestUser 001",
            //    BIRTH = DateTime.Parse("2014-02-28"),
            //    GENDER = "男",
            //    TEL = "021-62233333",
            //    TEL2 = "13823333333",
            //    EMAIL = "administrator@cs.ecnu.edu.cn",
            //    ADDRESS = "上海市普陀区中山北路3663号",
            //    BLOOD_TYPE = "O",
            //    ALLERGIC_HISTORY = "无",
            //    GENETIC_HISTORY = "无",
            //    CAPITAL_OPERATION = "无",
            //    EMERGENCY_NAME = "Tony",
            //    EMERGENCY_TEL = "13852333333",
            //    COMMENT = "呵呵"
            //};
            int userid = Global.CurrentUserID;
            //var userid = 1;
            if (userid == -1)
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

        public ActionResult Edit()
        {
            int userid = Global.CurrentUserID;
            //var userid = 1;
            if (userid == -1)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PATIENT p = db.PATIENT.Find(userid);
            if (p==null)
            {
                return HttpNotFound();
            }
            return View(p);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(
            [Bind(Include ="PATIENT_ID,INSDATE,PATIENT_NM,AGE,GENDER,TEL,TEL2,EMAIL,ADDRESS,ALLERGIC_HISTORY,GENETIC_HISTORY,CAPITAL_OPERATION,EMERGENCY_NAME,EMERGENCY_TEL,COMMENT")]
            PATIENT info)
        {
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
  
            //var userid = 1;
            //var p = db.PATIENT.Find(userid);
            var p = db.PATIENT.Find(id);
            return View(p);
        }

        public ActionResult MedicalRecords()
        {

            return View(db.V_RESERVATION.ToList());
        }
    }
}