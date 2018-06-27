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
    public class FeedBackController : BaseWebController
    {
        //
        // GET: /FeedBack/





        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SearchByKey()
        {
            string key = WebHelper.GetFormString("searchkey");
            //if (string.IsNullOrEmpty(key))
            //{
                
                FeedBackInfo info = BonSite.Services.FeedBack.GetFeedBackBySearchKey(key);

                if (info != null)
                {
                    string sb = string.Format("\"title\":\"{0}\",\"body\":\"{1}\",\"reply\":\"{2}\"", info.Title, info.Body, info.Reply);

                    return AjaxResult("success", "{" + sb + "}", true);
                }
                else
                {
                    return AjaxResult("error", "查询码错误或是问题尚未回复！");
                }

            //}
            //else
            //{
            //    return AjaxResult("error", "请输入查询码！");
            //}
        }

        public ActionResult SubmitFeedBack()
        {
            string key = Randoms.CreateRandomValue(2, true) + DateTime.Now.Ticks.ToString();
            //;

            FeedBackInfo info = new FeedBackInfo
            {
                FeedBackTypeId = WebHelper.GetFormInt("typeid"),
                Tag = WebHelper.GetFormString("tag"),
                LinkMan = WebHelper.GetFormString("LinkMan"),
                Tel = WebHelper.GetFormString("Tel"),
                Mobile = WebHelper.GetFormString("Moblie"),
                Email = WebHelper.GetFormString("Email"),
                Title = WebHelper.GetFormString("Title"),
                Body = WebHelper.GetFormString("body"),
                AddTime = DateTime.Now,
                Reply = "",
                ReplyTime = DateTime.Now,
                State = 0,
                IsOut = WebHelper.GetFormInt("isOut"),
                Ip = WebHelper.GetIP(),
                SearchKey = key
            };
            FeedBack.CreateFeedBackInfo(info);

            //发送邮件提醒
            Emails.SendFeedbackMsg( info);


            return AjaxResult("success", key);

        }


        public ActionResult List(int typeId,int page=0)
        {
            FeedBackTypeInfo typeinfo = FeedBack.GetFeedBackTypeById(typeId);
            if (typeinfo == null)
                return PromptView("/", "您访问的页面不存在");        

            PageModel pageModel = new PageModel(20, page, FeedBack.GetFeedBackListCount(typeId));

            FeedBackListModel model = new FeedBackListModel
            {
                //ArticleClassInfo = info,
                FeedBackTypeInfo = typeinfo,
                FeedBackList = FeedBack.GetFeedBackList(pageModel.PageSize, pageModel.PageNumber, typeId),
                PageModel = pageModel,
                FeedBackModel = new FeedBackModel()
            };
            return View(model);
        }


    }
}
