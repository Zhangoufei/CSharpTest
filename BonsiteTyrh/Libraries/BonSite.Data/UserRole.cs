using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using BonSite.Core;
using BonSite.Core.Domain.Site;

namespace BonSite.Data
{
    public class UserRole
    {
        /// <summary>
        /// 获得角色列表
        /// </summary>
        public static UserRoleInfo[] GetUserRoleList()
        {
            DataTable dt = BonSite.Core.BSData.RDBS.GetUserRoleList();
            UserRoleInfo[] userRoleList = new UserRoleInfo[dt.Rows.Count];

            int index = 0;
            foreach (DataRow row in dt.Rows)
            {
                UserRoleInfo userRoleInfo = new UserRoleInfo();
                userRoleInfo.RoleID = TypeHelper.ObjectToInt(row["RoleID"]);
                userRoleInfo.RoleName = row["RoleName"].ToString();
                userRoleInfo.Remark = row["Remark"].ToString();
                userRoleList[index] = userRoleInfo;
                index++;
              
            }
            return userRoleList;
        }

        /// <summary>
        /// 创建用户
        /// </summary>
        /// <returns></returns>
        public static int CreateUserRole(UserRoleInfo userRoleInfo)
        {
            return BonSite.Core.BSData.RDBS.CreateUserRole(userRoleInfo);
        }

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="RoleIDList">角色RoleID</param>
        public static void DeleteUserRoleId(string RoleIDList)
        {
            BonSite.Core.BSData.RDBS.DeleteUserRoleId(RoleIDList);
        }
        /// <summary>
        /// 获得角色
        /// </summary>
        /// <param name="RoleID">班级RoleID</param>
        /// <returns></returns>
        public static UserRoleInfo GetUserRoleById(int RoleID)
        {
            foreach (UserRoleInfo userRoleInfo in GetUserRoleList())
            {
                if (userRoleInfo.RoleID == RoleID)
                    return userRoleInfo;
            }

            return null;
        }

        /// <summary>
        /// 更新角色
        /// </summary>
        public static void UpdateUserRole(UserRoleInfo userRoleInfo)
        {
            BonSite.Core.BSData.RDBS.UpdateUserRole(userRoleInfo);
        }


        /// <summary>
        /// 创建角色菜单
        /// </summary>
        /// <param name="roleMenuInfo"></param>
        /// <returns></returns>
        public static int CreateRoleMenu(RoleMenuInfo roleMenuInfo)
        {
            return BonSite.Core.BSData.RDBS.CreateRoleMenu(roleMenuInfo);
        }

        /// <summary>
        /// 删除角色菜单
        /// </summary>
        /// <param name="RoleID"></param>
        /// <param name="ArticleClassId"></param>
        /// <returns></returns>
        public static bool DeleteRoleMenu(int RoleID, int ArticleClassId)
        {
            return BonSite.Core.BSData.RDBS.DeleteRoleMenu(RoleID, ArticleClassId);
        }

        /// <summary>
        /// 删除角色菜单
        /// </summary>
        /// <param name="RoleID"></param>
        /// <returns></returns>
        public static bool DeleteRoleMenu(int RoleID)
        {
            return BonSite.Core.BSData.RDBS.DeleteRoleMenu(RoleID);
        }



        /// <summary>
        /// 判断角色菜单是否存在
        /// </summary>
        /// <param name="RoleID"></param>
        /// <param name="articleClassId"></param>
        /// <returns></returns>
        public static bool ExistsRoleMenu(int RoleID, int articleClassId)
        {
            return BonSite.Core.BSData.RDBS.ExistsRoleMenu(RoleID, articleClassId);
        }
    }
}
