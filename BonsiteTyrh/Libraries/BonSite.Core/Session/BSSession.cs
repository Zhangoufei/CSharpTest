using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace BonSite.Core
{
    /// <summary>
    /// 
    /// </summary>
    public class BSSession
    {private static ISessionStrategy _sessionstrategy = null;//会话状态策略

        static BSSession()
        {
            Load();
        }

        /// <summary>
        /// 会话状态策略实例
        /// </summary>
        public static ISessionStrategy Instance
        {
            get { return _sessionstrategy; }
        }

        /// <summary>
        /// 加载会话状态策略
        /// </summary>
        private static void Load()
        {
            try
            {
                string[] fileNameList = Directory.GetFiles(System.Web.HttpRuntime.BinDirectory, "BonSite.SessionStrategy.*.dll", SearchOption.TopDirectoryOnly);
                _sessionstrategy = (ISessionStrategy)Activator.CreateInstance(Type.GetType(string.Format("BonSite.SessionStrategy.{0}.SessionStrategy, BonSite.SessionStrategy.{0}", fileNameList[0].Substring(fileNameList[0].IndexOf("SessionStrategy.") + 16).Replace(".dll", "")),
                                                                                           false,
                                                                                           true));
            }
            catch
            {
                throw new BSException("创建\"会话状态策略对象\"失败，可能存在的原因：未将\"会话状态策略程序集\"添加到bin目录中；将多个\"会话状态策略程序集\"添加到bin目录中；\"会话状态策略程序集\"文件名不符合\"BonSite.SessionStrategy.{策略名称}.dll\"格式");
            }
        }
    }
}
