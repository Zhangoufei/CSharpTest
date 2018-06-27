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
    public class SpecialController : BaseAdminController
    {
        //
        // GET: /Special/

        public ActionResult List()
        {
            SpecialListModel model = new SpecialListModel();
            model.SpecialList = Special.AdminGetList();

            string[] sizeList = StringHelper.SplitString(WorkContext.SiteConfig.FriendLinkThumbSize);

            ViewData["size"] = sizeList[sizeList.Length / 2];
            SiteUtils.SetAdminRefererCookie(Url.Action("list"));
            return View(model);

        }

        [HttpGet]
        public ActionResult Add()
        {
            SpecialModel model = new SpecialModel();
            Load();
            return View(model);
        }

        [HttpPost]
        public ActionResult Add(SpecialModel model)
        {
            if (model != null)
            {
                SpecialInfo specialInfo = new SpecialInfo
                {
                    Name = model.Name,
                    Title = model.Title,
                    ImgUrl = model.ImgUrl,
                    LogoUrl = model.LogoUrl,
                    IsOut = model.IsOut,
                    OutUrl = model.OutUrl,
                    Body = model.Body,
                    DisplayOrder = model.DisplayOrder
                };

                Special.Create(specialInfo);
                //AddAdminOperateLog("添加友情链接", "添加友情链接,友情链接为:" + model.FriendLinkName);
                return PromptView("专题添加成功");
                
            } 
            
            Load();
            return View(model);
        
        }


        [HttpGet]
        public ActionResult Edit(int id = -1)
        {
            SpecialInfo specialInfo = Special.AdminGetModelById(id);
            if (specialInfo == null)
                return PromptView("专题信息不存在");

            SpecialModel model = new SpecialModel
            {
                SpecialID=specialInfo.SpecialID,
                Name=specialInfo.Name,
                Title=specialInfo.Title,
                ImgUrl=specialInfo.ImgUrl,
                LogoUrl=specialInfo.LogoUrl,
                IsOut=specialInfo.IsOut,
                OutUrl=specialInfo.OutUrl,
                Body=specialInfo.Body,
                DisplayOrder=specialInfo.DisplayOrder
            };

            Load();
            return View(model);


        }

        [HttpPost]
        public ActionResult Edit(SpecialModel model, int id = -1)
        {
            SpecialInfo specialInfo = Special.AdminGetModelById(id);
            if (specialInfo == null)
                return PromptView("专题不存在");
            if (ModelState.IsValid)
            {
                specialInfo.Name = model.Name;
                specialInfo.Title = model.Title;
                specialInfo.ImgUrl = model.ImgUrl;
                specialInfo.LogoUrl = model.LogoUrl;
                specialInfo.IsOut = model.IsOut;
                specialInfo.OutUrl = model.OutUrl;
                specialInfo.Body = model.Body;
                specialInfo.DisplayOrder = model.DisplayOrder;

                Special.Update(specialInfo);

                return PromptView("专题编辑成功");
            }
            Load();
            return View(model);
        
        }


        public ActionResult Del(int idList)
        {
            Special.Delete(idList);
            //AddAdminOperateLog("删除", "删除友情链接,友情链接ID为:" + CommonHelper.IntArrayToString(idList));
            return PromptView("专题删除成功");
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
