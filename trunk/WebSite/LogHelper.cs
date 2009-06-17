using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace hwj.CommonLibrary.WebSite
{
    public class LogHelper : Base.LogHelper
    {
        public LogHelper(string fileName)
        {
            base.Initialization(fileName);
        }
        private const string fmt = "{0}<br>{1}";
        public void Info(string log, Exception ex, string EmailSubject)
        {
            WebLogInfo w = new WebLogInfo();
            base.InfoAction(string.Format(fmt, w.HTML, log), ex, EmailSubject);
        }

        public void Error(string log, Exception ex, string EmailSubject)
        {
            WebLogInfo w = new WebLogInfo();
            base.ErrorAction(string.Format(fmt, w.HTML, log), ex, EmailSubject);
        }

        public void Warn(string log, Exception ex, string EmailSubject)
        {
            WebLogInfo w = new WebLogInfo();
            base.WarnAction(string.Format(fmt, w.HTML, log), ex, EmailSubject);
        }
    }
}
