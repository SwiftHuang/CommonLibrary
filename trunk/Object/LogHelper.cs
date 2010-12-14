using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Mail;
using Microsoft.Win32;

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
        public static string GetOSInformation()
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                RegistryKey rk = Registry.LocalMachine.OpenSubKey("Software\\Microsoft\\Windows NT\\CurrentVersion");
                string informaction = rk.GetValue("ProductName") != null ? rk.GetValue("ProductName").ToString() : string.Empty;
                string version = rk.GetValue("CSDVersion") != null ? rk.GetValue("CSDVersion").ToString() : string.Empty;
                string versionCode = rk.GetValue("CurrentBuildNumber") != null ? rk.GetValue("CurrentBuildNumber").ToString() : string.Empty;

                sb.AppendFormat("{0} / {1} / {2}", informaction, version, versionCode);
                return sb.ToString();
            }
            catch 
            {
                return "Failed to get system information";
            }
        }
    }
}
