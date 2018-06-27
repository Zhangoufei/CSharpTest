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

//using BonSite.Core.Domain.Site;

namespace BonSite.Web.Admin.Controllers
{
    public class UserRoleController : BaseAdminController
    {
        //
        // GET: /UserRole/

        /// <summary>
        /// 角色列表
        public ActionResult List()
        {
            UserRoleModel model = new UserRoleModel();
            model.UserRoleList = UserRoles.GetUserRoleList();

            SiteUtils.SetAdminRefererCookie(Url.Action("list"));
           
            return View(model);
        }

        /// <summary>
        /// 添加角色
        /// </summary>
        [HttpGet]
        public ActionResult Add()
        {
            UserRoleModel model = new UserRoleModel();
            return View(model);
        }

       

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Add(UserRoleModel model)
        {
            UserRoleInfo userRoleInfo = new UserRoleInfo();

            if (ModelState.IsValid)
            {
                userRoleInfo.RoleName = model.RoleName;
                userRoleInfo.Remark = model.Remark;

                UserRoles.CreateUserRole(userRoleInfo);

                //AddAdminOperateLog
                return PromptView("角色新加成功");
            }
            return View(model);
        }

        /// <summary>
        /// 删除角色
        /// </summary>
        public ActionResult Del(int[] RoleIDList)
        {
            UserRoles.DeleteUserRoleById(RoleIDList);
            AddAdminOperateLog("删除角色", "删除角色,角色ID为:" + CommonHelper.IntArrayToString(RoleIDList));
            return PromptView("角色删除成功");
        }

        /// <summary>
        /// 编辑角色
        /// </summary>
        [HttpGet]
        public ActionResult Edit(int RoleID = -1)
        {
            UserRoleInfo userRoleInfo = UserRoles.GetUserRoleById(RoleID);

            if (userRoleInfo == null)
                return PromptView("角色不存在");

            UserRoleModel model = new UserRoleModel();
            model.RoleID = userRoleInfo.RoleID;
            model.RoleName = userRoleInfo.RoleName;
            model.Remark = userRoleInfo.Remark;

            //Load();

            return View(model);
        }

        /// <summary>
        /// 编辑角色
        /// </summary>
        [HttpPost]
        public ActionResult Edit(UserRoleModel model, int RoleID)
        {
            UserRoleInfo userRoleInfo = UserRoles.GetUserRoleById(RoleID);
            if (userRoleInfo == null)
                return PromptView("角色不存在");

            if (ModelState.IsValid)
            {
                userRoleInfo.RoleName = model.RoleName;
                userRoleInfo.Remark = model.Remark;
                userRoleInfo.RoleID = RoleID;


                UserRoles.UpdateUserRole(userRoleInfo);
                AddAdminOperateLog("修改角色", "修改角色,角色ID为:" + RoleID);
                return PromptView("角色修改成功");
            }

            // Load();
            return View(model);
        }





        //private void Load()
        //{
        //    string allowImgType = string.Empty;
        //    string[] imgTypeList = StringHelper.SplitString(BSConfig.SiteConfig.UploadImgType, ",");
        //    foreach (string imgType in imgTypeList)
        //        allowImgType += string.Format("*{0};", imgType.ToLower());

        //    string[] sizeList = StringHelper.SplitString(WorkContext.SiteConfig.FriendLinkThumbSize);

        //    ViewData["size"] = sizeList[sizeList.Length / 2];
        //    ViewData["allowImgType"] = allowImgType;
        //    ViewData["maxImgSize"] = BSConfig.SiteConfig.UploadImgSize;
        //    ViewData["referer"] = SiteUtils.GetAdminRefererCookie();
        //}

        //权限
        [HttpGet]
        public ActionResult RoleMenuEdit(int RoleId)
        {
            UserRoleInfo userRoleInfo = UserRoles.GetUserRoleById(RoleId);


            List<RoleMenuItem> rolemenulist = new List<RoleMenuItem>();
            List<ArticleClassInfo> menulist = Services.ArticleClass.AdminGetArticleClassTreeList();
            foreach (ArticleClassInfo list in menulist)
            {
                RoleMenuItem item = new RoleMenuItem()
                {
                    isSels = Services.UserRoles.ExistsRoleMenu(RoleId, list.ArticleClassID),
                    MenuIDs = list.ArticleClassID,
                    MenuNames = list.ClassName
                };
                rolemenulist.Add(item);
            }

            RoleMenuModel model = new RoleMenuModel()
            {
                UserRoleInfo = userRoleInfo,
                RoleMenuList = rolemenulist
            };

            return View(model);
        }

        //权限
        [HttpPost]
        public ActionResult RoleMenuEdit(int RoleId, FormCollection collection)
        {
            string temp = string.Empty;

            if (collection.GetValues("checkboxRole") != null)//这是判断name为checkboxRole的checkbox的值是否为空，若为空返回NULL;
            {
                Services.UserRoles.DeleteRoleMenu(RoleId);

                string strRoles = collection.GetValue("checkboxRole").AttemptedValue;//AttemptedValue返回一个以，分割的字符串
                string[] lstRoles = strRoles.Split(',');
                foreach (string r in lstRoles)
                {
                    Services.UserRoles.CreateRoleMenu(new RoleMenuInfo() { RoleId = RoleId, ArticleClassID = int.Parse(r) });
                    //temp += r;
                }
                return PromptView("修改成功！");
            }
            else
            {
                return PromptView("请至少选择一个项目！");
            }

            //return Content(temp);
        }

        
    }
}
