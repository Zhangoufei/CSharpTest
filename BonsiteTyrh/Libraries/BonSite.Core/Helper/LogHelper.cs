using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace BonSite.Core
{
    public class LogHelper
    {
        private string logFile =
            IOHelper.GetMapPath(string.Format("/App_Data/logs/{0}.log", DateTime.Now.ToString("yyyyMMdd")));
        /// <summary>
        /// 不带参数的构造函数
        /// </summary>
        public LogHelper()
        {
            FileInfo fileInfo = new FileInfo(logFile);
            if (!fileInfo.Directory.Exists)
            {
                fileInfo.Directory.Create();
            }
        }
        /// <summary>
        /// 带参数的构造函数
        /// </summary>
        /// <param name="logFile"></param>
        public LogHelper(string logFile)
        {
            this.logFile = logFile;
        }
        /// <summary>
        /// 追加一条信息
        /// </summary>
        /// <param name="text"></param>
        public void Write(string text)
        {
            using (StreamWriter sw = new StreamWriter(logFile, true, Encoding.UTF8))
            {
                sw.Write(DateTime.Now.ToString("[yyyy-MM-dd HH:mm:ss] \r\n") + text + "\r\n");
            }
        }
        /// <summary>
        /// 追加一条信息
        /// </summary>
        /// <param name="logFile"></param>
        /// <param name="text"></param>
        public void Write(string logFile, string text)
        {
            using (StreamWriter sw = new StreamWriter(logFile, true, Encoding.UTF8))
            {
                sw.Write(DateTime.Now.ToString("[yyyy-MM-dd HH:mm:ss] \r\n") + text + "\r\n");
            }
        }
        /// <summary>
        /// 追加一行信息
        /// </summary>
        /// <param name="text"></param>
        public void WriteLine(string text)
        {
            text += "\r\n";
            using (StreamWriter sw = new StreamWriter(logFile, true, Encoding.UTF8))
            {
                sw.Write(DateTime.Now.ToString("[yyyy-MM-dd HH:mm:ss] \r\n") + text + "\r\n");
            }
        }
        /// <summary>
        /// 追加一行信息
        /// </summary>
        /// <param name="logFile"></param>
        /// <param name="text"></param>
        public void WriteLine(string logFile, string text)
        {
            text += "\r\n";
            using (StreamWriter sw = new StreamWriter(logFile, true, Encoding.UTF8))
            {
                sw.Write(DateTime.Now.ToString("[yyyy-MM-dd HH:mm:ss] \r\n") + text + "\r\n");
            }
        }
    }
}
