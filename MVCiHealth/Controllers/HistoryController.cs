using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCiHealth.Controllers
{
    public class HistoryController : Controller
    {
        // GET: History
        public ActionResult PatientView()
        {
            return View();
        }
        public ActionResult DoctorView()
        {
            return View();
        }
    }
}