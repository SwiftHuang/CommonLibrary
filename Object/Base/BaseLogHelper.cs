﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net.Mail;
using System.Text;
using hwj.CommonLibrary.Object;
using log4net;

namespace hwj.CommonLibrary.Object.Base
{
    public abstract class LogHelper
    {
        #region Property
        private ILog LogInfo { get; set; }
        private ILog LogError { get; set; }
        private ILog LogWarn { get; set; }

        public bool EmailOpened { get; set; }

        public string EmailFrom { get; set; }
        /// <summary>
        /// 多个收件人请用逗号分隔
        /// </summary>
        public string EmailTo { get; set; }
        /// <summary>
        /// 多个抄送人请用逗号分隔
        /// </summary>
        public string EmailCC { get; set; }
        internal string EmailBodyFormat { get; private set; }

        public string EmailPassword { get; set; }
        public string EmailSMTPServer { get; set; }

        public string Subject { get; set; }
        public string Body { get; set; }
        #endregion

        public LogHelper()
        {
            //log4net.Config.DOMConfigurator.Configure(new FileInfo("LogHelper.Config"));
            //Init();
        }
        public LogHelper(string fileName)
        {
            //log4net.Config.DOMConfigurator.Configure(new FileInfo(fileName));
            //Init();
        }
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
            EmailFrom = "hpmis@sohu.com";
            EmailSMTPServer = "smtp.sohu.com";
            EmailPassword = "123456";
            EmailBodyFormat = "{0}\r\n{1}";
        }

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
            if (LogInfo.IsInfoEnabled)
            {
                if (ex == null)
                    LogInfo.Info(log);
                else
                    LogInfo.Info(log, ex);

                if (sendEmail)
                    Email(EmailSubject + " <Info>", log, ex, EmailTo, EmailCC);
            }
        }
        #endregion

        #region Error Function
        public void Error(string log, Exception ex)
        {
            ErrorAction(log, ex, null, EmailTo, EmailCC);
        }
        public void ErrorAction(string log, Exception ex, string EmailSubject)
        {
            ErrorAction(log, ex, EmailSubject, EmailTo, EmailCC);
        }
        public void ErrorAction(string log, Exception ex, string EmailSubject, string emailTo, string emailCC)
        {
            if (LogError.IsErrorEnabled)
            {
                LogError.Error(log, ex);
                Email(EmailSubject + " <Error>", log, ex, emailTo, emailCC);
            }
        }
        #endregion

        #region Warn Function
        public void Warn(string log)
        {
            WarnAction(log, null, null);
        }
        public void Warn(string log, string EmailSubject)
        {
            WarnAction(log, null, EmailSubject);
        }
        public void Warn(string log, Exception ex)
        {
            WarnAction(log, ex, null);
        }
        public void WarnAction(string log, Exception ex, string EmailSubject)
        {
            WarnAction(log, ex, EmailSubject, EmailTo, EmailCC);
        }
        public void WarnAction(string log, Exception ex, string EmailSubject, string emailTo, string emailCC)
        {
            if (LogWarn.IsWarnEnabled)
            {
                if (ex == null)
                    LogWarn.Warn(log);
                else
                    LogWarn.Warn(log, ex);
                Email(EmailSubject + " <Warn>", log, ex, emailTo, emailCC);
            }
        }
        #endregion

        #region Email Function
        //public void Email(string subject, string log, Exception ex)
        //{
        //    Email(subject, log, ex);
        //}
        public void Email(string subject, string log, Exception ex, string emailTo, string emailCC)
        {
            if (!string.IsNullOrEmpty(subject))
            {
                if (ex == null)
                    Email(subject, log, emailTo, emailCC);
                else
                    Email(subject, string.Format(EmailBodyFormat, log, FormatException(ex)), emailTo, emailCC);
            }
        }
        //public void Email(string subject, string body)
        //{
        //    Email(subject, body, EmailTo, EmailCC);
        //}
        public void Email(string subject, string body, string emailTo, string emailCC)
        {
            Subject = subject;
            Body = body;
            if (EmailOpened)
            {
                try
                {
                    EmailHelper.Send(EmailSMTPServer, EmailFrom, EmailPassword, emailTo, emailCC, subject, body, false);
                }
                catch
                {
                    try
                    {
                        EmailHelper.Send(EmailSMTPServer, EmailFrom, EmailPassword, emailTo, emailCC, subject, body, false);
                    }
                    catch (Exception ex)
                    {
                        LogError.Error(ex.Message);
                    }
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