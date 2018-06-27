using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BonSite.Core;
using BonSite.Services;
using BonSite.Web.Admin.Models;
using BonSite.Web.Framework;

namespace BonSite.Web.Admin.Controllers
{
    public class JobController : BaseAdminController
    {
        //
        // GET: /Job/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List(int pageSize = 15, int pageNumber = 1)
        {
            string condition = Job.AdminGetJobListCondition("");
            string sort = Job.AdminGetJobListSort("","");

            PageModel pageModel = new PageModel(pageSize, pageNumber, Job.AdminGetJobCount(condition));

            JobListModel model = new JobListModel()
            {
                DataList = Job.AdminGetJobList(pageSize,pageNumber,condition,sort),
                PageModel = pageModel
            };

            SiteUtils.SetAdminRefererCookie(Url.Action("List", "Job"));

            return View(model);

        }

        [HttpGet]
        public ActionResult Edit(int jobid = -1)
        {
            JobInfo jobinfo = Job.GetModelByJobID(jobid);

            if (jobinfo == null)
                return PromptView("信息不存在");

            JobModel model=new JobModel()
            {
                JobID = jobinfo.JobID,
                JobTitle = jobinfo.JobTitle,
                PubDate = jobinfo.PubDate,
                EndDate = jobinfo.EndDate,
                Number = jobinfo.Number,
                State = jobinfo.State,
                Body = jobinfo.Body,
                City = jobinfo.City
            };

            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(JobInfo model, int jobid)
        {
            JobInfo jobinfo = Job.GetModelByJobID(jobid);

            if (jobinfo == null)
                return PromptView("信息不存在");

            if (ModelState.IsValid)
            {
                jobinfo.JobTitle = model.JobTitle;
                jobinfo.PubDate = model.PubDate;
                jobinfo.EndDate = model.EndDate;
                jobinfo.Number = model.Number;
                jobinfo.State = model.State;
                jobinfo.Body = model.Body;
                jobinfo.City = model.City;

                Job.Update(jobinfo);

                return PromptView("信息修改成功！");

            }

            return View(model);

        }

        [HttpGet]
        public ActionResult Add()
        {
            JobModel model=new JobModel()
            {
                PubDate = DateTime.Now,
                EndDate = DateTime.Now.AddMonths(1),
                Number = 2,
                State = 1
            };

            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Add(JobInfo model)
        {
            JobInfo jobinfo=new JobInfo();

            if (ModelState.IsValid)
            {

                jobinfo.JobTitle = model.JobTitle;
                jobinfo.PubDate = model.PubDate;
                jobinfo.EndDate = model.EndDate;
                jobinfo.Number = model.Number;
                jobinfo.State = model.State;
                jobinfo.Body = model.Body;
                jobinfo.City = model.City;

                Job.Create(jobinfo);

                return PromptView("信息新加成功！");
            }

            return View(model);
        }

        public ActionResult Del(int[] jobIdList)
        {
            Job.Del(jobIdList);
            AddAdminOperateLog("删除内容", "删除内容,内容ID为:" + CommonHelper.IntArrayToString(jobIdList));

            return PromptView("招聘信息删除成功");
        }

    }
}
