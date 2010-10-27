using System;
using System.Collections.Generic;
using System.Text;

namespace hwj.CommonLibrary.Object.Email
{
    public class SMTPInfo
    {
        public SMTPInfo() { }
        public SMTPInfo(string smtpServer, string emailFrom)
        {
            new SMTPInfo(smtpServer, emailFrom, string.Empty);
        }
        public SMTPInfo(string smtpServer, string emailFrom, string emailFromPassword)
        {
            this.Active = true;
            this.SmtpServer = smtpServer;
            this.EmailFrom = emailFrom;
            this.EmailFromPassword = emailFromPassword;
            this.LastFailDate = DateTime.MinValue;
        }

        public bool Active { get; set; }
        public string SmtpServer { get; set; }
        public string EmailFrom { get; set; }
        public string EmailFromPassword { get; set; }
        public DateTime LastFailDate { get; set; }
        public Exception Exception { get; set; }
    }

    public class SMTPInfos : List<SMTPInfo>
    {
        public SMTPInfos() { }
        public SMTPInfos(string smtpServer, string emailFrom, string emailFromPassword)
        {
            SMTPInfo smtpInfo = new SMTPInfo(smtpServer, emailFrom, emailFromPassword);
            this.Add(smtpInfo);
        }
    }

    public class SMTPInfoComparer : IComparer<SMTPInfo>
    {
        public int Compare(SMTPInfo x, SMTPInfo y)
        {
            if (x == null && y == null)
                return 0;
            return x.LastFailDate.CompareTo(y.LastFailDate);
        }
    }
}
