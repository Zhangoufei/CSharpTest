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
using System.Text;

namespace BonSite.Web.Controllers
{
    public class JobController : BaseWebController
    {
        //
        // GET: /Job/

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

            string city = GetRouteString("city");
            if (city == "")
                city = WebHelper.GetQueryString("city");
            if (city == "")
                city = "全部";


            int page = GetRouteInt("page");
            if (page == 0)
                page = WebHelper.GetQueryInt("page");


            ArticleClassInfo info = ArticleClass.GetModelById(classid);
            if (info == null)
                return PromptView("/", "您访问的页面不存在");


            string condition = Job.GetJobListCondition(city,"");
            string sort = Job.GetJobListSort("", "asc");

            PageModel pageModel = new PageModel(20, page, Job.GetJobCount(condition));



            JobListModel model = new JobListModel
            {
                ArticleClassID = classid,
                ArticleClassInfo = info,
                ClassPath = ArticleClass.GetArticleClassPath(classid),
                City = city,
                JobList = Job.GetJobList(pageModel.PageSize, pageModel.PageNumber, condition, sort),
                PageModel = pageModel
            };

            //if (info.ListView.Length > 0)
            //    return View("List." + info.ListView, model);
            //else
            //    return View(model);


            List<SelectListItem> jobcityList = new List<SelectListItem>();

            jobcityList.Add(new SelectListItem()
            {
                Text = "全部",
                Value = ""
            });
            foreach (DataRow drRow in Job.GetJobCityList().Rows)
            {
                jobcityList.Add(new SelectListItem()
                {
                    Text = drRow["city"].ToString(),
                    Value = drRow["city"].ToString()
                });

            }

            ViewData["jobcityList"] = jobcityList;


            return View(model);
        }

    }
}
