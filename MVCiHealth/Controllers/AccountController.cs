using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCiHealth.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Login()
        {
            Global.IsLoggedIn = true;
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