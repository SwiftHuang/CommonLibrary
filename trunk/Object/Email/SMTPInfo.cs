﻿using System;
using System.Collections.Generic;
using System.Text;

namespace hwj.CommonLibrary.Object.Email
{
    public class SmtpInfo
    {
        #region Property
        public string SmtpServer { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public DateTime FailedOn { get; internal set; }
        public Exception Exception { get; internal set; }
        #endregion

        public SmtpInfo() { }
        public SmtpInfo(string smtpServer, string emailFrom)
            : this(smtpServer, emailFrom, null)
        {

        }
        public SmtpInfo(string smtpServer, string emailFrom, string emailFromPassword)
        {
            this.SmtpServer = smtpServer;
            this.UserName = emailFrom;
            this.Password = emailFromPassword;
            this.FailedOn = DateTime.MinValue;
        }

    }

    public class SmtpInfoList : List<SmtpInfo>
    {
        #region Property
        /// <summary>
        /// 获取最后一次成功发送的SMTP配置。
        /// </summary>
        public SmtpInfo LastSuccess { get; internal set; }
        /// <summary>
        /// 获取或设置SMTP配置发送失败后，间隔几多分钟后重新使用。
        /// </summary>
        public int Interval { get; set; }
        #endregion

        public SmtpInfoList() { }
        public SmtpInfoList(string smtpServer, string emailFrom, string emailFromPassword)
        {
            SmtpInfo smtpInfo = new SmtpInfo(smtpServer, emailFrom, emailFromPassword);
            this.Add(smtpInfo);
        }

        public SmtpInfoList GetValidList()
        {
            SmtpInfoList streamlineList = new SmtpInfoList();
            foreach (SmtpInfo smtpInfo in this)
            {
                if (smtpInfo != null && !string.IsNullOrEmpty(smtpInfo.SmtpServer) && !string.IsNullOrEmpty(smtpInfo.UserName) && DateTime.Now.Subtract(smtpInfo.FailedOn).Minutes >= Interval)
                {
                    streamlineList.Add(smtpInfo);
                }
            }
            return streamlineList;
        }
    }

    //internal class SmtpInfoComparer : IComparer<SmtpInfo>
    //{
    //    public int Compare(SmtpInfo x, SmtpInfo y)
    //    {
    //        if (x == null && y == null)
    //            return 0;
    //        if (x.FailedOn == DateTime.MinValue && y.FailedOn == DateTime.MinValue)
    //            return 0;
    //        return x.FailedOn.CompareTo(y.FailedOn);
    //    }
    //}
}
