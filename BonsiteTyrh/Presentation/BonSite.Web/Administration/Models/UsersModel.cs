using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

using BonSite.Core;
using BonSite.Services;
using BonSite.Web.Framework;
using BonSite.Web.Admin.Models;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;




namespace BonSite.Web.Admin.Models
{
    public class UsersListModel {
        public PageModel PageModel { get; set; }
        public DataTable DataList { get; set; }
    }

    public class UsersModel
    {
        public int UserID { get; set; }
        [Required(ErrorMessage = "名称不能为空")]
        [StringLength(25, ErrorMessage = "名称长度不能大于25")]
        public string UserName { get; set; }
        [Email]
        public string Email { get; set; }
        [Mobile]
        public string Mobile { get; set; }
        [Required(ErrorMessage = "密码不能为空")]
        [PassWord]
        public string Password { get; set; }
        public string NickName { get; set; }
        [Required(ErrorMessage = "用户等级不能为空")]
        public int UserRankID { get; set; }
        [Required(ErrorMessage = "用户角色不能为空")]
        public int AdminGroupID { get; set; }
        public string Avatar { get; set; }
        public int RandCredits { get; set; }
        public int VerifyEmail { get; set; }
        public int State { get; set; }
        public string Salt { get; set; }
    }

    public class AdminMenuModel
    {
        public PartUserInfo PartUserInfo { get; set; }
        public List<AdminMenuItem> AdminMenuList { get; set; }
    }

    public class AdminMenuItem
    {
        public bool isSel { get; set; }
        public int MenuID { get; set; }
        public string MenuName { get; set; }
    }
}