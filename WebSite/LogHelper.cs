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

        private string GetWebInfo(string log, WebLogInfo webLogInfo)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(webLogInfo.HTML);
            sb.AppendLine();
            sb.Append(log);
            return sb.ToString();
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

        public void InfoWithoutEmail(string log, Exception ex, WebLogInfo webLogInfo)
        {
            base.InfoWithoutEmail(GetWebInfo(log, webLogInfo), ex);
        }
        public void InfoWithEmail(string log, Exception ex, string emailSubject)
        {
            InfoWithEmail(log, ex, emailSubject, HttpContext.Current.Request);
        }
        public void InfoWithEmail(string log, Exception ex, string emailSubject, WebLogInfo webLogInfo)
        {
            base.InfoWithEmail(GetWebInfo(log, webLogInfo), ex, emailSubject);
        }
        public void InfoWithEmail(string log, Exception ex, string emailSubject, HttpRequest request)
        {
            base.InfoWithEmail(GetWebInfo(log, request), ex, emailSubject);
        }

        public void WarnWithoutEmail(string log, Exception ex, WebLogInfo webLogInfo)
        {
            base.WarnWithoutEmail(GetWebInfo(log, webLogInfo), ex);
        }
        public void WarnWithEmail(string log, Exception ex, string emailSubject)
        {
            WarnWithEmail(log, ex, emailSubject, HttpContext.Current.Request);
        }
        public void WarnWithEmail(string log, Exception ex, string emailSubject, WebLogInfo webLogInfo)
        {
            base.WarnWithEmail(GetWebInfo(log, webLogInfo), ex, emailSubject);
        }
        public void WarnWithEmail(string log, Exception ex, string emailSubject, HttpRequest request)
        {
            base.WarnWithEmail(GetWebInfo(log, request), ex, emailSubject);
        }

        public void ErrorWithoutEmail(string log, Exception ex, WebLogInfo webLogInfo)
        {
            base.ErrorWithoutEmail(GetWebInfo(log, webLogInfo), ex);
        }
        public void ErrorWithEmail(string log, Exception ex, string emailSubject)
        {
            ErrorWithEmail(log, ex, emailSubject, HttpContext.Current.Request);
        }
        public void ErrorWithEmail(string log, Exception ex, string emailSubject, HttpRequest request)
        {
            base.ErrorWithEmail(GetWebInfo(log, request), ex, emailSubject);
        }
        public void ErrorWithEmail(string log, Exception ex, string emailSubject, WebLogInfo webLogInfo)
        {
            base.ErrorWithEmail(GetWebInfo(log, webLogInfo), ex, emailSubject);
        }

    }
}
