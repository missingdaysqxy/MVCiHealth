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
    public class HomeController : Controller
    {
        iHealthEntities db = new iHealthEntities();
        public ActionResult Index()
        {
            //向本地数据库中添加示范数据
            //MVCiHealth.Utils.SampleDataFiller.FillData();

            var home = new Home();
            home.V_EVALUATIONLIST = new List<V_EVALUATION>();
            home.USERINFOLIST = new List<USERINFO>();
            home.DOCTORLIST = db.DOCTOR.OrderByDescending(m => m.LEVEL).Take(3).ToList();
            

            foreach (var i in home.DOCTORLIST)
            {
                int doc_id = i.DOCTOR_ID;
                var for_test = db.V_EVALUATION.Where(m => m.RATE >= 3 && m.DOCTOR_ID == doc_id).First();
                home.V_EVALUATIONLIST.Add(for_test);
            }
            return View(home);
        }

        [ActionName("_FirstPart")]
        public PartialViewResult FirstPart()
        {

            ViewBag.PATIENTNUM = db.USERINFO.Count(m => m.GROUP_ID == 1);
            ViewBag.DOCTORNUM = db.USERINFO.Count(m => m.GROUP_ID == 2);
            return PartialView();
        }
        [ActionName("_SecondPart")]
        public PartialViewResult SecondPart()
        {
            
            ViewBag.PartTitle = "Requirements Introduction";
            return PartialView();
        }
        public PartialViewResult ThirdPart()
        {

            return PartialView();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
    
}
