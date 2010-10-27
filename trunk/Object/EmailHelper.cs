using System;
using System.Collections.Generic;
using System.Net.Mail;
using hwj.CommonLibrary.Object.Email;

namespace hwj.CommonLibrary.Object
{
    public class EmailHelper
    {
        /// <summary>
        /// 发送电子邮件
        /// </summary>
        /// <param name="smtpServer">SMTP服务器</param>
        /// <param name="emailFrom">电子邮件的发件人地址</param>
        /// <param name="emailFromPassword">发件人密码（如果该密码为空，则取消验证发件人身份）</param>
        /// <param name="emailTo">收件人的地址</param>
        /// <param name="cc">抄送 (CC) 收件人的地址</param>
        /// <param name="subject">电子邮件的主题行</param>
        /// <param name="body">邮件正文</param>
        /// <param name="isBodyHtml">邮件正文是否为 Html 格式的值</param>
        /// <returns></returns>
        public static bool Send(string smtpServer, string emailFrom, string emailFromPassword, string emailTo, string cc, string subject, string body, bool isBodyHtml)
        {
            return Send(smtpServer, emailFrom, emailFromPassword, emailTo, cc, subject, body, isBodyHtml, null);
        }
        /// <summary>
        /// 发送电子邮件
        /// </summary>
        /// <param name="smtpServer">SMTP服务器</param>
        /// <param name="emailFrom">电子邮件的发件人地址</param>
        /// <param name="emailFromPassword">发件人密码（如果该密码为空，则取消验证发件人身份）</param>
        /// <param name="emailTo">收件人的地址</param>
        /// <param name="cc">抄送 (CC) 收件人的地址</param>
        /// <param name="subject">电子邮件的主题行</param>
        /// <param name="body">邮件正文</param>
        /// <param name="isBodyHtml">邮件正文是否为 Html 格式的值</param>
        /// <param name="attachments">此电子邮件的数据的附件集合</param>
        /// <returns></returns>
        public static bool Send(string smtpServer, string emailFrom, string emailFromPassword, string emailTo, string cc, string subject, string body, bool isBodyHtml, List<Attachment> attachments)
        {
            return Send(smtpServer, emailFrom, emailFromPassword, new string[] { emailTo }, new string[] { cc }, subject, body, isBodyHtml, attachments);
        }

        /// <summary>
        /// 发送含附件的电子邮件
        /// </summary>
        /// <param name="smtpServer">SMTP服务器</param>
        /// <param name="emailFrom">电子邮件的发件人地址</param>
        /// <param name="emailFromPassword">发件人密码（如果该密码为空，则取消验证发件人身份）</param>
        /// <param name="emailTo">收件人的地址集合</param>
        /// <param name="cc">抄送 (CC) 收件人的地址集合</param>
        /// <param name="subject">电子邮件的主题行</param>
        /// <param name="body">邮件正文</param>
        /// <param name="isBodyHtml">邮件正文是否为 Html 格式的值</param>
        /// <param name="attachments">此电子邮件的数据的附件集合</param>
        /// <returns></returns>
        public static bool Send(string smtpServer, string emailFrom, string emailFromPassword, string[] emailTo, string[] cc, string subject, string body, bool isBodyHtml, List<Attachment> attachments)
        {
            SMTPInfos smtpInfos = new SMTPInfos(smtpServer, emailFrom, emailFromPassword);
            return Send(emailTo, cc, subject, body, isBodyHtml, attachments, ref smtpInfos);
        }
        /// <summary>
        /// 发送含附件的电子邮件（可压缩附件）
        /// </summary>
        /// <param name="smtpServer">SMTP服务器</param>
        /// <param name="emailFrom">电子邮件的发件人地址</param>
        /// <param name="emailFromPassword">发件人密码（如果该密码为空，则取消验证发件人身份）</param>
        /// <param name="emailTo">收件人的地址集合</param>
        /// <param name="cc">抄送 (CC) 收件人的地址集合</param>
        /// <param name="subject">电子邮件的主题行</param>
        /// <param name="body">邮件正文</param>
        /// <param name="isBodyHtml">邮件正文是否为 Html 格式的值</param>
        /// <param name="streams">此电子邮件的数据的附件文件流的集合</param>
        /// <returns></returns>
        public static bool Send(string smtpServer, string emailFrom, string emailFromPassword, string[] emailTo, string[] cc, string subject, string body, bool isBodyHtml, List<StreamFile> streams)
        {
            SMTPInfos smtpInfos = new SMTPInfos(smtpServer, emailFrom, emailFromPassword);
            return Send(emailTo, cc, subject, body, isBodyHtml, streams, ref smtpInfos);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="smtpServer"></param>
        /// <param name="emailFrom"></param>
        /// <param name="emailFromPassword"></param>
        /// <param name="message"></param>
        /// <param name="isBodyHtml"></param>
        /// <returns></returns>
        public static bool Send(string smtpServer, string emailFrom, string emailFromPassword, MailMessage message, bool isBodyHtml)
        {
            SMTPInfos smtpInfos = new SMTPInfos(smtpServer, emailFrom, emailFromPassword);
            return Send(message, isBodyHtml, ref smtpInfos);
        }

        /// <summary>
        /// 发送电子邮件
        /// </summary>
        /// <param name="emailTo">收件人的地址</param>
        /// <param name="cc">抄送 (CC) 收件人的地址</param>
        /// <param name="subject">电子邮件的主题行</param>
        /// <param name="body">邮件正文</param>
        /// <param name="isBodyHtml">邮件正文是否为 Html 格式的值</param>
        /// <param name="smtpInfos">SMTP服务器集合</param>
        /// <returns></returns>
        public static bool Send(string emailTo, string cc, string subject, string body, bool isBodyHtml, ref SMTPInfos smtpInfos)
        {
            return Send(emailTo, cc, subject, body, isBodyHtml, null, ref smtpInfos);
        }
        /// <summary>
        /// 发送含附件的电子邮件
        /// </summary>
        /// <param name="emailTo">收件人的地址</param>
        /// <param name="cc">抄送 (CC) 收件人的地址</param>
        /// <param name="subject">电子邮件的主题行</param>
        /// <param name="body">邮件正文</param>
        /// <param name="isBodyHtml">邮件正文是否为 Html 格式的值</param>
        /// <param name="attachments">此电子邮件的数据的附件集合</param>
        /// <param name="smtpInfos">SMTP服务器集合</param>
        /// <returns></returns>
        public static bool Send(string emailTo, string cc, string subject, string body, bool isBodyHtml, List<Attachment> attachments, ref SMTPInfos smtpInfos)
        {
            return Send(new string[] { emailTo }, new string[] { cc }, subject, body, isBodyHtml, attachments, ref  smtpInfos);
        }

        /// <summary>
        /// 发送含附件的电子邮件（可压缩附件）
        /// </summary>
        /// <param name="emailTo">收件人的地址集合</param>
        /// <param name="cc">抄送 (CC) 收件人的地址集合</param>
        /// <param name="subject">电子邮件的主题行</param>
        /// <param name="body">邮件正文</param>
        /// <param name="isBodyHtml">邮件正文是否为 Html 格式的值</param>
        /// <param name="streams">此电子邮件的数据的附件文件流的集合</param>
        /// <param name="smtpInfos">SMTP服务器集合</param>
        /// <returns></returns>
        public static bool Send(string[] emailTo, string[] cc, string subject, string body, bool isBodyHtml, List<StreamFile> streams, ref SMTPInfos smtpInfos)
        {
            List<Attachment> attachments = new List<Attachment>();

            string HTML = @"<br><br>To uncompress the file you may need some software else<a href=http://www.winrar.com/>winrar</a>/<a href=http://www.7-zip.org/>7-zip</a>";
            string STR = @"\r\n\r\nTo uncompress the file you may need some software else winrar:http://www.winrar.com/ or 7-zip http://www.7-zip.org/";

            bool usedGzip = false;

            if (streams != null && streams.Count > 0)
            {
                foreach (StreamFile s in streams)
                {
                    if (s.UseGzip)
                    {
                        attachments.Add(new Attachment(hwj.CommonLibrary.Object.FileHelper.Stream2GzipStream(s.InStream), s.FileName + ".gz"));
                        usedGzip = true;
                    }
                    else
                        attachments.Add(new Attachment(s.InStream, s.FileName));
                }
            }
            if (usedGzip)
                body += isBodyHtml ? HTML : STR;
            return Send(emailTo, cc, subject, body, isBodyHtml, attachments, ref  smtpInfos);
        }
        /// <summary>
        /// 发送含附件的电子邮件
        /// </summary>
        /// <param name="emailTo">>收件人的地址集合</param>
        /// <param name="cc">抄送 (CC) 收件人的地址集合</param>
        /// <param name="subject">电子邮件的主题行</param>
        /// <param name="body">邮件正文</param>
        /// <param name="isBodyHtml">邮件正文是否为 Html 格式的值</param>
        /// <param name="attachments">此电子邮件的数据的附件集合</param>
        /// <param name="smtpInfos">SMTP服务器集合</param>
        /// <returns></returns>
        public static bool Send(string[] emailTo, string[] cc, string subject, string body, bool isBodyHtml, List<Attachment> attachments, ref SMTPInfos smtpInfos)
        {
            MailMessage message = new MailMessage();
            //message.From = new MailAddress(emailFrom);
            message.Subject = subject;
            message.Body = body;

            if (emailTo != null)
            {
                foreach (string to in emailTo)
                {
                    if (!string.IsNullOrEmpty(to))
                        message.To.Add(to);
                }
            }
            if (cc != null)
            {
                foreach (string c in cc)
                {
                    if (!string.IsNullOrEmpty(c))
                        message.CC.Add(c);
                }
            }

            if (attachments != null && attachments.Count > 0)
            {
                foreach (Attachment a in attachments)
                {
                    message.Attachments.Add(a);
                }
            }
            message.IsBodyHtml = isBodyHtml;
            return Send(message, isBodyHtml, ref smtpInfos);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="isBodyHtml">邮件正文是否为 Html 格式的值</param>
        /// <param name="smtpInfos">SMTP服务器集合</param>
        /// <returns></returns>
        public static bool Send(MailMessage message, bool isBodyHtml, ref SMTPInfos smtpInfos)
        {
            SMTPInfos streamlineList = new SMTPInfos();
            foreach (SMTPInfo smtpInfo in smtpInfos)
            {
                if (smtpInfo != null && smtpInfo.Active && !string.IsNullOrEmpty(smtpInfo.SmtpServer) && !string.IsNullOrEmpty(smtpInfo.EmailFrom) && smtpInfo.LastFailDate.Date < DateTime.Now.Date)
                {
                    streamlineList.Add(smtpInfo);
                }
            }
            streamlineList.Sort(new Email.SMTPInfoComparer());
            foreach (SMTPInfo smtpInfo in streamlineList)
            {
                try
                {
                    SmtpClient client = new SmtpClient(smtpInfo.SmtpServer);
                    if (!string.IsNullOrEmpty(smtpInfo.EmailFromPassword))
                    {
                        client.UseDefaultCredentials = false;
                        client.Credentials = new System.Net.NetworkCredential(smtpInfo.EmailFrom, smtpInfo.EmailFromPassword);
                    }
                    else
                        client.UseDefaultCredentials = true;
                    client.DeliveryMethod = SmtpDeliveryMethod.Network;

                    message.From = new MailAddress(smtpInfo.EmailFrom);
                    message.Priority = MailPriority.Normal;
                    message.BodyEncoding = System.Text.Encoding.UTF8;
                    message.IsBodyHtml = isBodyHtml;
                    client.Send(message);
                    return true;
                }
                catch (Exception ex)
                {
                    smtpInfo.LastFailDate = DateTime.Now;
                    smtpInfo.Exception = ex;

                    if (smtpInfo.Equals(streamlineList[streamlineList.Count - 1]))
                        throw;
                }
            }
            return false;
        }

        public static bool isValidEmail(string xEmailAddress)
        {
            bool myIsEmail = false;
            string myRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
            System.Text.RegularExpressions.Regex reg = new System.Text.RegularExpressions.Regex(myRegex);
            if (reg.IsMatch(xEmailAddress))
            {
                myIsEmail = true;
            }
            return myIsEmail;
        }

    }

}
