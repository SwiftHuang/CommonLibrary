﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Mail;
namespace hwj.CommonLibrary.Object
{
    public class LogHelper : Base.LogHelper
    {
        public LogHelper()
        {
            base.Initialization(null);
        }
        public LogHelper(string fileName)
        {
            base.Initialization(fileName);
        }

        #region Info
        public void Info(string log, Exception ex, string EmailSubject, bool sendEmail)
        {
            base.InfoAction(log, ex, EmailSubject, sendEmail);
        }
        public void Info(string log, Exception ex, string EmailSubject, string emailTo, string emailCC)
        {
            base.InfoAction(log, ex, EmailSubject, true, emailTo, emailCC);
        }
        public void Info(string log, Exception ex, string EmailSubject, string emailTo, string emailCC, List<Attachment> attachments)
        {
            base.InfoAction(log, ex, EmailSubject, true, emailTo, emailCC, attachments);
        }
        #endregion

        #region Warn
        public void Warn(string log, Exception ex, string EmailSubject)
        {
            base.WarnAction(log, ex, EmailSubject);
        }
        public void Warn(string log, Exception ex, string EmailSubject, string emailTo, string emailCC)
        {
            base.WarnAction(log, ex, EmailSubject, emailTo, emailCC);
        }
        public void Warn(string log, Exception ex, string EmailSubject, string emailTo, string emailCC, List<Attachment> attachments)
        {
            base.WarnAction(log, ex, EmailSubject, emailTo, emailCC, attachments);
        }
        #endregion

        #region Error
        public void Error(string log, Exception ex, string EmailSubject)
        {
            base.ErrorAction(log, ex, EmailSubject);
        }
        public void Error(string log, Exception ex, string EmailSubject, string emailTo, string emailCC)
        {
            base.ErrorAction(log, ex, EmailSubject, emailTo, emailCC);
        }
        public void Error(string log, Exception ex, string EmailSubject, string emailTo, string emailCC, List<Attachment> attachments)
        {
            base.ErrorAction(log, ex, EmailSubject, emailTo, emailCC, attachments);
        }
        #endregion

        /// <summary>
        /// 获取当前系统、版本、版本号
        /// </summary>
        /// <returns></returns>
        public static string GetOSVersion()
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                string informaction = Environment.OSVersion.VersionString.ToString();
                string version = Environment.Version.ToString();

                sb.AppendFormat("{0}({1})", informaction, version);
                return sb.ToString();
            }
            catch
            {
                return "Failed to get system information";
            }
        }
    }
}
