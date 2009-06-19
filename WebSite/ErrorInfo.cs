using System;
using System.Web;

namespace hwj.CommonLibrary.WebSite
{
    public class ErrorInfo : Base.BaseEntity<ErrorInfo>
    {
        public enum ErrorTypes
        {
            None,
            Login,
            Unauthorized,
            DefaultException,
            Exception
        }

        #region Property
        public ErrorTypes ErrorType { get; set; }
        public Exception Exceptions { get; set; }
        public string RedirectUrl { get; set; }
        public HttpRequest ErrorRequest { get; set; }

        #endregion
        public ErrorInfo()
        {

        }
    }

}
