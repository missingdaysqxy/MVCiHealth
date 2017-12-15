using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCiHealth.Models;
using MVCiHealth.Utils;
using System.IO;
using System.Web.Security;

namespace MVCiHealth.Controllers
{
    public class AccountController : Controller
    {
        private const string Session_VCode = "VCode";
        /// <summary>验证码长度</summary>
        private const uint ValiCodeLenth = 4;
        iHealthEntities db = new iHealthEntities();


        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            ViewBag.Count = 0;
            return View(new USERINFO());
        }

        [HttpPost]
        public ActionResult Login(USERINFO u, int? Count, string VCode)
        {
            if (Count != null)
                Count++;
            ViewBag.Count = Count ?? 1;
            if (Count > 2)//设置为加载验证码
            {
                ViewBag.ShowVCode = true;
            }
            if (!string.IsNullOrEmpty(VCode))
            {
                if (VCode.Trim().ToLower() == Session[Session_VCode].ToString().ToLower())
                    ModelState.AddModelError("VCodeErr", "验证码错误");
            }
            if (string.IsNullOrEmpty(u.LOGIN_NM) || string.IsNullOrEmpty(u.PASSWORD))
                return View(u);
            if (Global.TrySignIn(u.LOGIN_NM, u.PASSWORD))
            {
                try
                {
                    switch (Global.CurrentUserGroup)
                    {
                        case GroupType.Patient:
                            Response.Redirect("/Patient/Index");break;
                        case GroupType.Doctor:
                            Response.Redirect("/Doctors/Index"); break;
                        case GroupType.SupeAdmin:
                        case GroupType.WebAccendant:
                        case GroupType.CodeAccendant:
                        case GroupType.DataAccendant:
                            Response.Redirect("/Home/Index"); break;
                        default:
                            Response.Redirect("/Home/Index"); break;
                    }
                }
                catch
                {
                    throw;
                }
            }
            else
            {
                ModelState.AddModelError("Error", "用户名或密码错误");
            }
            return View(u);
        }

        /// <summary>  
        /// 返回验证码图片流  
        /// </summary>  
        /// <returns>图片文件</returns>
        [AllowAnonymous]
        public ActionResult GetVCode()
        {
            ValidateCodeCreater vCoder = new ValidateCodeCreater();
            string code = vCoder.CreateValidateCode(ValiCodeLenth);
            byte[] pic = (vCoder.CreateValidatePicture(code) as MemoryStream).ToArray();
            Session[Session_VCode] = code;
            return File(pic, @"image/jpeg");
        }

        // GET: Account
        public ActionResult Logout()
        {
            Global.SignOut();
            return View();
        }
        // GET: Account
        public ActionResult Regist()
        {
            return View();
        }

        public ActionResult ForgetPassword()
        {
            return View();
        }

    }
}