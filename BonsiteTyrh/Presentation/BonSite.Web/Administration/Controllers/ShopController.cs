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
    public class ShopController : BaseAdminController
    {
        //
        // GET: /Shop/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List(int pageSize = 15, int pageNumber = 1)
        {
            string condition = Shop.AdminGetShopListCondition("");
            string sort = Shop.AdminGetShopListSort("", "");

            PageModel pageModel = new PageModel(pageSize, pageNumber, Shop.AdminGetShopCount(condition));

            ShopListModel model=new ShopListModel()
            {
                DataList = Shop.AdminGetShopList(pageModel.PageSize,pageModel.PageNumber,condition,sort),
                PageModel = pageModel
            };

            SiteUtils.SetAdminRefererCookie(Url.Action("List", "Shop"));

            return View(model);
        }

        [HttpGet]
        public ActionResult Edit(int shopid = -1)
        {
            ShopInfo shopinfo = Shop.GetModelByShopID(shopid);

            if (shopinfo == null)
                return PromptView("信息不存在");

            ShopModel model = new ShopModel()
            {
                ShopID = shopinfo.ShopID,
                ShopName = shopinfo.ShopName,
                Address = shopinfo.Address,
                Tel = shopinfo.Tel,
                Fax = shopinfo.Fax,
                ShopImg = shopinfo.ShopImg,
                Position = shopinfo.Position,
                Body = shopinfo.Body,
                Area = shopinfo.Area,
                Type = shopinfo.Type,
                OrderID = shopinfo.OrderID,
                Remark = shopinfo.Remark
            };

            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(ShopInfo model, int shopid)
        {
            ShopInfo shopInfo = Shop.GetModelByShopID(shopid);

            if (shopInfo == null)
                return PromptView("信息不存在");

            if (ModelState.IsValid)
            {
                shopInfo.ShopName = model.ShopName;
                shopInfo.Address=model.Address;
                shopInfo.Tel = model.Tel;
                shopInfo.Fax = model.Fax;
                shopInfo.ShopImg = model.ShopImg;
                shopInfo.Position = model.Position;
                shopInfo.Body = model.Body;
                shopInfo.Area = model.Area;
                shopInfo.Type = model.Type;
                shopInfo.OrderID = model.OrderID;
                shopInfo.Remark = model.Remark;

                Shop.Update(shopInfo);

                return PromptView("信息修改成功");

            }

            return View(model);
        }

        [HttpGet]
        public ActionResult Add()
        {
            ShopModel model=new ShopModel();

            return View(model);
        }

        [HttpPost]
        public ActionResult Add(ShopInfo model)
        {
            ShopInfo shopInfo = new ShopInfo();
            
            if (ModelState.IsValid)
            {
                shopInfo.ShopName = model.ShopName;
                shopInfo.Address = model.Address;
                shopInfo.Tel = model.Tel;
                shopInfo.Fax = model.Fax;
                shopInfo.ShopImg = model.ShopImg;
                shopInfo.Position = model.Position;
                shopInfo.Body = model.Body;
                shopInfo.Area = model.Area;
                shopInfo.Type = model.Type;
                shopInfo.OrderID = model.OrderID;
                shopInfo.Remark = model.Remark;

                Shop.Create(shopInfo);

                return PromptView("信息新加成功");

            }

            return View(model);

        }


        public ActionResult Del(int[] shopIdList)
        {
            Shop.Del(shopIdList);
            AddAdminOperateLog("删除内容", "删除内容,内容ID为:" + CommonHelper.IntArrayToString(shopIdList));

            return PromptView("招聘信息删除成功");
        }
    }
}
