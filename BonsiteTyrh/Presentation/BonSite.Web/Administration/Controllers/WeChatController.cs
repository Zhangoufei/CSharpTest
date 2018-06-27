using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using BonSite.Core;
using BonSite.Services;
using BonSite.Web.Framework;
using BonSite.Web.Admin.Models;
using BonSite.Core.Domain.Site;


namespace BonSite.Web.Admin.Controllers
{
    public class WeChatController : BaseAdminController
    {
        //列表
        public ActionResult List()
        {
            WeChatModel model = new WeChatModel();
            model.WeChatList = WeChats.GetWeChatList();
            SiteUtils.SetAdminRefererCookie(Url.Action("list"));
            return View(model);
        }
        //添加
        [HttpGet]
        public ActionResult Add()
        {
            WeChatModel model = new WeChatModel();
            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Add(WeChatModel model)
        {
            WeChatInfo weChatInfo = new WeChatInfo();
            if (ModelState.IsValid)
            {
                weChatInfo.name = model.name;
                weChatInfo.appid = model.appid;
                weChatInfo.secret = model.secret;
                WeChats.CreateWeChat(weChatInfo);
                return PromptView("公众号添加成功");
            }
            return View(model);
        }
        //更新
        public ActionResult Edit(int id = -1)
        {
            WeChatInfo weChatInfo = WeChats.GetWeChatById(id);

            if (weChatInfo == null)
                return PromptView("公众号不存在");

            WeChatModel model = new WeChatModel();
            model.id = weChatInfo.id;
            model.name = weChatInfo.name;
            model.appid = weChatInfo.appid;
            model.secret = weChatInfo.secret;
            return View(model);
        }
        [HttpPost]
        public ActionResult Edit(WeChatModel model, int id)
        {
            WeChatInfo weChatInfo = WeChats.GetWeChatById(id);
            if (weChatInfo == null)
                return PromptView("公众号不存在");

            if (ModelState.IsValid)
            {
                weChatInfo.name = model.name;
                weChatInfo.appid = model.appid;
                weChatInfo.secret = model.secret;
                weChatInfo.id = model.id;
                WeChats.UpdateWeChat(weChatInfo);


                
                AddAdminOperateLog("修改公众号", "修改公众号,公众号ID为:" + id);
                return PromptView("公众号修改成功");
            }
            return View(model);
        }
        //删除
        public ActionResult Del(int[] idList)
        {
            WeChats.DeleteWeChat(idList);
            AddAdminOperateLog("删除公众号", "删除公众号,公众号ID为:" + CommonHelper.IntArrayToString(idList));
            return PromptView("公众号删除成功");
        }

    }
}