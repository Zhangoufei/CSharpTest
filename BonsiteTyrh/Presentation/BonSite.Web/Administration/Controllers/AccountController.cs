using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using BonSite.Core;
using BonSite.Services;
using BonSite.Web.Framework;
using BonSite.Web.Admin.Models;
using System.Text;

namespace BonSite.Web.Admin.Controllers
{
    public class AccountController : BaseAdminController
    {
        //
        // GET: /Account/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            string returnUrl = WebHelper.GetQueryString("returnUrl");
            if (returnUrl.Length == 0)
                returnUrl = "/";

            if (WebHelper.IsGet())
            {
                //如果是Get请求，则展现登录框
                LoginModel model = new LoginModel();
                model.ReturnUrl = returnUrl;
                model.ShadowName = WorkContext.SiteConfig.ShadowName;
                model.IsRemember = WorkContext.SiteConfig.IsRemember == 1;
                model.IsVerifyCode = CommonHelper.IsInArray(WorkContext.PageKey, WorkContext.SiteConfig.VerifyPages);
                return View(model);
            }

            //ajax请求
            string accountName = WebHelper.GetFormString("accountName");
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
                   // LoginFailLogs.AddLoginFailTimes(WorkContext.IP, DateTime.Now);//增加登陆失败次数

                    errorList.AppendFormat("{0}\"key\":\"{1}\",\"msg\":\"{2}\"{3},", "{", "password", "密码不正确", "}");
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
                AddLog(partUserInfo.UserName,"本地用户登录", "登录成功");
                return AjaxResult("success", "登录成功");
            }
        }
        
        [HttpGet]
        public ActionResult EditPassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SavePassword()
        {
            //string v = WebHelper.GetQueryString("v");
            ////解密字符串
            //string realV = SecureHelper.AESDecrypt(v, WorkContext.SiteConfig.SecretKey);

            ////数组第一项为uid，第二项为动作，第三项为验证时间,第四项为随机值
            //string[] result = StringHelper.SplitString(realV);
            //if (result.Length != 4)
            //    return AjaxResult("noauth", "您的权限不足");

            string oldpassword = WebHelper.GetFormString("oldpassword");
            string newpassword = WebHelper.GetFormString("newpassword");
            string newpassword1 = WebHelper.GetFormString("newpassword1");


            //检查密码
            if (string.IsNullOrWhiteSpace(oldpassword))
            {
                return AjaxResult("oldpassword", "原密码不能为空");
            }
            if (string.IsNullOrWhiteSpace(newpassword))
            {
                return AjaxResult("newpassword", "新密码不能为空");
            }
            if (newpassword != newpassword1)
            {
                return AjaxResult("newpassword1", "两次密码不相同");
            }

            string op = Users.CreateUserPassword(oldpassword, WorkContext.PartUserInfo.Salt);
            if (!WorkContext.PartUserInfo.Password.Equals(op))
            {
                return AjaxResult("oldpassword", "原密码不正确");
            }

            string p = Users.CreateUserPassword(newpassword, WorkContext.PartUserInfo.Salt);
            //设置新密码
            Users.UpdateUserPasswordByUid(WorkContext.Uid, p);
            //同步cookie中密码
            SiteUtils.SetCookiePassword(p);
            AddLog(WorkContext.PartUserInfo.UserName, "用户修改密码", "修改成功");
            string url = string.Format("http://{0}{1}", Request.Url.Authority, Url.Action("Login", "Account"));
            return AjaxResult("success", url);

        }
        private void AddLog(string title, string state, string oriName)
        {
            string clientIp = GetHostAddress();
            CommonLog.AddCommonnLog(WorkContext.UserName, state + ":" + oriName + ":" + title, title, clientIp);
        }

        /// <summary>
        /// 获取客户端IP地址（无视代理）
        /// </summary>
        /// <returns>若失败则返回回送地址</returns>
        public string GetHostAddress()
        {
            string userHostAddress = Request.UserHostAddress;

            if (string.IsNullOrEmpty(userHostAddress))
            {
                userHostAddress = Request.ServerVariables["REMOTE_ADDR"];
            }

            //最后判断获取是否成功，并检查IP地址的格式（检查其格式非常重要）
            if (!string.IsNullOrEmpty(userHostAddress) && CommonHelper.IsIP(userHostAddress))
            {
                return userHostAddress;
            }
            return "127.0.0.1";
        }
    }
}
