﻿using System;
using System.IO;

namespace BonSite.Core
{
    /// <summary>
    /// BrnShop随机性管理类
    /// </summary>
    public class BSRandom
    {
        private static IRandomStrategy _randomstrategy = null;//随机性策略

        static BSRandom()
        {
            Load();
        }

        /// <summary>
        /// 随机性策略实例
        /// </summary>
        public static IRandomStrategy Instance
        {
            get { return _randomstrategy; }
        }

        /// <summary>
        /// 加载随机性策略
        /// </summary>
        private static void Load()
        {
            try
            {
                string[] fileNameList = Directory.GetFiles(System.Web.HttpRuntime.BinDirectory, "BonSite.RandomStrategy.*.dll", SearchOption.TopDirectoryOnly);
                _randomstrategy = (IRandomStrategy)Activator.CreateInstance(Type.GetType(string.Format("BonSite.RandomStrategy.{0}.RandomStrategy, BonSite.RandomStrategy.{0}", fileNameList[0].Substring(fileNameList[0].IndexOf("RandomStrategy.") + 15).Replace(".dll", "")),
                                                                                         false,
                                                                                         true));
            }
            catch
            {
                throw new BSException("创建\"随机性策略对象\"失败，可能存在的原因：未将\"随机性策略程序集\"添加到bin目录中；将多个\"随机性策略程序集\"添加到bin目录中；\"随机性策略程序集\"文件名不符合\"BonSite.RandomStrategy.{策略名称}.dll\"格式");
            }
        }
    }
}
