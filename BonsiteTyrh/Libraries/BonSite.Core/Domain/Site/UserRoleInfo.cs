using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BonSite.Core.Domain.Site
{
    
    /// <summary>
    /// 角色管理信息类
    /// </summary>
    public class UserRoleInfo
    {
        private int _RoleID;//编号
        private string _RoleName;//名称
        private string _Remark;//备注

        /// <summary>
        /// 编号
        /// </summary>
        public int RoleID
        {
            get { return _RoleID; }
            set { _RoleID = value; }
        }
        /// <summary>
        /// 名称
        /// </summary>
        public string RoleName
        {
            get { return _RoleName; }
            set { _RoleName = value.TrimEnd(); }
        }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark
        {
            get { return _Remark; }
            set { _Remark = value; }
        }



        

    }
}
