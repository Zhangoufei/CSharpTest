using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using BonSite.Core;
using BonSite.Services;
using BonSite.Web.Admin.Models;
using BonSite.Web.Framework;

namespace BonSite.Web.Admin.Controllers
{
    public class InformationController : BaseAdminController
    {
        public ActionResult List()
        {
            FriendLinkListModel model = new FriendLinkListModel();
            model.FriendLinkList = FriendLinks.GetFriendLinkList();

            string[] sizeList = StringHelper.SplitString(WorkContext.SiteConfig.FriendLinkThumbSize);

            ViewData["size"] = sizeList[sizeList.Length / 2];
            SiteUtils.SetAdminRefererCookie(Url.Action("list"));
            return View(model);
        }
    }
}