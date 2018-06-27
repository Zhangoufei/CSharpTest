using System;
using System.IO;

namespace BonSite.Core
{
    /// <summary>
    /// BonSite配置管理类
    /// </summary>
    public class BSConfig
    {
        private static object _locker = new object();

        private static IConfigStrategy _configstrategy = null;//配置策略对象

        private static RDBSConfigInfo _rdbsconfiginfo = null;//数据库配置
        private static SysConfigInfo _sysconfiginfo = null;//系统整体配置
        private static SiteConfigInfo _siteconfiginfo = null;//站点信息配置
        private static RouteConfigInfo _routeconfiginfo = null;//站点路由信息配置
        private static EmailConfigInfo _emailConfigInfo = null;//邮件相关配置

        static BSConfig()
        {
            Load();
            //_sysconfiginfo=_sysconfiginfo
            _rdbsconfiginfo = _configstrategy.GetRDBSConfig();
            _sysconfiginfo = _configstrategy.GetSysConfig();
            _siteconfiginfo = _configstrategy.GetSiteConfig();
            _routeconfiginfo = _configstrategy.GetRouteConfig();
            _emailConfigInfo = _configstrategy.GetEmailConfig();
        }

        /// <summary>
        /// 加载配置策略
        /// </summary>
        private static void Load()
        {
            try
            {
                string[] fileNameList = Directory.GetFiles(System.Web.HttpRuntime.BinDirectory, "BonSite.ConfigStrategy.*.dll", SearchOption.TopDirectoryOnly);
                _configstrategy = (IConfigStrategy)Activator.CreateInstance(Type.GetType(string.Format("BonSite.ConfigStrategy.{0}.ConfigStrategy, BonSite.ConfigStrategy.{0}", fileNameList[0].Substring(fileNameList[0].LastIndexOf("ConfigStrategy.") + 15).Replace(".dll", "")),
                                                                                         false,
                                                                                         true));
            }
            catch
            {
                throw new BSException("创建\"配置策略对象\"失败，可能存在的原因：未将\"配置策略程序集\"添加到bin目录中；将多个\"配置策略程序集\"添加到bin目录中；\"配置策略程序集\"文件名不符合\"BonSite.ConfigStrategy.{策略名称}.dll\"格式");
            }
        }

        /// <summary>
        /// 关系数据库配置信息
        /// </summary>
        public static RDBSConfigInfo RDBSConfig
        {
            get { return _rdbsconfiginfo; }
        }

        /// <summary>
        /// 系统整体配置
        /// </summary>
        public static SysConfigInfo SysConfig
        {
            get { return _sysconfiginfo; }
        }

        /// <summary>
        /// 获取站点路由信息配置
        /// </summary>
        public static RouteConfigInfo RouteConfig
        {
            get { return _routeconfiginfo; }
        }

        /// <summary>
        /// 保存系统整体配置
        /// </summary>
        /// <param name="sysConfigInfo"></param>
        public static void SaveSysConfig(SysConfigInfo sysConfigInfo)
        {
            lock (_locker)
            {
                if (_configstrategy.SaveSysConfig(sysConfigInfo))
                    _sysconfiginfo = sysConfigInfo;
            }
        }

        /// <summary>
        /// 站点基本配置信息
        /// </summary>
        public static SiteConfigInfo SiteConfig
        {
            get { return _siteconfiginfo; }
        }

        /// <summary>
        /// 保存站点配置信息
        /// </summary>
        public static void SaveSiteConfig(SiteConfigInfo siteConfigInfo)
        {
            lock (_locker)
            {
                if (_configstrategy.SaveSiteConfig(siteConfigInfo))
                    _siteconfiginfo = siteConfigInfo;
            }
        }

        /// <summary>
        /// 获取邮件相关配置
        /// </summary>
        public static EmailConfigInfo EmailConfig
        {
            get { return _emailConfigInfo; }
        }

        /// <summary>
        /// 保存邮件相关配置
        /// </summary>
        /// <param name="emailConfigInfo"></param>
        public static void SaveEmailConfig(EmailConfigInfo emailConfigInfo)
        {
            lock (_locker)
            {
                if (_configstrategy.SaveEmailConfig(emailConfigInfo))
                    _emailConfigInfo = emailConfigInfo;
            }
        }
    }
}
