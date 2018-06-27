using System;


namespace BonSite.Core
{
    /// <summary>
    /// BonSite配置策略接口
    /// </summary>
    public interface IConfigStrategy
    {
        /// <summary>
        /// 获得关系数据库配置
        /// </summary>
        /// <returns></returns>
        RDBSConfigInfo GetRDBSConfig();

        /// <summary>
        /// 获取系统整体配置
        /// </summary>
        /// <returns></returns>
        SysConfigInfo GetSysConfig();

        /// <summary>
        /// 获取网站基本配置
        /// </summary>
        /// <returns></returns>
        SiteConfigInfo GetSiteConfig();

        /// <summary>
        /// 获取站点路由信息配置
        /// </summary>
        /// <returns></returns>
        RouteConfigInfo GetRouteConfig();

        /// <summary>
        /// 获取邮件相关信息配置
        /// </summary>
        /// <returns></returns>
        EmailConfigInfo GetEmailConfig();

        /// <summary>
        /// 保存系统整体配置
        /// </summary>
        /// <param name="configInfo"></param>
        /// <returns></returns>
        bool SaveSysConfig(SysConfigInfo configInfo);

        /// <summary>
        /// 保存网站基本配置
        /// </summary>
        /// <param name="configInfo"></param>
        /// <returns></returns>
        bool SaveSiteConfig(SiteConfigInfo configInfo);

        /// <summary>
        /// 保存邮件相关配置
        /// </summary>
        /// <param name="configinfo"></param>
        /// <returns></returns>
        bool SaveEmailConfig(EmailConfigInfo configinfo);
    }
}
