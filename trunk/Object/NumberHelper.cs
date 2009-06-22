using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace hwj.CommonLibrary.Object
{
    public class NumberHelper
    {
        public static string ToString(object value)
        {
            return ToString(value, null);
        }
        public static string ToString(object value, string format)
        {
            if (!string.IsNullOrEmpty(format))
                return decimal.Parse(value.ToString()).ToString(format);
            else
                return decimal.Parse(value.ToString()).ToString("##0.00");
        }

        public static bool IsNumeric(string value)
        {
            if (string.IsNullOrEmpty(value))
                return false;
            return Regex.IsMatch(value, @"^[+-]?\d*[.]?\d*$");
        }
        public static bool IsInt(string value)
        {
            if (string.IsNullOrEmpty(value))
                return false;
            return Regex.IsMatch(value, @"^[+-]?\d*$");
        }
        public static bool IsUnsign(string value)
        {
            if (string.IsNullOrEmpty(value))
                return false;
            return Regex.IsMatch(value, @"^\d*[.]?\d*$");
        }
    }
}
