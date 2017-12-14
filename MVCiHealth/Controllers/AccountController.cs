using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCiHealth.Models;

namespace MVCiHealth.Controllers
{
    public class AccountController : Controller
    {
        Models.iHealthEntities db = new iHealthEntities();
        // GET: Account
        public ActionResult Login()
        {
            //GET自动登陆
            return View();
        }

        [HttpPost]
        public ActionResult Login(USERINFO u)
        {
            //GET自动登陆
            var m = db.USERINFO.Find(u.USER_ID);
            if (m.USER_PW == u.USER_PW)
                Global.IsLoggedIn = true;
            else
                Global.IsLoggedIn = false;
            return View(m);
        }

        public ActionResult LoginForm()
        {
            return View();
        }

        // GET: Account
        public ActionResult Logout()
        {
            Global.IsLoggedIn = false;
            return View();
        }
        // GET: Account
        public ActionResult Regist()
        {
            return View();
        }
    }
}