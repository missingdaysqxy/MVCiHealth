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
            return View(db.V_DOCTORINFO.ToList());
        }

        // GET: Demo/Details/5
        public ActionResult Details(int? did, int? pid)
        {
            if (did == null || pid == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            V_DOCTORINFO info = db.V_DOCTORINFO.Find(did, pid);
            if (info == null)
            {
                return HttpNotFound();
            }
            return View(info);
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
        public ActionResult Create(
            [Bind(Include = "DOCTOR_ID,DOCTOR_NM,GENDER,AGE,TEL,PHOTO_URL,SECTION_ID,DISEASE_ID,INTRODUCTION,INSDATE,PATIENT_ID,PATIENT_NM")]
        V_DOCTORINFO info)
        {
            var doctor = new DOCTOR()
            {
                DOCTOR_ID = info.DOCTOR_ID,
                DOCTOR_NM = info.DOCTOR_NM,
                GENDER = info.GENDER,
                AGE = info.AGE,
                DISEASE_ID = info.DISEASE_ID,
                INTRODUCTION = info.INTRODUCTION,
                SECTION_ID = info.SECTION_ID,
                TEL = info.TEL,
                INSDATE = DateTime.Now,
            };
            if (ModelState.IsValid)
            {
                db.DOCTOR.Add(doctor);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(info);
        }

        // GET: Demo/Edit/5
        public ActionResult Edit(int? did, int? pid)
        {
            if (did == null || pid == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            V_DOCTORINFO info = db.V_DOCTORINFO.Find(did, pid);
            if (info == null)
            {
                return HttpNotFound();
            }
            return View(info);
        }

        // POST: Demo/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(
            [Bind(Include = "DOCTOR_ID,DOCTOR_NM,GENDER,AGE,TEL,PHOTO_URL,SECTION_ID,DISEASE_ID,INTRODUCTION,INSDATE,PATIENT_ID,PATIENT_NM")]
        V_DOCTORINFO info)
        {
            //此方法接收的参数info，里面包含了从页面回传的信息
            //应该先对信息内容进行校验与处理，然后再存入数据库
            //比如某些字段不能为空，比如日期时间是否正确等
            if (string.IsNullOrEmpty(info.DOCTOR_NM)) { ModelState.AddModelError("DOCTOR_NM", "姓名不能为空"); return View(info); }
            if (string.IsNullOrEmpty(info.PATIENT_NM)) { ModelState.AddModelError("PATIENT_NM", "姓名不能为空"); return View(info); }

            //因为这个Demo选取的是视图数据，是不支持直接存入的，所以实际在存入时是存到别的表里
            //因此要把传回来的info拆解成别的表里的数据
            //DOCTOR表的相关内容
            var doctor = new DOCTOR()
            {
                DOCTOR_ID = info.DOCTOR_ID,
                DOCTOR_NM = info.DOCTOR_NM,
                GENDER = info.GENDER,
                AGE = info.AGE,
                DISEASE_ID = info.DISEASE_ID,
                INTRODUCTION = info.INTRODUCTION,
                SECTION_ID = info.SECTION_ID,
                TEL = info.TEL,
                INSDATE = DateTime.Now,
            };
            //PATIENT表的相关内容
            var patient = new PATIENT()
            {
                PATIENT_ID = info.PATIENT_ID,
                PATIENT_NM = info.PATIENT_NM,
                INSDATE = DateTime.Now,
            };
            if (ModelState.IsValid)
            {
                //第一种方法：默认从页面传回的所有字段都发生了改变，
                //并且都符合数据库规范，则将此数据全部存入数据库。
                //注意如果有不合规范的数据（比如不能为空的字段成了空，
                //或者主键不存在等），在调用SaveChanges时会报错。
                //这种方法太过简单粗暴，在不能保证时尽量不用。
                db.Entry(doctor).State = EntityState.Modified;
                db.Entry(patient).State = EntityState.Modified;
                db.SaveChanges();

                //第二种方法：认为数据并非全部发生了变动，而是只有
                //部分字段发生了修改，则应该将数据整体先标记为未变化，
                //然后将发生变化的字段单独标记为已变化，最后再保存数据库。
                var a = db.Entry(patient);
                a.State = EntityState.Unchanged;
                a.Property(m => m.PATIENT_NM).IsModified = true;
                db.SaveChanges();

                //第三种方法：与第二种方法相反，认为只有少数数据没有变化（比如INSDATE
                //字段就应该永远不变），而大部分都已经改变，处理方法与第二种类似。
                var b = db.Entry(doctor);
                b.State = EntityState.Modified;
                b.Property(m => m.INSDATE).IsModified = false;
                db.SaveChanges();

                //第四种方法：不管页面中的数据有没有发生变化，都是先从数据库中
                //把数据重新读取出来，然后对其特定字段重新赋为页面回传的值，然后
                //再存回数据库。注意：使用此方法时不需要再使用Entry与State，因为
                //刚从数据库中读出的对象，还保持着与数据库缓存的联系，对此对象的
                //一切修改都会被上下文缓存自动记录，并在调用SaveChanges时提交。
                var c = db.DOCTOR.Find(info.DOCTOR_ID);
                doctor.INSDATE = c.INSDATE;
                c = doctor;
                var d = db.PATIENT.Find(info.PATIENT_ID);
                d.PATIENT_NM = patient.PATIENT_NM;
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(info);
        }

        // GET: Demo/Delete/5
        public ActionResult Delete(int? did, int? pid)
        {
            if (did == null || pid == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            V_DOCTORINFO info = db.V_DOCTORINFO.Find(did, pid);
            if (info == null)
            {
                return HttpNotFound();
            }
            return View(info);
        }

        // POST: Demo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? did, int? pid)
        {
            var c = db.DOCTOR.Find(did);
            db.DOCTOR.Remove(c);
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
