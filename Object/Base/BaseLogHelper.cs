using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using log4net;
using hwj.CommonLibrary.Object;

namespace hwj.CommonLibrary.Object.Base
{
    public abstract class LogHelper
    {
        #region Property
        private ILog LogInfo { get; set; }
        private ILog LogError { get; set; }
        private ILog LogWarn { get; set; }
        public string EmailTo { get; set; }
        public string EmailFrom { get; set; }
        public string EmailPassword { get; set; }
        public string EmailBodyFormat { get; set; }
        public bool EmailOpened { get; set; }
        public string EmailSMTPServer { get; set; }
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
            EmailBodyFormat = "{0}\r\n{1}\r\n{2}";
        }

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
            if (LogInfo.IsInfoEnabled)
            {
                if (ex == null)
                    LogInfo.Info(log);
                else
                    LogInfo.Info(log, ex);

                if (sendEmail)
                    Email(EmailSubject + " <Info>", log, ex);
            }
        }

        public void Error(string log, Exception ex)
        {
            ErrorAction(log, ex, null);
        }
        public void ErrorAction(string log, Exception ex, string EmailSubject)
        {
            if (LogError.IsErrorEnabled)
            {
                LogError.Error(log, ex);
                Email(EmailSubject + " <Error>", log, ex);
            }
        }

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
            if (LogWarn.IsWarnEnabled)
            {
                if (ex == null)
                    LogWarn.Warn(log);
                else
                    LogWarn.Warn(log, ex);
                Email(EmailSubject + " <Warn>", log, ex);
            }
        }

        public void Email(string subject, string log, Exception ex)
        {
            if (!string.IsNullOrEmpty(subject))
            {
                if (ex == null)
                    Email(subject, log);
                else
                    Email(subject, string.Format(EmailBodyFormat, log, ex.Message, ex.StackTrace.ToString()));
            }
        }
        public void Email(string subject, string body)
        {
            if (EmailOpened)
            {
                try
                {
                    EmailHelper.Send(EmailSMTPServer, EmailFrom, EmailPassword, EmailTo, subject, body, false);
                }
                catch
                {
                    EmailHelper.Send(EmailSMTPServer, EmailFrom, EmailPassword, EmailTo, subject, body, false);
                }
            }
        }
    }
}
