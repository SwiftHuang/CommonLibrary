using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace hwj.CommonLibrary.Object
{
    public class NetHelper
    {
        /// <summary>
        /// 获取本机名称
        /// </summary>
        /// <returns></returns>
        public static string GetHostName()
        {
            return Dns.GetHostName();
        }
        /// <summary>
        /// 获取本机IP地址(多个)
        /// </summary>
        /// <returns></returns>
        public static string GetHostIPAddress()
        {
            StringBuilder sb = new StringBuilder();
            IPAddress[] addr = Dns.GetHostAddresses(Dns.GetHostName());

            for (int i = 0; i < addr.Length; i++)
            {
                sb.AppendFormat("{0}:{1}/", i + 1, addr[i].ToString());
            }
            return sb.ToString().TrimEnd('/');
        }
    }
}
