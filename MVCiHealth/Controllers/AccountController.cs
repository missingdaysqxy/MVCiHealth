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
    [AllowAnonymous]
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
        public ActionResult Login(USERINFO u, int? Count, string VCode, bool AutoLogin = true)
        {
            if (Count != null)
                Count++;
            ViewBag.Count = Count ?? 1;
            if (Count > 2)//设置为加载验证码
            {
                ViewBag.ShowVCode = true;
            }
            if (Session[Session_VCode] != null)
            {
                if (string.IsNullOrEmpty(VCode))
                {
                    return Global.ErrorMessage("验证码不能为空");
                }
                else if (VCode.Trim().ToLower() == Session[Session_VCode].ToString().ToLower())
                {
                    return Global.ErrorMessage("验证码错误");
                }
            }
            if (string.IsNullOrEmpty(u.LOGIN_NM))
                return Global.ErrorMessage("用户名不能为空");
            else if (string.IsNullOrEmpty(u.PASSWORD))
                return Global.ErrorMessage("密码不能为空");
            else if (Global.TrySignIn(u.LOGIN_NM, u.PASSWORD, AutoLogin))
            {
                return RedirectToAction("Index", Global.PersonInfoController);
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
            return RedirectToAction("Index", "Home");
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