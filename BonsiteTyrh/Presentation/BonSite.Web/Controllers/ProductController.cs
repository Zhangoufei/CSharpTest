using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using BonSite.Core;
using BonSite.Services;
using BonSite.Web.Framework;
using BonSite.Web.Models;
using System.Text;

namespace BonSite.Web.Controllers
{
    public class ProductController : BaseWebController
    {
        //
        // GET: /Product/

        public ActionResult Index()
        {
            return View();
        }


        /// <summary>
        /// 产品列表
        /// </summary>
        /// <returns></returns>
        public ActionResult List()
        {
            int classid = GetRouteInt("classid");
            if (classid == 0)
                classid = WebHelper.GetQueryInt("classid");
            int page = GetRouteInt("page");
            if (page == 0)
                page = WebHelper.GetQueryInt("page");

            ArticleClassInfo info = ArticleClass.GetModelById(classid);
            if (info == null)
                return PromptView("/", "您访问的页面不存在");

            string condition = Product.GetProductListCondition(classid, "");
            string sort = Product.GetProductListSort("", "");

            PageModel pageModel = new PageModel(20, page, Product.GetProductCount(condition));



            ProductListModel model = new ProductListModel
            {
                ArticleClassID = classid,
                ArticleClassInfo = info,
                ClassPath = ProductClass.GetProductClassPath(classid),
                ProductList = Product.GetProductList(pageModel.PageSize, pageModel.PageNumber, condition, sort),
                PageModel = pageModel
            };

            if (info.ListView.Length > 0)
                return View("List." + info.ListView, model);
            else
                return View(model);
        }

        /// <summary>
        /// 产品详情
        /// </summary>
        /// <returns></returns>
        public ActionResult Details()
        {
            int id = GetRouteInt("id");
            if (id == 0)
                id = WebHelper.GetQueryInt("id");

            ProductInfo info = Product.GetModelByProductID(id);
            if (info == null)
                return PromptView("/", "您访问的页面不存在");


            ProductModel model = new ProductModel
            {
                ProductInfo = info,
                ArticleClassInfo = ArticleClass.GetModelById(info.ProductClassID),
                ClassPath = ArticleClass.GetArticleClassPath(info.ProductClassID)
            };

            if (model.ArticleClassInfo.ContentView.Length > 0)
                return View("details." + model.ArticleClassInfo.ContentView, model);
            else
                return View(model);
        }
    }
}
