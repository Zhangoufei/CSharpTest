using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;


using BonSite.Core;
using BonSite.Services;
using BonSite.Web.Framework;
using BonSite.Web.Admin.Models;

namespace BonSite.Web.Admin.Controllers
{
    public class FeedBackController : BaseAdminController
    {
        //
        // GET: /FeedBack/

        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 反馈列表
        /// </summary>
        /// <param name="typeId"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <returns></returns>
        public ActionResult FeedBackList(int typeId = 0, int pageSize = 15, int pageNumber = 1)
        {
            pageSize = WorkContext.SiteConfig.pageSize;
            PageModel pageModel = new PageModel(pageSize, pageNumber, FeedBack.AdminGetFeedBackListCount(typeId));

            FeedBackListModel model = new FeedBackListModel
            {
                PageModel = pageModel,
                FeedBackTypeId = typeId,
                FeedBackList = FeedBack.AdminGetFeedBackList(pageModel.PageSize, pageModel.PageNumber, typeId)
            };
            SiteUtils.SetAdminRefererCookie(Url.Action("FeedBackList", new { typeId = typeId }));
            return View(model);
        }

        [HttpGet]
        public ActionResult EditFeedBack(int Id = -1)
        {
            FeedBackInfo feedBackInfo = FeedBack.GetFeedBackById(Id);
            if (feedBackInfo==null)
                return PromptView("信息反馈信息不存在");

            FeedBackModel model = new FeedBackModel
            {
                Id = feedBackInfo.Id,
                FeedBackTypeId = feedBackInfo.FeedBackTypeId,
                Tag = feedBackInfo.Tag,
                LinkMan = feedBackInfo.LinkMan,
                Tel = feedBackInfo.Tel,
                Mobile = feedBackInfo.Mobile,
                Email = feedBackInfo.Email,
                Title = feedBackInfo.Title,
                Body = feedBackInfo.Body,
                AddTime = feedBackInfo.AddTime,
                Reply = feedBackInfo.Reply,
                ReplyTime = feedBackInfo.ReplyTime,
                State = feedBackInfo.State,
                IsOut = feedBackInfo.IsOut,
                Ip = feedBackInfo.Ip,
                SearchKey = feedBackInfo.SearchKey
            };

            ViewData["referer"] = SiteUtils.GetAdminRefererCookie();
            return View(model);
        }

        public ActionResult EditFeedBack(FeedBackModel model, int Id = -1)
        {

            FeedBackInfo feedBackInfo = FeedBack.GetFeedBackById(Id);
            if (feedBackInfo == null)
                return PromptView("信息反馈信息不存在");

            if (ModelState.IsValid)
            {
                feedBackInfo.Tag = model.Tag;
                feedBackInfo.LinkMan = model.LinkMan;
                feedBackInfo.Tel = model.Tel;
                feedBackInfo.Mobile = model.Mobile;
                feedBackInfo.Email = model.Email;
                feedBackInfo.Title = model.Title;
                feedBackInfo.Body = model.Body;
                feedBackInfo.Reply = model.Reply;
                feedBackInfo.State = model.State;
                feedBackInfo.IsOut = model.IsOut;

                if (model.State.Equals(1))
                    feedBackInfo.ReplyTime = DateTime.Now;

                FeedBack.UpdateFeedBackInfo(feedBackInfo);

                return PromptView("信息反馈修改成功");

            }
            ViewData["referer"] = SiteUtils.GetAdminRefererCookie();
            return View(model);

        }

        /// <summary>
        /// 新加反馈分类
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult AddFeedBackType()
        {
            FeedBackTypeModel model = new FeedBackTypeModel();
            ViewData["referer"] = SiteUtils.GetAdminRefererCookie();
            return View(model);
        
        }

        /// <summary>
        /// 新加反馈分类
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AddFeedBackType(FeedBackTypeModel model)
        {
            if (ModelState.IsValid)
            {
                FeedBackTypeInfo feedBackInfo = new FeedBackTypeInfo
                {
                    TypeName = model.TypeName,
                    IsShowList = model.IsShowList,
                    Body = model.Body,
                    Tags = model.Tags
                };

                FeedBack.CreateFeedBackType(feedBackInfo);

                return PromptView("信息反馈分类添加成功");
            }
            ViewData["referer"] = SiteUtils.GetAdminRefererCookie();
            return View(model);
        
        }

        
        [HttpGet]
        public ActionResult EditFeedBackType(int TypeId = -1)
        {
            FeedBackTypeInfo feedBackTypeInfo = FeedBack.GetFeedBackTypeById(TypeId);
            if (feedBackTypeInfo == null)
                return PromptView("信息反馈分类不存在");

            FeedBackTypeModel model = new FeedBackTypeModel
            {
                TypeName = feedBackTypeInfo.TypeName,
                IsShowList = feedBackTypeInfo.IsShowList,
                Body = feedBackTypeInfo.Body,
                Tags = feedBackTypeInfo.Tags
            };

            return View(model);

        }

        /// <summary>
        /// 编辑信息反馈分类
        /// </summary>
        /// <param name="model"></param>
        /// <param name="TypeId"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult EditFeedBackType(FeedBackTypeModel model, int TypeId = -1)
        {
            FeedBackTypeInfo feedBackTypeInfo = FeedBack.GetFeedBackTypeById(TypeId);
            if (feedBackTypeInfo == null)
                return PromptView("信息反馈分类不存在");

            if (ModelState.IsValid)
            {
                feedBackTypeInfo.TypeName = model.TypeName;
                feedBackTypeInfo.IsShowList = model.IsShowList;
                feedBackTypeInfo.Tags = model.Tags;
                feedBackTypeInfo.Body = model.Body;

                FeedBack.UpdateFeedBackType(feedBackTypeInfo);

                //AddAdminOperateLog("修改广告位置", "修改广告位置,广告位置ID为:" + adPosId);
                return PromptView("信息反馈分类修改成功");
            }

            ViewData["referer"] = SiteUtils.GetAdminRefererCookie();
            return View(model);
        }

        /// <summary>
        /// 信息反馈分类列表
        /// </summary>
        /// <returns></returns>
        public ActionResult FeedBackTypeList()
        {
            FeedBackTypeListModel model = new FeedBackTypeListModel
            {
                FeedBackTypeList = FeedBack.GetFeedBackTypeList()
            };

            SiteUtils.SetAdminRefererCookie(Url.Action("FeedBackTypeList"));
            return View(model);
        }

        /// <summary>
        /// 删除信息反馈分类
        /// </summary>
        /// <param name="TypeId"></param>
        /// <returns></returns>
        public ActionResult DelFeedBackType(int TypeId = -1)
        {
            FeedBack.DeleteFeedBackType(TypeId);
            return PromptView("信息反馈分类删除成功");
        }

        /// <summary>
        /// 删除信息反馈
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ActionResult DelFeedBack(int Id = -1)
        {
            FeedBack.DeleteFeedBackInfo(Id);
            return PromptView("信息反馈删除成功");

        }
    }
}
