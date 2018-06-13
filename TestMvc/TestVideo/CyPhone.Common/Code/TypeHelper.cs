using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CyPhone.Common
{
    /// <summary>
    /// 类型帮助类
    /// </summary>
    public class TypeHelper
    {
        #region Unix时间戳转换
        /// <summary>
        /// 时间戳转为C#格式时间
        /// </summary>
        /// <param name="timeStamp">Unix时间戳格式</param>
        /// <returns>C#格式时间</returns>
        public static DateTime GetTime(string timeStamp, string defalut = "")
        {
            if (string.IsNullOrWhiteSpace(timeStamp))
            {
                timeStamp = defalut;
            }
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            long lTime = long.Parse(timeStamp + "0000000");
            TimeSpan toNow = new TimeSpan(lTime);
            return dtStart.Add(toNow);
        }
        /// <summary>
        /// DateTime时间格式转换为Unix时间戳格式
        /// </summary>
        /// <param name="time"> DateTime时间格式</param>
        /// <returns>Unix时间戳格式</returns>
        public static int ConvertDateTimeInt(DateTime time)
        {
            DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            return (int)(time - startTime).TotalSeconds;
        }
        #endregion

        #region 返回时间差
        public static string DateDiff(DateTime DateTime1, DateTime DateTime2)
        {
            string dateDiff = null;
            try
            {
                //TimeSpan ts1 = new TimeSpan(DateTime1.Ticks);
                //TimeSpan ts2 = new TimeSpan(DateTime2.Ticks);
                //TimeSpan ts = ts1.Subtract(ts2).Duration();
                TimeSpan ts = DateTime2 - DateTime1;
                if (ts.Days >= 1)
                {
                    dateDiff = DateTime1.Month.ToString() + "月" + DateTime1.Day.ToString() + "日";
                }
                else
                {
                    if (ts.Hours > 1)
                    {
                        dateDiff = ts.Hours.ToString() + "小时前";
                    }
                    else
                    {
                        dateDiff = ts.Minutes.ToString() + "分钟前";
                    }
                }
            }
            catch
            { }
            return dateDiff;
        }
        #endregion

        #region 转Int

        /// <summary>
        /// 将string类型转换成int类型
        /// </summary>
        /// <param name="s">目标字符串</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public static int StringToInt(string s, int defaultValue)
        {
            if (!string.IsNullOrWhiteSpace(s))
            {
                int result;
                if (int.TryParse(s, out result))
                    return result;
            }

            return defaultValue;
        }

        /// <summary>
        /// 将string类型转换成int类型
        /// </summary>
        /// <param name="s">目标字符串</param>
        /// <returns></returns>
        public static int StringToInt(string s)
        {
            return StringToInt(s, 0);
        }

        /// <summary>
        /// 将object类型转换成int类型
        /// </summary>
        /// <param name="s">目标对象</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public static int ObjectToInt(object o, int defaultValue)
        {
            if (o != null)
                return StringToInt(o.ToString(), defaultValue);

            return defaultValue;
        }

        /// <summary>
        /// 将object类型转换成int类型
        /// </summary>
        /// <param name="s">目标对象</param>
        /// <returns></returns>
        public static int ObjectToInt(object o)
        {
            return ObjectToInt(o, 0);
        }

        #endregion

        #region 转Bool

        /// <summary>
        /// 将string类型转换成bool类型
        /// </summary>
        /// <param name="s">目标字符串</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public static bool StringToBool(string s, bool defaultValue)
        {
            if (s == "false")
                return false;
            else if (s == "true")
                return true;

            return defaultValue;
        }

        /// <summary>
        /// 将string类型转换成bool类型
        /// </summary>
        /// <param name="s">目标字符串</param>
        /// <returns></returns>
        public static bool ToBool(string s)
        {
            return StringToBool(s, false);
        }

        /// <summary>
        /// 将object类型转换成bool类型
        /// </summary>
        /// <param name="s">目标对象</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public static bool ObjectToBool(object o, bool defaultValue)
        {
            if (o != null)
                return StringToBool(o.ToString(), defaultValue);

            return defaultValue;
        }

        /// <summary>
        /// 将object类型转换成bool类型
        /// </summary>
        /// <param name="s">目标对象</param>
        /// <returns></returns>
        public static bool ObjectToBool(object o)
        {
            return ObjectToBool(o, false);
        }

        #endregion

        #region string转DateTime

        /// <summary>
        /// 将string类型转换成datetime类型
        /// </summary>
        /// <param name="s">目标字符串</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public static DateTime StringToDateTime(string s, DateTime defaultValue)
        {
            if (!string.IsNullOrWhiteSpace(s))
            {
                DateTime result;
                if (DateTime.TryParse(s, out result))
                    return result;
            }
            return defaultValue;
        }

        /// <summary>
        /// 将string类型转换成datetime类型
        /// </summary>
        /// <param name="s">目标字符串</param>
        /// <returns></returns>
        public static DateTime StringToDateTime(string s)
        {
            return StringToDateTime(s, DateTime.Now);
        }

        /// <summary>
        /// 将object类型转换成datetime类型
        /// </summary>
        /// <param name="s">目标对象</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public static DateTime ObjectToDateTime(object o, DateTime defaultValue)
        {
            if (o != null)
                return StringToDateTime(o.ToString(), defaultValue);

            return defaultValue;
        }

        /// <summary>
        /// 将object类型转换成datetime类型
        /// </summary>
        /// <param name="s">目标对象</param>
        /// <returns></returns>
        public static DateTime ObjectToDateTime(object o)
        {
            return ObjectToDateTime(o, DateTime.Now);
        }

        #endregion

        #region 转Decimal

        /// <summary>
        /// 将string类型转换成decimal类型
        /// </summary>
        /// <param name="s">目标字符串</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public static decimal StringToDecimal(string s, decimal defaultValue)
        {
            if (!string.IsNullOrWhiteSpace(s))
            {
                decimal result;
                if (decimal.TryParse(s, out result))
                    return result;
            }

            return defaultValue;
        }

        /// <summary>
        /// 将string类型转换成decimal类型
        /// </summary>
        /// <param name="s">目标字符串</param>
        /// <returns></returns>
        public static decimal StringToDecimal(string s)
        {
            return StringToDecimal(s, 0m);
        }

        /// <summary>
        /// 将object类型转换成decimal类型
        /// </summary>
        /// <param name="s">目标对象</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public static decimal ObjectToDecimal(object o, decimal defaultValue)
        {
            if (o != null)
                return StringToDecimal(o.ToString(), defaultValue);

            return defaultValue;
        }

        /// <summary>
        /// 将object类型转换成decimal类型
        /// </summary>
        /// <param name="s">目标对象</param>
        /// <returns></returns>
        public static decimal ObjectToDecimal(object o)
        {
            return ObjectToDecimal(o, 0m);
        }

        #endregion


    }
}
