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
        iHealthEntities db = new iHealthEntities();
        // GET: History
        public ActionResult PatientView(int? PATIENT_ID)
        {
            if (PATIENT_ID == null)
            {
                PATIENT_ID = Global.CurrentUserID;
            }
            var p_list = db.PATIENT_HISTORY.Where(m => m.PATIENT_ID == PATIENT_ID);
            var path = Server.MapPath(@"~/Resources/Images/History");
            foreach (var item in p_list)
            {
                path = path + item.UPDATE;
            }
            try
            {
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
            }
            catch (Exception)
            {
                // ??????
                
            }
            
            //ViewBag.LineList = LineList;
            return View();
        }
        [HttpPost]
        public ActionResult PatientView(int PATIENT_ID)
        {
            var p_list = db.PATIENT_HISTORY.Where(m => m.PATIENT_ID == PATIENT_ID);
            var path = Server.MapPath(@"~/Resources/Images/History");
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


        public ActionResult DoctorView(int? PATIENT_ID)
        {
            if (PATIENT_ID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var p = db.PATIENT_HISTORY.Find(PATIENT_ID);
            var path = Server.MapPath(@"~/Resources/Images/History");
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
            var path = Server.MapPath(@"~/Resources/Images/History");
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
            var filePath = Server.MapPath(Server.MapPath(@"~/Resources/Images/History"));
            file.SaveAs(Path.Combine(filePath, fileName));
            var path = Server.MapPath(@"~/Resources/Images/History");
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