using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using BonSite.Core;
using BonSite.Services;
using BonSite.Web.Framework;
using BonSite.Web.Admin.Models;

namespace BonSite.Web.Admin.Controllers
{
    public class VoteController :BaseAdminController
    {
        //
        // GET: /Vote/

        public ActionResult Index()
        {
            return View();
        }


        public ActionResult VoteList(int pageSize = 15, int pageNumber = 1)
        {
            PageModel pageModel = new PageModel(pageSize, pageNumber, Vote.AdminGetVoteCount());

            VoteListModel model = new VoteListModel()
            {
                PageModel = pageModel,
                VoteList = Vote.AdminGetVoteList(pageModel.PageSize, pageModel.PageNumber)
            };

            SiteUtils.SetAdminRefererCookie(Url.Action("VoteList"));

            return View(model);
        }

        [HttpGet]
        public ActionResult AddVote()
        {
            VoteModel model = new VoteModel()
            {
                StartTime = DateTime.Now,
                EndTime = DateTime.Now.AddMonths(1),
                State = 1
            };
            ViewData["referer"] = SiteUtils.GetAdminRefererCookie();
            return View(model);
        }

        [HttpPost]
        public ActionResult AddVote(VoteModel model)
        {
            if (ModelState.IsValid)
            {
                VoteInfo voteInfo = new VoteInfo();
                voteInfo.Title = model.Title;
                voteInfo.StartTime = model.StartTime;
                voteInfo.EndTime = model.EndTime;
                voteInfo.State = model.State;
                voteInfo.Type = model.Type;

                Vote.CreateVote(voteInfo);

                return PromptView("投票基本信息添加成功");
            }
            ViewData["referer"] = SiteUtils.GetAdminRefererCookie();
            return View(model);        
        }

        [HttpGet]
        public ActionResult EditVote(int Id=-1)
        {
            VoteInfo voteInfo = Vote.GetVote(Id);
            if (voteInfo == null)
                return PromptView("投票信息不存在");

            VoteModel model = new VoteModel
            {
                Title = voteInfo.Title,
                StartTime = voteInfo.StartTime,
                EndTime = voteInfo.EndTime,
                State = voteInfo.State,
                Type = voteInfo.Type
            };

            ViewData["referer"] = SiteUtils.GetAdminRefererCookie();

            return View(model);        
        }

        public ActionResult EditVote(VoteModel model, int Id = -1)
        {
            VoteInfo voteInfo = Vote.GetVote(Id);
            if (voteInfo == null)
                return PromptView("投票信息不存在");

            if (ModelState.IsValid)
            {
                voteInfo.Title = model.Title;
                voteInfo.StartTime = model.StartTime;
                voteInfo.EndTime = model.EndTime;
                voteInfo.State = model.State;
                voteInfo.Type = model.Type;

                Vote.UpdateVote(voteInfo);
                return PromptView("投票信息修改成功");
            }
            ViewData["referer"] = SiteUtils.GetAdminRefererCookie();
            return View(model);
        
        }


        public ActionResult DelVote(int Id = -1)
        {
            Vote.DeleteVote(Id);
            return PromptView("投票删除成功");
        }


        public ActionResult VoteResultsList(int Id = -1)
        {
            List<VoteResultInfo> voteResultInfo = Vote.GetVoteResultsList(Id);

            VoteResultListModel model = new VoteResultListModel
            {
                ResultsList = voteResultInfo
            };


            SiteUtils.SetAdminRefererCookie(Url.Action("VoteResultsList", new { Id = Id }));

            return View(model);
        }

        [HttpGet]
        public ActionResult AddVoteResult(int voteID = -1)
        {
            VoteResultModel model = new VoteResultModel
            {
                VoteId = voteID
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult AddVoteResult(VoteResultModel model)
        {
            if (ModelState.IsValid)
            {
                VoteResultInfo voteResultInfo = new VoteResultInfo { 
                    VoteId=model.VoteId,
                    DisplayOrder=model.DisplayOrder,
                    Result=model.Result,
                    Count=model.Count
                };
                Vote.CreateVoteResults(voteResultInfo);

                return PromptView("投票选项添加成功");
            }

            ViewData["referer"] = SiteUtils.GetAdminRefererCookie();
            return View(model);
        }

        [HttpGet]
        public ActionResult EditVoteResult(int Id = -1, int resultId = -1)
        {
            VoteResultInfo voteResultInfo = Vote.GetVoteResult(resultId);
            if (voteResultInfo == null)
                return PromptView("投票结果不存在");

            VoteResultModel model = new VoteResultModel
            {
                VoteId = voteResultInfo.VoteId,
                DisplayOrder = voteResultInfo.DisplayOrder,
                Result = voteResultInfo.Result,
                Count = voteResultInfo.Count
            };

            return View(model);            
        }

        [HttpPost]
        public ActionResult EditVoteResult(VoteResultInfo model, int Id = -1, int resultId = -1)
        {
            VoteResultInfo voteResultInfo = Vote.GetVoteResult(resultId);
            if (voteResultInfo == null)
                return PromptView("投票结果不存在");

            if (ModelState.IsValid)
            { 
                voteResultInfo.DisplayOrder=model.DisplayOrder;
                voteResultInfo.Result=model.Result;
                voteResultInfo.Count = model.Count;

                Vote.UpdateVoteResults(voteResultInfo);

                return PromptView("投票结果修改成功");

            };

            ViewData["referer"] = SiteUtils.GetAdminRefererCookie();
            return View(model);
        }


        public ActionResult DelVoteResult(int resultId = -1)
        {
            Vote.DeleteVoteResults(resultId);
            return PromptView("投票结果删除成功");

        }
    }
}
