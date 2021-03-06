﻿using System;
using System.IO;

namespace BonSite.Core
{
    /// <summary>
    /// BrnShop邮件管理类
    /// </summary>
    public class BSEmail
    {
        private static IEmailStrategy _emailstrategy = null;//邮件策略

        static BSEmail()
        {
            Load();
        }

        /// <summary>
        /// 邮件策略实例
        /// </summary>
        public static IEmailStrategy Instance
        {
            get { return _emailstrategy; }
        }

        /// <summary>
        /// 加载邮件策略
        /// </summary>
        private static void Load()
        {
            try
            {
                string[] fileNameList = Directory.GetFiles(System.Web.HttpRuntime.BinDirectory, "BonSite.EmailStrategy.*.dll", SearchOption.TopDirectoryOnly);
                _emailstrategy = (IEmailStrategy)Activator.CreateInstance(Type.GetType(string.Format("BonSite.EmailStrategy.{0}.EmailStrategy, BonSite.EmailStrategy.{0}", fileNameList[0].Substring(fileNameList[0].IndexOf("EmailStrategy.") + 14).Replace(".dll", "")),
                                                                                       false,
                                                                                       true));
            }
            catch
            {
                throw new BSException("创建\"邮件策略对象\"失败，可能存在的原因：未将\"邮件策略程序集\"添加到bin目录中；将多个\"邮件策略程序集\"添加到bin目录中；\"邮件策略程序集\"文件名不符合\"BrnShop.EmailStrategy.{策略名称}.dll\"格式");
            }
        }

    }
}
