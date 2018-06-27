using System;
using BonSite.Core;

namespace BonSite.ConfigStrategy.File
{
    public class ConfigStrategy:IConfigStrategy
    {
        #region 私有字段

        private readonly string _sysconfigfilepath = "/App_Data/system.config";//系统整体配置
        private string _rdbsconfigfilepath = "/App_Data/rdbs.config";//关系数据库配置信息文件路径
        private string _siteconfigfilepath = "/App_Data/site.config";//站点基本配置信息文件路径
        private string _routeconfigfilepath = "App_Data/route.config";//站点route配置文件路径
        private string _emailconfigfilepath = "/App_Data/email.config";//邮件配置文件路径

        #endregion
        public ConfigStrategy()
        {
            //重新定义数据库和站点配置文件的指向地址
            _rdbsconfigfilepath = string.Format("/Themes/{0}/Config/rdbs.config", GetSysConfig().ThemeName);
            _siteconfigfilepath = string.Format("/Themes/{0}/Config/site.config", GetSysConfig().ThemeName);
            _routeconfigfilepath = string.Format("/Themes/{0}/Config/route.config", GetSysConfig().ThemeName);
            _emailconfigfilepath = string.Format("/Themes/{0}/Config/email.config", GetSysConfig().ThemeName);
        }

        #region 帮助方法

        /// <summary>
        /// 从文件中加载配置信息
        /// </summary>
        /// <param name="configInfoType">配置信息类型</param>
        /// <param name="configInfoFile">配置信息文件路径</param>
        /// <returns>配置信息</returns>
        private IConfigInfo LoadConfigInfo(Type configInfoType, string configInfoFile)
        {
            return (IConfigInfo)IOHelper.DeserializeFromXML(configInfoType, configInfoFile);
        }

        /// <summary>
        /// 将配置信息保存到文件中
        /// </summary>
        /// <param name="configInfo">配置信息</param>
        /// <param name="configInfoFile">保存路径</param>
        /// <returns>是否保存成功</returns>
        private bool SaveConfigInfo(IConfigInfo configInfo, string configInfoFile)
        {
            return IOHelper.SerializeToXml(configInfo, configInfoFile);
        }

        

        #endregion

        /// <summary>
        /// 获得关系数据库配置
        /// </summary>
        public RDBSConfigInfo GetRDBSConfig()
        {
            return (RDBSConfigInfo)LoadConfigInfo(typeof(RDBSConfigInfo), IOHelper.GetMapPath(_rdbsconfigfilepath));
        }

        /// <summary>
        /// 保存站点基本配置
        /// </summary>
        /// <param name="configInfo">商城基本配置信息</param>
        /// <returns>是否保存结果</returns>
        public bool SaveSiteConfig(SiteConfigInfo configInfo)
        {
            return SaveConfigInfo(configInfo, IOHelper.GetMapPath(_siteconfigfilepath));
        }

        /// <summary>
        /// 获得站点基本配置
        /// </summary>
        public SiteConfigInfo GetSiteConfig()
        {
            return (SiteConfigInfo)LoadConfigInfo(typeof(SiteConfigInfo), IOHelper.GetMapPath(_siteconfigfilepath));
        }

        /// <summary>
        /// 获取系统基本信息
        /// </summary>
        /// <returns></returns>
        public SysConfigInfo GetSysConfig()
        {
            return (SysConfigInfo)LoadConfigInfo(typeof(SysConfigInfo), IOHelper.GetMapPath(_sysconfigfilepath));
        }

        /// <summary>
        /// 保存系统基本信息
        /// </summary>
        /// <param name="configInfo"></param>
        /// <returns></returns>
        public bool SaveSysConfig(SysConfigInfo configInfo)
        {
            return SaveConfigInfo(configInfo, IOHelper.GetMapPath(_sysconfigfilepath));
        }

        /// <summary>
        /// 获取route的基本配置
        /// </summary>
        /// <returns></returns>
        public RouteConfigInfo GetRouteConfig()
        {
            return (RouteConfigInfo)LoadConfigInfo(typeof(RouteConfigInfo),IOHelper.GetMapPath(_routeconfigfilepath));
        }

        /// <summary>
        /// 保存邮件的配置信息
        /// </summary>
        /// <param name="configInfo"></param>
        /// <returns></returns>
        public bool SaveEmailConfig(EmailConfigInfo configInfo)
        {
            return SaveConfigInfo(configInfo, IOHelper.GetMapPath(_emailconfigfilepath));
        }

        /// <summary>
        /// 获取邮件配置信息
        /// </summary>
        /// <returns></returns>
        public EmailConfigInfo GetEmailConfig()
        {
            return (EmailConfigInfo) LoadConfigInfo(typeof (EmailConfigInfo), IOHelper.GetMapPath(_emailconfigfilepath));
        }
    }
}
