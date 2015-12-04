using System;
using System.Collections.Generic;
using System.Text;

namespace hwj.CommonLibrary.Object.Extensions
{
    public static class ExStringHelper
    {
        public static string ToUpperTrim(this string str)
        {
            if (str == null)
                return str;
            return str.ToUpper().Trim();
        }

        public static string ToLowerTrim(this string str)
        {
            if (str == null)
                return str;
            return str.ToLower().Trim();
        }
    }
}
