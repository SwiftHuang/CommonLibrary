using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace hwj.CommonLibrary.WebSite
{
    public class LogHelper : Object.Base.LogHelper
    {
        public LogHelper(string fileName)
        {
            base.Initialization(fileName);
        }
        private string GetWebInfo(string log)
        {
            WebLogInfo w = new WebLogInfo();
            StringBuilder sb = new StringBuilder();
            sb.AppendLine();
            sb.Append(w.HTML);
            sb.AppendLine();
            sb.Append(log);
            return sb.ToString();
        }
        public void Info(string log, Exception ex, string EmailSubject)
        {
            base.InfoAction(GetWebInfo(log), ex, EmailSubject);
        }

        public void Error(string log, Exception ex, string EmailSubject)
        {
            base.ErrorAction(GetWebInfo(log), ex, EmailSubject);
        }

        public void Warn(string log, Exception ex, string EmailSubject)
        {
            base.WarnAction(GetWebInfo(log), ex, EmailSubject);
        }
    }
}
