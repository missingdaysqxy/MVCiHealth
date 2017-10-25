using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MVCiHealth
{
    /// <summary>
    /// 枚举可用的用户组类型
    /// </summary>
    [Flags]
    public enum GroupType
    {
        /// <summary>身份不明，仅拥有游客权限</summary>
        Unknown,
        /// <summary>超级管理员，拥有最高权限</summary>
        SupeAdmin,
        /// <summary>网页维护员</summary>
        WebAccendant,
        /// <summary>数据维护员</summary>
        DataAccendant,
        /// <summary>代码维护员</summary>
        CodeAccendant,
        /// <summary>病人</summary>
        Patient,
        /// <summary>医生</summary>
        Doctor,
    }


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