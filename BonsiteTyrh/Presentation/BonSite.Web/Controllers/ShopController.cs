using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using BonSite.Core;
using BonSite.Services;
using BonSite.Web.Framework;
using BonSite.Web.Models;

namespace BonSite.Web.Controllers
{
    public class ShopController : BaseWebController
    {
        //
        // GET: /Shop/

        public ActionResult Index()
        {
            return View();
        }


        /// <summary>
        /// 门店列表
        /// </summary>
        /// <returns></returns>
        public ActionResult List()
        {

            int classid = GetRouteInt("classid");
            if (classid == 0)
                classid = WebHelper.GetQueryInt("classid");

            string area = GetRouteString("area");
            if (area == "")
                area = WebHelper.GetQueryString("area");
            if (area == "")
                area = "全部";


            int page = GetRouteInt("page");
            if (page == 0)
                page = WebHelper.GetQueryInt("page");


            ArticleClassInfo info = ArticleClass.GetModelById(classid);
            if (info == null)
                return PromptView("/", "您访问的页面不存在");


            string condition = Shop.GetShopListCondition(area,"全部", "");
            string sort = Shop.GetShopListSort("", "asc");

            PageModel pageModel = new PageModel(20, page, Shop.GetShopCount(condition));



            ShopListModel model = new ShopListModel
            {
                ArticleClassID = classid,
                ArticleClassInfo = info,
                ClassPath = ArticleClass.GetArticleClassPath(classid),
                Area = area,
                ShopList = Shop.GetShopList(pageModel.PageSize,pageModel.PageNumber,condition,sort),
                PageModel = pageModel
            };

            //if (info.ListView.Length > 0)
            //    return View("List." + info.ListView, model);
            //else
            //    return View(model);


            List<SelectListItem> shopareaList = new List<SelectListItem>();

            shopareaList.Add(new SelectListItem()
            {
                Text = "全部",
                Value = ""
            });
            foreach (DataRow drRow in Shop.GetShopAreaList().Rows)
            {
                shopareaList.Add(new SelectListItem()
                {
                    Text = drRow["area"].ToString(),
                    Value = drRow["area"].ToString()
                });

            }

            ViewData["shopareaList"] = shopareaList;

            return View(model);
        }
    }
}
