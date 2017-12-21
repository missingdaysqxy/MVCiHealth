using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCiHealth.Models;
using System.IO;
using System.Text;
using System.Collections;
using Newtonsoft.Json;
using System.Net;

namespace MVCiHealth.Controllers
{
    public class HistoryController : Controller
    {
        const string historyPath = @"~/DataFiles/History/";
        iHealthEntities db = new iHealthEntities();
        // GET: History
        public ActionResult PatientView(int? PATIENT_ID)
        {
            if (PATIENT_ID == null)
            {
                PATIENT_ID = Global.CurrentUserID;
            }
            var list = new List<HistoryView>();
            var p_list = db.PATIENT_HISTORY.Where(m => m.PATIENT_ID == PATIENT_ID);
            foreach (var item in p_list)
            {
                var doc = db.DOCTOR.Find(item.DOCTOR_ID);
                var patient = db.PATIENT.Find(item.PATIENT_ID);
                var name = string.Format("病历编号{0}|上传医师：{1}|修改时间：{2}", item.HISTORY_ID, doc.DOCTOR_NM, item.UPDATE);
                list.Add(new HistoryView { HISTORY = item, DOCTOR = doc, PATIENT = patient, Name = name });
                //var path = Server.MapPath(historyPath);
                //path = path + item.HISTORY_URL;
                //try
                //{
                //    StreamReader objReader = new StreamReader(path);
                //    string sLine = "";
                //    ArrayList LineList = new ArrayList();
                //    while (sLine != null)
                //    {
                //        sLine = objReader.ReadLine();
                //        if (sLine != null && !sLine.Equals(""))
                //            LineList.Add(sLine);
                //    }
                //    objReader.Close();
                //}
                //catch (Exception)
                //{
                //    // ??????

                //}
            }
            //ViewBag.LineList = LineList;
            return View(list);
        }

        [HttpPost]
        public JsonResult Detail(int? id)
        {
            if (id == null)
                return null;
            var history = db.PATIENT_HISTORY.Find(id);
            if (history == null)
                return null;
            var doc = db.DOCTOR.Find(history.DOCTOR_ID);
            var patient = db.PATIENT.Find(history.PATIENT_ID);
            var title = string.Format("病历编号{0}", history.HISTORY_ID);

            var path = Server.MapPath(historyPath);
            path = path + history.HISTORY_URL;
            try
            {
                StreamReader sr = new StreamReader(path);
                string content = sr.ReadToEnd();
                sr.Close();
                var logtime = history.INSDATE == null ? ((DateTime)history.INSDATE).ToString("yyyy年MM月dd日 HH:mm:ss") : "无";
                var uptime = history.UPDATE == null ? ((DateTime)history.UPDATE).ToString("yyyy年MM月dd日 HH:mm:ss") : "无";
                object detail = new { title, doctor = doc.DOCTOR_NM, logtime, uptime, content };
                var str = JsonConvert.SerializeObject(detail);
                return Json(str);
            }
            catch (Exception)
            {
                return null;
            }
        }


        public ActionResult DoctorView(int? PATIENT_ID)
        {
            if (PATIENT_ID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var p = db.PATIENT_HISTORY.Find(PATIENT_ID);
            var path = Server.MapPath(historyPath);
            path = path + p.UPDATE;
            StreamReader objReader = new StreamReader(path);
            string sLine = "";
            ArrayList LineList = new ArrayList();
            while (sLine != null)
            {
                sLine = objReader.ReadLine();
                if (sLine != null && !sLine.Equals(""))
                    LineList.Add(sLine);
            }
            objReader.Close();
            ViewBag.LineList = LineList;
            return View();
        }
        [HttpPost]
        public ActionResult DoctorView(int PATIENT_ID)
        {
            var userid = Global.CurrentUserID;
            var p_list = db.PATIENT_HISTORY.Where(m => m.PATIENT_ID == PATIENT_ID);
            var path = Server.MapPath(historyPath);
            foreach (var item in p_list)
            {
                path = path + item.UPDATE;
            }
            StreamReader objReader = new StreamReader(path);
            string sLine = "";
            ArrayList LineList = new ArrayList();
            while (sLine != null)
            {
                sLine = objReader.ReadLine();
                if (sLine != null && !sLine.Equals(""))
                    LineList.Add(sLine);
            }
            objReader.Close();
            //ViewBag.LineList = LineList;
            return View();
        }
        public ActionResult DoctorViewEdit(int PATIENT_ID)
        {
            var userid = Global.CurrentUserID;
            var p = db.PATIENT_HISTORY.Find(PATIENT_ID);
            return View(p);
        }

        [HttpPost]
        public ActionResult DoctorViewEdit(int PATIENT_ID, HttpPostedFileBase file)//传图片
        {
            var p = db.PATIENT_HISTORY.Find(PATIENT_ID);
            var fileName = file.FileName;
            var filePath = Server.MapPath(Server.MapPath(historyPath));
            file.SaveAs(Path.Combine(filePath, fileName));
            var path = Server.MapPath(historyPath);
            path = path + p.UPDATE;
            StreamWriter sw = new StreamWriter(path);//写文件
            var his = new PATIENT_HISTORY()
            {
                HISTORY_ID = Global.NextHistoryID(),
                INSDATE = DateTime.Now,
            };
            var str = JsonConvert.SerializeObject(his);
            sw.WriteLine(str);
            sw.Close();
            sw.Dispose();
            return View();
        }
    }
}