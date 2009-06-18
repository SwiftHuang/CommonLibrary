using System;
using System.Globalization;
using System.Web.UI;
using hwj.CommonLibrary.WebSite;

namespace hwj.CommonLibrary.WebSite.Base
{
    /// <summary>
    /// 所有页面的基类
    /// </summary>
    public class BaseControl : UserControl
    {
        #region Property
        public ProfileHelper Profiles { get; set; }
        #endregion

        public BaseControl()
        {
            Profiles = ProfileHelper.GetSession();
        }
    }
}



