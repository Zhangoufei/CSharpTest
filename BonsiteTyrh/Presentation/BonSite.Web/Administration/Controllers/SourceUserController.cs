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
using System.Net;
using System.IO;
using System.Text;
using Newtonsoft.Json.Linq;

namespace BonSite.Web.Admin.Controllers
{
    public class SourceUserController : BaseAdminController
    {
        #region 列表
        public ActionResult List(string articleTitle, string Keyword, string sortColumn, string sortDirection, int? articleClassId1, int pageSize = 15, int pageNumber = 1)
        {
            pageSize = WorkContext.SiteConfig.pageSize;
            int articleClassId = 0;

            if (articleClassId1 == null)
            {
                articleClassId = 243;
            }
            else
            {
                articleClassId = (int)articleClassId1;
            }

            ArticleClassInfo classInfo = ArticleClass.AdminGetModelById(articleClassId);

            int userID = WorkContext.Uid;

            string condition = Article.AdminGetArticleListCondition(articleClassId, articleTitle);

            string condition1 = @" ( " + condition + @" or [Keyword] = 4 ) " + " and [AdminID] = " + userID;
            string sort = Article.AdminGetArticleListSort(sortColumn, sortDirection);
            PageModel pageModel = new PageModel(pageSize, pageNumber, Article.AdminGetArticleCount(condition1));

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
            SiteUtils.SetAdminRefererCookie(string.Format("{0}?pageNumber={1}&pageSize={2}&sortColumn={3}&sortDirection={4}&articleClassId={5}&newsTitle={6}",
                                                          Url.Action("List"),
                                                          pageModel.PageNumber,
                                                          pageModel.PageSize,
                                                          sortColumn,
                                                          sortDirection,
                                                          articleClassId,
                                                          articleTitle));
            List<ArticleClassInfo> classPath = ArticleClass.GetArticleClassPath(articleClassId);
            ViewData["classPath"] = classPath;
            return View(model);
        }

        #endregion
    }
}