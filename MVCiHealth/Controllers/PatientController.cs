using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCiHealth.Models;

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
            var userid = Global.CurrentUserID;
            var p = db.PATIENT.Find(int.Parse(userid));
            return View(p);
        }

        public ActionResult Edit()
        {
            //var p = new PATIENT()
            //{
            //    PATIENT_NM = "TestUser",
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
            var userid = Global.CurrentUserID;
            var p = db.PATIENT.Find(int.Parse(userid));
            return View(p);
        }

        [HttpPost]
        public ActionResult Edit(PATIENT p)
        {
            db.Entry(p).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return View(p);
        }

        public ActionResult Detail()
        {
            //var p = new PATIENT()
            //{
            //    PATIENT_NM = "TestUser",
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
            var userid = Global.CurrentUserID;
            var p = db.PATIENT.Find(int.Parse(userid));
            return View(p);
        }
    }
}