using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CyPhone.Common;
using CyPhone.Common.UI;
using CyPhone.BLL;
using CyPhone.Model;

namespace CyPhone.Web.Controllers
{
    public class NewsController : Controller
    {
        // GET: News
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult search()
        {
            int pageIndex = WebHelper.GetQueryInt("pn", 1);
            int pageSize = ConstConfig.PageSize;
            //查询条件
            string word = WebHelper.GetQueryString("wd");
            ArticleBll bll = new ArticleBll();
            #region 查询结果
            List<bs_Article> newslist = bll.GetArticleListByKeyWords(word, pageIndex, pageSize); 
            int listcnt = bll.GetArticleRecordCount(word);
            ViewData["keywords"] = word;
            //当前页
            ViewData["pn"] = pageIndex;
            //总数据数
            ViewData["listcnt"] = listcnt;
            //分页页数
            ViewData["pagecnt"] = Math.Ceiling((float)listcnt / pageSize);
            #endregion

            #region 最新资讯
            //前10条
            List<bs_Article> newlist = bll.GetArticleListByKeyWords("", 1, 10);
            ViewData["newlist"] = newlist;
            #endregion
            return View(newslist);
        }
    }
}