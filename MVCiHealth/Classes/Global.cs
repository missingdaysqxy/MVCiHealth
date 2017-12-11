using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.Mvc;

namespace MVCiHealth
{
    public static class Global
    {
        private const string loginID = "user_id";
        private const string loginNM = "user_nm";
        private const string loginIP = "login_ip";
        private static bool _loggedin=false;

        /// <summary>
        /// 获取用户是不是已经登录
        /// </summary>
        public static bool IsLoggedIn
        {
            get
            {
                return _loggedin;
            }
            internal set { _loggedin = value; }
        }

        /// <summary>
        /// 返回当前用户所属用户组
        /// </summary>
        public static GroupType UserGroup
        {
            get
            {
                try
                {
                    return GroupType.SupeAdmin;
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
        public static string CurrentUserID
        {
            get
            {
                try
                {
                return  (HttpContext.Current.Session[loginID] as string) ?? "1";
                }
                catch
                {
                    return "1";
                }
            }
        }
    }
}