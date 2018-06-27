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
    public class VoteController : BaseWebController
    {
        //
        // GET: /Vote/

        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 获取投票结果详情
        /// </summary>
        /// <returns></returns>
        public ActionResult VoteResult()
        {
            int id = GetRouteInt("id");
            if (id == 0)
                id = WebHelper.GetQueryInt("id");

            VoteInfo voteInfo = Vote.GetVote(id);
            if (voteInfo==null)
                return PromptView("/","您访问的页面不存在");

            List<VoteResultInfo> voteResultInfo = Vote.GetVoteResultsList(id);

            VoteResultModel model = new VoteResultModel
            {
                ResultsList = voteResultInfo,
                Id = voteInfo.Id,
                Title = voteInfo.Title,
                State = voteInfo.State,
                Type = voteInfo.Type

            };

            return View(model);
        
        }

        public ActionResult SubmitVote()
        {
            int voteid = WebHelper.GetFormInt("voteid");
            string resultid = WebHelper.GetFormString("resultid");

            if (resultid.Length==0)
            {
                return AjaxResult("error", "请选择投票项目！");             
            }

            string[] resultidlist = resultid.Split(',');

            if (resultidlist.Length > 0)
            {
                for (int i = 0; i < resultidlist.Length; i++)
                {
                    Vote.VoteResults(int.Parse(resultidlist[i]));
                }

                return AjaxResult("success", "投票成功");
            }
            else
            {
                return AjaxResult("error", "请选择投票项目！");            
            }


        }

    }
}
