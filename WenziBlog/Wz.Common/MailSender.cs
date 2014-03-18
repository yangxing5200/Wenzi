using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Configuration;
using System.Web;
using System.IO;
using System.Net;
using System.Net.Mail;

namespace Maticsoft.Common
{
    public class MailSender
    {
        public static void Send(string server, string sender, string recipient, string subject,
    string body, bool isBodyHtml, Encoding encoding, bool isAuthentication, params string[] files)
        {
            var smtpClient = new SmtpClient(server);
            var message = new MailMessage(sender, recipient)
                {
                    IsBodyHtml = isBodyHtml,
                    SubjectEncoding = encoding,
                    BodyEncoding = encoding,
                    Subject = subject,
                    Body = body
                };

            message.Attachments.Clear();
            if (files != null && files.Length != 0)
            {
                foreach (string t in files)
                {
                    var attach = new Attachment(t);
                    message.Attachments.Add(attach);
                }
            }

            if (isAuthentication)
            {
                smtpClient.Credentials = new NetworkCredential(SmtpConfig.Create().SmtpSetting.User,
                    SmtpConfig.Create().SmtpSetting.Password);
            }
            smtpClient.Send(message);


        }

        public static void Send(string recipient, string subject, string body)
        {
            Send(SmtpConfig.Create().SmtpSetting.Server, SmtpConfig.Create().SmtpSetting.Sender, recipient, subject, body, true, Encoding.Default, true, null);
        }

        public static void Send(string recipient, string sender, string subject, string body)
        {
            Send(SmtpConfig.Create().SmtpSetting.Server, sender, recipient, subject, body, true, Encoding.UTF8, true, null);
        }

        static readonly string SmtpServer = ConfigurationManager.AppSettings["SmtpServer"];
        static readonly string UserName = ConfigurationManager.AppSettings["UserName"];
        static readonly string Pwd = ConfigurationManager.AppSettings["Pwd"];
        static readonly int SmtpPort = Convert.ToInt32(ConfigurationManager.AppSettings["SmtpPort"]);
        static readonly string AuthorName = ConfigurationManager.AppSettings["AuthorName"];
        static readonly string To = ConfigurationManager.AppSettings["To"];


        public void Send(string subject, string body)
        {

            List<string> toList = StringPlus.GetSubStringList(StringPlus.ToDBC(To), ',');
            var smtp = new OpenSmtp.Mail.Smtp(SmtpServer, UserName, Pwd, SmtpPort);
            foreach (string s in toList)
            {
                var msg = new OpenSmtp.Mail.MailMessage {From = new OpenSmtp.Mail.EmailAddress(UserName, AuthorName)};

                msg.AddRecipient(s, OpenSmtp.Mail.AddressType.To);

                //设置邮件正文,并指定格式为 html 格式
                msg.HtmlBody = body;
                //设置邮件标题
                msg.Subject = subject;
                //指定邮件正文的编码
                msg.Charset = "gb2312";
                //发送邮件
                smtp.SendMail(msg);
            }
        }
    }

    public class SmtpSetting
    {
        public string Server { get; set; }

        public bool Authentication { get; set; }

        public string User { get; set; }

        public string Sender { get; set; }

        public string Password { get; set; }
    }

    public class SmtpConfig
    {
        private static SmtpConfig _smtpConfig;
        private string ConfigFile
        {
            get
            {
                string configPath = ConfigurationManager.AppSettings["SmtpConfigPath"];
                if (string.IsNullOrEmpty(configPath) || configPath.Trim().Length == 0)
                {
                    configPath = HttpContext.Current.Request.MapPath("/Config/SmtpSetting.config");
                }
                else
                {
                    configPath = !Path.IsPathRooted(configPath) ? HttpContext.Current.Request.MapPath(Path.Combine(configPath, "SmtpSetting.config")) : Path.Combine(configPath, "SmtpSetting.config");
                }
                return configPath;
            }
        }
        public SmtpSetting SmtpSetting
        {
            get
            {
                var doc = new XmlDocument();
                doc.Load(ConfigFile);
                var smtpSetting = new SmtpSetting
                    {
                        Server = doc.DocumentElement.SelectSingleNode("Server").InnerText,
                        Authentication = Convert.ToBoolean(doc.DocumentElement.SelectSingleNode("Authentication").InnerText),
                        User = doc.DocumentElement.SelectSingleNode("User").InnerText,
                        Password = doc.DocumentElement.SelectSingleNode("Password").InnerText,
                        Sender = doc.DocumentElement.SelectSingleNode("Sender").InnerText
                    };

                return smtpSetting;
            }
        }
        private SmtpConfig()
        {

        }
        public static SmtpConfig Create()
        {
            return _smtpConfig ?? (_smtpConfig = new SmtpConfig());
        }
    }
}
