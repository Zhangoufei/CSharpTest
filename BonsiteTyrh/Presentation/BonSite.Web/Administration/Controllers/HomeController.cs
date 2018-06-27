using System;
using System.Web.Mvc;
using BonSite.Core;
using BonSite.Services;
using BonSite.Web.Framework;
using BonSite.Web.Admin.Models;

namespace BonSite.Web.Admin.Controllers
{
    public class HomeController : BaseAdminController
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            //WorkContext.AdminGroupID = ArticleClass.GetToTaleNavList();
            return View();
        }

        public ActionResult Menu()
        {
            MenuModel model = new MenuModel();
            model.MenuList = BonSite.Services.ArticleClass.GetAdminMenuList(WorkContext.Uid, WorkContext.AdminGroupID);

            return View(model);
        }

        public ActionResult SiteRunInfo()
        {

            SiteRunInfoModel model = new SiteRunInfoModel();
            
            //model.OnlineUserCount = OnlineUsers.GetOnlineUserCount();
            //model.OnlineGuestCount = OnlineUsers.GetOnlineGuestCount();
            model.OnlineMemberCount = model.OnlineUserCount - model.OnlineGuestCount;

            model.SiteVersion = BSVersion.SITE_VERSION;
            model.NetVersion = Environment.Version.ToString();
            model.OSVersion = Environment.OSVersion.ToString();
            model.TickCount = (Environment.TickCount / 1000 / 60).ToString();
            model.ProcessorCount = Environment.ProcessorCount.ToString();
            model.WorkingSet = (Environment.WorkingSet / 1024 / 1024).ToString();

            model.WorkingTick = SiteUtils.GetCache();

            SiteUtils.SetAdminRefererCookie(Url.Action("siteruninfo"));
            return View(model);

        }

        /// <summary>
        /// 清除缓存
        /// </summary>
        /// <returns></returns>
        public ActionResult Clear()
        {
            SiteUtils.ClearAllCache();
            return AjaxResult("success", "缓存清除成功");
        }



    }
}
