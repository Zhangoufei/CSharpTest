using System;
using System.Web;
using System.Web.Mvc;
using System.Collections.Generic;

using BonSite.Core;
using BonSite.Services;
using BonSite.Web.Framework;
using BonSite.Web.Admin.Models;

namespace BonSite.Web.Admin.Controllers
{
    public class BannerController : BaseAdminController
    {
        /// <summary>
        /// Banner位置列表
        /// </summary>
        /// <returns></returns>
        public ActionResult BannerPositionList()
        {
            
            BannerPositionListModel model = new BannerPositionListModel
            {
                BannerPositionList = Banners.GetAllBannerPosition()

            };
            SiteUtils.SetAdminRefererCookie(Url.Action("bannerpositionlist"));
            return View(model);
        }

        /// <summary>
        /// 添加banner位置
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult AddBannerPosition()
        {
            BannerPositionModel model = new BannerPositionModel();
            ViewData["referer"] = SiteUtils.GetAdminRefererCookie();
            return View(model);
        }

        /// <summary>
        /// 新加Banner位置
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddBannerPosition(BannerPositionModel model)
        {
            if (ModelState.IsValid)
            {
                BannerPositionInfo bannerPositionInfo = new BannerPositionInfo()
                {
                    Title = model.Title,
                    Description = model.Description
                };

                Banners.CreateBannerPosition(bannerPositionInfo);

                //AddAdminOperateLog("添加广告位置", "添加广告位置,广告位置为:" + model.Title);
                return PromptView("广告位置添加成功");

            }
            ViewData["referer"] = SiteUtils.GetAdminRefererCookie();
            return View(model);
        }

        /// <summary>
        /// 编辑Banner位置
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult EditBannerPosition(int id=-1)
        {
            BannerPositionInfo banPositionInfo = Banners.GetBannerPositionById(id);
            if (banPositionInfo == null)
                return PromptView("Banner位置不存在");

            BannerPositionModel model = new BannerPositionModel() { 
                Title=banPositionInfo.Title,
                Description=banPositionInfo.Description
            };

            ViewData["referer"] = SiteUtils.GetAdminRefererCookie();
            return View(model);        
        }

        public ActionResult EditBannerPosition(BannerPositionModel model,int id=-1)
        {
            BannerPositionInfo bannerPositionInfo = Banners.GetBannerPositionById(id);
            if (bannerPositionInfo == null)
                return PromptView("Banner位置不存在");

            if (ModelState.IsValid)
            {
                bannerPositionInfo.Title = model.Title;
                bannerPositionInfo.Description = model.Description;

                Banners.UpdateBannerPosition(bannerPositionInfo);

                return PromptView("Banner位置修改成功！");
            }
            ViewData["referer"] = SiteUtils.GetAdminRefererCookie();
            return View(model);
        
        }

        /// <summary>
        /// 删除Banner位置
        /// </summary>
        public ActionResult DelBannerPosition(int id)
        {
            Banners.DeleteBannerPositionById(id);
            //AddAdminOperateLog("删除广告位置", "删除广告位置,广告位置ID为:" + adPosId);
            return PromptView("Banner位置删除成功");
        }


        /// <summary>
        /// 广告列表
        /// </summary>
        public ActionResult BannerList(int banPosId = 0, int pageSize = 15, int pageNumber = 1)
        {

            PageModel pageModel = new PageModel(pageSize, pageNumber, Banners.AdminGetBannerCount(banPosId));

            BannerListModel model = new BannerListModel()
            {
                PageModel = pageModel,
                BanPosId = banPosId,
                BannerList = Banners.AdminGetBannerList(pageModel.PageSize, pageModel.PageNumber, banPosId)
            };

            List<SelectListItem> itemList = new List<SelectListItem>();
            itemList.Add(new SelectListItem() { Text = "全部Banner位置", Value = "0" });
            foreach (BannerPositionInfo bannerPositionInfo in Banners.GetAllBannerPosition())
            {
                itemList.Add(new SelectListItem() { Text = bannerPositionInfo.Title, Value = bannerPositionInfo.BanPosId.ToString() });
            }
            ViewData["bannerPositionList"] = itemList;
            SiteUtils.SetAdminRefererCookie(Url.Action("bannerlist"));
            return View(model);


        }

        /// <summary>
        /// 添加广告
        /// </summary>
        [HttpGet]
        public ActionResult AddBanner()
        {
            BannerModel model = new BannerModel();
            Load();
            return View(model);
        }

        /// <summary>
        /// 添加广告
        /// </summary>
        [HttpPost]
        public ActionResult AddBanner(BannerModel model)
        {
            if (Banners.GetBannerPositionById(model.BanPosId) == null)
                ModelState.AddModelError("BanPosId", "Banner位置不存在");

            if (ModelState.IsValid)
            {
                BannerInfo bannerInfo = new BannerInfo()
                {
                    BanPosId = model.BanPosId,
                    StartTime = model.StartTime,
                    EndTime = model.EndTime,
                    IsShow = model.IsShow,
                    Title = model.BannerTitle == null ? "" : model.BannerTitle,
                    Img = model.Img,
                    Url = model.Url,
                    DisplayOrder = model.DisplayOrder
                };

                Banners.CreateBanner(bannerInfo);
                AddAdminOperateLog("添加banner", "添加banner,banner为:" + model.BannerTitle);
                return PromptView("banner添加成功");

            }
            Load();
            return View(model);
        }

        /// <summary>
        /// 编辑banner
        /// </summary>
        [HttpGet]
        public ActionResult EditBanner(int id = -1)
        {
            BannerInfo bannerInfo = Banners.AdminGetBannerById(id);
            if (bannerInfo == null)
                return PromptView("Banner不存在");

            BannerModel model = new BannerModel();
            model.BanPosId = bannerInfo.BanPosId;
            model.StartTime = bannerInfo.StartTime;
            model.EndTime = bannerInfo.EndTime;
            model.IsShow = bannerInfo.IsShow;
            model.BannerTitle = bannerInfo.Title;
            model.Img = bannerInfo.Img;
            model.Url = bannerInfo.Url;
            model.DisplayOrder = bannerInfo.DisplayOrder;
            Load();

            return View(model);
        }

        /// <summary>
        /// 编辑banner
        /// </summary>
        [HttpPost]
        public ActionResult EditBanner(BannerModel model, int id = -1)
        {
            BannerInfo bannerInfo = Banners.AdminGetBannerById(id);
            if (bannerInfo == null)
                return PromptView("Banner不存在");

            if (ModelState.IsValid)
            {
                bannerInfo.BanPosId = model.BanPosId;
                bannerInfo.StartTime = model.StartTime;
                bannerInfo.EndTime = model.EndTime;
                bannerInfo.IsShow = model.IsShow;
                bannerInfo.Title = model.BannerTitle == null ? "" : model.BannerTitle;
                bannerInfo.Img = model.Img;
                bannerInfo.Url = model.Url;
                bannerInfo.DisplayOrder = model.DisplayOrder;

                Banners.UpdateBanner(model.BanPosId, bannerInfo);
                AddAdminOperateLog("修改banner", "修改banner,bannerID为:" + id);
                return PromptView("banner修改成功");
            }

            Load();
            return View(model);
        }

        /// <summary>
        /// 删除banner
        /// </summary>
        public ActionResult DelBanner(int id)
        {
            Banners.DeleteBannerById(id);
            //AddAdminOperateLog("删除banner", "删除banner,bannerID为:" + CommonHelper.IntArrayToString(idList));
            return PromptView("banner删除成功");
        }

        private void Load()
        {
            List<SelectListItem> itemList = new List<SelectListItem>();
            itemList.Add(new SelectListItem() { Text = "请选择Banner位置", Value = "0" });
            foreach (BannerPositionInfo bannerPositionInfo in Banners.GetAllBannerPosition())
            {
                itemList.Add(new SelectListItem() { Text = bannerPositionInfo.Title, Value = bannerPositionInfo.BanPosId.ToString() });
            }
            ViewData["bannerPositionList"] = itemList;
            ViewData["referer"] = SiteUtils.GetAdminRefererCookie();
        }

    }
}
