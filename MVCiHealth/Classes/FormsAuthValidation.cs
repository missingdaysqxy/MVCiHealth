using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MVCiHealth.Classes
{
    ///// <summary>
    ///// 用于权限验证的特性类
    ///// </summary>
    //public sealed class AuthValidateAttribute : ActionFilterAttribute
    //{
    //    public override void OnActionExecuting(ActionExecutingContext filterContext)
    //    {
    //        //如果存在身份信息
    //        object[] attrs = filterContext.ActionDescriptor.GetCustomAttributes(typeof(AllowAnonymousAttribute), false);
    //        if (attrs.Length <= 0)
    //            if (!HttpContext.Current.User.Identity.IsAuthenticated)
    //            {
    //                filterContext.Result = new RedirectResult(FormsAuthentication.LoginUrl);
    //            }
    //        //else
    //        //{
    //        //    //此处可以通过自定义IIdentity和IPrincipal来设置权限系统

    //        //}
    //    }
    //}

    /// <summary>
    /// 自动登录类
    /// </summary>
    /// <remarks>
    /// 1.检测当前有没有登录，如果已经登录，则返回
    /// 2.读取Cookie信息，查看有无登录信息票证，若没有，则返回
    /// 3.读取票证，判断票证是否有效，若无效，则返回
    /// 4.通过票证，获得用户保存在客户端的登录信息，尝试登录
    /// </remarks>
    public sealed class AutoLogInAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            try
            {
                if (Global.IsLoggedIn) return;
                if (filterContext.HttpContext.User.Identity == null) return;
                if (filterContext.HttpContext.User.Identity is FormsIdentity)
                {
                    var id = filterContext.HttpContext.User.Identity as FormsIdentity;
                    var ticket = id.Ticket;
                    if (!ticket.IsPersistent) return;
                    var userdata = new TicketUserData(ticket.UserData);
                    Global.TrySignIn(userdata.UserName, userdata.Password, true);
                }
            }
            catch
            {
                return;
            }
        }
    }


}