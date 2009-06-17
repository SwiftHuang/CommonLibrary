using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace hwj.CommonLibrary.Object
{
    public class LogHelper : Base.LogHelper
    {
        public LogHelper()
        {
            base.Initialization(null);
        }
        public void Info(string log, Exception ex, string EmailSubject)
        {
            base.InfoAction(log, ex, EmailSubject);
        }
        public void Warn(string log, Exception ex, string EmailSubject)
        {
            base.WarnAction(log, ex, EmailSubject);
        }
        public void Error(string log, Exception ex, string EmailSubject)
        {
            base.ErrorAction(log, ex, EmailSubject);
        }
    }
}
