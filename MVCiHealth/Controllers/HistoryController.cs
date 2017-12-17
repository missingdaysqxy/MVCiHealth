using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCiHealth.Models;
using System.IO;
using System.Text;
using System.Collections;

namespace MVCiHealth.Controllers
{
    public class HistoryController : Controller
    {
        iHealthEntities db = new iHealthEntities();
        // GET: History
        public ActionResult PatientView()
        {
            //var userid = Global.CurrentUserID;
            //var p = db.PATIENT_HISTORY.Find(int.Parse(userid));            
            //StreamReader objReader = new StreamReader(p.HISTORY_URL+"/txt");
            //string sLine = "";
            //ArrayList LineList = new ArrayList();
            //while (sLine != null)
            //{
            //    sLine = objReader.ReadLine();
            //    if (sLine != null && !sLine.Equals(""))
            //        LineList.Add(sLine);
            //}
            //objReader.Close();
            //return View(LineList);
            return View();

        }
        public ActionResult DoctorView()
        {
            var userid = Global.CurrentUserID;
            var p = db.PATIENT_HISTORY.Find(userid);
            return View(p);
        }
        public ActionResult DoctorViewEdit()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DoctorViewEdit(HttpPostedFileBase file)//传图片
        {
            var fileName = file.FileName;
            var filePath = Server.MapPath(string.Format("~/{0}", "File"));
            file.SaveAs(Path.Combine(filePath, fileName));
            //StreamWriter sw = new StreamWriter(@"F:\1.txt");//写文件
            //foreach (string s in array)
            //{
            //    sw.WriteLine(s);
            //}
            //sw.Close();
            //sw.Dispose();
            return View();
        }
    }
}