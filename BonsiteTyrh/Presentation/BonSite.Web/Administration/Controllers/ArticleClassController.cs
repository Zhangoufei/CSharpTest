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
    public class ArticleClassController : BaseAdminController
    {
        //
        // GET: /ArticleClass/

        //public ActionResult Index()
        //{
        //    return View();
        //}

        public ActionResult List(int pageSize = 10, int pageNumber = 1)
        {
            pageSize = WorkContext.SiteConfig.pageSize;
            string condition = "";
            PageModel pageModel = new PageModel(pageSize, pageNumber, BonSite.Services.ArticleClass.AdminGetArticleClassTreeList().ToList().Count);
            ArticleClassListmodel model = new ArticleClassListmodel
            {
                DataInfoList = BonSite.Services.ArticleClass.AdminGetArticleClassTreeList().Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList(),
                PageModel = pageModel
            };
            string[] sizeList = StringHelper.SplitString(WorkContext.SiteConfig.ArticleClassThumbSize);
            ViewData["size"] = sizeList[sizeList.Length / 2];
            SiteUtils.SetAdminRefererCookie(string.Format("{0}",
                                                          Url.Action("List")));
            return View(model);
        }

        [HttpGet]
        public ActionResult Add()
        {
            ArticleClassModel model = new ArticleClassModel { ClassType = 2 };

            Load();
            Loads();
            return View(model);
        }

        [HttpPost]
        public ActionResult Add(ArticleClassModel model)
        {
            ArticleClassInfo info = new ArticleClassInfo();
            //获取当前登录用户信息
            string ck = WebHelper.GetCookie("bs", "uname");
            PartUserInfo partUserInfo = Users.GetPartUserByName(ck);
            if (ModelState.IsValid)
            {

                info.ClassName = model.ClassName;
                info.ParentArticleClassID = model.ParentArticleClassID;
                info.ClassType = model.ClassType;
                info.Target = model.Target;
                info.IsWeb = model.IsWeb;
                info.IsNav = model.IsNav;
                info.WebUrl = model.WebUrl == null ? "" : model.WebUrl;
                info.IsAdmin = model.IsAdmin;
                info.AdminUrl = model.AdminUrl == null ? "" : model.AdminUrl;
                info.DisplayOrder = model.DisplayOrder;
                info.IsOpen = model.IsOpen;
                info.ListView = model.ListView;
                info.ContentView = model.ContentView;
                info.Code = model.Code;
                info.ImgUrl = model.ImgUrl;
                info.Keyword = model.Keyword == null ? "" : model.Keyword;
                info.Description = model.Description == null ? "" : model.Description;
                info.IsClassBrand = model.IsClassBrand;
                info.Subhead = model.Subhead;
                info.Auditor = ck;
                info.IsShowNews = model.IsShowNews;
                ArticleClass.Create(info);
                AddLog(info, "分类新加成功");
                //AddAdminOperateLog
                return PromptView("分类新加成功");
            }
            Loads();
            return View(model);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {

            ArticleClassInfo classinfo = ArticleClass.AdminGetModelById(id);

            if (classinfo == null)
                return PromptView("分类不存在");

            ArticleClassModel model = new ArticleClassModel();

            model.ArticleClassID = classinfo.ArticleClassID;
            model.ClassName = classinfo.ClassName;
            model.ClassType = classinfo.ClassType;
            model.ParentArticleClassID = classinfo.ParentArticleClassID;
            model.Target = classinfo.Target;
            model.IsNav = classinfo.IsNav;
            model.IsWeb = classinfo.IsWeb;
            model.WebUrl = classinfo.WebUrl;
            model.IsAdmin = classinfo.IsAdmin;
            model.AdminUrl = classinfo.AdminUrl;
            model.DisplayOrder = classinfo.DisplayOrder;
            model.IsOpen = classinfo.IsOpen;
            model.ListView = classinfo.ListView;
            model.ContentView = classinfo.ContentView;
            model.Code = classinfo.Code;
            model.ImgUrl = classinfo.ImgUrl;
            model.Keyword = classinfo.Keyword;
            model.Description = classinfo.Description;
            model.IsClassBrand = classinfo.IsClassBrand;
            model.Subhead = classinfo.Subhead;
            model.IsShowNews = classinfo.IsShowNews;

            Load();
            Loads();
            ViewData["referer"] = SiteUtils.GetAdminRefererCookie();

            return View(model);

        }

        [HttpPost]
        public ActionResult Edit(ArticleClassModel model, int id = -1)
        {
            ArticleClassInfo info = ArticleClass.AdminGetModelById(id);
            //获取当前登录用户信息
            string ck = WebHelper.GetCookie("bs", "uname");
            PartUserInfo partUserInfo = Users.GetPartUserByName(ck);
            if (ModelState.IsValid)
            {

                info.ClassName = model.ClassName;
                info.ParentArticleClassID = model.ParentArticleClassID;
                info.ClassType = model.ClassType;
                info.Target = model.Target;
                info.IsWeb = model.IsWeb;
                info.IsNav = model.IsNav;
                info.WebUrl = model.WebUrl == null ? "" : model.WebUrl;
                info.IsAdmin = model.IsAdmin;
                info.AdminUrl = model.AdminUrl == null ? "" : model.AdminUrl;
                info.DisplayOrder = model.DisplayOrder;
                info.IsOpen = model.IsOpen;
                info.ListView = model.ListView;
                info.ContentView = model.ContentView;
                info.Code = model.Code;
                info.ImgUrl = model.ImgUrl;
                info.Keyword = model.Keyword == null ? "" : model.Keyword;
                info.Description = model.Description == null ? "" : model.Description;
                info.IsClassBrand = model.IsClassBrand;
                info.Subhead = model.Subhead;
                info.Auditor = ck;
                info.IsShowNews = model.IsShowNews;
                ArticleClass.Update(info);
                AddLog(info, "分类编辑成功");
                //AddAdminOperateLog
                return PromptView("分类编辑成功");
            }
            Loads();
            return View(model);
        }

        private void AddLog(ArticleClassInfo info, string title)
        {
            string clientIp = GetHostAddress();
            CommonLog.AddArticleClassLog(info, title, clientIp);
        }
        /// <summary>
        /// 获取客户端IP地址（无视代理）
        /// </summary>
        /// <returns>若失败则返回回送地址</returns>
        public string GetHostAddress()
        {
            string userHostAddress = Request.UserHostAddress;

            if (string.IsNullOrEmpty(userHostAddress))
            {
                userHostAddress = Request.ServerVariables["REMOTE_ADDR"];
            }

            //最后判断获取是否成功，并检查IP地址的格式（检查其格式非常重要）
            if (!string.IsNullOrEmpty(userHostAddress) && CommonHelper.IsIP(userHostAddress))
            {
                return userHostAddress;
            }
            return "127.0.0.1";
        }

        /// <summary>
        /// 初始化，载入下拉菜单等
        /// </summary>
        public void Load()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem() { Text = "郑州八中校本部", Value = "0" });

            //分校区 列表添加
            list.Add(new SelectListItem() { Text = "郑州市经纬中学", Value = "-1" });
            list.Add(new SelectListItem() { Text = "高新区实验学校", Value = "-2" });

            foreach (ArticleClassInfo info in ArticleClass.AdminGetArticleClassTreeList())
            {
                list.Add(new SelectListItem() { Text = info.ClassName, Value = info.ArticleClassID.ToString() });
            }

            ViewData["articleClassList"] = list;

        }


        /// <summary>
        /// 删除栏目
        /// </summary>
        public ActionResult Del(int id = -1)
        {
            ArticleClassInfo info = ArticleClass.AdminGetModelById(id);

            if (!ArticleClass.HaveChild(id))
            {
                ArticleClass.Delete(id);
                if (info != null)
                {
                    AddLog(info, "栏目删除成功");
                }
                //AddAdminOperateLog("删除广告位置", "删除广告位置,广告位置ID为:" + adPosId);
                return PromptView("栏目删除成功");
            }
            else
            {
                return PromptView("本分类下还有子分类，请先删除小分类后再删除大分类");

            }
        }
        //图片上传
        private void Loads()
        {
            string allowImgType = string.Empty;
            string[] imgTypeList = StringHelper.SplitString(BSConfig.SiteConfig.UploadImgType, ",");
            foreach (string imgType in imgTypeList)
                allowImgType += string.Format("*{0};", imgType.ToLower());

            string[] sizeList = StringHelper.SplitString(WorkContext.SiteConfig.ArticleClassThumbSize);

            ViewData["size"] = sizeList[sizeList.Length / 2];
            ViewData["allowImgType"] = allowImgType;
            ViewData["maxImgSize"] = BSConfig.SiteConfig.UploadImgSize;
            ViewData["referer"] = SiteUtils.GetAdminRefererCookie();
        }
    }
}
