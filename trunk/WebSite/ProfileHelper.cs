using System;
using System.Web;
using System.Collections.Generic;
using System.Text;

namespace hwj.CommonLibrary.WebSite
{
    public class ProfileHelper
    {
        public string UserCode { get; set; }
        public string UserName { get; set; }
        public DateTime LastLogin { get; set; }
        public const string ProfileHelperKeys = "hwj_ProfileHelperKeys";
        public static ProfileHelper GetSession()
        {
            if (HttpContext.Current == null || HttpContext.Current.Session == null || HttpContext.Current.Session[ProfileHelperKeys] == null)
                return null;
            else
                return HttpContext.Current.Session[ProfileHelperKeys] as ProfileHelper;
        }
        public void SetSession()
        {
            HttpContext.Current.Session[ProfileHelperKeys] = this;
        }
        public void ClearSession()
        {
            HttpContext.Current.Session.Remove(ProfileHelperKeys);
        }
    }
}
