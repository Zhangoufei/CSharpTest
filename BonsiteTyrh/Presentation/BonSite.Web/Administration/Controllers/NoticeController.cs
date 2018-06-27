using System.Web.Mvc;

using BonSite.Core;
using BonSite.Services;
using BonSite.Web.Framework;
using BonSite.Web.Admin.Models;

namespace BonSite.Web.Admin.Controllers
{
    public class NoticeController : BaseAdminController
    {
        public ActionResult List()
        {
            NoticeListModel model = new NoticeListModel();
            //model.FriendLinkList = FriendLinks.GetFriendLinkList();

            string[] sizeList = StringHelper.SplitString(WorkContext.SiteConfig.FriendLinkThumbSize);

            ViewData["size"] = sizeList[sizeList.Length / 2];
            SiteUtils.SetAdminRefererCookie(Url.Action("list"));
            return View(model);
        }
    }
}