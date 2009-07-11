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

        /// <summary>
        /// 是否数字(建议使用TryParse)
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsNumeric(string value)
        {
            if (string.IsNullOrEmpty(value))
                return false;
            return Regex.IsMatch(value, @"^[+-]?\d*[.]?\d*$");
        }
        /// <summary>
        /// 是否整数(建议使用TryParse)
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
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
        /// <summary>
        /// 四舍五入(向上取整 2.5->3/2.4->2)
        /// </summary>
        /// <param name="valueToRound"></param>
        /// <returns></returns>
        public static double RoundUp(double valueToRound)
        {
            return (Math.Floor(valueToRound + 0.5));
        }
        /// <summary>
        /// 四舍五入(向上取整 2.5->3/2.4->2)
        /// </summary>
        /// <param name="valueToRound"></param>
        /// <returns></returns>
        public static decimal RoundUp(decimal valueToRound)
        {
            return (Math.Floor(valueToRound + decimal.Parse("0.5")));
        }
        /// <summary>
        /// 向下取整(2.5->2/2.4->2)
        /// </summary>
        /// <param name="valueToRound"></param>
        /// <returns></returns>
        public static double RoundDown(double valueToRound)
        {
            double floorValue = Math.Floor(valueToRound);
            if ((valueToRound - floorValue) > .5)
                return (floorValue + 1);
            else
                return floorValue;
        }
        /// <summary>
        /// 向下取整(2.5->2/2.4->2)
        /// </summary>
        /// <param name="valueToRound"></param>
        /// <returns></returns>
        public static decimal RoundDown(decimal valueToRound)
        {
            decimal floorValue = Math.Floor(valueToRound);
            if ((valueToRound - floorValue) > decimal.Parse("0.5"))
                return (floorValue + 1);
            else
                return floorValue;
        }
        /// <summary>
        /// 取整数(不进行四舍五入,只取整数)
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static decimal GetInteger(decimal value)
        {
            return Math.Truncate(value);
        }
        /// <summary>
        /// 取整数(不进行四舍五入,只取整数)
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static double GetInteger(double value)
        {
            return Math.Truncate(value);
        }
        /// <summary>
        /// 是否偶数值
        /// </summary>
        /// <param name="intValue"></param>
        /// <returns></returns>
        public static bool IsEven(int value)
        {
            return ((value % 2) == 0);
        }
        /// <summary>
        /// 是否奇数值
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsOdd(int value)
        {
            return ((value % 2) == 1);
        }

    }
}
