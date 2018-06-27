using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using BonSite.Core;
using BonSite.Services;
using BonSite.Web.Framework;
using BonSite.Web.Admin;
using BonSite.Web.Admin.Models;

namespace BonSite.Web.Admin.Controllers
{
    public class ProductController : BaseAdminController
    {
        //
        // GET: /Product/

        public ActionResult Index()
        {
            return View();
        }



        /// <summary>
        /// 列表
        /// </summary>
        /// <param name="articleTitle"></param>
        /// <param name="sortColumn"></param>
        /// <param name="sortDirection"></param>
        /// <param name="articleClassId"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <returns></returns>
        public ActionResult List(string productTitle, string sortColumn, string sortDirection, int productClassId, int pageSize = 15, int pageNumber = 1)
        {
            pageSize = WorkContext.SiteConfig.pageSize;
            ArticleClassInfo classInfo = ArticleClass.AdminGetModelById(productClassId);

            if (classInfo == null)
                return PromptView("文章分类不存在");


            string condition = Product.AdminGetProductListCondition(productClassId, productTitle);
            string sort = Product.AdminGetProductListSort(sortColumn, sortDirection);
            PageModel pageModel = new PageModel(pageSize, pageNumber, Product.AdminGetProductCount(condition));

            ProductListModel model = new ProductListModel()
            {
                DataList = Product.AdminGetProductList(pageModel.PageSize, pageModel.PageNumber, condition, sort),
                PageModel = pageModel,
                SortColumn = sortColumn,
                SortDirection = sortDirection,
                ProductClassID = productClassId,
                ProductTitle = productTitle,
                ClassInfo = classInfo

            };

            SiteUtils.SetAdminRefererCookie(string.Format("{0}?pageNumber={1}&pageSize={2}&sortColumn={3}&sortDirection={4}&productClassId={5}&productTitle={6}",
                                                          Url.Action("List"),
                                                          pageModel.PageNumber,
                                                          pageModel.PageSize,
                                                          sortColumn,
                                                          sortDirection,
                                                          productClassId,
                                                          productTitle));

            //List<SelectListItem> list = new List<SelectListItem>();
            //list.Add(new SelectListItem() { Text = "全部类型", Value = "0" });
            //foreach (NewsTypeInfo newsTypeInfo in AdminNews.GetNewsTypeList())
            //{
            //    list.Add(new SelectListItem() { Text = newsTypeInfo.Name, Value = newsTypeInfo.NewsTypeId.ToString() });
            //}
            //ViewData["newsTypeList"] = list;
            List<ArticleClassInfo> classPath = ArticleClass.GetArticleClassPath(productClassId);
            ViewData["classPath"] = classPath;
            return View(model);
        }




        [HttpGet]
        public ActionResult Edit(int productid = -1)
        {
            ProductInfo productInfo = Product.GetModelByProductID(productid);

            if (productInfo == null)
                return PromptView("产品不存在");

            ProductModel model = new ProductModel()
            {
                ProductClassID = productInfo.ProductClassID,
                Title = productInfo.Title,
                Code = productInfo.Code,
                Type = productInfo.Type,
                Provider = productInfo.Provider,
                Digest = productInfo.Digest,
                ImgUrl = productInfo.ImgUrl,
                Body = productInfo.Body,
                IsShow = productInfo.IsShow,
                IsBest = productInfo.IsBest,
                IsTop = productInfo.IsTop,
                AddTime = productInfo.AddTime,
                AdminID = productInfo.AdminID,
                BigImgUrl = productInfo.BigImgUrl,
                DisplayOrder = productInfo.DisplayOrder,
                Hits = productInfo.Hits,
                Keys = productInfo.Keys,
                UpdateTime = productInfo.UpdateTime,
                Keyword = productInfo.Keyword,
                Description = productInfo.Description
            };


            List<ArticleClassInfo> classPath = ArticleClass.GetArticleClassPath(productInfo.ProductClassID);
            ViewData["classPath"] = classPath;

            Load();

            ViewData["referer"] = SiteUtils.GetAdminRefererCookie();

            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(ProductModel model, int productid = -1)
        {
            ProductInfo productInfo = Product.GetModelByProductID(productid);
            if (productInfo == null)
                return PromptView("产品不存在");

            if (ModelState.IsValid)
            {

                productInfo.ProductClassID = model.ProductClassID;
                productInfo.Title = model.Title;
                productInfo.Code = model.Code;
                productInfo.Type = model.Type;
                productInfo.Provider = model.Provider;
                productInfo.Digest = model.Digest;
                productInfo.ImgUrl = model.ImgUrl;
                productInfo.BigImgUrl = model.BigImgUrl;
                productInfo.Body = model.Body ?? "";
                productInfo.IsShow = model.IsShow;
                productInfo.IsBest = model.IsBest;
                productInfo.IsTop = model.IsTop;
                productInfo.AddTime = model.AddTime;
                productInfo.UpdateTime = DateTime.Now;
                productInfo.DisplayOrder = model.DisplayOrder;
                productInfo.Hits = model.Hits;
                productInfo.AdminID = model.AdminID;
                productInfo.Keys = model.Keys;
                productInfo.Keyword = model.Keyword == null ? "" : model.Keyword;
                productInfo.Description = model.Description == null ? "" : model.Description;

                Product.Update(productInfo);

                //AddAdminOperateLog
                return PromptView("产品修改成功");
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult Add(int productClassId)
        {
            ProductModel model = new ProductModel() { AddTime = DateTime.Now, UpdateTime = DateTime.Now, IsShow = 1 };

            List<ArticleClassInfo> classPath = ArticleClass.GetArticleClassPath(productClassId);
            ViewData["classPath"] = classPath;

            Load();

            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Add(ProductModel model)
        {
            ProductInfo productInfo = new ProductInfo();

            if (ModelState.IsValid)
            {

                productInfo.ProductClassID = model.ProductClassID;
                productInfo.Title = model.Title;
                productInfo.Code = model.Code;
                productInfo.Type = model.Type;
                productInfo.Provider = model.Provider;
                productInfo.Digest = model.Digest;
                productInfo.ImgUrl = model.ImgUrl;
                productInfo.BigImgUrl = model.BigImgUrl;
                productInfo.Body = model.Body ?? "";
                productInfo.IsShow = model.IsShow;
                productInfo.IsBest = model.IsBest;
                productInfo.IsTop = model.IsTop;
                productInfo.UpdateTime = DateTime.Now;
                productInfo.AddTime = model.AddTime;
                productInfo.AdminID = WorkContext.Uid;
                productInfo.Hits = 0;
                productInfo.Keys = model.Keys;
                productInfo.DisplayOrder = model.DisplayOrder;
                productInfo.Keyword = model.Keyword == null ? "" : model.Keyword;
                productInfo.Description = model.Description == null ? "" : model.Description;

                Product.Create(productInfo);

                //AddAdminOperateLog
                return PromptView("产品新加成功");
            }
            return View(model);
        }

        public ActionResult Del(int[] productIdList)
        {
            Product.Del(productIdList);
            AddAdminOperateLog("删除内容", "删除内容,内容ID为:" + CommonHelper.IntArrayToString(productIdList));

            return PromptView("产品删除成功");
        }



        /// <summary>
        /// 初始化，载入下拉菜单等
        /// </summary>
        public void Load()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem() { Text = "顶级分类", Value = "0" });
            foreach (ArticleClassInfo info in ArticleClass.AdminGetArticleClassTreeList())
            {
                list.Add(new SelectListItem() { Text = info.ClassName, Value = info.ArticleClassID.ToString() });
            }

            ViewData["productClassList"] = list;


            List<SelectListItem> list1 = new List<SelectListItem>();
            list1.Add(new SelectListItem() { Text = "玻璃CD ", Value = "玻璃CD" });
            list1.Add(new SelectListItem() { Text = "CD", Value = "CD" });
            list1.Add(new SelectListItem() { Text = "DVD", Value = "DVD" });
            list1.Add(new SelectListItem() { Text = "黑胶", Value = "黑胶" });
            list1.Add(new SelectListItem() { Text = "发烧碟", Value = "发烧碟" });


            ViewData["typelist"] = list1;



            string allowImgType = string.Empty;
            string[] imgTypeList = StringHelper.SplitString(BSConfig.SiteConfig.UploadImgType, ",");
            foreach (string imgType in imgTypeList)
                allowImgType += string.Format("*{0};", imgType.ToLower());

            string[] sizeList = StringHelper.SplitString(WorkContext.SiteConfig.ArticleImgThumbSize);

            ViewData["size"] = sizeList[sizeList.Length / 2];
            ViewData["allowImgType"] = allowImgType;
            ViewData["maxImgSize"] = BSConfig.SiteConfig.UploadImgSize;
            ViewData["referer"] = SiteUtils.GetAdminRefererCookie();
        }
    }
}
