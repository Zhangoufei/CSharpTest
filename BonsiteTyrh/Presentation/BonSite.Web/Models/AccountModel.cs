using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BonSite.Web.Models
{
    /// <summary>
    /// 登录模型类
    /// </summary>
    public class LoginModel
    {
        /// <summary>
        /// 登录后返回地址
        /// </summary>
        public string ReturnUrl { get; set; }
        /// <summary>
        /// 影子账户
        /// </summary>
        public string ShadowName { get; set; }
        /// <summary>
        /// 是否记住用户名
        /// </summary>
        public bool IsRemember { get; set; }
        /// <summary>
        /// 是否启用验证码
        /// </summary>
        public bool IsVerifyCode { get; set; }
    }
}