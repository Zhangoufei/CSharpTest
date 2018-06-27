using System;
using System.Collections.Generic;
using System.Text;

using BonSite.Core;

namespace BonSite.Web.Framework
{
    /// <summary>
    /// 网站前台工作上下文类
    /// </summary>
    public class WebWorkContext
    {
        public SysConfigInfo SysConfig = BSConfig.SysConfig;
        public SiteConfigInfo SiteConfig = BSConfig.SiteConfig;

        public string IP;//用户ip

        public string Url;//当前Url

        public string UrlReferrer;//上一次访问的Url

        public string Sid;//用户sID

        public int Uid = -1;//用户id


        public string UserName;//用户名

        public string UserEmail;//用户邮箱

        public string UserMobile;//用户手机号

        public string NickName;//用户昵称

        public string Avatar;//用户头像

        public string Password;//用户密码
        
        public PartUserInfo PartUserInfo;//用户信息

        public string EncryptPwd;//加密密码

        public string Controller;//控制器

        public string Action;//动作方法

        public string PageKey;//页面标示符

        public string ThemeName;//当前主题名称

        public string ImageDir;//图片目录

        public string CSSDir;//css目录

        public string ScriptDir;//脚本目录

        public List<NavInfo> NavList;//导航列表

        public List<NavInfo> ClassList;//分类列表

        public FriendLinkInfo[] FriendLinkList;//友情链接列表

        public int AdminGid = -1;//用户管理员组id


        public string ExecuteDetail;//执行的sql语句细节

        public string SiteVersion = BSVersion.SITE_VERSION;//商城版本

        public string SiteCopyright = BSVersion.SITE_COPYRIGHT;//商城版权

        public int Type = 0; //主站、分站类型  默认主站

    }
}
