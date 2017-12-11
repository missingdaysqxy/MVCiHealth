using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCiHealth.Models;

namespace MVCiHealth.Controllers
{
    public class ReservationController : Controller
    {
        Models.iHealthEntities db = new iHealthEntities();
        // GET: Reservation
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Reserve()
        {
            //查询科室表中的科室名称

            ViewBag.Message = "Your reservation page.";
           // var section = db.SECTION_TYPE.Select(s => s.SECTION_NM).ToList();
            return View( );
        }

        #region  查询科室表中的科室名称
        public ActionResult DisplaySection()
        {
            var section = db.SECTION_TYPE.Select(s=>s.SECTION_NM).ToList() ;
            return View();
        }
#endregion

    }
}