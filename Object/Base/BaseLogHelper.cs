using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Net.Mail;
using System.Text;
using hwj.CommonLibrary.Object;
using hwj.CommonLibrary.Object.Email;
using log4net;

namespace hwj.CommonLibrary.Object.Base
{
    public abstract class LogHelper
    {
        #region Property
        private ILog LogInfo { get; set; }
        private ILog LogError { get; set; }
        private ILog LogWarn { get; set; }

        /// <summary>
        /// 是否抛出SmtpList异常
        /// </summary>
        public bool ShowInvalidSmtpError { get; set; }

        public bool EmailOpened { get; set; }

        /// <summary>
        /// 多个收件人请用逗号分隔
        /// </summary>
        public string EmailTo { get; set; }
        /// <summary>
        /// 多个抄送人请用逗号分隔
        /// </summary>
        public string EmailCC { get; set; }
        internal string EmailBodyFormat { get; private set; }
        /// <summary>
        /// 获取或设置是否使用多个SMTP服务器的模式
        /// </summary>
        public bool MultSmtpEnabled { get; set; }
        /// <summary>
        /// 获取或设置SMTP服务器列表
        /// </summary>
        public Email.SmtpInfoList SmtpList { get; set; }

        public string Subject { get; set; }
        public string Body { get; set; }
        #endregion

        public LogHelper()
        {
            ShowInvalidSmtpError = true;
            //log4net.Config.DOMConfigurator.Configure(new FileInfo("LogHelper.Config"));
            //Init();
        }
        //public LogHelper(string fileName)
        //{
        //    ShowInvalidSmtpError = true;
        //    //log4net.Config.DOMConfigurator.Configure(new FileInfo(fileName));
        //    //Init();
        //}
        public void Initialization(string fileName)
        {
            if (!string.IsNullOrEmpty(fileName))
                log4net.Config.DOMConfigurator.Configure(new FileInfo(fileName));
            else
                log4net.Config.DOMConfigurator.Configure(new FileInfo("LogHelper.Config"));
            LogInfo = log4net.LogManager.GetLogger("loginfo");
            LogError = log4net.LogManager.GetLogger("logerror");
            LogWarn = log4net.LogManager.GetLogger("logwarn");

            EmailOpened = false;
            EmailTo = "vinsonhwj@gmail.com";
            //EmailFrom = "hpmis@sohu.com";
            //EmailSMTPServer = "smtp.sohu.com";
            //EmailPassword = "123456";
            EmailBodyFormat = "{0}\r\n{1}";
        }

        #region Smtp Function
        public void SetSingleSmtp(string smtpServer, string emailFrom, string emailFromPassword)
        {
            SmtpList = new SmtpInfoList(smtpServer, emailFrom, emailFromPassword);
        }
        #endregion

        #region Info Function
        public void Info(string log, bool sendEmail)
        {
            InfoAction(log, null, null, sendEmail);
        }
        public void Info(string log, string EmailSubject, bool sendEmail)
        {
            InfoAction(log, null, EmailSubject, sendEmail);
        }
        public void Info(string log, Exception ex, bool sendEmail)
        {
            InfoAction(log, ex, null, sendEmail);
        }
        public void InfoAction(string log, Exception ex, string EmailSubject, bool sendEmail)
        {
            InfoAction(log, ex, EmailSubject, sendEmail, EmailTo, EmailCC);
        }
        public void InfoAction(string log, Exception ex, string EmailSubject, bool sendEmail, string emailTo, string emailCC)
        {
            InfoAction(log, ex, EmailSubject, sendEmail, emailTo, emailCC, null);
        }
        public void InfoAction(string log, Exception ex, string EmailSubject, bool sendEmail, string emailTo, string emailCC, List<Attachment> attachments)
        {
            if (LogInfo.IsInfoEnabled)
            {
                if (ex == null)
                    LogInfo.Info(log);
                else
                    LogInfo.Info(log, ex);

                if (sendEmail)
                    Email(EmailSubject + " <Info>", log, ex, EmailTo, EmailCC, attachments);
            }
        }

        #endregion

        #region Error Function
        public void Error(string log, Exception ex)
        {
            ErrorAction(log, ex, null, EmailTo, EmailCC);
        }
        public void Error(string log, Exception ex, bool sendEmail)
        {
            if (sendEmail)
                ErrorAction(log, ex, null, EmailTo, EmailCC);
            else
                ErrorAction(log, ex, false, null, null, null, null);
        }
        public void ErrorAction(string log, Exception ex, string EmailSubject)
        {
            ErrorAction(log, ex, EmailSubject, EmailTo, EmailCC);
        }
        public void ErrorAction(string log, Exception ex, string EmailSubject, string emailTo, string emailCC)
        {
            ErrorAction(log, ex, EmailSubject, emailTo, emailCC, null);
        }
        public void ErrorAction(string log, Exception ex, string EmailSubject, string emailTo, string emailCC, List<Attachment> attachments)
        {
            ErrorAction(log, ex, true, EmailSubject, emailTo, emailCC, attachments);
        }
        private void ErrorAction(string log, Exception ex, bool sendEmail, string EmailSubject, string emailTo, string emailCC, List<Attachment> attachments)
        {
            if (LogError.IsErrorEnabled)
            {
                LogError.Error(log, ex);
                if (sendEmail)
                    Email(EmailSubject + " <Error>", log, ex, emailTo, emailCC, attachments);
            }
        }
        #endregion

        #region Warn Function
        public void Warn(string log)
        {
            WarnAction(log, null, null);
        }
        public void Warn(string log, bool sendEmail)
        {
            Warn(log, null, sendEmail);
        }
        public void Warn(string log, Exception ex)
        {
            WarnAction(log, ex, null);
        }
        public void Warn(string log, Exception ex, bool sendEmail)
        {
            if (sendEmail)
                WarnAction(log, ex, null);
            else
                WarnAction(log, ex, false, null, null, null, null);
        }



        public void Warn(string log, string emailSubject)
        {
            WarnAction(log, null, emailSubject);
        }
        public void WarnAction(string log, Exception ex, string EmailSubject)
        {
            WarnAction(log, ex, EmailSubject, EmailTo, EmailCC);
        }
        public void WarnAction(string log, Exception ex, string EmailSubject, string emailTo, string emailCC)
        {
            WarnAction(log, ex, EmailSubject, emailTo, emailCC, null);
        }
        public void WarnAction(string log, Exception ex, string EmailSubject, string emailTo, string emailCC, List<Attachment> attachments)
        {
            WarnAction(log, ex, true, EmailSubject, emailTo, emailCC, attachments);
        }
        private void WarnAction(string log, Exception ex, bool sendEmail, string EmailSubject, string emailTo, string emailCC, List<Attachment> attachments)
        {
            if (LogWarn.IsWarnEnabled)
            {
                if (ex == null)
                    LogWarn.Warn(log);
                else
                    LogWarn.Warn(log, ex);
                if (sendEmail)
                    Email(EmailSubject + " <Warn>", log, ex, emailTo, emailCC, attachments);
            }
        }

        #endregion

        #region Email Function
        public void Email(string subject, string log, Exception ex, string emailTo, string emailCC)
        {
            Email(subject, log, ex, emailTo, emailCC, null);
        }
        public void Email(string subject, string log, Exception ex, string emailTo, string emailCC, List<Attachment> attachments)
        {
            if (!string.IsNullOrEmpty(subject))
            {
                if (ex == null)
                    Email(subject, log, emailTo, emailCC, attachments);
                else
                    Email(subject, string.Format(EmailBodyFormat, log, FormatException(ex)), emailTo, emailCC, attachments);
            }
        }
        public void Email(string subject, string body, string emailTo, string emailCC)
        {
            Email(subject, body, emailTo, emailCC, null);
        }
        public void Email(string subject, string body, string emailTo, string emailCC, List<Attachment> attachments)
        {
            Subject = subject;
            Body = body;
            if (EmailOpened)
            {
                if (SmtpList != null && SmtpList.Count > 0)
                {
                    if (MultSmtpEnabled)
                    {
                        SmtpInfoList smtpList = SmtpList;
                        EmailHelper.Send(emailTo, emailCC, subject, body, false, attachments, ref smtpList);
                        SmtpList = smtpList;
                    }
                    else
                    {
                        try
                        {
                            EmailHelper.Send(SmtpList[0].SmtpServer, SmtpList[0].UserName, SmtpList[0].Password, emailTo, emailCC, subject, body, false, attachments);
                        }
                        catch
                        {
                            try
                            {
                                EmailHelper.Send(SmtpList[0].SmtpServer, SmtpList[0].UserName, SmtpList[0].Password, emailTo, emailCC, subject, body, false, attachments);
                            }
                            catch (Exception ex)
                            {
                                LogError.Error(ex.Message);
                            }
                        }
                    }
                }
                else
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append("<<Invaild SmtpList>> Email Body Content:");
                    sb.AppendLine();
                    sb.Append(body);
                    LogWarn.Warn(sb.ToString());

                    if (ShowInvalidSmtpError)
                        throw new Exception("Invaild SmtpList");
                }
            }
        }

        private string FormatException(Exception ex)
        {
            StringBuilder sb = new StringBuilder();

            if (ex.Data != null && ex.Data.Count > 0)
            {
                sb.AppendLine("----------------Exception.Data List----------------");
                foreach (DictionaryEntry obj in ex.Data)
                {
                    try
                    {
                        sb.AppendLine(string.Format("Key:[{0}]  Value:[{1}]", obj.Key, obj.Value));
                    }
                    catch
                    {
                        sb.AppendLine("Exception.Data Error");
                    }
                }
            }
            if (ex.GetBaseException() != null)
            {
                sb.AppendLine();
                sb.AppendLine("----------------Base Exception----------------");
                sb.AppendLine(ex.GetBaseException().Message);
                sb.AppendLine(ex.GetBaseException().StackTrace);
            }
            sb.AppendLine();
            sb.AppendLine("----------------Current Exception----------------");
            sb.AppendLine(ex.Message);
            sb.AppendLine(ex.StackTrace);

            return sb.ToString();
        }
        #endregion
    }
}
