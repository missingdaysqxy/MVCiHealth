using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.Mvc;
using MVCiHealth.Models;

namespace MVCiHealth
{
    /// <summary>
    /// 枚举可用的用户组类型
    /// </summary>
    public enum GroupType
    {
        /// <summary>身份不明，仅拥有游客权限</summary>
        Unknown = -1,
        /// <summary>病人</summary>
        Patient = 1,
        /// <summary>医生</summary>
        Doctor = 2,
        /// <summary>超级管理员，拥有最高权限</summary>
        SupeAdmin = 100,
        /// <summary>网页维护员</summary>
        WebAccendant = 101,
        /// <summary>数据维护员</summary>
        DataAccendant = 102,
        /// <summary>代码维护员</summary>
        CodeAccendant = 103,
    }

    /// <summary>
    /// 枚举可用的性别
    /// </summary>
    public enum Gender
    {
        /// <summary>保密</summary>
        Secret,
        /// <summary>男性</summary>
        Male,
        /// <summary>女性</summary>
        Female,
        /// <summary>双性</summary>
        Bisexual,
        /// <summary>无性</summary>
        Nonsexual,
        /// <summary>女变男的变性人(FTM)</summary>
        Female2Male,
        /// <summary>男变女的变性人(MTF</summary>
        Male2Female,
        /// <summary>其它可能性别</summary>
        Others,
    }

    public static class Global
    {
        private const string loginID = "user_id";
        private const string loginNM = "login_nm";
        private const string loginIP = "login_ip";
        private const string personInfo = "personInfo";

        /// <summary>
        /// 获取用户是不是已经登录
        /// </summary>
        public static bool IsLoggedIn
        {
            get
            {
                //Session为空返回false
                if (string.IsNullOrEmpty((HttpContext.Current.Session[loginID] ?? "").ToString()))
                    return false;
                var cookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
                var ticket = FormsAuthentication.Decrypt(cookie.Value);
                //票据不过期
                if (!ticket.Expired)
                    //票据用户名不为空
                    if (!string.IsNullOrEmpty(ticket.Name))
                        //用户验证通过
                        if (HttpContext.Current.User.Identity.IsAuthenticated)
                            return true;
                return false;
            }
        }

        /// <summary>
        /// 返回当前用户所属用户组
        /// </summary>
        public static GroupType CurrentUserGroup
        {
            get
            {
                try
                {
                    //判断游客
                    if (!IsLoggedIn) return GroupType.Unknown;
                    //读票据用户组
                    var cookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
                    var ticket = FormsAuthentication.Decrypt(cookie.Value);
                    var group = (GroupType)int.Parse(ticket.UserData);
                    ////读实时用户组
                    //iHealthEntities db = new iHealthEntities();
                    //var sgroup = (GroupType)db.GROUPINFO.Find(db.USERINFO.Find(CurrentUserID).GROUP_ID).AUTHENTICATION;
                    //if (sgroup == group)//Session有效
                    return group;
                    //else//Session无效s
                    //    return GroupType.Unknown;
                }
                catch
                {
                    return GroupType.Unknown;
                }
            }
        }

        /// <summary>
        /// 返回当前用户的ID
        /// </summary>
        public static int CurrentUserID
        {
            get
            {
                try
                {
                    //票据用户ID
                    var cookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
                    var ticket = FormsAuthentication.Decrypt(cookie.Value);
                    var id = int.Parse(ticket.Name);
                    ////Session用户ID
                    //var sid = int.Parse(HttpContext.Current.Session[loginID].ToString());
                    //if (id == sid)
                    return id;
                    //throw new Exception();
                }
                catch
                {
                    return -1;
                }
            }
        }

        /// <summary>
        /// 返回当前用户的登录名
        /// </summary>
        public static string CurrentLogginName
        {
            get
            {
                try
                {
                    var nm = HttpContext.Current.Session[loginNM].ToString();
                    return nm;
                }
                catch
                {
                    return "未命名";
                }
            }
        }

        /// <summary>
        /// 获取当前客户的IP地址
        /// </summary>
        public static string CurrentUserIPAddress
        {
            get
            {
                string user_IP = string.Empty;
                if (System.Web.HttpContext.Current.Request.ServerVariables["HTTP_VIA"] != null)
                {
                    if (System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null)
                    {
                        user_IP = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString();
                    }
                    else
                    {
                        user_IP = System.Web.HttpContext.Current.Request.UserHostAddress;
                    }
                }
                else
                {
                    user_IP = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();
                }
                return user_IP;
            }
        }

        /// <summary>
        /// 获取当前用户个人信息页地址
        /// </summary>
        public static string PersonalInfoPage
        {
            get
            {
                try
                {
                    return HttpContext.Current.Session[personInfo].ToString();
                }
                catch
                {
                    return FormsAuthentication.DefaultUrl;
                }
            }
        }

        /// <summary>
        /// 尝试登录（会先注销当前已用户）
        /// </summary>
        /// <param name="login_name">用户名</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        public static bool TrySignIn(string login_name, string password)
        {
            iHealthEntities db = new iHealthEntities();
            var result = new System.Data.Entity.Core.Objects.ObjectParameter("iscorrect", typeof(string));
            var user_id = new System.Data.Entity.Core.Objects.ObjectParameter("user_id", typeof(int));
            db.VeryfyPassword(login_name, password, result, user_id);
            if (result.Value.ToString().Equals("T"))
            {
                //注销之前用户
                SignOut();
                //获取用户详细信息
                var user = db.USERINFO.Find((int)user_id.Value);
                if (user == null) return false;
                //创建一个FormsAuthenticationTicket，它包含登录名以及额外的用户数据。
                var ticket =
                    new FormsAuthenticationTicket(
                        1, user.USER_ID.ToString(), DateTime.Now, DateTime.Now.AddDays(7), true, user.GROUP_ID.ToString());
                //加密Ticket，变成一个加密的字符串。
                var cookieValue = FormsAuthentication.Encrypt(ticket);
                //根据加密结果创建登录Cookie
                var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, cookieValue)
                {
                    HttpOnly = true,
                    Secure = FormsAuthentication.RequireSSL,
                    Domain = FormsAuthentication.CookieDomain,
                    Path = FormsAuthentication.FormsCookiePath
                };
                cookie.Expires = DateTime.Now.AddDays(7);
                //写登录Cookie
                HttpContext.Current.Response.Cookies.Remove(cookie.Name);
                HttpContext.Current.Response.Cookies.Add(cookie);
                // 设置Session值
                HttpContext.Current.Session[loginID] = user.USER_ID;
                HttpContext.Current.Session[loginNM] = user.LOGIN_NM;
                HttpContext.Current.Session[loginIP] = CurrentUserIPAddress;
                switch ((GroupType)user.GROUP_ID)
                {
                    case GroupType.Patient:
                        HttpContext.Current.Session[personInfo] = "~/Patient/Index";
                        break;
                    case GroupType.Doctor:
                        HttpContext.Current.Session[personInfo] = "~/Doctors/main";
                        break;
                    case GroupType.SupeAdmin:
                    case GroupType.WebAccendant:
                    case GroupType.CodeAccendant:
                    case GroupType.DataAccendant:
                    default:
                        HttpContext.Current.Session[personInfo] = "~/Home/Index";
                        break;
                }
                return true;
            }
            return false;
        }

        /// <summary>
        /// 注销当前用户
        /// </summary>
        public static void SignOut()
        {
            FormsAuthentication.SignOut();
            HttpContext.Current.Response.Cookies.Remove(FormsAuthentication.FormsCookieName);
            HttpContext.Current.Session.Remove(loginID);
            HttpContext.Current.Session.Remove(loginNM);
            HttpContext.Current.Session.Remove(loginIP);
        }
    }
}