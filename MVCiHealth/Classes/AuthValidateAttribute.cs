using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MVCiHealth
{
    /// <summary>
    /// 用于权限验证的特性类
    /// </summary>
    public sealed class AuthValidateAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //如果存在身份信息
            object[] attrs = filterContext.ActionDescriptor.GetCustomAttributes(typeof(AllowAnonymousAttribute), false);
            if (attrs.Length <= 0)
                if (!HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    filterContext.Result = new RedirectResult(FormsAuthentication.LoginUrl);
                }
                //else
                //{
                //    //此处可以通过自定义IIdentity和IPrincipal来设置权限系统

                //}
        }
    }

    /// <summary>
    /// 如果特性中有此类，则跳过AuthValidate权限验证
    /// </summary>
    public sealed class LogInAttribute : Attribute
    {
    }
}