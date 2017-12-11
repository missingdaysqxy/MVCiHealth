using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCiHealth.Models;

namespace MVCiHealth.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [ActionName("_FirstPart")]
        public PartialViewResult FirstPart()
        {
            ViewBag.PartTitle = "Description of iHealth";
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