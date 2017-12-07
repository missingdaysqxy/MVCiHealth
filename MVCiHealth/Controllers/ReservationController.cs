using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCiHealth.Controllers
{
    public class ReservationController : Controller
    {
        // GET: Reservation
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Reserve()
        {
            ViewBag.Message = "Your reservation page.";

            return View();
        }

    }
}