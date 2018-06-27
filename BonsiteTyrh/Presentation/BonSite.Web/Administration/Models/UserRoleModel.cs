using BonSite.Core.Domain.Site;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BonSite.Web.Admin.Models
{
    public class UserRoleModel
    {
        public int RoleID { get; set; }
        public string RoleName { get; set; }

        public string Remark { get; set; }

        public UserRoleInfo[] UserRoleList { get; set; }
    }
    public class UserRoleListModel
    {
        public UserRoleInfo[] UserRoleList { get; set; }
    }
    public class RoleMenuModel
    {
        public UserRoleInfo UserRoleInfo { get; set; }
        public List<RoleMenuItem> RoleMenuList { get; set; }
    }

    public class RoleMenuItem
    {
        public bool isSels { get; set; }
        public int MenuIDs { get; set; }
        public string MenuNames { get; set; }
    }
}

