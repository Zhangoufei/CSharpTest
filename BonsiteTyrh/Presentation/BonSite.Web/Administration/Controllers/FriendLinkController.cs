using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using BonSite.Core;
using BonSite.Services;
using BonSite.Web.Framework;
using BonSite.Web.Admin.Models;

namespace BonSite.Web.Admin.Controllers
{
    public class FriendLinkController : BaseAdminController
    {
        //
        // GET: /FriendLink/

        /// <summary>
        /// 友情链接列表
        /// </summary>
        public ActionResult List()
        {
            FriendLinkListModel model = new FriendLinkListModel();
            model.FriendLinkList = FriendLinks.GetFriendLinkList();

            string[] sizeList = StringHelper.SplitString(WorkContext.SiteConfig.FriendLinkThumbSize);

            ViewData["size"] = sizeList[sizeList.Length / 2];
            SiteUtils.SetAdminRefererCookie(Url.Action("list"));
            return View(model);
        }

        /// <summary>
        /// 添加友情链接
        /// </summary>
        [HttpGet]
        public ActionResult Add()
        {
            FriendLinkModel model = new FriendLinkModel();
            Load();
            return View(model);
        }

        /// <summary>
        /// 添加友情链接
        /// </summary>
        [HttpPost]
        public ActionResult Add(FriendLinkModel model)
        {
            if (ModelState.IsValid)
            {
                FriendLinkInfo friendLinkInfo = new FriendLinkInfo()
                {
                    Name = model.FriendLinkName,
                    Title = model.FriendLinkTitle == null ? "" : model.FriendLinkTitle,
                    Logo = model.FriendLinkLogo == null ? "" : model.FriendLinkLogo,
                    Url = model.FriendLinkUrl,
                    Target = model.Target,
                    DisplayOrder = model.DisplayOrder
                };

                FriendLinks.CreateFriendLink(friendLinkInfo);
                AddAdminOperateLog("添加友情链接", "添加友情链接,友情链接为:" + model.FriendLinkName);
                return PromptView("友情链接添加成功");
            }
            Load();
            return View(model);
        }

        /// <summary>
        /// 编辑友情链接
        /// </summary>
        [HttpGet]
        public ActionResult Edit(int id = -1)
        {
            FriendLinkInfo friendLinkInfo = FriendLinks.GetFriendLinkById(id);
            if (friendLinkInfo == null)
                return PromptView("友情链接不存在");

            FriendLinkModel model = new FriendLinkModel();
            model.FriendLinkName = friendLinkInfo.Name;
            model.FriendLinkTitle = friendLinkInfo.Title;
            model.FriendLinkLogo = friendLinkInfo.Logo;
            model.FriendLinkUrl = friendLinkInfo.Url;
            model.Target = friendLinkInfo.Target;
            model.DisplayOrder = friendLinkInfo.DisplayOrder;
            Load();

            return View(model);
        }

        /// <summary>
        /// 编辑友情链接
        /// </summary>
        [HttpPost]
        public ActionResult Edit(FriendLinkModel model, int id = -1)
        {
            FriendLinkInfo friendLinkInfo = FriendLinks.GetFriendLinkById(id);
            if (friendLinkInfo == null)
                return PromptView("友情链接不存在");

            if (ModelState.IsValid)
            {
                friendLinkInfo.Name = model.FriendLinkName;
                friendLinkInfo.Title = model.FriendLinkTitle == null ? "" : model.FriendLinkTitle;
                friendLinkInfo.Logo = model.FriendLinkLogo == null ? "" : model.FriendLinkLogo;
                friendLinkInfo.Url = model.FriendLinkUrl;
                friendLinkInfo.Target = model.Target;
                friendLinkInfo.DisplayOrder = model.DisplayOrder;

                FriendLinks.UpdateFriendLink(friendLinkInfo);
                AddAdminOperateLog("修改友情链接", "修改友情链接,友情链接ID为:" + id);
                return PromptView("友情链接修改成功");
            }

            Load();
            return View(model);
        }

        /// <summary>
        /// 删除友情链接
        /// </summary>
        public ActionResult Del(int[] idList)
        {
            FriendLinks.DeleteFriendLinkById(idList);
            AddAdminOperateLog("删除友情链接", "删除友情链接,友情链接ID为:" + CommonHelper.IntArrayToString(idList));
            return PromptView("友情链接删除成功");
        }

        private void Load()
        {
            string allowImgType = string.Empty;
            string[] imgTypeList = StringHelper.SplitString(BSConfig.SiteConfig.UploadImgType, ",");
            foreach (string imgType in imgTypeList)
                allowImgType += string.Format("*{0};", imgType.ToLower());

            string[] sizeList = StringHelper.SplitString(WorkContext.SiteConfig.FriendLinkThumbSize);

            ViewData["size"] = sizeList[sizeList.Length / 2];
            ViewData["allowImgType"] = allowImgType;
            ViewData["maxImgSize"] = BSConfig.SiteConfig.UploadImgSize;
            ViewData["referer"] = SiteUtils.GetAdminRefererCookie();
        }

    }
}
