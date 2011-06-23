using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

namespace hwj.CommonLibrary.WebSite
{
    public class LogHelper : Object.Base.LogHelper
    {
        public LogHelper(string fileName)
        {
            base.Initialization(fileName);
        }

        private string GetWebInfo(string log, HttpRequest request)
        {
            WebLogInfo w = new WebLogInfo(request);
            StringBuilder sb = new StringBuilder();
            sb.Append(w.HTML);
            sb.AppendLine();
            sb.Append(log);
            return sb.ToString();
        }
        public void Info(string log, Exception ex, string EmailSubject, bool sendEmail)
        {
            Info(log, ex, EmailSubject, HttpContext.Current.Request, sendEmail);
        }
        public void Info(string log, Exception ex, string EmailSubject, HttpRequest request, bool sendEmail)
        {
            base.InfoAction(GetWebInfo(log, request), ex, EmailSubject, sendEmail);
        }

        public void Error(string log, Exception ex, string EmailSubject)
        {
            Error(log, ex, EmailSubject, HttpContext.Current.Request);
        }
        public void Error(string log, Exception ex, string EmailSubject, HttpRequest request)
        {
            base.ErrorAction(GetWebInfo(log, request), ex, EmailSubject);
        }

        public void Warn(string log, Exception ex, string EmailSubject, bool sendEmail)
        {
            Warn(log, ex, EmailSubject, HttpContext.Current.Request, sendEmail);
        }
        public void Warn(string log, Exception ex, string EmailSubject, HttpRequest request, bool sendEmail)
        {
            base.WarnAction(GetWebInfo(log, request), ex, EmailSubject, sendEmail);
        }
    }
}
