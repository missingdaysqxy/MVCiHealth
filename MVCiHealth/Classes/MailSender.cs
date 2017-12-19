using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;

namespace MVCiHealth.Utils
{
    /// <summary>
    /// 一个用于发送邮件的静态类
    /// </summary>
    public static class MailSender
    {
        static string smtpServer = "smtp.163.com";
        static int smtpPort = 25;
        static string senderAccount = "ihealth_666@163.com";
        static string senderName = "iHealth管理员";
        static string senderPassword = "i666666";

        /// <summary>
        /// 获取或设置服务器登录用户名（即发送者的邮箱地址）
        /// </summary>
        /// <exception cref="ArgumentException">
        /// 当邮箱地址不合法时触发异常</exception>
        public static string SenderAccount
        {
            get { return senderAccount; }
            set
            {
                if (IsMailAddress(value)) senderAccount = value;
                else throw new ArgumentException(value + "不是合法的邮箱地址！");
            }
        }
        /// <summary>
        /// 获取或设置发送者昵称
        /// </summary>
        public static string SenderName { get { return senderName; } set { senderName = value; } }
        /// <summary>
        /// 设置服务器登录密码（即发送者的登录密码）
        /// </summary>
        public static string SenderPassword { set { senderPassword = value; } }
        /// <summary>
        /// 获取或设置SMTP服务器地址
        /// </summary>
        public static string SmtpServer { get { return smtpServer; } set { smtpServer = value; } }
        /// <summary>
        /// 获取或设置SMTP服务器端口号
        /// </summary>
        public static int SmtpPort { get { return smtpPort; } set { smtpPort = value; } }

        static MailSender()
        {
            try
            {
            }
            catch { }
        }

        /// <summary>
        /// 发送电子邮件
        /// </summary>
        /// <param name="recieverAddress">收件人地址</param>
        /// <param name="mailTitle">邮件标题</param>
        /// <param name="mailContent">邮件正文</param>
        /// <returns>是否发送成功</returns>
        public static bool SendMail(string recieverAddress, string mailTitle, string mailContent)
        {
            string msg;
            return SendMail(new string[] { recieverAddress }, mailTitle, mailContent, null, out msg);
        }
        /// <summary>
        /// 发送电子邮件
        /// </summary>
        /// <param name="recieverAddress">收件人地址</param>
        /// <param name="mailTitle">邮件标题</param>
        /// <param name="mailContent">邮件正文</param>
        /// <param name="resources">需要嵌入的Html链接资源（即邮件附件）</param>
        /// <returns>是否发送成功</returns>
        public static bool SendMail(string recieverAddress, string mailTitle, string mailContent, LinkedResource[] resources)
        {
            string msg;
            return SendMail(new string[] { recieverAddress }, mailTitle, mailContent, resources, out msg);
        }
        /// <summary>
        /// 发送电子邮件
        /// </summary>
        /// <param name="recieverAddress">收件人地址</param>
        /// <param name="mailTitle">邮件标题</param>
        /// <param name="mailContent">邮件正文</param>
        /// <param name="errorMsg">此参数返回出错信息（如果有的话）</param>
        /// <returns>是否发送成功</returns>
        public static bool SendMail(string recieverAddress, string mailTitle, string mailContent, out string errorMsg)
        {
            return SendMail(new string[] { recieverAddress }, mailTitle, mailContent, null, out errorMsg);
        }
        /// <summary>
        /// 发送电子邮件
        /// </summary>
        /// <param name="recieverAddress">收件人地址</param>
        /// <param name="mailTitle">邮件标题</param>
        /// <param name="mailContent">邮件正文</param>
        /// <param name="resources">需要嵌入的Html链接资源（即邮件附件）</param>
        /// <param name="errorMsg">此参数返回出错信息（如果有的话）</param>
        /// <returns>是否发送成功</returns>
        public static bool SendMail(string recieverAddress, string mailTitle, string mailContent, LinkedResource[] resources, out string errorMsg)
        {
            return SendMail(new string[] { recieverAddress }, mailTitle, mailContent, resources, out errorMsg);
        }
        /// <summary>
        /// 群发电子邮件
        /// </summary>
        /// <param name="recieverAddresses">群发收件人地址列表</param>
        /// <param name="mailTitle">邮件标题</param>
        /// <param name="mailContent">邮件正文</param>
        /// <returns>是否发送成功</returns>
        public static bool SendMail(string[] recieverAddresses, string mailTitle, string mailContent)
        {
            string msg;
            return SendMail(recieverAddresses, mailTitle, mailContent, null, out msg);
        }
        /// <summary>
        /// 群发电子邮件
        /// </summary>
        /// <param name="recieverAddresses">群发收件人地址列表</param>
        /// <param name="mailTitle">邮件标题</param>
        /// <param name="mailContent">邮件正文</param>
        /// <param name="resources">需要嵌入的Html链接资源（即邮件附件）</param>
        /// <returns>是否发送成功</returns>
        public static bool SendMail(string[] recieverAddresses, string mailTitle, string mailContent, LinkedResource[] resources)
        {
            string msg;
            return SendMail(recieverAddresses, mailTitle, mailContent, resources, out msg);
        }
        /// <summary>
        /// 群发电子邮件
        /// </summary>
        /// <param name="recieverAddresses">群发收件人地址列表</param>
        /// <param name="mailTitle">邮件标题</param>
        /// <param name="mailContent">邮件正文</param>
        /// <param name="errorMsg">此参数返回出错信息（如果有的话）</param>
        /// <returns>是否发送成功</returns>
        public static bool SendMail(string[] recieverAddresses, string mailTitle, string mailContent, out string errorMsg)
        {
            return SendMail(recieverAddresses, mailTitle, mailContent, null, out errorMsg);
        }
        /// <summary>
        /// 群发电子邮件
        /// </summary>
        /// <param name="recieverAddresses">群发收件人地址列表</param>
        /// <param name="mailTitle">邮件标题</param>
        /// <param name="mailContent">邮件正文</param>
        /// <param name="resources">需要嵌入的Html链接资源（即邮件附件）</param>
        /// <param name="errorMsg">此参数返回出错信息（如果有的话）</param>
        /// <returns>是否发送成功</returns>
        public static bool SendMail(string[] recieverAddresses, string mailTitle, string mailContent, LinkedResource[] resources, out string errorMsg)
        {
            errorMsg = "";
            if (recieverAddresses != null && recieverAddresses.Length > 0 && !string.IsNullOrEmpty(mailTitle) && !string.IsNullOrEmpty(mailContent))
            {
                try
                {
                    //声明一个Mail对象
                    MailMessage mymail = new MailMessage();
                    //发件人地址
                    //如是自己，在此输入自己的邮箱
                    mymail.From = new MailAddress(senderAccount, senderName);
                    //收件人地址
                    foreach (var addrs in recieverAddresses)
                    {
                        if (IsMailAddress(addrs))
                            mymail.To.Add(new MailAddress(addrs));
                        else
                            errorMsg += "警告：非法收件人地址：" + addrs + "\r\n";
                    }
                    if (mymail.To.Count <= 0)
                    {
                        errorMsg += "错误：没有一条合法的收件人地址，邮件发送失败！\r\n";
                        return false;
                    }
                    //邮件主题
                    mymail.Subject = mailTitle;
                    //邮件标题编码
                    mymail.SubjectEncoding = System.Text.Encoding.UTF8;
                    //发送邮件的内容
                    //纯文本格式
                    var textBody = AlternateView.CreateAlternateViewFromString(mailContent, Encoding.UTF8, "text/plain");
                    mymail.AlternateViews.Add(textBody);
                    //HTML格式
                    var htmlBody = AlternateView.CreateAlternateViewFromString(mailContent, Encoding.UTF8, "text/html");
                    //添加Html链接资源（即邮件附件）
                    if (resources != null && resources.Length > 0)
                        foreach (var src in resources)
                        {
                            htmlBody.LinkedResources.Add(src);
                        }
                    mymail.AlternateViews.Add(htmlBody);
                    //邮件内容编码
                    mymail.BodyEncoding = System.Text.Encoding.UTF8;
                    //邮件Html属性
                    mymail.IsBodyHtml = true;
                    //抄送到其他邮箱
                    //mymail.CC.Add(new MailAddress(tb_cc.Text));
                    //是否是HTML邮件
                    mymail.IsBodyHtml = true;
                    //邮件优先级
                    mymail.Priority = MailPriority.Normal;
                    //创建一个邮件服务器类
                    SmtpClient myclient = new SmtpClient();
                    myclient.Host = smtpServer;
                    //SMTP服务端口
                    myclient.Port = smtpPort;
                    //指定 SmtpClient 使用安全套接字层(SSL)加密连接
                    myclient.EnableSsl = true;
                    //验证登录
                    myclient.Credentials = new NetworkCredential(senderAccount, senderPassword);//输入有效的用户名和密码
                    myclient.Send(mymail);
                    errorMsg = "success!";
                    return true;
                }
                catch (Exception e)
                {
                    errorMsg += e.Message ;
                    while (e.InnerException != null)
                    {
                        errorMsg += e.InnerException.Message;
                        e = e.InnerException;
                    }
                    return false;
                }
            }
            errorMsg += "错误：参数不能为空！";
            return false;
        }

        /// <summary>
        /// 判断一个字符串是不是合法的邮箱地址
        /// </summary>
        /// <param name="addr"></param>
        /// <returns></returns>
        private static bool IsMailAddress(string addr)
        {
            string expression = @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
            if (Regex.IsMatch(addr, expression, RegexOptions.Compiled))
                return true;
            return false;
        }
    }
}