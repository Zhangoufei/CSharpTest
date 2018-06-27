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
    public class WeChatController : BaseWebController
    {
        public ActionResult Index(int? id)
        {
            int id1 = id == null ? 0 : (int)id;

            ArticleInfo info = Article.GetModelByArticleID(id1);
            if (info == null)
                return PromptView("/Null/Index", "链接失效!");

            ArticleModel model = new ArticleModel();


            model.ArticleInfo = info;
            model.ArticleClassInfo = ArticleClass.GetModelById(info.ArticleClassID);
            model.ClssPath = ArticleClass.GetArticleClassPath(info.ArticleClassID);
            //访问量加一
            ArticleInfo info2 = Article.GetModelByArticleID(id1);
            int Hits = Convert.ToInt32(info2.Hits) + 1;
            info2.Hits = Hits;
            Article.Update(info2);
            return View(model);

        }
    }
}