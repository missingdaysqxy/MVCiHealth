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
        private const string Session_LoginCount = "LogCount";
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
            if (Session[Session_LoginCount] == null)
                Session[Session_LoginCount] = 0;
            else if ((int)Session[Session_LoginCount] >= 2)
                ViewBag.ShowVCode = true;
            return View(new USERINFO());
        }

        [HttpPost]
        public ActionResult Login(USERINFO u, int? Count, string VCode, bool AutoLogin = true)
        {
            int? count = (int?)Session[Session_LoginCount];
            if (count == null)
                Session[Session_LoginCount] = 0;
            else
                Session[Session_LoginCount] = count + 1;
            if (count == 1)//设置为加载验证码
            {
                ViewBag.ShowVCode = true;
                return this.CallFunction("showVCode");
            }
            if (count >= 2)
            {
                ViewBag.ShowVCode = true;
                if (string.IsNullOrEmpty(VCode))
                {
                    return this.ContactScripts(this.HideMessage(), this.ShowMessage("验证码不能为空", MessageType.Error, true));
                }
                else if (VCode.Trim().ToLower() != Session[Session_VCode].ToString().ToLower())
                {
                    return this.ShowMessage("验证码错误", MessageType.Error, true);
                }
            }
            if (string.IsNullOrEmpty(u.LOGIN_NM))
                return this.ShowMessage("LOGIN_NM", "错误", "用户名不能为空", MessageType.Error);
            else if (string.IsNullOrEmpty(u.PASSWORD))
                return this.ShowMessage("PASSWORD", "错误", "密码不能为空", MessageType.Error);
            else if (Global.TrySignIn(u.LOGIN_NM, u.PASSWORD, AutoLogin))
            {
                return this.ParentRedirectTo("Index", Global.PersonInfoController);
            }
            else
            {
                return this.ShowMessage("用户名或密码错误", MessageType.Error, true);
            }
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