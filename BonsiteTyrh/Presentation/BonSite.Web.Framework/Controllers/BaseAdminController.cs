using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

using BonSite.Core;
using BonSite.Services;

namespace BonSite.Web.Framework
{
    public class BaseAdminController:Controller
    {
        //工作上下文
        public AdminWorkContext WorkContext = new AdminWorkContext();

        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);

            WorkContext.IsHttpAjax = WebHelper.IsAjax();
            WorkContext.IP = WebHelper.GetIP();
            //WorkContext.RegionInfo = Regions.GetRegionByIP(WorkContext.IP);
            //WorkContext.RegionId = WorkContext.RegionInfo.RegionId;
            WorkContext.Url = WebHelper.GetUrl();
            WorkContext.UrlReferrer = WebHelper.GetUrlReferrer();

            //获得用户唯一标示符sid
            WorkContext.Sid = SiteUtils.GetSidCookie();
            if (WorkContext.Sid.Length == 0)
            {
                //生成sid
                WorkContext.Sid = Sessions.GenerateSid();
                //将sid保存到cookie中
                SiteUtils.SetSidCookie(WorkContext.Sid);
            }

            PartUserInfo partUserInfo;

            //获得用户id
            int uid = SiteUtils.GetUidCookie();
            if (uid < 1)//当用户为游客时
            {
                //创建游客
                partUserInfo = Users.CreatePartGuest();
            }
            else//当用户为会员时
            {
                //获得保存在cookie中的密码
                string encryptPwd = SiteUtils.GetCookiePassword();
                //防止用户密码被篡改为危险字符
                if (encryptPwd.Length == 0 || !SecureHelper.IsBase64String(encryptPwd))
                {
                    //创建游客
                    partUserInfo = Users.CreatePartGuest();
                    encryptPwd = string.Empty;
                    SiteUtils.SetUidCookie(-1);
                    SiteUtils.SetCookiePassword("");
                }
                else
                {
                    partUserInfo = Users.GetPartUserByUidAndPwd(uid, SiteUtils.DecryptCookiePassword(encryptPwd));
                    if (partUserInfo != null)
                    {
                        //发放登陆积分
                        //Credits.SendLoginCredits(ref partUserInfo, DateTime.Now);
                    }
                    else//当会员的账号或密码不正确时，将用户置为游客
                    {
                        partUserInfo = Users.CreatePartGuest();
                        encryptPwd = string.Empty;
                        SiteUtils.SetUidCookie(-1);
                        SiteUtils.SetCookiePassword("");
                    }
                }
                WorkContext.EncryptPwd = encryptPwd;
            }

            ////设置用户等级
            //if (UserRanks.IsBanUserRank(partUserInfo.UserRid) && partUserInfo.LiftBanTime <= DateTime.Now)
            //{
            //    UserRankInfo userRankInfo = UserRanks.GetUserRankByCredits(partUserInfo.PayCredits);
            //    Users.UpdateUserRankByUid(partUserInfo.Uid, userRankInfo.UserRid);
            //    partUserInfo.UserRid = userRankInfo.UserRid;
            //}

            WorkContext.PartUserInfo = partUserInfo;

            WorkContext.Uid = partUserInfo.UserID;
            WorkContext.UserName = partUserInfo.UserName;
            WorkContext.UserEmail = partUserInfo.Email;
            WorkContext.UserMobile = partUserInfo.Mobile;
            WorkContext.Password = partUserInfo.Password;
            WorkContext.NickName = partUserInfo.NickName;
            WorkContext.Avatar = partUserInfo.Avatar;

            WorkContext.UserRid = partUserInfo.UserRankID;
            //WorkContext.UserRankInfo = UserRanks.GetUserRankById(partUserInfo.UserRid);
            //WorkContext.UserRankTitle = WorkContext.UserRankInfo.Title;
            //设置用户管理员组
            WorkContext.AdminGroupID = partUserInfo.AdminGroupID;
            //WorkContext.AdminGroupInfo = AdminGroups.GetAdminGroupById(partUserInfo.AdminGid);
            //WorkContext.AdminGroupTitle = WorkContext.AdminGroupInfo.Title;

            //设置当前控制器类名
            WorkContext.Controller = RouteData.Values["controller"].ToString().ToLower();
            //设置当前动作方法名
            WorkContext.Action = RouteData.Values["action"].ToString().ToLower();
            WorkContext.PageKey = string.Format("/{0}/{1}", WorkContext.Controller, WorkContext.Action);
        }

        protected override void OnAuthorization(AuthorizationContext filterContext)
        {
            //不能应用在子方法上
            if (filterContext.IsChildAction)
                return;

            //当用户ip不在允许的后台访问ip列表时
            if (!string.IsNullOrEmpty(WorkContext.SiteConfig.AdminAllowAccessIP) && !ValidateHelper.InIPList(WorkContext.IP, WorkContext.SiteConfig.AdminAllowAccessIP))
            {
                if (WorkContext.IsHttpAjax)
                    filterContext.Result = AjaxResult("404", "您访问的网址不存在");
                else
                    filterContext.Result = new RedirectResult("/");
                return;
            }

            ////当用户IP被禁止时
            //if (BannedIPs.CheckIP(WorkContext.IP))
            //{
            //    if (WorkContext.IsHttpAjax)
            //        filterContext.Result = AjaxResult("404", "您访问的网址不存在");
            //    else
            //        filterContext.Result = new RedirectResult("/");
            //    return;
            //}

            //当用户等级是禁止访问等级时
            //if (WorkContext.UserRid == 1)
            //{
            //    if (WorkContext.IsHttpAjax)
            //        filterContext.Result = AjaxResult("404", "您访问的网址不存在");
            //    else
            //        filterContext.Result = new RedirectResult("/");
            //    return;
            //}

            //如果当前用户没有登录
            if (WorkContext.Uid < 1)
            {
                if (WorkContext.IsHttpAjax)
                    filterContext.Result = AjaxResult("404", "您尚未登录");
                else
                    filterContext.Result = new RedirectResult("/Account/Login");
                return;
            }

            //如果当前用户不是管理员
            if (WorkContext.AdminGroupID ==0)
            {
                if (WorkContext.IsHttpAjax)
                    filterContext.Result = AjaxResult("404", "您访问的网址不存在");
                else
                    filterContext.Result = new RedirectResult("/");
                return;
            }

            //判断当前用户是否有访问当前页面的权限
            //if (WorkContext.Controller != "home" && !AdminGroups.CheckAuthority(WorkContext.AdminGid, WorkContext.Controller, WorkContext.PageKey))
            //{
            //    if (WorkContext.IsHttpAjax)
            //        filterContext.Result = AjaxResult("nopermit", "您没有当前操作的权限");
            //    else
            //        filterContext.Result = PromptView("您没有当前操作的权限！");
            //    return;
            //}
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //不能应用在子方法上
            if (filterContext.IsChildAction)
                return;

            //当用户为会员时,更新用户的在线时间
            //if (WorkContext.Uid > 0)
            //    Users.UpdateUserOnlineTime(WorkContext.Uid);

            //更新在线用户
            //Asyn.UpdateOnlineUser(WorkContext.Uid, WorkContext.Sid, WorkContext.NickName, WorkContext.IP, WorkContext.RegionId);
            //更新PV统计
            //if (WorkContext.ShopConfig.UpdatePVStatTimespan != 0)
            //    Asyn.UpdatePVStat(WorkContext.Uid, WorkContext.RegionId, WebHelper.GetBrowserType(), WebHelper.GetOSType());
        }

        protected override void OnException(ExceptionContext filterContext)
        {
            //测试Log  WL
            //SiteUtils.WriteLogFile(filterContext.Exception);
            //if (WorkContext.IsHttpAjax)
            //    filterContext.Result = AjaxResult("error", "系统错误,请联系管理员");
            //else
            //    filterContext.Result = new ViewResult() { ViewName = "error" };
        }

        /// <summary>
        /// 提示信息视图
        /// </summary>
        /// <param name="message">提示信息</param>
        /// <returns></returns>
        protected ViewResult PromptView(string message)
        {
            return View("prompt", new PromptModel(SiteUtils.GetAdminRefererCookie(), message));
        }

        /// <summary>
        /// 提示信息视图
        /// </summary>
        /// <param name="backUrl">返回地址</param>
        /// <param name="message">提示信息</param>
        /// <returns></returns>
        protected ViewResult PromptView(string backUrl, string message)
        {
            return View("prompt", new PromptModel(backUrl, message));
        }

        /// <summary>
        /// 提示信息视图
        /// </summary>
        /// <param name="backUrl">返回地址</param>
        /// <param name="message">提示信息</param>
        /// <param name="isAutoBack">是否自动返回</param>
        /// <returns></returns>
        protected ViewResult PromptView(string backUrl, string message, bool isAutoBack)
        {
            return View("prompt", new PromptModel(backUrl, message) { IsAutoBack = isAutoBack });
        }

        /// <summary>
        /// 添加管理员操作日志
        /// </summary>
        /// <param name="operation">操作行为</param>
        protected void AddAdminOperateLog(string operation)
        {
            AddAdminOperateLog(operation, "");
        }

        /// <summary>
        /// 添加管理员操作日志
        /// </summary>
        /// <param name="operation">操作行为</param>
        /// <param name="description">操作描述</param>
        protected void AddAdminOperateLog(string operation, string description)
        {
            //AdminOperateLogs.CreateAdminOperateLog(WorkContext.Uid, WorkContext.NickName, WorkContext.AdminGid, WorkContext.AdminGTitle, WorkContext.IP, operation, description);
        }

        /// <summary>
        /// ajax请求结果
        /// </summary>
        /// <param name="state">状态</param>
        /// <param name="content">内容</param>
        /// <returns></returns>
        protected ActionResult AjaxResult(string state, string content)
        {
            return Content(string.Format("{0}\"state\":\"{1}\",\"content\":\"{2}\"{3}", "{", state, content, "}"));
        }


        /// <summary>
        /// ajax请求结果
        /// </summary>
        /// <param name="state">状态</param>
        /// <param name="content">内容</param>
        /// <param name="isObject">是否为对象</param>
        /// <returns></returns>
        protected ActionResult AjaxResult(string state, string content, bool isObject)
        {
            return Content(string.Format("{0}\"state\":\"{1}\",\"content\":{2}{3}{4}{5}", "{", state, isObject ? "" : "\"", content, isObject ? "" : "\"", "}"));
        }
    }
}
