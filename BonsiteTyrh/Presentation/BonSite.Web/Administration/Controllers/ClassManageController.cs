using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using BonSite.Core;
using BonSite.Services;
using BonSite.Web.Framework;
using BonSite.Web.Admin.Models;
using BonSite.Core.Domain.Site;

namespace BonSite.Web.Admin.Controllers
{
    public class ClassManageController : BaseAdminController
    {
        //
        // GET: /ClassID/

        /// <summary>
        /// 班级管理列表
        /// </summary>
        public ActionResult List()
        {
            ClassManageListModel model = new ClassManageListModel();
            model.ClassManageList = ClassManages.GetClassManageList();
             
           //string[] sizeList = StringHelper.SplitString(WorkContext.SiteConfig.FriendLinkThumbSize);

           //ViewData["size"] = sizeList[sizeList.Length / 2];
           // //SiteUtils.SetAdminRefererCookie(Url.Action("list"));
            return View(model);
            
        }
        /// <summary>
        /// 添加班级
        /// </summary>
        [HttpGet]
       
        public ActionResult Add()
        {
            ClassManageModel model = new ClassManageModel();
            //{
            //    AdminGroupID = 2
            //};
            //Load();
            return View(model);
        }
        //添加
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Add(ClassManageModel model)
        {
            ClassManageInfo classManageInfo = new ClassManageInfo();

            if (ModelState.IsValid)
            {
                classManageInfo.ClassID = model.ClassID;
                classManageInfo.ClassName = model.ClassName;

                ClassManages.CreateClassManage(classManageInfo);               
                return PromptView("班级新加成功");
            }
            return View(model);
        }

        /// <summary>
        /// 编辑班级
        /// </summary>
        [HttpGet]
        public ActionResult Edit(int id = -1)
        {
            ClassManageInfo classManageInfo = ClassManages.GetClassManageById(id);
            if (classManageInfo == null)
                return PromptView("班级不存在");

            ClassManageModel model = new ClassManageModel();
            model.ClassID = classManageInfo.ClassID;
            model.ClassName = classManageInfo.ClassName; 
            
            //Load();

            return View(model);
        }

        /// <summary>
        /// 编辑班级
        /// </summary>
        [HttpPost]
        public ActionResult Edit(ClassManageModel model, int id = -1)
        {
            ClassManageInfo classManageInfo = ClassManages.GetClassManageById(id);
            if (classManageInfo == null)
                return PromptView("班级不存在");

            if (ModelState.IsValid)
            {
                classManageInfo.ClassName = model.ClassName;
                classManageInfo.ClassID = id;

                ClassManages.UpdateClassManage(classManageInfo);
                AddAdminOperateLog("修改班级", "修改班级,班级ID为:" + id);
                return PromptView("班级修改成功");
            }

           // Load();
            return View(model);
        }
        /// <summary>
        /// 删除班级
        /// </summary>
        public ActionResult Del(int[] idList)
        {
            ClassManages.DeleteClassManageById(idList);
            AddAdminOperateLog("删除班级", "删除班级,班级ID为:" + CommonHelper.IntArrayToString(idList));
            return PromptView("班级删除成功");
        }

       
    }
}