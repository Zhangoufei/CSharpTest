using System;
using System.Collections.Generic;
using System.Text;

using BonSite.Core;
using System.Data;

namespace BonSite.Services
{
    public class Users
    {

        /// <summary>
        /// 获得部分用户
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <returns></returns>
        public static PartUserInfo GetPartUserById(int uid)
        {
            if (uid > 0)
                return BonSite.Data.Users.GetPartUserById(uid);

            return null;
        }

        /// <summary>
        /// 获得用户
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <returns></returns>
        public static UserInfo GetUserById(int uid)
        {
            if (uid > 0)
                return BonSite.Data.Users.GetUserById(uid);

            return null;
        }

        /// <summary>
        /// 获得用户细节
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <returns></returns>
        public static UserDetailInfo GetUserDetailById(int uid)
        {
            if (uid > 0)
                return BonSite.Data.Users.GetUserDetailById(uid);

            return null;
        }

        /// <summary>
        /// 获得部分用户
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        public static PartUserInfo GetPartUserByUidAndPwd(int uid, string password)
        {
            PartUserInfo partUserInfo = GetPartUserById(uid);
            if (partUserInfo != null && partUserInfo.Password == password)
                return partUserInfo;
            return null;
        }

        /// <summary>
        /// 获得部分用户
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns></returns>
        public static PartUserInfo GetPartUserByName(string userName)
        {
            return BonSite.Data.Users.GetPartUserByName(userName);
        }

        /// <summary>
        /// 获得部分用户
        /// </summary>
        /// <param name="email">用户邮箱</param>
        /// <returns></returns>
        public static PartUserInfo GetPartUserByEmail(string email)
        {
            return BonSite.Data.Users.GetPartUserByEmail(email);
        }

        /// <summary>
        /// 获得部分用户
        /// </summary>
        /// <param name="mobile">用户手机</param>
        /// <returns></returns>
        public static PartUserInfo GetPartUserByMobile(string mobile)
        {
            return BonSite.Data.Users.GetPartUserByMobile(mobile);
        }

        /// <summary>
        /// 获得用户id
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns></returns>
        public static int GetUidByUserName(string userName)
        {
            if (string.IsNullOrWhiteSpace(userName))
                return -1;

            return BonSite.Data.Users.GetUidByUserName(userName);
        }

        /// <summary>
        /// 获得用户id
        /// </summary>
        /// <param name="email">用户邮箱</param>
        /// <returns></returns>
        public static int GetUidByEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return -1;

            return BonSite.Data.Users.GetUidByEmail(email);
        }

        /// <summary>
        /// 获得用户id
        /// </summary>
        /// <param name="mobile">用户手机</param>
        /// <returns></returns>
        public static int GetUidByMobile(string mobile)
        {
            if (string.IsNullOrWhiteSpace(mobile))
                return -1;

            return BonSite.Data.Users.GetUidByMobile(mobile);
        }

        /// <summary>
        /// 获得用户id
        /// </summary>
        /// <param name="accountName">账户名</param>
        /// <returns></returns>
        public static int GetUidByAccountName(string accountName)
        {
            if (string.IsNullOrWhiteSpace(accountName))
                return -1;

            if (ValidateHelper.IsEmail(accountName))//邮箱
            {
                return GetUidByEmail(accountName);
            }
            else if (ValidateHelper.IsMobile(accountName))//手机
            {
                return GetUidByMobile(accountName);
            }
            else//用户名
            {
                return GetUidByUserName(accountName);
            }
        }

        /// <summary>
        /// 创建用户
        /// </summary>
        /// <returns></returns>
        public static int CreateUser(UserInfo userInfo)
        {
            return BonSite.Data.Users.CreateUser(userInfo);
        }

        /// <summary>
        /// 创建部分用户
        /// </summary>
        /// <returns></returns>
        public static PartUserInfo CreatePartGuest()
        {
            return new PartUserInfo
            {
                UserID = -1,
                UserName = "guest",
                Email = "",
                Mobile = "",
                Password = "",
                UserRankID = 6,
                AdminGroupID = 1,
                NickName = "游客",
                Avatar = "",
                RankCredits = 0,
                VerifyEmail = 0,
                VerifyMobile = 0,
                //LiftBanTime = new DateTime(1900, 1, 1),
                Salt = ""
            };
        }
        /// <summary>
        /// 删除用户
        /// </summary>
        /// <returns></returns>

        public static void DeleteUserById(int UserID)
        {
            if (UserID != null)
            {
                BonSite.Data.Users.DeleteUserById(UserID);
            }
        }



        /// <summary>
        /// 更新用户
        /// </summary>
        /// <returns></returns>
        public static void UpdateUser(UserInfo userInfo)
        {
            BonSite.Data.Users.UpdateUser(userInfo);
        }

        /// <summary>
        /// 更新部分用户
        /// </summary>
        /// <returns></returns>
        public static void UpdatePartUser(PartUserInfo partUserInfo)
        {
            BonSite.Data.Users.UpdatePartUser(partUserInfo);
        }

        /// <summary>
        /// 更新用户细节
        /// </summary>
        /// <returns></returns>
        public static void UpdateUserDetail(UserDetailInfo userDetailInfo)
        {
            BonSite.Data.Users.UpdateUserDetail(userDetailInfo);
        }

        ///// <summary>
        ///// 更新用户最后访问
        ///// </summary>
        ///// <param name="uid">用户id</param>
        ///// <param name="visitTime">访问时间</param>
        ///// <param name="ip">ip</param>
        ///// <param name="regionId">区域id</param>
        //public static void UpdateUserLastVisit(int uid, DateTime visitTime, string ip, int regionId)
        //{
        //    BonSite.Data.Users.UpdateUserLastVisit(uid, visitTime, ip, regionId);
        //}

        /// <summary>
        /// 用户名是否存在
        /// </summary>
        /// <param name="userName">用户名</param>
        public static bool IsExistUserName(string userName)
        {
            return GetUidByUserName(userName) > 0 ? true : false;
        }

        /// <summary>
        /// 邮箱是否存在
        /// </summary>
        /// <param name="userName">邮箱</param>
        public static bool IsExistEmail(string email)
        {
            return GetUidByEmail(email) > 0 ? true : false;
        }

        /// <summary>
        /// 手机是否存在
        /// </summary>
        /// <param name="userName">手机</param>
        public static bool IsExistMobile(string mobile)
        {
            return GetUidByMobile(mobile) > 0 ? true : false;
        }


        /// <summary>
        /// 创建用户密码
        /// </summary>
        /// <param name="password">真实密码</param>
        /// <param name="salt">散列盐值</param>
        /// <returns></returns>
        public static string CreateUserPassword(string password, string salt)
        {
            return SecureHelper.MD5(password + salt);
        }

        /// <summary>
        /// 更新用户密码
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="password"></param>
        public static void UpdateUserPasswordByUid(int userId, string password)
        {
            BonSite.Data.Users.UpdateUserPasswordByUid(userId, password); 
        }


        /// <summary>
        /// 后台获得用户列表
        /// </summary>
        /// <param name="pageSize">每页数</param>
        /// <param name="pageNumber">当前页数</param>
        /// <param name="condition">条件</param>
        /// <param name="sort">排序</param>
        /// <returns></returns>
        public static DataTable AdminGetUserList(int pageSize, int pageNumber, string condition, string sort)
        {
            return BonSite.Data.Users.AdminGetUserList(pageSize, pageNumber, condition, sort);
        }

        /// <summary>
        /// 后台获得新闻数量
        /// </summary>
        /// <param name="condition">条件</param>
        /// <returns></returns>
        public static int AdminGetUserCount(string condition)
        {
            return BonSite.Data.Users.AdminGetUserCount(condition);
        }

        /// <summary>
        /// 生成用户盐值
        /// </summary>
        /// <returns></returns>
        public static string GenerateUserSalt()
        {
            return Randoms.CreateRandomValue(6);
        }

        /// <summary>
        /// 创建管理员菜单
        /// </summary>
        /// <param name="adminMenuInfo"></param>
        /// <returns></returns>
        public static int CreateAdminMenu(AdminMenuInfo adminMenuInfo)
        {
            return BonSite.Data.Users.CreateAdminMenu(adminMenuInfo);
        }

        /// <summary>
        /// 删除管理员菜单
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="articleClassId"></param>
        /// <returns></returns>
        public static bool DeleteAdminMenu(int userId, int articleClassId)
        {
            return BonSite.Data.Users.DeleteAdminMenu(userId, articleClassId);
        }

        /// <summary>
        /// 删除管理员菜单
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static bool DeleteAdminMenu(int userId)
        {
            return BonSite.Data.Users.DeleteAdminMenu(userId);
        }


        /// <summary>
        /// 判断管理菜单是否存在
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="articleClassId"></param>
        /// <returns></returns>
        public static bool ExistsAdminMenu(int userId, int articleClassId)
        {
            return BonSite.Data.Users.ExistsAdminMenu(userId, articleClassId);
        }
    }
}
