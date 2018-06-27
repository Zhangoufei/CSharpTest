using System;
using System.Collections.Generic;
using System.Text;

using BonSite.Core;
using BonSite.Core.Domain.Site;


namespace BonSite.Services
{
    public class UserRoles
    {
        /// <summary>
        /// 获得角色列表
        /// </summary>
        public static UserRoleInfo[] GetUserRoleList()
        {
          // UserRoleInfo[] userRoleList = BonSite.Core.BSCache.Get(CacheKeys.SITE_USERROLE_LIST) as UserRoleInfo[];
           
           // if (userRoleList == null)
            //{
            UserRoleInfo[] userRoleList = BonSite.Data.UserRole.GetUserRoleList();
            //    BonSite.Core.BSCache.Insert(CacheKeys.SITE_USERROLE_LIST, userRoleList);
            //}
            return userRoleList;
        }

        ///// <summary>
        ///// 获得角色
        ///// </summary>
        ///// <param name="id">角色</param>
        ///// <returns></returns>
        //public static UserRoleInfo GetUserRoleById(int RoleID)
        //{
        //    foreach (UserRoleInfo userRoleInfo in GetUserRoleList())
        //    {
        //        if (userRoleInfo.RoleID == RoleID)
        //            return userRoleInfo;
        //    }
        //    return null;
        //}
        /// <summary>
        /// 创建角色
        /// </summary>
        /// <returns></returns>
        public static void CreateUserRole(UserRoleInfo userRoleInfo)
        {
            BonSite.Data.UserRole.CreateUserRole(userRoleInfo);
            BonSite.Core.BSCache.Remove(CacheKeys.SITE_USERROLE_LIST);

        }

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="RoleIDList">角色RoleID</param>
        public static void DeleteUserRoleById(int[] RoleIDList)
        {
            if (RoleIDList != null && RoleIDList.Length > 0)
            {
                BonSite.Data.UserRole.DeleteUserRoleId(CommonHelper.IntArrayToString(RoleIDList));
                BonSite.Core.BSCache.Remove(CacheKeys.SITE_USERROLE_LIST);
            }
        }

        /// <summary>
        /// 根据ID获得角色
        /// </summary>
        /// <param name="ClassID">角色RoleID</param>
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
            BonSite.Data.UserRole.UpdateUserRole(userRoleInfo);
            BonSite.Core.BSCache.Remove(CacheKeys.SITE_USERROLE_LIST);
        }


        /// <summary>
        /// 创建角色菜单
        /// </summary>
        /// <param name="adminMenuInfo"></param>
        /// <returns></returns>
        public static int CreateRoleMenu(RoleMenuInfo roleMenuInfo)
        {
            return BonSite.Data.UserRole.CreateRoleMenu(roleMenuInfo);
        }


        /// <summary>
        /// 删除角色菜单
        /// </summary>
        /// <param name="RoleID"></param>
        /// <param name="ArticleClassId"></param>
        /// <returns></returns>
        public static bool DeleteRoleMenu(int RoleID, int ArticleClassId)
        {
            return BonSite.Data.UserRole.DeleteRoleMenu(RoleID, ArticleClassId);
        }

        /// <summary>
        /// 删除角色菜单
        /// </summary>
        /// <param name="RoleID"></param>
        /// <returns></returns>
        public static bool DeleteRoleMenu(int RoleID)
        {
            return BonSite.Data.UserRole.DeleteRoleMenu(RoleID);
        }


        /// <summary>
        /// 判断角色菜单是否存在
        /// </summary>
        /// <param name="RoleID"></param>
        /// <param name="articleClassId"></param>
        /// <returns></returns>
        public static bool ExistsRoleMenu(int roleID, int articleClassId)
        {
            return BonSite.Data.UserRole.ExistsRoleMenu(roleID, articleClassId);
        }

    }
}
