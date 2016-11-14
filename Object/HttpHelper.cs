using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace hwj.CommonLibrary.Object
{
    public class HttpHelper
    {
        //private const string GetContentType = "text/plain";
        private const int defaultTimeOut = 30000;

        private const string PostContentType = "application/x-www-form-urlencoded";
        private const string Get = "GET";

        public static string PostAction(string url, string param, Encoding encoding, int timeout)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Timeout = timeout > defaultTimeOut ? timeout : defaultTimeOut;
            request.Method = "POST";
            request.ContentType = PostContentType;

            Stream dataStream = request.GetRequestStream();
            byte[] bytes = DataToBytes(param, encoding);

            dataStream.Write(bytes, 0, bytes.Length);
            dataStream.Close();

            string rs = GetResponeString(request);
            return rs;
        }

        public static string GetAction(string url, Dictionary<string, string> data)
        {
            if (data != null && data.Count > 0)
            {
                url = CombineQueryUrl(url, data);
            }
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            //request.Timeout = timeOut;
            //request.ContentType = GetContentType;
            request.Method = "GET";
            string rs = GetResponeString(request);
            return rs;
        }

        private static string GetResponeString(HttpWebRequest rq)
        {
            HttpWebResponse httpWebResponse = (HttpWebResponse)rq.GetResponse();
            using (Stream responseStream = httpWebResponse.GetResponseStream())
            using (StreamReader sr = new StreamReader(responseStream))
            {
                string str = sr.ReadToEnd();
                return str;
            }
        }

        private static string CombineQueryUrl(string url, Dictionary<string, string> data)
        {
            string query = GetFormatedData(data);
            if (!url.Contains("?"))
            {
                url += "?" + query;
            }
            else
            {
                url += "&" + query;
            }
            return url;
        }

        private static string GetFormatedData(Dictionary<string, string> data)
        {
            StringBuilder sb = new StringBuilder(200);
            if (data != null && data.Count > 0)
            {
                foreach (var i in data)
                {
                    sb.AppendFormat("{0}={1}&", i.Key, i.Value);
                }
            }
            string rs = sb.ToString();
            if (!string.IsNullOrEmpty(rs))
            {
                rs = rs.TrimEnd('&');
            }
            return rs;
        }

        private static byte[] DataToBytes(string param, Encoding encoding)
        {
            byte[] bytes = new byte[0];
            bytes = encoding.GetBytes(param);
            return bytes;
        }

        private static byte[] DataToBytes(Dictionary<string, string> data, Encoding encoding)
        {
            string param = GetFormatedData(data);
            return DataToBytes(param, encoding);
        }

        public static string GetIP()
        {
            string IP4Address = String.Empty;

            foreach (IPAddress ipa in Dns.GetHostAddresses(System.Web.HttpContext.Current.Request.UserHostAddress))
            {
                if (ipa.AddressFamily.ToString() == "InterNetwork")
                {
                    IP4Address = ipa.ToString();
                    break;
                }
            }

            if (IP4Address != String.Empty)
            {
                return IP4Address;
            }

            foreach (IPAddress IPA in Dns.GetHostAddresses(Dns.GetHostName()))
            {
                if (IPA.AddressFamily.ToString() == "InterNetwork")
                {
                    IP4Address = IPA.ToString();
                    break;
                }
            }

            return IP4Address;
        }
    }
}