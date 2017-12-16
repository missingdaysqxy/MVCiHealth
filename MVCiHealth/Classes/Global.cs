using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.Mvc;
using MVCiHealth.Models;
using Newtonsoft.Json;

namespace MVCiHealth
{
    #region 公开类型

    /// <summary>
    /// 枚举可用的用户组类型
    /// </summary>
    public enum GroupType
    {
        /// <summary>游客</summary>
        Guest = -1,
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

    /// <summary>
    /// 存储在票证中的用户数据
    /// </summary>
    public struct TicketUserData
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public TicketUserData(string userName, string password)
        {
            this.UserName = userName;
            this.Password = password;
        }
        public TicketUserData(string jsonStr)
        {
            try
            {
                var t = JsonConvert.DeserializeObject<TicketUserData>(jsonStr);
                this.UserName = t.UserName;
                this.Password = t.Password;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }

    #endregion

    /// <summary>
    /// 提供一组公共属性与方法以便完成各种通用功能
    /// </summary>
    public static class Global
    {
        private const string currentUser = "currentUser";
        private const string loginID = "user_id";
        private const string loginIP = "login_ip";
        private const string personInfoPage = "personInfo";
        private static USERINFO GetDefaultGuest()
        {
            return new USERINFO()
            {
                USER_ID = -1,
                LOGIN_NM = "游客",
                GROUP_ID = (int)GroupType.Guest,
                VALID = "T",
                INSDATE = DateTime.MinValue,
            };
        }

        #region 公开属性

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
                else
                    return true;
                //var cookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
                //var ticket = FormsAuthentication.Decrypt(cookie.Value);
                ////票证不过期
                //if (!ticket.Expired)
                //    //票证用户名不为空
                //    if (!string.IsNullOrEmpty(ticket.Name))
                //        //用户验证通过
                //        if (HttpContext.Current.User.Identity.IsAuthenticated)
                //            return true;
                //return false;
            }
        }

        /// <summary>
        /// 返回当前已登录用户
        /// </summary>
        public static USERINFO CurrentUser
        {
            get
            {
                try
                {
                    var user = HttpContext.Current.Session[currentUser] as USERINFO;
                    return user ?? GetDefaultGuest();
                }
                catch
                {
                    return GetDefaultGuest();
                }
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
                    if (!IsLoggedIn) return GroupType.Guest;
                    ////读票证用户组
                    //var cookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
                    //var ticket = FormsAuthentication.Decrypt(cookie.Value);
                    //var group = (GroupType)int.Parse(ticket.UserData);
                    ////读实时用户组
                    //iHealthEntities db = new iHealthEntities();
                    //var sgroup = (GroupType)db.GROUPINFO.Find(db.USERINFO.Find(CurrentUserID).GROUP_ID).AUTHENTICATION;
                    //if (sgroup == group)//Session有效
                    var user = HttpContext.Current.Session[currentUser] as USERINFO;
                    return (GroupType)user.GROUP_ID;
                    //else//Session无效s
                    //    return GroupType.Unknown;
                }
                catch
                {
                    return GroupType.Guest;
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
                    ////票证用户ID
                    //var cookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
                    //var ticket = FormsAuthentication.Decrypt(cookie.Value);
                    //var id = int.Parse(ticket.Name);
                    //Session用户ID
                    var id = int.Parse(HttpContext.Current.Session[loginID].ToString());
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
                    var user = HttpContext.Current.Session[currentUser] as USERINFO;
                    return user.LOGIN_NM;
                }
                catch
                {
                    return "未命名";
                }
            }
        }

        #region 

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
        /// 获取当前用户个人信息页的控制器名称
        /// </summary>
        public static string PersonInfoController
        {
            get
            {
                try
                {
                    return HttpContext.Current.Session[personInfoPage] as string;
                }
                catch
                {
                    return  "Home";
                }
            }
        }

        #endregion
        #endregion

        #region 公开方法

        /// <summary>
        /// 尝试登录（会先注销当前已用户）
        /// </summary>
        /// <param name="login_name">用户名</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        internal static bool TrySignIn(string login_name, string password, bool autoLogin = false)
        {
            iHealthEntities db = new iHealthEntities();
            //先通过调用数据库存储过程来判断用户密码是否正确
            var result = new System.Data.Entity.Core.Objects.ObjectParameter("iscorrect", typeof(string));
            var user_id = new System.Data.Entity.Core.Objects.ObjectParameter("user_id", typeof(int));
            db.VeryfyPassword(login_name, password, result, user_id);
            if (result.Value.ToString().Equals("T"))//密码正确
            {
                //注销之前用户
                SignOut();
                //获取用户详细信息
                var user = db.USERINFO.Find((int)user_id.Value);
                if (user == null) return false;
                //如果启用自动登录，则将包含登录信息的Cookie存入客户端，
                //以便用户下次访问时通过读取Cookie来自动调用此方法登入系统
                if (autoLogin)
                {
                    //创建一个FormsAuthenticationTicket，它包含登录名以及额外的用户数据。
                    var data = new TicketUserData(user.LOGIN_NM, user.PASSWORD);
                    var ticket = new FormsAuthenticationTicket
                        (1, user.USER_ID.ToString(), DateTime.Now,
                        DateTime.Now.AddDays(7), true, data.ToString());
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
                }
                // 设置Session值
                HttpContext.Current.Session[loginID] = user.USER_ID;
                HttpContext.Current.Session[currentUser] = user;
                HttpContext.Current.Session[loginIP] = CurrentUserIPAddress;
                switch ((GroupType)user.GROUP_ID)
                {
                    case GroupType.Patient:
                        HttpContext.Current.Session[personInfoPage] = "Patient";
                        break;
                    case GroupType.Doctor:
                        HttpContext.Current.Session[personInfoPage] = "Doctor";
                        break;
                    case GroupType.SupeAdmin:
                    case GroupType.WebAccendant:
                    case GroupType.CodeAccendant:
                    case GroupType.DataAccendant:
                    default:
                        HttpContext.Current.Session[personInfoPage] = "Home";
                        break;
                }
                return true;
            }
            return false;
        }

        /// <summary>
        /// 注销当前用户
        /// </summary>
        internal static void SignOut()
        {
            FormsAuthentication.SignOut();
            //HttpContext.Current.Response.Cookies.Remove(FormsAuthentication.FormsCookieName);
            HttpContext.Current.Session.Remove(loginID);
            HttpContext.Current.Session.Remove(currentUser);
            HttpContext.Current.Session.Remove(loginIP);
            HttpContext.Current.Session.Remove(personInfoPage);
        }

        #endregion
    }
}