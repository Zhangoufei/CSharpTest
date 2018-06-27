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
    public class InfoController : BaseAdminController
    {
        public ActionResult List(string articleTitle, string Keyword, string sortColumn, string sortDirection, int? articleClassId1, int pageSize = 10, int pageNumber = 1)
        {
            pageSize = WorkContext.SiteConfig.pageSize;
            int articleClassId = 0;

            if (articleClassId1 == null)
            {
                articleClassId = 233;
            }
            else
            {
                articleClassId = (int)articleClassId1;
            }

            ArticleClassInfo classInfo = ArticleClass.AdminGetModelById(articleClassId);

            //if (classInfo == null)
            //    return PromptView("文章分类不存在");

            ////管理菜单
            //if ((classInfo.ClassType.Equals(-1)) || (classInfo.ClassType.Equals(0)))
            //    Response.Redirect(classInfo.AdminUrl);
            ////单页
            //if (classInfo.ClassType.Equals(1))
            //    return RedirectToAction("Page", new { articleClassId = articleClassId });
            string condition = Article.AdminGetArticleListCondition(articleClassId, articleTitle);
            //string condition = " [Keyword] = 1 ";           
            string condition1 = condition + @" or [Keyword] = 1  ";
            string sort = Article.AdminGetArticleListSort(sortColumn, sortDirection);
            PageModel pageModel = new PageModel(pageSize, pageNumber, Article.AdminGetArticleCount(condition1));
            //
            ArticleListModel model = new ArticleListModel()
            {
                DataList = Article.AdminGetArticleList(pageModel.PageSize, pageModel.PageNumber, condition1, sort),
                PageModel = pageModel,
                SortColumn = sortColumn,
                SortDirection = sortDirection,
                ArticleClassID = articleClassId,
                ArticleTitle = articleTitle,
                ClassInfo = classInfo
            };
            List<ArticleClassInfo> classPath = ArticleClass.GetArticleClassPath(articleClassId);
            ViewData["classPath"] = classPath;
            SiteUtils.SetAdminRefererCookie(string.Format("{0}?pageNumber={1}&pageSize={2}&sortColumn={3}&sortDirection={4}&articleClassId={5}&newsTitle={6}",
                                                          Url.Action("List"),
                                                          pageModel.PageNumber,
                                                          pageModel.PageSize,
                                                          sortColumn,
                                                          sortDirection,
                                                          articleClassId,
                                                          articleTitle));
            return View(model);
        }
    }
}




