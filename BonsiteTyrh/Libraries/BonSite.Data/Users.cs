using System;
using System.Collections.Generic;
using System.Text;

using BonSite.Core;
using System.Data;

namespace BonSite.Data
{
    /// <summary>
    /// 用户数据访问类
    /// </summary>
    public class Users
    {

        #region 辅助方法

        /// <summary>
        /// 从IDataReader创建PartUserInfo
        /// </summary>
        public static PartUserInfo BuildPartUserFromReader(IDataReader reader)
        {
            PartUserInfo partUserInfo = new PartUserInfo();

            partUserInfo.UserID = TypeHelper.ObjectToInt(reader["UserID"]);
            partUserInfo.UserName = reader["username"].ToString();
            partUserInfo.Email = reader["email"].ToString();
            partUserInfo.Mobile = reader["mobile"].ToString();
            partUserInfo.Password = reader["password"].ToString();
            partUserInfo.NickName = reader["nickname"].ToString();
            partUserInfo.UserRankID = TypeHelper.ObjectToInt(reader["UserRankid"]);
            partUserInfo.AdminGroupID = TypeHelper.ObjectToInt(reader["AdminGroupID"]);
            partUserInfo.Avatar = reader["avatar"].ToString();
            partUserInfo.RankCredits = TypeHelper.ObjectToInt(reader["rankcredits"]);
            partUserInfo.VerifyEmail = TypeHelper.ObjectToInt(reader["verifyemail"]);
            partUserInfo.VerifyMobile = TypeHelper.ObjectToInt(reader["verifymobile"]);
            partUserInfo.State = TypeHelper.ObjectToInt(reader["State"]);
            partUserInfo.Salt = reader["salt"].ToString();

            return partUserInfo;
        }

        /// <summary>
        /// 从IDataReader创建UserInfo
        /// </summary>
        public static UserInfo BuildUserFromReader(IDataReader reader)
        {
            UserInfo userInfo = new UserInfo();

            userInfo.UserID = TypeHelper.ObjectToInt(reader["UserID"]);
            userInfo.UserName = reader["username"].ToString();
            userInfo.Email = reader["email"].ToString();
            userInfo.Mobile = reader["mobile"].ToString();
            userInfo.Password = reader["password"].ToString();
            userInfo.NickName = reader["nickname"].ToString();
            userInfo.UserRankID = TypeHelper.ObjectToInt(reader["UserRankid"]);
            userInfo.AdminGroupID = TypeHelper.ObjectToInt(reader["AdminGroupID"]);
            userInfo.Avatar = reader["avatar"].ToString();
            userInfo.RankCredits = TypeHelper.ObjectToInt(reader["rankcredits"]);
            userInfo.VerifyEmail = TypeHelper.ObjectToInt(reader["verifyemail"]);
            userInfo.VerifyMobile = TypeHelper.ObjectToInt(reader["verifymobile"]);
            userInfo.State = TypeHelper.ObjectToInt(reader["State"]);
            userInfo.Salt = reader["salt"].ToString();
            userInfo.RealName = reader["RealName"].ToString();
            userInfo.Birthday = TypeHelper.ObjectToDateTime(reader["Birthday"]);
            userInfo.Gender = TypeHelper.ObjectToInt(reader["gender"]);
            userInfo.IdCard = reader["idcard"].ToString();
            userInfo.RegionId = TypeHelper.ObjectToInt(reader["regionid"]);
            userInfo.Address = reader["address"].ToString();
            userInfo.RegTime = TypeHelper.ObjectToDateTime(reader["regtime"]);
            userInfo.RegIP = reader["regip"].ToString();
            userInfo.LastTime = TypeHelper.ObjectToDateTime(reader["LastTime"]);
            userInfo.LastIP = reader["lastip"].ToString();
            userInfo.Body = reader["Body"].ToString();

            return userInfo;
        }

        /// <summary>
        /// 从IDataReader创建UserDetailInfo
        /// </summary>
        public static UserDetailInfo BuildUserDetailFromReader(IDataReader reader)
        {
            UserDetailInfo userDetailInfo = new UserDetailInfo();


            userDetailInfo.UserID = TypeHelper.ObjectToInt(reader["UserID"]);
            userDetailInfo.RealName = reader["RealName"].ToString();
            userDetailInfo.Birthday = TypeHelper.ObjectToDateTime(reader["Birthday"]);
            userDetailInfo.Gender = TypeHelper.ObjectToInt(reader["gender"]);
            userDetailInfo.IdCard = reader["idcard"].ToString();
            userDetailInfo.RegionId = TypeHelper.ObjectToInt(reader["regionid"]);
            userDetailInfo.Address = reader["address"].ToString();
            userDetailInfo.RegTime = TypeHelper.ObjectToDateTime(reader["regtime"]);
            userDetailInfo.RegIP = reader["regip"].ToString();
            userDetailInfo.LastTime = TypeHelper.ObjectToDateTime(reader["LastTime"]);
            userDetailInfo.LastIP = reader["lastip"].ToString();
            userDetailInfo.Body = reader["Body"].ToString();

            return userDetailInfo;
        }

        #endregion



        /// <summary>
        /// 获得用户
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <returns></returns>
        public static UserInfo GetUserById(int uid)
        {
            UserInfo userInfo = null;

            IDataReader reader = BonSite.Core.BSData.RDBS.GetUserById(uid);
            if (reader.Read())
            {
                userInfo = BuildUserFromReader(reader);
            }
            reader.Close();
            return userInfo;
        }

        /// <summary>
        /// 获得部分用户
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <returns></returns>
        public static PartUserInfo GetPartUserById(int uid)
        {
            PartUserInfo partUserInfo = null;

            //if (_usernosql != null)
            //{
            //    partUserInfo = _usernosql.GetPartUserById(uid);
            //    if (partUserInfo == null)
            //    {
            //        IDataReader reader = BrnShop.Core.BSPData.RDBS.GetPartUserById(uid);
            //        if (reader.Read())
            //        {
            //            partUserInfo = BuildPartUserFromReader(reader);
            //        }
            //        reader.Close();
            //        if (partUserInfo != null)
            //            _usernosql.CreatePartUser(partUserInfo);
            //    }
            //}
            //else
            //{
                IDataReader reader = BonSite.Core.BSData.RDBS.GetPartUserById(uid);
                if (reader.Read())
                {
                    partUserInfo = BuildPartUserFromReader(reader);
                }
                reader.Close();
            //}

            return partUserInfo;
        }

        /// <summary>
        /// 获得用户细节
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <returns></returns>
        public static UserDetailInfo GetUserDetailById(int uid)
        {
            UserDetailInfo userDetailInfo = null;

            //if (_usernosql != null)
            //{
            //    userDetailInfo = _usernosql.GetUserDetailById(uid);
            //    if (userDetailInfo == null)
            //    {
            //        IDataReader reader = BrnShop.Core.BSPData.RDBS.GetUserDetailById(uid);
            //        if (reader.Read())
            //        {
            //            userDetailInfo = BuildUserDetailFromReader(reader);
            //        }
            //        reader.Close();
            //        if (userDetailInfo != null)
            //            _usernosql.CreateUserDetail(userDetailInfo);
            //    }
            //}
            //else
            //{
                IDataReader reader = BonSite.Core.BSData.RDBS.GetUserDetailById(uid);
                if (reader.Read())
                {
                    userDetailInfo = BuildUserDetailFromReader(reader);
                }
                reader.Close();
            //}

            return userDetailInfo;
        }

        /// <summary>
        /// 获得部分用户
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns></returns>
        public static PartUserInfo GetPartUserByName(string userName)
        {
            PartUserInfo partUserInfo = null;

            IDataReader reader = BonSite.Core.BSData.RDBS.GetPartUserByName(userName);
            if (reader.Read())
            {
                partUserInfo = BuildPartUserFromReader(reader);
            }
            reader.Close();
            return partUserInfo;
        }

        /// <summary>
        /// 获得部分用户
        /// </summary>
        /// <param name="email">用户邮箱</param>
        /// <returns></returns>
        public static PartUserInfo GetPartUserByEmail(string email)
        {
            PartUserInfo partUserInfo = null;

            IDataReader reader = BonSite.Core.BSData.RDBS.GetPartUserByEmail(email);
            if (reader.Read())
            {
                partUserInfo = BuildPartUserFromReader(reader);
            }
            reader.Close();
            return partUserInfo;
        }

        /// <summary>
        /// 获得部分用户
        /// </summary>
        /// <param name="mobile">用户手机</param>
        /// <returns></returns>
        public static PartUserInfo GetPartUserByMobile(string mobile)
        {
            PartUserInfo partUserInfo = null;

            IDataReader reader = BonSite.Core.BSData.RDBS.GetPartUserByMobile(mobile);
            if (reader.Read())
            {
                partUserInfo = BuildPartUserFromReader(reader);
            }
            reader.Close();
            return partUserInfo;
        }

        /// <summary>
        /// 获得用户id
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns></returns>
        public static int GetUidByUserName(string userName)
        {
            return BonSite.Core.BSData.RDBS.GetUserIDByUserName(userName);
        }

        /// <summary>
        /// 获得用户id
        /// </summary>
        /// <param name="email">用户邮箱</param>
        /// <returns></returns>
        public static int GetUidByEmail(string email)
        {
            return BonSite.Core.BSData.RDBS.GetUserIDByEmail(email);
        }

        /// <summary>
        /// 获得用户id
        /// </summary>
        /// <param name="mobile">用户手机</param>
        /// <returns></returns>
        public static int GetUidByMobile(string mobile)
        {
            return BonSite.Core.BSData.RDBS.GetUserIDByMobile(mobile);
        }

        /// <summary>
        /// 创建用户
        /// </summary>
        /// <returns></returns>
        public static int CreateUser(UserInfo userInfo)
        {
            return BonSite.Core.BSData.RDBS.CreateUser(userInfo);
        }
        /// <summary>
        /// 删除用户
        /// </summary>
        /// <returns></returns>
        public static void DeleteUserById(int userID)
        {
            BonSite.Core.BSData.RDBS.DeleteUserById(userID);
        }
       
        /// <summary>
        /// 更新用户
        /// </summary>
        /// <returns></returns>
        public static void UpdateUser(UserInfo userInfo)
        {
            BonSite.Core.BSData.RDBS.UpdateUser(userInfo);
            //if (_usernosql != null)
            //    _usernosql.UpdateUser(userInfo);
        }

        /// <summary>
        /// 更新部分用户
        /// </summary>
        /// <returns></returns>
        public static void UpdatePartUser(PartUserInfo partUserInfo)
        {
            BonSite.Core.BSData.RDBS.UpdatePartUser(partUserInfo);
            //if (_usernosql != null)
            //    _usernosql.UpdatePartUser(partUserInfo);
        }

        /// <summary>
        /// 更新用户细节
        /// </summary>
        /// <returns></returns>
        public static void UpdateUserDetail(UserDetailInfo userDetailInfo)
        {
            BonSite.Core.BSData.RDBS.UpdateUserDetail(userDetailInfo);
            //if (_usernosql != null)
            //    _usernosql.UpdateUserDetail(userDetailInfo);
        }

        /// <summary>
        /// 更新用户密码
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="password"></param>
        public static void UpdateUserPasswordByUid(int userId,string password)
        {
            BonSite.Core.BSData.RDBS.UpdateUserPasswordByUid(userId, password);
        }

        /// <summary>
        /// 管理员获取用户列表
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <param name="condition"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        public static DataTable AdminGetUserList(int pageSize,int pageNumber,string condition,string sort)
        {
            return BonSite.Core.BSData.RDBS.AdminGetUserList(pageSize, pageNumber, condition, sort);  
        }

        /// <summary>
        /// 获取用户列表搜索条件
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="email"></param>
        /// <param name="mobile"></param>
        /// <param name="userRid"></param>
        /// <param name="adminGid"></param>
        /// <returns></returns>
        public static string AdminGetUserListCondition(string userName,string email,string mobile,int userRid,int adminGid)
        {
            return BonSite.Core.BSData.RDBS.AdminGetUserListCondition(userName,email,mobile,userRid,adminGid);
        }


        /// <summary>
        /// 管理员获取用户排序
        /// </summary>
        /// <param name="sortColumn"></param>
        /// <param name="sortDirection"></param>
        /// <returns></returns>
        public static string AdminGetUserListSort(string sortColumn, string sortDirection)
        {
            return BonSite.Core.BSData.RDBS.AdminGetUserListSort(sortColumn, sortDirection);
        }

        public static int AdminGetUserCount(string condition)
        {
            return BonSite.Core.BSData.RDBS.AdminGetUserCount(condition);
        }

        /// <summary>
        /// 创建管理菜单
        /// </summary>
        /// <param name="adminMenuInfo"></param>
        /// <returns></returns>
        public static int CreateAdminMenu(AdminMenuInfo adminMenuInfo)
        {
            return BonSite.Core.BSData.RDBS.CreateAdminMenu(adminMenuInfo);
        }

        /// <summary>
        /// 删除管理菜单
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="articleClassId"></param>
        /// <returns></returns>
        public static bool DeleteAdminMenu(int userId, int articleClassId)
        {
            return BonSite.Core.BSData.RDBS.DeleteAdminMenu(userId, articleClassId);
        }

        /// <summary>
        /// 删除管理员菜单
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static bool DeleteAdminMenu(int userId)
        {
            return BonSite.Core.BSData.RDBS.DeleteAdminMenu(userId);
        }

        /// <summary>
        /// 判断管理菜单是否存在
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="articleClassId"></param>
        /// <returns></returns>
        public static bool ExistsAdminMenu(int userId, int articleClassId)
        {
            return BonSite.Core.BSData.RDBS.ExistsAdminMenu(userId, articleClassId);
        }
    }
}
