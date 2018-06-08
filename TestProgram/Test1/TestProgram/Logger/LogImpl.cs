using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Reflection;

namespace JAAJ.Log.Common
{
    public class LogImpl
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        static LogImpl()
        {
            log4net.Config.XmlConfigurator.ConfigureAndWatch(
            new System.IO.FileInfo("log4net.config"));
        }
        public static string hellO()
        {
            return "32432";
        }
        /// <summary>
        /// 调试，优先等级：0
        /// </summary>
        /// <param name="message"></param>
        public static void Debug(string message)
        {
            System.Diagnostics.Debug.Print("LogDebug:" + message);
            log.Debug(message);
        }

        /// <summary>
        /// 通知，优先等级：1
        /// </summary>
        /// <param name="message"></param>
        public static void Info(object message)
        {
            System.Diagnostics.Debug.Print("LogInfo:" + message.ToString());
            log.Info(message);
        }

        /// <summary>
        /// 警告，优先等级：2
        /// </summary>
        /// <param name="message"></param>
        public static void Warn(object message)
        {
            System.Diagnostics.Debug.Print("LogWarn:" + message.ToString());
            log.Warn(message);
        }

        /// <summary>
        /// 错误，优先等级：3
        /// </summary>
        /// <param name="message"></param>
        public static void Error(object message)
        {
            System.Diagnostics.Debug.Print("LogError:" + message.ToString());
            log.Error(message);
        }

        /// <summary>
        /// 失败，优先等级：4
        /// </summary>
        /// <param name="message"></param>
        public static void Fatal(object message)
        {
            System.Diagnostics.Debug.Print("LogFatal:" + message.ToString());
            log.Fatal(message);
        }
    }
}
