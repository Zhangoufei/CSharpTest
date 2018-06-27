using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;

using BonSite.Core;
using BonSite.Services;
using BonSite.Web.Framework;
using BonSite.Web.Admin.Models;

namespace BonSite.Web.Admin.Controllers
{
    public class AdvertController : BaseAdminController
    {
        /// <summary>
        /// 广告位置列表
        /// </summary>
        public ActionResult AdvertPositionList(int pageSize = 15, int pageNumber = 1)
        {
            PageModel pageModel = new PageModel(pageSize, pageNumber, Adverts.GetAdvertPositionCount());

            AdvertPositionListModel model = new AdvertPositionListModel()
            {
                PageModel = pageModel,
                AdvertPositionList = Adverts.GetAdvertPositionList(pageSize, pageNumber)
            };

            SiteUtils.SetAdminRefererCookie(string.Format("{0}?pageNumber={1}&pageSize={2}",
                                                          Url.Action("advertpositionlist"),
                                                          pageModel.PageNumber,
                                                          pageModel.PageSize));
            return View(model);
        }

        /// <summary>
        /// 添加广告位置
        /// </summary>
        [HttpGet]
        public ActionResult AddAdvertPosition()
        {
            AdvertPositionModel model = new AdvertPositionModel();
            ViewData["referer"] = SiteUtils.GetAdminRefererCookie();
            return View(model);
        }

        /// <summary>
        /// 添加广告位置
        /// </summary>
        [HttpPost]
        public ActionResult AddAdvertPosition(AdvertPositionModel model)
        {
            if (ModelState.IsValid)
            {
                AdvertPositionInfo advertPositionInfo = new AdvertPositionInfo()
                {
                    Title = model.Title,
                    Description = model.Description ?? ""
                };

                Adverts.CreateAdvertPosition(advertPositionInfo);
                AddAdminOperateLog("添加广告位置", "添加广告位置,广告位置为:" + model.Title);
                return PromptView("广告位置添加成功");
            }
            ViewData["referer"] = SiteUtils.GetAdminRefererCookie();
            return View(model);
        }

        /// <summary>
        /// 编辑广告位置
        /// </summary>
        [HttpGet]
        public ActionResult EditAdvertPosition(int adPosId = -1)
        {
            AdvertPositionInfo advertPositionInfo = Adverts.GetAdvertPositionById(adPosId);
            if (advertPositionInfo == null)
                return PromptView("广告位置不存在");

            AdvertPositionModel model = new AdvertPositionModel();
            model.Title = advertPositionInfo.Title;
            model.Description = advertPositionInfo.Description;

            ViewData["referer"] = SiteUtils.GetAdminRefererCookie();
            return View(model);
        }

        /// <summary>
        /// 编辑广告位置
        /// </summary>
        [HttpPost]
        public ActionResult EditAdvertPosition(AdvertPositionModel model, int adPosId = -1)
        {
            AdvertPositionInfo advertPositionInfo = Adverts.GetAdvertPositionById(adPosId);
            if (advertPositionInfo == null)
                return PromptView("广告位置不存在");

            if (ModelState.IsValid)
            {
                advertPositionInfo.Title = model.Title;
                advertPositionInfo.Description = model.Description ?? "";

                Adverts.UpdateAdvertPosition(advertPositionInfo);
                AddAdminOperateLog("修改广告位置", "修改广告位置,广告位置ID为:" + adPosId);
                return PromptView("广告位置修改成功");
            }

            ViewData["referer"] = SiteUtils.GetAdminRefererCookie();
            return View(model);
        }

        /// <summary>
        /// 删除广告位置
        /// </summary>
        public ActionResult DelAdvertPosition(int adPosId = -1)
        {
            Adverts.DeleteAdvertPositionById(adPosId);
            AddAdminOperateLog("删除广告位置", "删除广告位置,广告位置ID为:" + adPosId);
            return PromptView("广告位置删除成功");
        }




        /// <summary>
        /// 广告列表
        /// </summary>
        public ActionResult AdvertList(int adPosId = 0, int pageSize = 15, int pageNumber = 1)
        {
            PageModel pageModel = new PageModel(pageSize, pageNumber, Adverts.AdminGetAdvertCount(adPosId));

            AdvertListModel model = new AdvertListModel()
            {
                PageModel = pageModel,
                AdPosId = adPosId,
                AdvertList = Adverts.AdminGetAdvertList(pageModel.PageSize, pageModel.PageNumber, adPosId)
            };

            List<SelectListItem> itemList = new List<SelectListItem>();
            itemList.Add(new SelectListItem() { Text = "全部广告位置", Value = "0" });
            foreach (AdvertPositionInfo advertPositionInfo in Adverts.GetAllAdvertPosition())
            {
                itemList.Add(new SelectListItem() { Text = advertPositionInfo.Title, Value = advertPositionInfo.AdPosId.ToString() });
            }
            ViewData["advertPositionList"] = itemList;
            SiteUtils.SetAdminRefererCookie(Url.Action("advertlist"));
            return View(model);
        }

        /// <summary>
        /// 添加广告
        /// </summary>
        [HttpGet]
        public ActionResult AddAdvert()
        {
            AdvertModel model = new AdvertModel();
            Load();
            return View(model);
        }

        /// <summary>
        /// 添加广告
        /// </summary>
        [HttpPost]
        public ActionResult AddAdvert(AdvertModel model)
        {
            if (Adverts.GetAdvertPositionById(model.AdPosId) == null)
                ModelState.AddModelError("AdPosId", "广告位置不存在");

            if (ModelState.IsValid)
            {
                AdvertInfo advertInfo = new AdvertInfo()
                {
                    ClickCount = 0,
                    AdPosId = model.AdPosId,
                    Type = model.Type,
                    Title = model.Title,
                    Body = model.Body,
                    Url = model.Url ?? "",
                    StartTime = model.StartTime,
                    EndTime = model.EndTime,
                    State = model.State,
                    DisplayOrder = model.DisplayOrder
                };

                Adverts.CreateAdvert(advertInfo);
                AddAdminOperateLog("添加广告", "添加广告,广告为:" + model.Title);
                return PromptView("广告添加成功");
            }
            Load();
            return View(model);
        }

        /// <summary>
        /// 编辑广告
        /// </summary>
        [HttpGet]
        public ActionResult EditAdvert(int adId = -1)
        {
            AdvertInfo advertInfo = Adverts.AdminGetAdvertById(adId);
            if (advertInfo == null)
                return PromptView("广告不存在");

            AdvertModel model = new AdvertModel();
            model.AdPosId = advertInfo.AdPosId;
            model.Type = advertInfo.Type;
            model.Title = advertInfo.Title;
            model.Body = advertInfo.Body;
            model.Url = advertInfo.Url;
            model.StartTime = advertInfo.StartTime;
            model.EndTime = advertInfo.EndTime;
            model.State = advertInfo.State;
            model.DisplayOrder = advertInfo.DisplayOrder;

            Load();
            return View(model);
        }

        /// <summary>
        /// 编辑广告
        /// </summary>
        [HttpPost]
        public ActionResult EditAdvert(AdvertModel model, int adId = -1)
        {
            AdvertInfo advertInfo = Adverts.AdminGetAdvertById(adId);
            if (advertInfo == null)
                return PromptView("广告不存在");

            int oldAdPosId = advertInfo.AdPosId;
            if (ModelState.IsValid)
            {
                advertInfo.AdPosId = model.AdPosId;
                advertInfo.Type = model.Type;
                advertInfo.Title = model.Title;
                advertInfo.Body = model.Body;
                advertInfo.Url = model.Url ?? "";
                advertInfo.StartTime = model.StartTime;
                advertInfo.EndTime = model.EndTime;
                advertInfo.State = model.State;
                advertInfo.DisplayOrder = model.DisplayOrder;

                Adverts.UpdateAdvert(oldAdPosId, advertInfo);
                AddAdminOperateLog("修改广告", "修改广告,广告ID为:" + adId);
                return PromptView("广告修改成功");
            }

            Load();
            return View(model);
        }

        /// <summary>
        /// 删除广告
        /// </summary>
        public ActionResult DelAdvert(int adId = -1)
        {
            Adverts.DeleteAdvertById(adId);
            AddAdminOperateLog("删除广告", "删除广告,广告ID为:" + adId);
            return PromptView("广告删除成功");
        }

        private void Load()
        {
            List<SelectListItem> itemList = new List<SelectListItem>();
            itemList.Add(new SelectListItem() { Text = "请选择广告位置", Value = "0" });
            foreach (AdvertPositionInfo advertPositionInfo in Adverts.GetAllAdvertPosition())
            {
                itemList.Add(new SelectListItem() { Text = advertPositionInfo.Title, Value = advertPositionInfo.AdPosId.ToString() });
            }
            ViewData["advertPositionList"] = itemList;
            ViewData["referer"] = SiteUtils.GetAdminRefererCookie();
        }

    }
}
