
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


using BonSite.Core;
using BonSite.Services;
using BonSite.Web.Framework;
using BonSite.Web.Admin;
using BonSite.Web.Admin.Models;
using BonSite.Core.Domain.Site;
using System.Text.RegularExpressions;

namespace BonSite.Web.Admin.Controllers
{
    public class UsersController : BaseAdminController
    {
        //
        // GET: /Users/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AdminList(int pageSize = 10, int pageNumber = 1)
        {
            pageSize = WorkContext.SiteConfig.pageSize;
            string condition = "AdminGroupID > 0";
            string sort = "UserID desc";
            PageModel pageModel = new PageModel(pageSize, pageNumber, Users.AdminGetUserCount(condition));

            UsersListModel model = new UsersListModel()
            {
                DataList = Users.AdminGetUserList(pageModel.PageSize, pageModel.PageNumber, condition, sort),
                PageModel = pageModel

            };

            UserRoleModel userRoleModel = new UserRoleModel();
            userRoleModel.UserRoleList = UserRoles.GetUserRoleList();
            List<UserRoleInfo> resultList = new List<UserRoleInfo>();
            resultList = userRoleModel.UserRoleList.ToList();
            TempData["userModelList"] = resultList;

            SiteUtils.SetAdminRefererCookie(string.Format("{0}?pageNumber={1}&pageSize={2}",
                                                          Url.Action("AdminList"),
                                                          pageModel.PageNumber,
                                                          pageModel.PageSize));

            return View(model);

        }

        [HttpGet]
        public ActionResult AdminEdit(int userid = -1)
        {
            PartUserInfo userInfo = Users.GetPartUserById(userid);

            if (userInfo == null)
                return PromptView("用户不存在");
            UsersModel model = new UsersModel()
            {
                UserID = userInfo.UserID,
                UserName = userInfo.UserName,
                Password = userInfo.Password,
                NickName = userInfo.NickName,
                Email = userInfo.Email,
                Mobile = userInfo.Mobile,
                AdminGroupID = userInfo.AdminGroupID,
                Avatar = userInfo.Avatar,
                RandCredits = userInfo.RankCredits,
                UserRankID = userInfo.UserRankID,
                VerifyEmail = userInfo.VerifyEmail,
                State = userInfo.State,
                Salt = userInfo.Salt
            };
            //            List<ArticleClassInfo> classPath = ArticleClass.GetArticleClassPath(articleInfo.ArticleClassID);
            //          Load();

            UserRoleModel model1 = new UserRoleModel();
            model1.UserRoleList = UserRoles.GetUserRoleList();
            List<UserRoleInfo> resultList = new List<UserRoleInfo>();
            resultList = model1.UserRoleList.ToList();
            //Load();
            TempData["modelList"] = resultList;

            ViewData["referer"] = SiteUtils.GetAdminRefererCookie();
            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AdminEdit(UsersModel model, int userid)
        {
            PartUserInfo userInfo = Users.GetPartUserById(userid);

            if (userInfo == null)
                return PromptView("用户不存在");




            userInfo.UserID = model.UserID;
            userInfo.UserName = model.UserName;
            //userInfo.Password,
            userInfo.NickName = model.NickName;
            userInfo.Email = model.Email;
            userInfo.Mobile = model.Mobile;
            userInfo.AdminGroupID = model.AdminGroupID;
            userInfo.Avatar = model.Avatar;
            userInfo.RankCredits = model.RandCredits;
            userInfo.UserRankID = model.UserRankID;
            userInfo.VerifyEmail = model.VerifyEmail;
            userInfo.State = model.State;
            userInfo.Salt = model.Salt;

            Users.UpdatePartUser(userInfo);
            //AddAdminOperateLog
            return PromptView("用户修改成功");



        }

        [HttpGet]
        public ActionResult AdminAdd()
        {
            UsersModel model = new UsersModel()
            {
                //AdminGroupID=2
            };
            UserRoleModel model1 = new UserRoleModel();
            model1.UserRoleList = UserRoles.GetUserRoleList();
            List<UserRoleInfo> resultList = new List<UserRoleInfo>();
            resultList = model1.UserRoleList.ToList();
            //Load();
            TempData["modelList"] = resultList;
            return View("AdminAdd");
        }
        //添加
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AdminAdd(UsersModel model)
        {
            UserInfo userInfo = new UserInfo();

            if (ModelState.IsValid)
            {
                //验证用户名是否重复
                PartUserInfo info = Users.GetPartUserByName(model.UserName);
                if (info != null)
                {
                    return PromptView("用户名不能重复");
                }
                //model.Email = Request.Form.Get("Email").ToString();
                //Regex regex = new Regex(@"([a-zA-Z0-9_-])+@([a-zA-Z0-9_-])+(.[a-zA-Z0-9_-])");
                //bool a = regex.IsMatch(model.Email);
                //if (a == false)
                //{
                //    return PromptView("/admin/Users/AdminAdd", "请输入正确的邮箱格式!");
                //}
                //model.Mobile = Request.Form.Get("Mobile").ToString();
                //Regex regem = new Regex(@"1[34578]\d{9}");
                //bool b = regem.IsMatch(model.Mobile);
                //if (b == false)
                //{
                //    return PromptView("/admin/Users/AdminAdd", "请输入正确的手机号码!");
                //}
                //model.Password = Request.Form.Get("Password").ToString();
                //Regex regeb = new Regex(@"\d{6}");
                //bool c = regeb.IsMatch(model.Password);
                //if (c == false)
                //{
                //    return PromptView("/admin/Users/AdminAdd", "请至少输入6位数的密码!");
                //}
                string ck = WebHelper.GetCookie("bs", "uname");
                userInfo.UserID = model.UserID;
                userInfo.UserName = model.UserName;
                //userInfo.Password,
                userInfo.Salt = Users.GenerateUserSalt();
                userInfo.Password = Users.CreateUserPassword(model.Password, userInfo.Salt);
                if (Request.Form.Get("NickName").ToString().Equals(""))
                {
                    userInfo.NickName = ck;
                }
                else
                {
                    userInfo.NickName = model.NickName;
                }
                userInfo.Email = model.Email;
                userInfo.Mobile = model.Mobile;
                userInfo.AdminGroupID = model.AdminGroupID;
                userInfo.Avatar = "";
                userInfo.RankCredits = 0;
                userInfo.UserRankID = 0;
                userInfo.VerifyEmail = 0;
                userInfo.State = 0;
                userInfo.Address = "";
                userInfo.Birthday = DateTime.Parse("1900-1-1");
                userInfo.Body = "";
                userInfo.Gender = 0;
                userInfo.IdCard = "";
                userInfo.LastIP = "";
                userInfo.LastTime = DateTime.Now;
                userInfo.RegionId = 0;
                userInfo.RegIP = "";
                userInfo.RegTime = DateTime.Now;
                Users.CreateUser(userInfo);

                //AddAdminOperateLog
                return PromptView("管理员新加成功");
            }
            List<UserRoleInfo> resultList = new List<UserRoleInfo>();
            resultList = UserRoles.GetUserRoleList().ToList();
            //Load();
            TempData["modelList"] = resultList;
            return View(model);
        }


        //删除
        public ActionResult AdminDel(int UserID)
        {
            //Users.d
            //Article.Del(articleIdList);

            Users.DeleteUserById(UserID);

            AddAdminOperateLog("删除内容", "删除用户,用户ID为:" + UserID);

            return PromptView("用户删除成功");
        }

        //修改
        [HttpGet]
        public ActionResult AdminMenuEdit(int userid)
        {
            PartUserInfo partUserInfo = Users.GetPartUserById(userid);

            List<AdminMenuItem> adminmenulist = new List<AdminMenuItem>();
            List<ArticleClassInfo> menulist = Services.ArticleClass.AdminGetArticleClassTreeList();
            foreach (ArticleClassInfo list in menulist)
            {
                AdminMenuItem item = new AdminMenuItem()
                {
                    isSel = Services.Users.ExistsAdminMenu(userid, list.ArticleClassID),
                    MenuID = list.ArticleClassID,
                    MenuName = list.ClassName
                };
                adminmenulist.Add(item);
            }

            AdminMenuModel model = new AdminMenuModel()
            {
                PartUserInfo = partUserInfo,
                AdminMenuList = adminmenulist
            };

            return View(model);
        }

        //修改
        [HttpPost]
        public ActionResult AdminMenuEdit(int userid, FormCollection collection)
        {
            string temp = string.Empty;

            if (collection.GetValues("checkboxRole") != null)//这是判断name为checkboxRole的checkbox的值是否为空，若为空返回NULL;
            {
                Services.Users.DeleteAdminMenu(userid);

                string strRoles = collection.GetValue("checkboxRole").AttemptedValue;//AttemptedValue返回一个以，分割的字符串
                string[] lstRoles = strRoles.Split(',');
                foreach (string r in lstRoles)
                {
                    Services.Users.CreateAdminMenu(new AdminMenuInfo() { UserId = userid, ArticleClassID = int.Parse(r) });
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
