using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web;
using System.Web.UI;
using hwj.CommonLibrary.WebSite;

namespace hwj.CommonLibrary.WebSite.Base
{
    /// <summary>
    /// 所有页面的基类
    /// </summary>
    public class BasePage : Page
    {
        #region Property
        private static CultureInfo defaultCulture = new CultureInfo("zh-cn");
        /// <summary>
        /// 无效的权限
        /// </summary>
        public bool InvalidPermissions { get; set; }
        /// <summary>
        /// 无效的用户信息
        /// </summary>
        public bool InvalidProfiles { get; set; }
        /// <summary>
        /// 登录用户编号
        /// </summary>
        public string UserCode { get; set; }
        /// <summary>
        /// 开启检查登录信息
        /// </summary>
        public bool EnabledLoginCheck { get; set; }
        /// <summary>
        /// 开启错误处理
        /// </summary>
        public bool EnabledErrorHandle { get; set; }
        /// <summary>
        /// 设置登录页面
        /// </summary>
        public string RedirectLoginUrl { get; set; }
        /// <summary>
        /// 设置错误页面
        /// </summary>
        public string RedirectErrorUrl { get; set; }
        /// <summary>
        /// 分页页码
        /// </summary>
        public int PageNumber
        {
            get
            {
                if (!string.IsNullOrEmpty(Request["Page"]))
                    return int.Parse(Request["Page"]);
                else
                    return 1;
            }
        }
        #endregion

        public BasePage()
        {
            InvalidProfiles = false;
            InvalidPermissions = false;
            EnabledLoginCheck = true;
            EnabledErrorHandle = true;
            RedirectErrorUrl = "~/Error.aspx";
        }

        #region Override Funtion
        protected override void OnLoad(EventArgs e)
        {
            this.ValidateLogin();
            base.OnLoad(e);
        }
        protected override void InitializeCulture()
        {
            string lang = Request["lang"];
            if (lang != null)
            {
                lang = lang.ToLower();
            }

            if ("zh-tw".Equals(lang) || "zh-cn".Equals(lang) || "en-us".Equals(lang))
            {
                Session["CurrentUICulture"] = new System.Globalization.CultureInfo(lang);
                Session["culture_string"] = lang;
            }

            System.Globalization.CultureInfo ci = Session["CurrentUICulture"] as System.Globalization.CultureInfo;
            if (ci != null)
            {
                System.Threading.Thread.CurrentThread.CurrentUICulture = ci;
            }
            // Don't know why, but sometimes the browser culture gets automatically assigned
            // into current culture, so datetime passed into the database gets Chinese month names
            System.Threading.Thread.CurrentThread.CurrentCulture = defaultCulture;
        }
        #endregion

        #region Funtion
        private void ValidateLogin()
        {
            if (EnabledLoginCheck)
            {
                if (InvalidProfiles)
                {
                    ErrorInfo err = new ErrorInfo();
                    err.RedirectUrl = RedirectLoginUrl;
                    err.ErrorType = ErrorInfo.ErrorTypes.Login;
                    err.SetSession();
                    Response.Redirect(RedirectErrorUrl);
                }
                if (!InvalidProfiles && InvalidPermissions)
                {
                    ErrorInfo err = new ErrorInfo();
                    err.RedirectUrl = RedirectLoginUrl;
                    err.ErrorType = ErrorInfo.ErrorTypes.Unauthorized;
                    err.SetSession();
                    Response.Redirect(RedirectErrorUrl);
                }
            }
        }
        protected void Page_Error(object sender, EventArgs e)
        {
            if (EnabledErrorHandle)
            {
                ErrorInfo err = new ErrorInfo();
                Exception objErr = Server.GetLastError().GetBaseException();

                if (objErr.GetType().Name == "xException")
                {
                    err.ErrorType = ErrorInfo.ErrorTypes.DefaultException;
                }
                else
                {
                    err.ErrorType = ErrorInfo.ErrorTypes.Exception;
                }

                if (HttpContext.Current != null && HttpContext.Current.Request != null)
                    err.ErrorRequest = HttpContext.Current.Request;
                err.RedirectUrl = RedirectLoginUrl;
                err.Exceptions = objErr;
                err.SetSession();
                Server.ClearError();

                Response.Redirect(RedirectErrorUrl);
            }
        }
        #endregion

    }
}



