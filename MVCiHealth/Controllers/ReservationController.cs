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
        public ActionResult Reserve(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                ModelState.AddModelError("", "You click reservation id is " + id);

            }
            var list = db.SECTION_TYPE.Where(m => true).ToList();
            return View(list);
        }

        [HttpPost]
        public ActionResult Reserve(string id, string name)
        {
            //查询科室表中的科室名称
            ModelState.AddModelError("", "You click reservation id is " + id);
            // var section = db.SECTION_TYPE.Select(s => s.SECTION_NM).ToList();
            return View();
        }

        #region  查询科室表中的科室名称
        public ActionResult DisplaySection()
        {
            //var section = db.SECTION_TYPE.Select(s=>s.SECTION_NM).ToList() ;
            return View();
        }
        #endregion

    }
}