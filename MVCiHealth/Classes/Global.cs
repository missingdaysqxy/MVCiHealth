using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.Mvc;
using MVCiHealth.Models;
using Newtonsoft.Json;
using System.IO;
using System.Reflection;

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
    /// 提示信息类型
    /// </summary>
    public enum MessageType
    {
        /// <summary>绿色成功提示</summary>
        Success,
        /// <summary>浅蓝信息提示</summary>
        Infomation,
        /// <summary>橙色警告提示</summary>
        Warning,
        /// <summary>红色错误提示</summary>
        Error,
        /// <summary>蓝色重要提示</summary>
        Important,
        /// <summary>灰色次要提示</summary>
        Secondary,
        /// <summary>深灰色提示</summary>
        Dark,
        /// <summary>浅灰色提示</summary>
        Light,
    }

    public enum MessageBoxButton
    {

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
    public static partial class Global
    {
        #region 私有字段

        private const string currentUser = "currentUser";
        private const string loginID = "user_id";
        private const string loginIP = "login_ip";
        private const string personInfoPage = "personInfo";
        private const string htmlMessageSpanClass = "_messagehandler_";
        private const string htmlMessageSpanName = "_messagename_";
        private static string[] messageType = new string[]
        {
            "alert-success",
            "alert-info",
            "alert-warning",
            "alert-danger",
            "alert-primary",
            "alert-secondary",
            "alert-dark",
            "alert-light",
        };
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

        #endregion

        #region 公开属性——用户信息

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

        #endregion

        #region 公开属性——跳转信息

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
                    return "Home";
                }
            }
        }

        #endregion

        #region 公开方法——账号管理

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

        #region 公开方法——页面交互

        #region 动作连接

        /// <summary>
        /// 按顺序将多个<see cref="JavaScriptResult"/>类型的返回值连接成一个值
        /// </summary>
        /// <param name="scripts">要连接的javascripts代码</param>
        /// <returns></returns>
        public static JavaScriptResult ConcatJScripts(this Controller controller, params JavaScriptResult[] scripts)
        {
            var content = new JavaScriptResult();
            foreach (var item in scripts)
            {
                content.Script += item.Script;
            }
            return content;
        }

        /// <summary>
        /// 按顺序将多个<see cref="JavaScriptResult"/>类型的返回值连接成一个值
        /// </summary>
        /// <param name="scripts">要连接的javascripts代码</param>
        /// <returns></returns>
        public static JavaScriptResult ConcatJScripts(this Controller controller, IEnumerable<JavaScriptResult> scripts)
        {
            var content = new JavaScriptResult();
            foreach (var item in scripts)
            {
                content.Script += item.Script;
            }
            return content;
        }

        #endregion

        #region 弹出框

        public static JavaScriptResult MessageBox(this Controller controller, string message, string title = "", string footer = "")
        {
            var content = new JavaScriptResult()
            {
                Script = "MessageBox('" + message + "','" + title + "','" + footer + "');",
            };
            return content;
        }

        #endregion

        #region 提示信息

        #region 信息占位符
        /// <summary>
        /// 在此处生成一个 HTML 标记，用于显示由<see cref="ShowMessage"/>方法所生成的提示消息
        /// </summary>
        /// <returns></returns>
        public static MvcHtmlString MessageHandler(this HtmlHelper html)
        {
            var exp = string.Format("<span class=\"{0}\"></span>", htmlMessageSpanClass);
            return new MvcHtmlString(exp);
        }
        /// <summary>
        /// 在此处生成一个含有自定义属性的 HTML 标记，用于显示由<see cref="ShowMessage"/>方法所生成的提示消息
        /// </summary>
        /// <param name="htmlAttribute">标记所包含的自定义属性</param>
        /// <returns></returns>
        public static MvcHtmlString MessageHandler(this HtmlHelper html, object htmlAttribute)
        {
            var exp = string.Format("<span class=\"{0}", htmlMessageSpanClass);
            if (htmlAttribute != null)
            {
                try
                {
                    string attr = "";
                    foreach (var m in htmlAttribute.GetType().GetProperties())
                    {
                        if (m.Name == "class")
                        {
                            try
                            {
                                var v = m.GetValue(htmlAttribute);
                                if (v != null) exp += " " + v;
                                else throw new Exception();
                            }
                            catch { throw new ArgumentException("参数给出的 class 属性不是有效值"); }
                        }
                        else
                        {
                            attr += " " + m.Name;
                            try
                            {
                                var v = m.GetValue(htmlAttribute);
                                if (v != null) attr += "=\"" + v + "\"";
                            }
                            catch { throw new ArgumentException("参数给出的 " + m.Name + " 属性不是有效值"); }
                        }
                    }
                    exp += "\"" + attr;
                }
                catch { throw new ArgumentException("参数格式不正确"); }
            }
            if (!exp.EndsWith("\"")) exp += "\"";
            exp += "></span>";
            return new MvcHtmlString(exp);
        }
        /// <summary>
        /// 在此处生成一个含有特定名称与可选自定义属性的 HTML 标记，用于显示由<see cref="ShowMessage"/>方法所生成的提示消息
        /// </summary>
        /// <param name="messageName">要显示消息的名称</param>
        /// <param name="htmlAttribute">标记所包含的自定义属性</param>
        /// <returns></returns>
        public static MvcHtmlString MessageHandler(this HtmlHelper html, string messageName, object htmlAttribute = null)
        {
            var exp = string.Format("<span {0}=\"{1}\" class=\"{2}", htmlMessageSpanName, messageName, htmlMessageSpanClass);
            if (htmlAttribute != null)
            {
                try
                {
                    string attr = "";
                    foreach (var m in htmlAttribute.GetType().GetProperties())
                    {
                        if (m.Name == "class")
                        {
                            try
                            {
                                var v = m.GetValue(htmlAttribute);
                                if (v != null) exp += " " + v;
                                else throw new Exception();
                            }
                            catch { throw new ArgumentException("参数给出的 class 属性不是有效值"); }
                        }
                        else
                        {
                            attr += " " + m.Name;
                            try
                            {
                                var v = m.GetValue(htmlAttribute);
                                if (v != null) attr += "=\"" + v + "\"";
                            }
                            catch { throw new ArgumentException("参数给出的 " + m.Name + " 属性不是有效值"); }
                        }
                    }
                    exp += "\"" + attr;
                }
                catch { throw new ArgumentException("参数格式不正确"); }
            }
            if (!exp.EndsWith("\"")) exp += "\"";
            exp += "></span>";
            return new MvcHtmlString(exp);
        }
        #endregion

        #region 显示信息
        /// <summary>
        /// 在页面上由<see cref="MessageHandler"/>标记的位置显示信息
        /// </summary>
        /// <param name="message">要显示的信息</param>
        /// <param name="ignoreNamedMessageHandler">若为true，则忽略页面上已经指定了
        /// messageName的<see cref="MessageHandler"/>，否则将会在所有
        /// <see cref="MessageHandler"/>上显示此信息</param>
        /// <param name="type">（可选）信息类型</param>
        /// <returns></returns>
        public static JavaScriptResult ShowMessage(this Controller controller,
            string message, MessageType type = MessageType.Infomation, bool ignoreNamedMessageHandler = false)
        {
            string msgtype = messageType[(int)type];
            string script;
            if (ignoreNamedMessageHandler)
                script = string.Format(
                   "$('span.{0}:not([{1}])').html('<div class=\"alert {2}\">{3}</div>');",
                   htmlMessageSpanClass, htmlMessageSpanName, msgtype, message);
            else
                script = string.Format(
                   "$('span.{0}').html('<div class=\"alert {1}\">{2}</div>');",
                   htmlMessageSpanClass, msgtype, message);
            return new JavaScriptResult() { Script = script };
        }

        /// <summary>
        /// 在页面上由名为<paramref name="messageName"/>的<see cref="MessageHandler"/>标记的位置显示信息
        /// </summary>
        /// <param name="message">要显示的信息</param>
        /// <param name="messageName">信息对应的<see cref="MessageHandler"/>的名称</param>
        /// <param name="type">（可选）信息类型</param>
        /// <returns></returns>
        public static JavaScriptResult ShowMessage(this Controller controller,
            string message, string messageName, MessageType type = MessageType.Infomation)
        {
            string msgtype = messageType[(int)type];
            var script = string.Format(
                "$('span.{0}[{1}=\"{2}\"]').html('<div class=\"alert {3}\">{4}</div>');",
                 htmlMessageSpanClass, htmlMessageSpanName, messageName, msgtype, message);
            return new JavaScriptResult() { Script = script };
        }

        #endregion

        #region 隐藏信息
        /// <summary>
        /// 隐藏页面上所有由<see cref="ShowMessage"/>显示的信息
        /// </summary>
        /// <returns></returns>
        public static JavaScriptResult HideMessage(this Controller controller)
        {
            var script = string.Format("$('span.{0}').html('');", htmlMessageSpanClass);
            return new JavaScriptResult() { Script = script };
        }

        /// <summary>
        /// 隐藏页面上由<see cref="ShowMessage"/>生成的指定名称的信息
        /// </summary>
        /// <returns></returns>
        public static JavaScriptResult HideMessage(this Controller controller, string messageName)
        {
            var script = string.Format("$('span.{0}[{1}=\"{2}\"]').html('');", htmlMessageSpanClass, htmlMessageSpanName, messageName);
            return new JavaScriptResult() { Script = script };
        }
        #endregion

        #endregion

        #region 页面跳转

        /// <summary>
        /// 利用JavaScript将当前页面重定向到目标操作，适用于无法使用<see cref="Controller.RedirectToAction"/>的情况
        /// </summary>
        /// <param name="actionName">操作的名称</param>
        /// <returns></returns>
        public static JavaScriptResult RedirectTo(this Controller controller, string actionName)
        {
            var script = string.Format("location.href='/{0}';", actionName);
            return new JavaScriptResult() { Script = script };
        }

        /// <summary>
        /// 利用JavaScript将当前页面重定向到目标操作与路由，适用于无法使用<see cref="Controller.RedirectToAction"/>的情况
        /// </summary>
        /// <param name="actionName">操作的名称</param>
        /// <param name="controllerName">路由的名称</param>
        /// <returns></returns>
        public static JavaScriptResult RedirectTo(this Controller controller, string actionName, string controllerName)
        {
            var script = string.Format("location.href='/{0}/{1}';", controllerName, actionName);
            return new JavaScriptResult() { Script = script };
        }

        /// <summary>
        /// 利用JavaScript将当前页面的父页面（如果有）重定向到目标操作，适用于无法使用<see cref="Controller.RedirectToAction"/>的情况
        /// </summary>
        /// <param name="actionName">操作的名称</param>
        /// <returns></returns>
        public static JavaScriptResult RedirectParentTo(this Controller controller, string actionName)
        {
            var script = string.Format("if (this === window.top)location.href='/{0}';else window.top.location.href='/{0}';", actionName);
            return new JavaScriptResult() { Script = script };
        }

        /// <summary>
        /// 利用JavaScript将当前页面的父页面（如果有）重定向到目标操作与路由，适用于无法使用<see cref="Controller.RedirectToAction"/>的情况
        /// </summary>
        /// <param name="actionName">操作的名称</param>
        /// <param name="controllerName">路由的名称</param>
        /// <returns></returns>
        public static JavaScriptResult RedirectParentTo(this Controller controller, string actionName, string controllerName)
        {
            var script = string.Format("if (this === window.top)location.href='/{0}/{1}';else window.top.location.href='/{0}/{1}';", controllerName, actionName);
            return new JavaScriptResult() { Script = script };
        }

        /// <summary>
        /// 根据<see cref="ViewResult"/>直接替换当前页面的内容，适用于在当前<see cref="Controller"/>内进行的重定向操作
        /// </summary>
        /// <param name="view">要显示的视图，一般由<see cref="Controller.View"/>获取</param>
        /// <returns></returns>
        public static JavaScriptResult ChangePage(this Controller controller, ViewResult view)
        {
            var html = RenderViewToString(controller, view);
            html = html.Replace("\r", "").Replace("\n", "").Replace("\"", "\\\"").Replace("'", "\\'");
            var script = string.Format("$('html').html('{0}');", html);
            var content = new JavaScriptResult()
            {
                Script = script,
            };
            controller.Response.Clear();
            return content;
        }

        #endregion

        #region 函数调用

        /// <summary>
        /// 调用一个当前页面已经存在的JavaScript函数
        /// </summary>
        /// <param name="funtionName">要调用的函数名称</param>
        /// <param name="parameters">（可选）函数要使用的参数值</param>
        /// <returns></returns>
        public static JavaScriptResult CallFunction(this Controller controller,
            string funtionName, params object[] parameters)
        {
            var script = funtionName + "('";
            foreach (var p in parameters)
            {
                script += p.ToString() + ",";
            }
            script = script.TrimEnd(',');
            script += "');";
            var content = new JavaScriptResult()
            {
                Script = script,
            };
            return content;
        }

        #endregion

        #endregion

        #region 私有方法

        private static string RenderViewToString(Controller controller, ViewResult viewResult)
        {
            viewResult.ExecuteResult(controller.ControllerContext);
            IView view = viewResult.View;
            using (StringWriter writer = new StringWriter())
            {
                ViewContext viewContext = new ViewContext(controller.ControllerContext, view, controller.ViewData, controller.TempData, writer);
                viewContext.View.Render(viewContext, writer);
                return writer.ToString();
            }
        }

        private static string RenderPartialViewToString(Controller controller, PartialViewResult partialViewResult)
        {
            IView view = partialViewResult.View;
            using (StringWriter writer = new StringWriter())
            {
                ViewContext viewContext = new ViewContext(controller.ControllerContext, view, controller.ViewData, controller.TempData, writer);
                viewContext.View.Render(viewContext, writer);
                return writer.ToString();
            }
        }

        /// <summary>
        /// 利用反射执行一个特定方法
        /// </summary>
        /// <param name="nameSpace">命名空间</param>
        /// <param name="className">类名</param>
        /// <param name="methodName">方法名</param>
        /// <param name="parameters">参数列表</param>
        /// <returns></returns>
        private static object Execute(string nameSpace, string className, string methodName, params object[] parameters)
        {
            //先从程序集中查找所需要的类并使用系统激活器创建实例
            object instance = Assembly.Load(nameSpace).CreateInstance(nameSpace + "." + className);
            Type type = instance.GetType();
            //查找指定的方法，然后返回结果
            MethodInfo methodinfo = type.GetMethod(methodName);
            return methodinfo.Invoke(instance, parameters);
        }

        #endregion
    }
}