using System;
using System.Collections.Generic;
using System.Text;

namespace hwj.CommonLibrary.Object
{
    public class DateHelper
    {
        public static DateTime ToDate(string value, string format)
        {
            DateTime date = DateTime.MinValue;
            IFormatProvider culture = new System.Globalization.CultureInfo("en-us", true);
            DateTime.TryParse(value, culture, System.Globalization.DateTimeStyles.AssumeLocal, out date);

            return date;
        }
    }
}
