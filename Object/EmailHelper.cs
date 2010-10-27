using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.IO;

namespace hwj.CommonLibrary.Object
{
    public class EmailHelper
    {
        public static bool Send(string smtpServer, string emailFrom, string emailFromPassword, string emailTo, string cc, string subject, string body, bool isBodyHtml)
        {
            return Send(smtpServer, emailFrom, emailFromPassword, emailTo, cc, subject, body, isBodyHtml, null);
        }
        public static bool Send(string smtpServer, string emailFrom, string emailFromPassword, string emailTo, string cc, string subject, string body, bool isBodyHtml, List<Attachment> attachments)
        {
            return Send(smtpServer, emailFrom, emailFromPassword, new string[] { emailTo }, new string[] { cc }, subject, body, isBodyHtml, attachments);
        }
        public static bool Send(string smtpServer, string emailFrom, string emailFromPassword, string[] emailTo, string[] cc, string subject, string body, bool isBodyHtml, List<Attachment> attachments)
        {
            SmtpInfos smtpInfos = TransToList(smtpServer, emailFrom, emailFromPassword);
            return Send(emailTo, cc, subject, body, isBodyHtml, attachments, ref smtpInfos);
        }
        public static bool Send(string smtpServer, string emailFrom, string emailFromPassword, string[] emailTo, string[] cc, string subject, string body, bool isBodyHtml, List<StreamFile> streams, bool useGzip)
        {
            SmtpInfos smtpInfos = TransToList(smtpServer, emailFrom, emailFromPassword);
            return Send(emailTo, cc, subject, body, isBodyHtml, streams, useGzip, ref smtpInfos);
        }
        public static bool Send(string smtpServer, string emailFrom, string emailFromPassword, MailMessage message, bool isBodyHtml)
        {
            SmtpInfos smtpInfos = TransToList(smtpServer, emailFrom, emailFromPassword);
            return Send(message, isBodyHtml, ref smtpInfos);
        }

        public static bool Send(MailMessage message, bool isBodyHtml, ref SmtpInfos smtpInfos)
        {
            SmtpInfos streamlineList = new SmtpInfos();
            foreach (SmtpInfo smtpInfo in smtpInfos)
            {
                if (smtpInfo.Active && !string.IsNullOrEmpty(smtpInfo.SmtpServer) && !string.IsNullOrEmpty(smtpInfo.EmailFrom) && smtpInfo.LastFailDate.Date < DateTime.Now.Date)
                {
                    streamlineList.Add(smtpInfo);
                }
            }
            streamlineList.Sort(new SmtpInfoComparer());
            foreach (SmtpInfo smtpInfo in streamlineList)
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
        public static bool Send(string[] emailTo, string[] cc, string subject, string body, bool isBodyHtml, List<Attachment> attachments, ref SmtpInfos smtpInfos)
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
        public static bool Send(string[] emailTo, string[] cc, string subject, string body, bool isBodyHtml, List<StreamFile> streams, bool useGzip, ref SmtpInfos smtpInfos)
        {
            List<Attachment> attachments = new List<Attachment>();

            if (streams != null && streams.Count > 0)
            {
                foreach (StreamFile s in streams)
                {
                    if (useGzip)
                        attachments.Add(new Attachment(hwj.CommonLibrary.Object.FileHelper.Stream2GzipStream(s.InStream), s.FileName + ".gz"));
                    else
                        attachments.Add(new Attachment(s.InStream, s.FileName));
                }
            }
            return Send(emailTo, cc, subject, body, isBodyHtml, attachments, ref  smtpInfos);
        }
        public static bool Send(string emailTo, string cc, string subject, string body, bool isBodyHtml, List<Attachment> attachments, ref SmtpInfos smtpInfos)
        {
            return Send(new string[] { emailTo }, new string[] { cc }, subject, body, isBodyHtml, attachments, ref  smtpInfos);
        }
        public static bool Send(string emailTo, string cc, string subject, string body, bool isBodyHtml, ref SmtpInfos smtpInfos)
        {
            return Send(emailTo, cc, subject, body, isBodyHtml, null, ref smtpInfos);
        }
        public static SmtpInfos TransToList(string smtpServer, string emailFrom, string emailFromPassword)
        {
            SmtpInfo smtpInfo = new SmtpInfo();
            smtpInfo.SmtpServer = smtpServer;
            smtpInfo.EmailFrom = emailFrom;
            smtpInfo.EmailFromPassword = emailFromPassword;
            smtpInfo.Active = true;
            smtpInfo.LastFailDate = DateTime.MinValue;
            SmtpInfos smtpInfos = new SmtpInfos();
            smtpInfos.Add(smtpInfo);
            return smtpInfos;
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

        public class SmtpInfoComparer : IComparer<SmtpInfo>
        {
            public int Compare(SmtpInfo x, SmtpInfo y)
            {
                if (x == null && y == null)
                    return 0;
                if (x.LastFailDate == DateTime.MinValue)
                {
                    if (y.LastFailDate == DateTime.MinValue)
                    {
                        return 0;
                    }
                    else
                    {
                        return -1;
                    }
                }
                else
                {
                    if (y.LastFailDate == DateTime.MinValue)
                    {
                        return 1;
                    }
                    else
                    {
                        return x.LastFailDate.CompareTo(y.LastFailDate);
                    }
                }
            }
        }
    }
    public class SmtpInfo
    {
        public bool Active = false;
        public string SmtpServer = string.Empty;
        public string EmailFrom = string.Empty;
        public string EmailFromPassword = string.Empty;
        public DateTime LastFailDate = DateTime.MinValue;
        public Exception Exception = null;
    }
    public class SmtpInfos : List<SmtpInfo> { }

    public class StreamFile
    {
        public Stream InStream;
        public string FileName = string.Empty;
    }
}
