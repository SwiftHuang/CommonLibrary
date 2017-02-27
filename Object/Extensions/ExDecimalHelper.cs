﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace hwj.CommonLibrary.Object.Extensions
{
    public static class ExDecimalHelper
    {
        /// <summary>
        /// 计算中位数
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        public static decimal Median(this IList<decimal> array)
        {
            if (array == null)
            {
                throw new ArgumentNullException("array");
            }

            return array.ToArray().Median();
        }

        /// <summary>
        /// 计算中位数
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        public static decimal Median(this decimal[] array)
        {
            if (array == null)
            {
                throw new ArgumentNullException("array");
            }

            int endIndex = array.Length / 2;

            for (int i = 0; i <= endIndex; i++)
            {
                for (int j = 0; j < array.Length - i - 1; j++)
                {
                    if (array[j + 1] < array[j])
                    {
                        decimal temp = array[j + 1];
                        array[j + 1] = array[j];
                        array[j] = temp;
                    }
                }
            }

            if (array.Length % 2 != 0)
            {
                return array[endIndex];
            }

            return (array[endIndex - 1] + array[endIndex]) / 2;
        }
    }
}