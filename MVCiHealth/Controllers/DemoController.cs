using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVCiHealth.Models;

namespace MVCiHealth.Controllers
{
    public class DemoController : Controller
    {
        private iHealthEntities db = new iHealthEntities();
        

        // GET: Demo
        public ActionResult Index()
        {
            return View(db.V_DOCTORINFO.Take(5).ToList());
        }

        // GET: Demo/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            V_DOCTORINFO v_DOCTORINFO = db.V_DOCTORINFO.Find(id);
            if (v_DOCTORINFO == null)
            {
                return HttpNotFound();
            }
            return View(v_DOCTORINFO);
        }

        // GET: Demo/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Demo/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DOCTOR_ID,DOCTOR_NM,GENDER,AGE,TEL,PHOTO_URL,SECTION_ID,DISEASE_ID,INTRODUCTION,INSDATE,PATIENT_ID,PATIENT_NM")] V_DOCTORINFO v_DOCTORINFO)
        {
            if (ModelState.IsValid)
            {
                db.V_DOCTORINFO.Add(v_DOCTORINFO);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(v_DOCTORINFO);
        }

        // GET: Demo/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            V_DOCTORINFO v_DOCTORINFO = db.V_DOCTORINFO.Find(id);
            if (v_DOCTORINFO == null)
            {
                return HttpNotFound();
            }
            return View(v_DOCTORINFO);
        }

        // POST: Demo/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DOCTOR_ID,DOCTOR_NM,GENDER,AGE,TEL,PHOTO_URL,SECTION_ID,DISEASE_ID,INTRODUCTION,INSDATE,PATIENT_ID,PATIENT_NM")] V_DOCTORINFO v_DOCTORINFO)
        {
            if (ModelState.IsValid)
            {
                
                db.Entry(v_DOCTORINFO).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(v_DOCTORINFO);
        }

        // GET: Demo/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            V_DOCTORINFO v_DOCTORINFO = db.V_DOCTORINFO.Find(id);
            if (v_DOCTORINFO == null)
            {
                return HttpNotFound();
            }
            return View(v_DOCTORINFO);
        }

        // POST: Demo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            V_DOCTORINFO v_DOCTORINFO = db.V_DOCTORINFO.Find(id);
            db.V_DOCTORINFO.Remove(v_DOCTORINFO);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
