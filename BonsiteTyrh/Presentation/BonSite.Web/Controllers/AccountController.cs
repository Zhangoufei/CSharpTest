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
    public class AccountController : BaseWebController
    {
        //
        // GET: /Account/

        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <returns></returns>
        public ActionResult Login()
        {
            string returnUrl = WebHelper.GetQueryString("returnUrl");
            returnUrl = "/admin/";
            if (returnUrl.Length == 0)
                returnUrl = "/";

            if (WebHelper.IsGet())
            {
                //如果是Get请求，则展现登录框
                LoginModel model = new LoginModel();
                model.ReturnUrl = returnUrl;
                model.ShadowName = WorkContext.SiteConfig.ShadowName;
                model.IsRemember = WorkContext.SiteConfig.IsRemember == 1;
                model.IsVerifyCode = CommonHelper.IsInArray(WorkContext.PageKey,WorkContext.SiteConfig.VerifyPages);
                return View(model);
            }

            //ajax请求
            string accountName = WebHelper.GetFormString(WorkContext.SiteConfig.ShadowName);
            string password = WebHelper.GetFormString("password");
            string verifyCode = WebHelper.GetFormString("verifyCode");
            int isRemember = WebHelper.GetFormInt("isRemember");

            StringBuilder errorList = new StringBuilder("[");
            //验证账户名
            if (string.IsNullOrWhiteSpace(accountName))
            {
                errorList.AppendFormat("{0}\"key\":\"{1}\",\"msg\":\"{2}\"{3},", "{", "accountName", "账户名不能为空", "}");
            }
            else if (accountName.Length < 4 || accountName.Length > 50)
            {
                errorList.AppendFormat("{0}\"key\":\"{1}\",\"msg\":\"{2}\"{3},", "{", "accountName", "账户名必须大于3且不大于50个字符", "}");
            }
            else if ((!SecureHelper.IsSafeSqlString(accountName, false)))
            {
                errorList.AppendFormat("{0}\"key\":\"{1}\",\"msg\":\"{2}\"{3},", "{", "accountName", "账户名不存在", "}");
            }

            //验证密码
            if (string.IsNullOrWhiteSpace(password))
            {
                errorList.AppendFormat("{0}\"key\":\"{1}\",\"msg\":\"{2}\"{3},", "{", "password", "密码不能为空", "}");
            }
            else if (password.Length < 4 || password.Length > 32)
            {
                errorList.AppendFormat("{0}\"key\":\"{1}\",\"msg\":\"{2}\"{3},", "{", "password", "密码必须大于3且不大于32个字符", "}");
            }


            //验证验证码
            if (CommonHelper.IsInArray(WorkContext.PageKey, WorkContext.SiteConfig.VerifyPages))
            {
                if (string.IsNullOrWhiteSpace(verifyCode))
                {
                    errorList.AppendFormat("{0}\"key\":\"{1}\",\"msg\":\"{2}\"{3},", "{", "verifyCode", "验证码不能为空", "}");
                }
                else if (verifyCode.ToLower() != Sessions.GetValueString(WorkContext.Sid, "verifyCode"))
                {
                    errorList.AppendFormat("{0}\"key\":\"{1}\",\"msg\":\"{2}\"{3},", "{", "verifyCode", "验证码不正确", "}");
                }
            }

            //当以上验证全部通过时
            PartUserInfo partUserInfo = null;
            if (errorList.Length == 1)
            {
                if (BSConfig.SiteConfig.LoginType.Contains("2") && ValidateHelper.IsEmail(accountName))//邮箱登陆
                {
                    partUserInfo = Users.GetPartUserByEmail(accountName);
                    if (partUserInfo == null)
                        errorList.AppendFormat("{0}\"key\":\"{1}\",\"msg\":\"{2}\"{3},", "{", "accountName", "邮箱不存在", "}");
                }
                else if (BSConfig.SiteConfig.LoginType.Contains("3") && ValidateHelper.IsMobile(accountName))//手机登陆
                {
                    partUserInfo = Users.GetPartUserByMobile(accountName);
                    if (partUserInfo == null)
                        errorList.AppendFormat("{0}\"key\":\"{1}\",\"msg\":\"{2}\"{3},", "{", "accountName", "手机不存在", "}");
                }
                else if (BSConfig.SiteConfig.LoginType.Contains("1"))//用户名登陆
                {
                    partUserInfo = Users.GetPartUserByName(accountName);
                    if (partUserInfo == null)
                        errorList.AppendFormat("{0}\"key\":\"{1}\",\"msg\":\"{2}\"{3},", "{", "accountName", "用户名不存在", "}");
                }
                //判断密码是否正确
                if (partUserInfo != null && Users.CreateUserPassword(password, partUserInfo.Salt) != partUserInfo.Password)
                {
                    //LoginFailLogs.AddLoginFailTimes(WorkContext.IP, DateTime.Now);//增加登陆失败次数
                    
                    //密码先注释掉  WL
                    //errorList.AppendFormat("{0}\"key\":\"{1}\",\"msg\":\"{2}\"{3},", "{", "password", "密码不正确", "}");
                }
            }
            if (errorList.Length > 1)//验证失败时
            {
                return AjaxResult("error", errorList.Remove(errorList.Length - 1, 1).Append("]").ToString(), true);
            }
            else//验证成功时
            {
                ////当用户等级是禁止访问等级时
                //if (partUserInfo.UserRid == 1)
                //    return AjaxResult("lockuser", "您的账号当前被锁定,不能访问");

                ////删除登陆失败日志
                //LoginFailLogs.DeleteLoginFailLogByIP(WorkContext.IP);
                ////更新用户最后访问
                //Users.UpdateUserLastVisit(partUserInfo.Uid, DateTime.Now, WorkContext.IP, WorkContext.RegionId);
                
                //将用户信息写入cookie中
                SiteUtils.SetUserCookie(partUserInfo, (WorkContext.SiteConfig.IsRemember == 1 && isRemember == 1) ? 30 : -1);

                return AjaxResult("success", "登录成功");
            }
        }

        #region   统一认证登录
        public ActionResult UtrustLogin(string user_token)
        {
            UserService.UserService ussc = new UserService.UserService();
            string appid = WorkContext.SiteConfig.appid;
            if (string.IsNullOrEmpty(user_token))
            {
                //return PromptView("/", "登录失败!");
                return RedirectToAction("indexPc", "home");  
            }
            else
            {
                string userResult = ussc.getuserinfo(appid, user_token);
                userResult ur = JsonHelper.DeserializeJsonToObject<userResult>(userResult);
                if (ur.retData != null)
                {
                    UserInfo ui = new UserInfo();
                    //手机号
                    string user_mobile = ur.retData.user_mobile;
                    if (!string.IsNullOrEmpty(user_mobile))
                    {
                        //如果手机号不为空，通过手机号查询网站后台是否存在该管理员
                       PartUserInfo partUserInfo= Users.GetPartUserByMobile(user_mobile);
                       if (partUserInfo != null)
                       {
                           SiteUtils.SetUserCookie(partUserInfo, 30);
                           Response.Write("<script language='javascript'>window.location='/admin'</script>");
                           return null;
                          
                       }
                    }
                    string tempRole = ur.retData.user_role;
                    //判断用户用户角色编码是否存在
                    if (!string.IsNullOrEmpty(tempRole))
                    {
                        //判断角色是否是教师
                        if (tempRole.Contains("120"))
                        {
                            UserInfo userInfo = new UserInfo();
                            if (tempRole.Contains("110") || tempRole.Contains("102"))
                            {
                                //赋予管理员权限
                                userInfo.AdminGroupID = WorkContext.SiteConfig.wzAdmin;
                                userInfo.UserName = ur.retData.user_name;
                                userInfo.NickName = "网站管理员";
                                userInfo.Mobile = ur.retData.user_mobile;
                                userInfo.Password = "123456";
                                userInfo.Email = "";
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
                                int userid=Users.CreateUser(userInfo);
                                if (userid > 0)
                                {
                                    PartUserInfo partUserInfo0 = Users.GetPartUserById(userid);
                                    PartUserInfo partUserInfo = Users.GetPartUserByMobile(partUserInfo0.Mobile);
                                    if (partUserInfo != null && partUserInfo.Mobile.Trim().Equals(ur.retData.user_mobile.Trim()))
                                    {
                                        SiteUtils.SetUserCookie(partUserInfo, 30);
                                        Response.Write("<script language='javascript'>window.location='/admin'</script>");
                                        return null;
                                    }
                                }
                            }
                            else
                            {
                                //赋予投稿员权限
                                userInfo.AdminGroupID = WorkContext.SiteConfig.Contributor;
                                userInfo.UserName = ur.retData.user_name;
                                userInfo.NickName = "投稿员";
                                userInfo.Mobile = ur.retData.user_mobile;
                                userInfo.Password = "123456";
                                userInfo.Email = "";
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
                                int userid = Users.CreateUser(userInfo);
                                if (userid > 0)
                                {
                                    PartUserInfo partUserInfo0 = Users.GetPartUserById(userid);
                                    PartUserInfo partUserInfo = Users.GetPartUserByMobile(partUserInfo0.Mobile);
                                    if (partUserInfo != null && partUserInfo.Mobile.Trim().Equals(ur.retData.user_mobile.Trim()))
                                    {
                                        SiteUtils.SetUserCookie(partUserInfo, 30);
                                        Response.Write("<script language='javascript'>window.location='/admin'</script>");
                                        return null;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return RedirectToAction("indexPc", "home");  
        }
        #endregion
        /// <summary>
        /// 退出
        /// </summary>
        /// <returns></returns>
        public ActionResult Logout()
        {
            if (WorkContext.Uid > 0)
            {
                WebHelper.DeleteCookie("bs");
                Sessions.RemoverSession(WorkContext.Sid);
                //OnlineUsers.DeleteOnlineUserBySid(WorkContext.Sid);
            }
            return Redirect("/");
        }
    }
}
