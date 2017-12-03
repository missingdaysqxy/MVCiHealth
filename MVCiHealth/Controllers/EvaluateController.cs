using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCiHealth.Models;

namespace MVCiHealth.Controllers
{
    public class EvaluateController : Controller
    {
        iHealthEntities db = new iHealthEntities();
        // GET: Evaluate
        public ActionResult Index()
        {
            var d = new DOCTOR()
            {
                DOCTOR_NM = "ADOCTOR"
            };
            return View(d);
        }
    }
}