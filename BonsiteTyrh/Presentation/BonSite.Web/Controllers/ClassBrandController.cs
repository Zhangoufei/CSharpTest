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
    public class ClassBrandController:BaseWebController
    {
        //班牌内容
        public ActionResult View(int? id)
        {
            
            //select TOP 1 [ArticleId] from [bs_Article] where (ArticleClassId=243) order by UpdateTime desc 
            int id1 = 0;
            int articleClassId = 0;
            articleClassId=Article.GetArticleClassIdByIsClassBrand();

            if (id == null)
            {
                //获取班牌专栏最新新闻
                id1 = Article.GetTopArticleIdByArticleClassId(articleClassId);
            }
            else
            {
                id1 = (int)id;
            }
            
            ArticleInfo info = Article.GetModelByArticleID(id1);
            if (info == null)
                return PromptView("/Null/Index", "链接失效!");

            //if (info.IsShow == 0)
            //    return PromptView("/", "您访问的页面不存在");
            ArticleModel model = new ArticleModel();
            
            if(info.IsClassBrand==1){
                model.ArticleInfo = info;
                model.ArticleClassInfo = ArticleClass.GetModelById(info.ArticleClassID);
                model.ClssPath = ArticleClass.GetArticleClassPath(info.ArticleClassID);

            return View(model);
            }else{
                return PromptView("/Null/Index", "链接失效!");
            }

        }
    }
}