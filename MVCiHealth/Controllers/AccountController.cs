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
        /// <summary>无验证码尝试次数限制</summary>
        private const uint NoVCodeLimit = 2;
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
            var actionlist = new List<JavaScriptResult>
            {
                //清空页面上所有消息（如果有）
                this.HideMessage()
            };
            //检查用户名与密码是否填写
            if (string.IsNullOrEmpty(u.LOGIN_NM))
                actionlist.Add(this.ShowMessage("LOGIN_NM", "用户名不能为空", MessageType.Error));
            if (string.IsNullOrEmpty(u.PASSWORD))
                actionlist.Add(this.ShowMessage("PASSWORD", "密码不能为空", MessageType.Error));
            //检查是否启用验证码
            int? count = (int?)Session[Session_LoginCount];
            if (count == null)
                Session[Session_LoginCount] = 0;
            else
                Session[Session_LoginCount] = count + 1;
            if (count > NoVCodeLimit)//验证码已启用
            {
                ViewBag.ShowVCode = true;
                //验证码是否填写
                if (string.IsNullOrEmpty(VCode))
                {
                    actionlist.Add(this.ShowMessage("验证码不能为空", MessageType.Error, true));
                    actionlist.Add(this.CallFunction("refreshVCode", DateTime.Now.ToBinary().ToString()));
                }
                //验证码是否正确
                else if (VCode.Trim().ToLower() != Session[Session_VCode].ToString().ToLower())
                {
                    actionlist.Add(this.ShowMessage("验证码错误", MessageType.Error, true));
                    actionlist.Add(this.CallFunction("refreshVCode", DateTime.Now.ToBinary().ToString()));
                }
            }
            //保存错误数量
            var errorCount = actionlist.Count - 1;
            //当验证码未启用时检查是否需要启用验证码
            if (count == NoVCodeLimit)
            {
                ViewBag.ShowVCode = true;
                actionlist.Add(this.CallFunction("showVCode"));
            }
            //如果已有错误，则返回错误信息
            if (errorCount > 0)
            {
                return this.ConcatJScripts(actionlist);
            }
            else if (Global.TrySignIn(u.LOGIN_NM, u.PASSWORD, AutoLogin))
            {
                //成功登录，跳转页面到个人信息页
                return this.RedirectParentTo("Index", Global.PersonInfoController);

            }
            else
            {
                //登录失败，返回错误
                actionlist.Add(this.ShowMessage("用户名或密码错误", MessageType.Error, true));
                return this.ConcatJScripts(actionlist);
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