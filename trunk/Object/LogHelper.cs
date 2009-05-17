using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using log4net;
namespace hwj.CommonLibrary.Object
{
    public class LogHelper
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
            log4net.Config.DOMConfigurator.Configure(new FileInfo("LogHelper.Config"));
            Init();
        }
        public LogHelper(string fileName)
        {
            log4net.Config.DOMConfigurator.Configure(new FileInfo(fileName));
            Init();
        }
        private void Init()
        {
            LogInfo = log4net.LogManager.GetLogger("loginfo");
            LogError = log4net.LogManager.GetLogger("logerror");
            LogWarn = log4net.LogManager.GetLogger("logwarn");

            EmailOpened = false;
            EmailTo = "vinsonhwj@gmail.com";
            EmailFrom = "hpmis@sohu.com";
            EmailSMTPServer = "smtp.sohu.com";
            EmailPassword = "123456";
            EmailBodyFormat = "<b>{0}</b><br><b>{1}</b><br>{2}";
        }

        public void Info(string log)
        {
            Info(log, null, null);
        }
        public void Info(string log, string EmailSubject)
        {
            Info(log, null, EmailSubject);
        }
        public void Info(string log, Exception ex)
        {
            Info(log, ex, null);
        }
        public void Info(string log, Exception ex, string EmailSubject)
        {
            if (LogInfo.IsInfoEnabled)
            {
                if (ex == null)
                    LogInfo.Info(log);
                else
                    LogInfo.Info(log, ex);
                Email(EmailSubject + " <Info>", log, ex);
            }
        }

        public void Error(string log, Exception ex)
        {
            Error(log, ex, null);
        }
        public void Error(string log, Exception ex, string EmailSubject)
        {
            if (LogError.IsErrorEnabled)
            {
                LogError.Error(log, ex);
                Email(EmailSubject + " <Error Info>", log, ex);
            }
        }

        public void Warn(string log)
        {
            Warn(log, null, null);
        }
        public void Warn(string log, string EmailSubject)
        {
            Warn(log, null, EmailSubject);
        }
        public void Warn(string log, Exception ex)
        {
            Warn(log, ex, null);
        }
        public void Warn(string log, Exception ex, string EmailSubject)
        {
            if (LogWarn.IsWarnEnabled)
            {
                if (ex == null)
                    LogWarn.Warn(log);
                else
                    LogWarn.Warn(log, ex);
                Email(EmailSubject + " <Warn Info>", log, ex);
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
                    EmailHelper.Send(EmailSMTPServer, EmailFrom, EmailPassword, EmailTo, subject, body);
                }
                catch
                {
                    EmailHelper.Send(EmailSMTPServer, EmailFrom, EmailPassword, EmailTo, subject, body);
                }
            }
        }
    }
}
