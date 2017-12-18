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
        iHealthEntities db = new iHealthEntities();
        // GET: Reservation 显示数据库中科室名称
        public ActionResult Index(int? id)
        {
            if (Global.CurrentUserGroup == GroupType.Patient)
            {

                var list = db.SECTION_TYPE.ToArray();
                List<DOCTOR> doclist = null;
                if (id != null)
                {
                    doclist = db.DOCTOR.Where(m => m.SECTION_ID == id).ToList();
                }
                var ri = new ReservationIndex()
                {
                    DOCTOR_LIST = doclist ?? new List<DOCTOR>(),
                    SECTION_LIST = list,
                };
                return View(ri);
            }
            else if(Global.CurrentUserGroup==GroupType.Doctor)
            {
                return RedirectToAction("Detail");
            }
            return RedirectToAction("../Home/Index");//之前是home，大佬弄的时候是正常的，我弄的时候就崩了，所以改为了不会出错的跳主页面先，现在要登陆才能预约了
        }


        [HttpPost]
        public ContentResult Index(int? doctor_id, DateTime? time,string comment)
        {
            // var section = db.SECTION_TYPE.Select(s => s.SECTION_NM).ToList();
            if(doctor_id==null||time==null)
            {
                return Content("error");
            }
            if (Global.CurrentUserGroup != GroupType.Patient)
            {
                return Content("group error");
            }
            var r = new RESERVATION()
            {
                RESERVATION_ID = Global.NextReservationID(),
                DOCTOR_ID = (int)doctor_id,
                TIME_START = time,
                CONFIRMED = "F",
                VALID = time > DateTime.Now ? "T" : "F",
                PATIENT_ID = Global.CurrentUserID,
                COMMENT = comment,
                INSDATE = DateTime.Now,
            };
            try
            {
                db.RESERVATION.Add(r);
                db.SaveChanges();
            }
            catch (Exception)
            {
                return Content("database error");
            }
            Response.Redirect("~/Patient/Index");
            return Content("true");
        }


        

        public ActionResult Detail()
        {
            if(Global.CurrentUserGroup==GroupType.Doctor)
            {
                var reservationList = db.V_RESERVATION.Where(m => m.DOCTOR_ID == Global.CurrentUserID).ToList();//此处有缺憾，希望加个orderby,但未遂
                return View(reservationList);

            }
            return View();
        }


        public ActionResult DetailReservation_Previous()
        {
            if (Global.CurrentUserGroup == GroupType.Doctor)
            {
                var reservationList = db.V_RESERVATION.Where(m => m.DOCTOR_ID == Global.CurrentUserID&&m.TIME_START<DateTime.Now).ToList();//此处有缺憾，希望加个orderby,但未遂
                return View(reservationList);

            }
            return View();

        }

        public ActionResult DetailReservation_Prospective()
        {
            if (Global.CurrentUserGroup == GroupType.Doctor)
            {
                var reservationList = db.V_RESERVATION.Where(m => m.DOCTOR_ID == Global.CurrentUserID && (m.TIME_START >= DateTime.Now)).ToList();//此处有缺憾，希望加个orderby,但未遂
                return View(reservationList);

            }
            return View();

        }
    }
}