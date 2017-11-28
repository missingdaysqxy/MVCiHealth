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
        Models.LocalDBEntities db = new LocalDBEntities();
        // GET: Account
        public ActionResult Login()
        {
            //GET自动登陆
            var m = db.USERINFO.First();
            Global.IsLoggedIn = true;
            return View(m);
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