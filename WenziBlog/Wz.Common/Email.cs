using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using System.Net;
using System.IO;
using System.Configuration;

namespace Wz.Common
{
    public class Email
    {

        //static string EServer = "mail.wellness-online.com.cn";// "mail.800teleservices.com";
        //static string EUser = "support@wellness-online.com.cn";//"sunlight.ouyang@teleservices.com";//
        //static string EPwd = "wel2102";//"oygynh2011$";//
        //static string EFrom = "support@wellness-online.com.cn";
        static string _eServer = "smtp.163.com";// "mail.800teleservices.com";
        static string _eUser = "18217099613@163.com";//"sunlight.ouyang@teleservices.com";//
        static string _ePwd = "hold@0603";//"oygynh2011$";//
        static string _eFrom = "18217099613@163.com";

        public Email(string Server, string User, string Pwd, string From)
        {
            _eServer = Server;
            _eUser = User;
            _ePwd = Pwd;
            _eFrom = From;

        }

        public static void Send(string to, string cc, string tiltle, string content, bool text, string att, string type)
        {
            try
            {
                //是否真正发送
                string IsSend = ConfigurationManager.AppSettings["Email"];

                if (IsSend != null)
                {

                    return;
                }

                if (to == "")
                    to = "leon.zhu@800teleservices.com";
                SmtpClient smtp = new SmtpClient(_eServer);
                smtp.UseDefaultCredentials = false;
                smtp.EnableSsl = false;


                //CredentialCache myCache = new CredentialCache();
                //myCache.Add(EServer , 25, "login", new NetworkCredential(EUser, EPwd));
                //smtp.Credentials = myCache;
                smtp.Credentials = new NetworkCredential(_eUser, _ePwd);
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;

                MailMessage mm = new MailMessage();

                mm.From = new MailAddress(_eFrom, _eUser);

                string[] toArr = to.Split(';');
                foreach (string toStr in toArr)
                {
                    if (toStr.Trim() != "")
                    {
                        mm.To.Add(new MailAddress(toStr));
                    }
                }
                if (cc != "")
                {
                    string[] ccArr = cc.Split(';');
                    foreach (string ccStr in ccArr)
                    {
                        if (ccStr.Trim() != "")
                        {
                            mm.CC.Add(new MailAddress(ccStr));
                        }
                    }
                }

                mm.Subject = tiltle;
                mm.Body = content;
                mm.IsBodyHtml = true;
                mm.BodyEncoding = System.Text.Encoding.GetEncoding("UTF-8");
                mm.SubjectEncoding = System.Text.Encoding.GetEncoding("UTF-8");
                mm.HeadersEncoding = System.Text.Encoding.GetEncoding("UTF-8");
                //添加附件
                if (att != null && att != "")
                {
                    string[] attchstr = att.Split(';');
                    foreach (string attch in attchstr)
                    {
                        if (attch.Trim() != "")
                        {
                            if (File.Exists(attch))
                            {
                                FileInfo fi = new FileInfo(attch);
                                FileStream fs = new FileStream(attch, FileMode.Open, FileAccess.Read);
                                //StreamReader s = new StreamReader(fs);

                                Attachment attachment = new Attachment(fs, fi.Name);//, MediaTypeNames.Application.Octet);
                                //attachment.TransferEncoding = System.Net.Mime.TransferEncoding.SevenBit ;
                                attachment.NameEncoding = System.Text.Encoding.UTF8;
                                string sName = attachment.Name;
                                //Attachment attachment = new Attachment(attch, MediaTypeNames.Application.Octet);
                                mm.Attachments.Add(attachment);
                            }

                        }

                    }

                }
                //smtp.SendCompleted += new SendCompletedEventHandler(smtp_SendCompleted);
                //smtp.SendAsync(mm,mm.Subject);
                smtp.Send(mm);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void Send(string Server, string User, string Pwd, string From, string to, string cc, string tiltle, string content, bool text, string att, string type)
        {
            try
            {
                //是否真正发送
                string IsSend = ConfigurationManager.AppSettings["Email"];

                if (IsSend != null)
                {

                    return;
                }


                if (to == "")
                    to = "leon.zhu@800teleservices.com";
                SmtpClient smtp = new SmtpClient(Server);
                smtp.UseDefaultCredentials = false;
                smtp.EnableSsl = false;


                //CredentialCache myCache = new CredentialCache();
                //myCache.Add(EServer , 25, "login", new NetworkCredential(EUser, EPwd));
                //smtp.Credentials = myCache;
                smtp.Credentials = new NetworkCredential(User, Pwd);
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;

                MailMessage mm = new MailMessage();

                mm.From = new MailAddress(From, User);

                string[] toArr = to.Split(';');
                foreach (string toStr in toArr)
                {
                    if (toStr.Trim() != "")
                    {
                        mm.To.Add(new MailAddress(toStr));
                    }
                }
                if (cc != "")
                {
                    string[] ccArr = cc.Split(';');
                    foreach (string ccStr in ccArr)
                    {
                        if (ccStr.Trim() != "")
                        {
                            mm.CC.Add(new MailAddress(ccStr));
                        }
                    }
                }

                mm.Subject = tiltle;
                mm.Body = content;
                mm.IsBodyHtml = true;
                mm.BodyEncoding = System.Text.Encoding.GetEncoding("UTF-8");
                mm.SubjectEncoding = System.Text.Encoding.GetEncoding("UTF-8");
                mm.HeadersEncoding = System.Text.Encoding.GetEncoding("UTF-8");
                //添加附件
                if (att != null && att != "")
                {
                    string[] attchstr = att.Split(';');
                    foreach (string attch in attchstr)
                    {
                        if (attch.Trim() != "")
                        {
                            if (File.Exists(attch))
                            {
                                FileInfo fi = new FileInfo(attch);
                                FileStream fs = new FileStream(attch, FileMode.Open, FileAccess.Read);
                                //StreamReader s = new StreamReader(fs);

                                Attachment attachment = new Attachment(fs, fi.Name);//, MediaTypeNames.Application.Octet);
                                //attachment.TransferEncoding = System.Net.Mime.TransferEncoding.SevenBit ;
                                attachment.NameEncoding = System.Text.Encoding.UTF8;
                                string sName = attachment.Name;
                                //Attachment attachment = new Attachment(attch, MediaTypeNames.Application.Octet);
                                mm.Attachments.Add(attachment);
                            }

                        }

                    }

                }
                //smtp.SendCompleted += new SendCompletedEventHandler(smtp_SendCompleted);
                //smtp.SendAsync(mm,mm.Subject);
                smtp.Send(mm);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        static void smtp_SendCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            string mailTitle = e.UserState.ToString();
            if (e.Cancelled)
                throw new Exception(string.Format("已经取消发送主题为：{0}的邮件！", mailTitle));
            if (e.Error != null)
                throw new Exception(string.Format("发送主题为：{0}的邮件出现错误：{1}！", mailTitle, e.Error.Message));
            else
                throw new Exception(string.Format("已经成功发送主题为：{0}的邮件！", mailTitle));
        }

        private static string ToBase64(string instr)
        {
            byte[] bt = Encoding.GetEncoding("ASCII").GetBytes(instr);
            return Convert.ToBase64String(bt);
        }
    }
}
