using System;
using System.IO;
using System.Web;
using System.Xml;
using System.Data;
using System.Text;
using System.Drawing;
using System.Web.Mvc;
using System.Reflection;
using System.Drawing.Text;
using System.Collections.Generic;

using BonSite.Core;
using BonSite.Services;
using BonSite.Web.Framework;
using BonSite.Web.Admin.Models;

namespace BonSite.Web.Admin.Controllers
{
    public class SetController : BaseAdminController
    {
        //
        // GET: /Set/

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Site()
        {
            SiteConfigInfo siteConfigInfo = BSConfig.SiteConfig;

            SetModel model = new SetModel
            {
                SiteName = siteConfigInfo.SiteName,
                SiteUrl = siteConfigInfo.SiteUrl,
                SiteTitle = siteConfigInfo.SiteTitle,
                SEOKeyword = siteConfigInfo.SEOKeyword,
                SEODescription = siteConfigInfo.SEODescription,
                ICP = siteConfigInfo.ICP,
                Script = siteConfigInfo.Script,
                IsLicensed = siteConfigInfo.IsLicensed,
                ShowSEO=siteConfigInfo.ShowSEO
            };
            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Site(SetModel model)
        {
            if (ModelState.IsValid)
            {
                SiteConfigInfo siteCofnigInfo = BSConfig.SiteConfig;

                siteCofnigInfo.SiteName = model.SiteName == null ? "" : model.SiteName;
                siteCofnigInfo.SiteUrl = model.SiteUrl == null ? "" : model.SiteUrl;
                siteCofnigInfo.SiteTitle = model.SiteTitle == null ? "" : model.SiteTitle;
                siteCofnigInfo.SEOKeyword = model.SEOKeyword == null ? "" : model.SEOKeyword;
                siteCofnigInfo.SEODescription = model.SEODescription == null ? "" : model.SEODescription;
                siteCofnigInfo.ICP = model.ICP == null ? "" : model.ICP;
                siteCofnigInfo.Script = model.Script == null ? "" : model.Script;
                siteCofnigInfo.IsLicensed = model.IsLicensed;

                BSConfig.SaveSiteConfig(siteCofnigInfo);
                return PromptView(Url.Action("site"), "修改站点信息成功");

            
            }
            return View(model);
        }

        /// <summary>
        /// 上传
        /// </summary>
        [HttpGet]
        public ActionResult Upload()
        {
            SiteConfigInfo siteConfigInfo = BSConfig.SiteConfig;

            UploadModel model = new UploadModel();
            model.UploadImgType = siteConfigInfo.UploadImgType;
            model.UploadImgSize = siteConfigInfo.UploadImgSize / 1000;
            model.UploadAttType = siteConfigInfo.UploadAttType;
            model.UploadAttSize = siteConfigInfo.UploadAttSize / 1000;
            model.WatermarkType = siteConfigInfo.WatermarkType;
            
            model.WatermarkQuality = siteConfigInfo.WatermarkQuality;
            model.WatermarkPosition = siteConfigInfo.WatermarkPosition;
            model.WatermarkImg = siteConfigInfo.WatermarkImg;
            model.WatermarkImgOpacity = siteConfigInfo.WatermarkImgOpacity;
            model.WatermarkText = siteConfigInfo.WatermarkText;
            model.WatermarkTextFont = siteConfigInfo.WatermarkTextFont;
            model.WatermarkTextSize = siteConfigInfo.WatermarkTextSize;
            model.ArticleImgThumbSize = siteConfigInfo.ArticleImgThumbSize;
            model.FriendLinkThumbSize = siteConfigInfo.FriendLinkThumbSize;
            model.ArticleClassThumbSize = siteConfigInfo.ArticleClassThumbSize;
            model.SpecialImgThumbSize = siteConfigInfo.SpecialImgThumbSize;
            model.UserAvatarThumbSize = siteConfigInfo.UserAvatarThumbSize;
            model.UserRankAvatarThumbSize = siteConfigInfo.UserRankAvatarThumbSize;

            LoadFont();
            return View(model);
        }


        /// <summary>
        /// 上传
        /// </summary>
        [HttpPost]
        public ActionResult Upload(UploadModel model)
        {
            if (ModelState.IsValid)
            {
                SiteConfigInfo siteConfigInfo = BSConfig.SiteConfig;

                siteConfigInfo.UploadImgType = model.UploadImgType;
                siteConfigInfo.UploadImgSize = model.UploadImgSize * 1000;
                siteConfigInfo.UploadAttType = model.UploadAttType;
                siteConfigInfo.UploadAttSize = model.UploadAttSize * 1000;
                siteConfigInfo.WatermarkType = model.WatermarkType;
                siteConfigInfo.WatermarkQuality = model.WatermarkQuality;
                siteConfigInfo.WatermarkPosition = model.WatermarkPosition;
                siteConfigInfo.WatermarkImg = model.WatermarkImg == null ? "" : model.WatermarkImg;
                siteConfigInfo.WatermarkImgOpacity = model.WatermarkImgOpacity;
                siteConfigInfo.WatermarkText = model.WatermarkText == null ? "" : model.WatermarkText;
                siteConfigInfo.WatermarkTextFont = model.WatermarkTextFont;
                siteConfigInfo.WatermarkTextSize = model.WatermarkTextSize;
                siteConfigInfo.ArticleImgThumbSize = model.ArticleImgThumbSize;
                siteConfigInfo.FriendLinkThumbSize = model.FriendLinkThumbSize;
                siteConfigInfo.ArticleClassThumbSize = model.ArticleClassThumbSize;
                siteConfigInfo.SpecialImgThumbSize = model.SpecialImgThumbSize;
                siteConfigInfo.UserAvatarThumbSize = model.UserAvatarThumbSize;
                siteConfigInfo.UserRankAvatarThumbSize = model.UserRankAvatarThumbSize;

                BSConfig.SaveSiteConfig(siteConfigInfo);
                //Emails.ResetShop();
                //SMSes.ResetShop();
                AddAdminOperateLog("修改上传设置");
                return PromptView(Url.Action("upload"), "修改上传设置成功");
            }

            LoadFont();
            return View(model);
        }


        private void LoadFont()
        {
            List<SelectListItem> itemList = new List<SelectListItem>();
            InstalledFontCollection fontList = new InstalledFontCollection();
            foreach (FontFamily family in fontList.Families)
                itemList.Add(new SelectListItem() { Text = family.Name, Value = family.Name });
            ViewData["fontList"] = itemList;
        }
    }
}
