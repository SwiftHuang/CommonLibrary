using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace hwj.CommonLibrary.WebSite
{
    public class WebLogInfo
    {
        #region Property
        private const string _LogTimeFmt = "DateTime: {0}<br>";
        private const string _LogServerFmt = "Machine Name: {0}<br>";
        private const string _LogRequestFmt = "User Agent</span>: {0}<br>" +
                                              "User Host</span>: {1}<br>" +
                                              "Url Referrer</span>: {2}<br>" +
                                              "Page Url</span>: {3}<br>" +
                                              "Page Path</span>: {4}<br>" +
                                              "RequestType</span>: {5}<br>" +
                                              "Params</span>: {6}<br>";
        public string UserAgent { get; set; }
        public string PhysicalPath { get; set; }
        public string Url { get; set; }
        public string UserHostAddress { get; set; }
        public string RequestType { get; set; }
        public string UrlReferrer { get; set; }
        public string Params { get; set; }
        public string ServerMachineName { get; set; }
        public string HTML { get; set; }
        #endregion

        public WebLogInfo()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat(_LogTimeFmt, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            if (HttpContext.Current != null)
            {
                if (HttpContext.Current.Server != null)
                {
                    HttpServerUtility sr = HttpContext.Current.Server;
                    ServerMachineName = sr.MachineName;
                    sb.AppendFormat(_LogServerFmt, ServerMachineName);
                }
                if (HttpContext.Current.Request != null)
                {
                    HttpRequest rq = HttpContext.Current.Request;
                    UserAgent = rq.UserAgent;
                    PhysicalPath = rq.PhysicalPath;
                    UrlReferrer = rq.UrlReferrer == null ? "" : rq.UrlReferrer.ToString();
                    UserHostAddress = rq.UserHostAddress;
                    RequestType = rq.RequestType;
                    Url = rq.Url == null ? "" : rq.Url.ToString().Split('?')[0];
                    if (RequestType == "POST")
                        Params = rq.Form == null ? "" : rq.Form.ToString();
                    else
                        Params = rq.QueryString == null ? "" : rq.QueryString.ToString();

                    sb.AppendFormat(_LogRequestFmt,
                                        UserAgent,
                                        UserHostAddress,
                                        UrlReferrer,
                                        Url,
                                        PhysicalPath,
                                        RequestType,
                                        Params);
                }

            }
            HTML = sb.ToString();
        }
    }
}
