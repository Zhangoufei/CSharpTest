using System;
using System.Text;
using System.Data;
using System.Data.Common;

using BonSite.Core;

namespace BonSite.RDBSStrategy.SqlServer
{
    public partial class RDBSStrategy:IRDBSStrategy
    {

        #region 用户
        /// <summary>
        /// 获取用户基本信息
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public IDataReader GetPartUserById(int uid)
        {
            DbParameter[] parms = {
                                    GenerateInParam("@UserID", SqlDbType.Int, 4, uid)    
                                   };
            string commandText = string.Format("SELECT {1} FROM [{0}User] WHERE [UserID]=@UserID",
                                                RDBSHelper.RDBSTablePre,
                                                RDBSFields.PARTUSERS);
            return RDBSHelper.ExecuteReader(CommandType.Text, commandText, parms);
        }

        /// <summary>
        /// 获取用户所有基本信息
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public IDataReader GetUserById(int uid)
        {
            DbParameter[] parms = {
                                    GenerateInParam("@UserID", SqlDbType.Int, 4, uid)    
                                   };
            string commandText = string.Format("SELECT top 1 a.*,b.* FROM [{0}User] a,[{0}UserDetails] b WHERE a.UserID=b.UserID and a.UserID =@UserID",
                                                RDBSHelper.RDBSTablePre);
            return RDBSHelper.ExecuteReader(CommandType.Text, commandText, parms);
        }

        /// <summary>
        /// 获取用户详细信息
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public IDataReader GetUserDetailById(int uid)
        {
            DbParameter[] parms = {
                                    GenerateInParam("@UserID", SqlDbType.Int, 4, uid)    
                                   };
            string commandText = string.Format("SELECT {1} FROM [{0}UserDetails] WHERE [UserID]=@UserID",
                                                RDBSHelper.RDBSTablePre,
                                                RDBSFields.USERDETAILS);
            return RDBSHelper.ExecuteReader(CommandType.Text, commandText, parms);
        }

        /// <summary>
        /// 根据用户名获取用户基本信息
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public IDataReader GetPartUserByName(string userName)
        {
            DbParameter[] parms = {
                                    GenerateInParam("@UserName", SqlDbType.NVarChar, 50, userName)    
                                   };
            string commandText = string.Format("SELECT {1} FROM [{0}User] WHERE [UserName]=@UserName",
                                                RDBSHelper.RDBSTablePre,
                                                RDBSFields.PARTUSERS);
            return RDBSHelper.ExecuteReader(CommandType.Text, commandText, parms);
        }

        /// <summary>
        /// 根据邮箱获取用户基本信息
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public IDataReader GetPartUserByEmail(string email)
        {
            DbParameter[] parms = {
                                    GenerateInParam("@Email", SqlDbType.NVarChar, 50, email)    
                                   };
            string commandText = string.Format("SELECT {1} FROM [{0}User] WHERE [Email]=@Email",
                                                RDBSHelper.RDBSTablePre,
                                                RDBSFields.PARTUSERS);
            return RDBSHelper.ExecuteReader(CommandType.Text, commandText, parms);
        }

        /// <summary>
        /// 根据手机获取用户基本信息
        /// </summary>
        /// <param name="mobile"></param>
        /// <returns></returns>
        public IDataReader GetPartUserByMobile(string mobile)
        {
            DbParameter[] parms = {
                                    GenerateInParam("@Mobile", SqlDbType.NVarChar, 50, mobile)    
                                   };
            string commandText = string.Format("SELECT {1} FROM [{0}User] WHERE [Mobile]=@Mobile",
                                                RDBSHelper.RDBSTablePre,
                                                RDBSFields.PARTUSERS);
            return RDBSHelper.ExecuteReader(CommandType.Text, commandText, parms);
        }

        /// <summary>
        /// 根据用户名获取UserID
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public int GetUserIDByUserName(string userName)
        {
            DbParameter[] parms = {
									   GenerateInParam("@userName",SqlDbType.NVarChar,50, userName)
								   };
            string commandText = string.Format("SELECT UserID FROM [{0}User] WHERE [userName]=@userName",
                                                RDBSHelper.RDBSTablePre);
            return TypeHelper.ObjectToInt(RDBSHelper.ExecuteScalar(CommandType.Text,commandText,parms), -1);
        }

        /// <summary>
        /// 根据Email获取UserID
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public int GetUserIDByEmail(string email)
        {
            DbParameter[] parms = {
									   GenerateInParam("@Email",SqlDbType.NVarChar,50, email)
								   };
            string commandText = string.Format("SELECT UserID FROM [{0}User] WHERE [Email]=@Email",
                                                RDBSHelper.RDBSTablePre);
            return TypeHelper.ObjectToInt(RDBSHelper.ExecuteScalar(CommandType.Text, commandText, parms), -1);
        }

        /// <summary>
        /// 根据手机获取UserID
        /// </summary>
        /// <param name="mobile"></param>
        /// <returns></returns>
        public int GetUserIDByMobile(string mobile)
        {
            DbParameter[] parms = {
									   GenerateInParam("@Mobile",SqlDbType.NVarChar,50, mobile)
								   };
            string commandText = string.Format("SELECT UserID FROM [{0}User] WHERE [Mobile]=@Mobile",
                                                RDBSHelper.RDBSTablePre);
            return TypeHelper.ObjectToInt(RDBSHelper.ExecuteScalar(CommandType.Text, commandText, parms), -1);
        }

        /// <summary>
        /// 创建新用户
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        public int CreateUser(UserInfo userInfo)
        {
            DbParameter[] parms = {
									   GenerateInParam("@username",SqlDbType.NChar,20,userInfo.UserName),
									   GenerateInParam("@email",SqlDbType.Char,50,userInfo.Email),
                                       GenerateInParam("@mobile",SqlDbType.Char,15,userInfo.Mobile),
									   GenerateInParam("@password",SqlDbType.Char,32,userInfo.Password),									   
									   GenerateInParam("@nickname",SqlDbType.NChar,20,userInfo.NickName),
									   GenerateInParam("@userrankid",SqlDbType.TinyInt,1,userInfo.UserRankID),
                                       GenerateInParam("@admingroupid",SqlDbType.TinyInt,1,userInfo.AdminGroupID),
									   GenerateInParam("@avatar",SqlDbType.Char,40,userInfo.Avatar),
									   GenerateInParam("@rankcredits",SqlDbType.Int,4,userInfo.RankCredits),
									   GenerateInParam("@verifyemail",SqlDbType.TinyInt,1,userInfo.VerifyEmail),
									   GenerateInParam("@verifymobile",SqlDbType.TinyInt,1,userInfo.VerifyMobile),
									   GenerateInParam("@state",SqlDbType.TinyInt,1,userInfo.State),
                                       GenerateInParam("@salt",SqlDbType.NChar,6,userInfo.Salt)
								   };


            string commandText = string.Format("insert into {0}User(UserName,Email,Mobile,Password,NickName,UserRankID,AdminGroupID,Avatar,RankCredits,VerifyEmail,VerifyMobile,State,Salt) values (@UserName,@Email,@Mobile,@Password,@NickName,@UserRankID,@AdminGroupID,@Avatar,@RankCredits,@VerifyEmail,@VerifyMobile,@State,@Salt) ;select @@IDENTITY;", RDBSHelper.RDBSTablePre);
            int userid= TypeHelper.ObjectToInt(RDBSHelper.ExecuteScalar(CommandType.Text, commandText, parms), -1);


            DbParameter[] parms1 = {
									   GenerateInParam("@userid",SqlDbType.Int,4,userid),
                                       GenerateInParam("@realname",SqlDbType.NVarChar,10,userInfo.RealName),
									   GenerateInParam("@birthday",SqlDbType.DateTime,8,userInfo.Birthday),
									   GenerateInParam("@gender",SqlDbType.TinyInt,1,userInfo.Gender),
                                       GenerateInParam("@idcard",SqlDbType.VarChar,18,userInfo.IdCard),
									   GenerateInParam("@regionid",SqlDbType.SmallInt,2,userInfo.RegionId),
									   GenerateInParam("@address",SqlDbType.NVarChar,150,userInfo.Address),
									   //GenerateInParam("@regtime",SqlDbType.DateTime,8,userInfo.RegTime),
                                       GenerateInParam("@regip",SqlDbType.Char,15,userInfo.RegIP),                                    
									   //GenerateInParam("@lasttime",SqlDbType.DateTime,8,userInfo.LastTime),
                                       GenerateInParam("@lastip",SqlDbType.Char,15,userInfo.LastIP),
                                       GenerateInParam("@Body",  SqlDbType.NText, 0,userInfo.Body)
								   };
            string commandText1 = string.Format("insert into {0}UserDetails(UserID,RealName,Birthday,Gender,IDCard,RegionID,Address,RegTime,[RegIp],LastTime,LastIp,Body) values (@UserID,@RealName,@Birthday,@Gender,@IDCard,@RegionID,@Address,getdate(),@RegIp,getdate(),@LastIp,@Body) ", RDBSHelper.RDBSTablePre);
            TypeHelper.ObjectToInt(RDBSHelper.ExecuteScalar(CommandType.Text, commandText1, parms1), -1);

            return userid;
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="idList"></param>
        /// <returns></returns>
        public bool DeleteUserById(string UserIDList)
        {
            string commandText = String.Format("DELETE FROM [{0}User] WHERE [UserID] IN ({1})",
                                                RDBSHelper.RDBSTablePre,
                                                UserIDList);
            if (RDBSHelper.ExecuteNonQuery(CommandType.Text, commandText) > 0)
                return true;
            else
                return false;
        }

        public void UpdateUser(UserInfo userInfo)
        {
            DbParameter[] parms = {
									   GenerateInParam("@username",SqlDbType.NChar,20,userInfo.UserName),
									   GenerateInParam("@email",SqlDbType.Char,50,userInfo.Email),
                                       GenerateInParam("@mobile",SqlDbType.Char,15,userInfo.Mobile),								   
									   GenerateInParam("@nickname",SqlDbType.NChar,20,userInfo.NickName),
									   GenerateInParam("@userrankid",SqlDbType.TinyInt,1,userInfo.UserRankID),
                                       GenerateInParam("@admingroupid",SqlDbType.TinyInt,1,userInfo.AdminGroupID),
									   GenerateInParam("@avatar",SqlDbType.Char,40,userInfo.Avatar),
									   GenerateInParam("@rankcredits",SqlDbType.Int,4,userInfo.RankCredits),
									   GenerateInParam("@verifyemail",SqlDbType.TinyInt,1,userInfo.VerifyEmail),
									   GenerateInParam("@verifymobile",SqlDbType.TinyInt,1,userInfo.VerifyMobile),
									   GenerateInParam("@state",SqlDbType.TinyInt,1,userInfo.State),
                                       GenerateInParam("@userid",SqlDbType.Int,4,userInfo.UserID)
								   };


            string commandText = string.Format("update {0}User set UserName =@username,Email=@Email,Mobile=@obile,NickName=@nickname,UserRankID=@userrankid,AdminGroupID=@admingroupid,Avatar=@avatar,RankCredits=@rankcredits,VerifyEmail=@verifyemail,VerifyMobile=@verifymobile,State=@state where UserID =@userid ", RDBSHelper.RDBSTablePre);

            RDBSHelper.ExecuteNonQuery(CommandType.Text,  commandText,  parms);


            DbParameter[] parms1 = {
									   GenerateInParam("@userid",SqlDbType.Int,4,userInfo.UserID),
                                       GenerateInParam("@realname",SqlDbType.NVarChar,10,userInfo.RealName),
									   GenerateInParam("@birthday",SqlDbType.DateTime,8,userInfo.Birthday),
									   GenerateInParam("@gender",SqlDbType.TinyInt,1,userInfo.Gender),
                                       GenerateInParam("@idcard",SqlDbType.VarChar,18,userInfo.IdCard),
									   GenerateInParam("@regionid",SqlDbType.SmallInt,2,userInfo.RegionId),
									   GenerateInParam("@address",SqlDbType.NVarChar,150,userInfo.Address),           
									   GenerateInParam("@lasttime",SqlDbType.DateTime,8,userInfo.LastTime),
                                       GenerateInParam("@lastip",SqlDbType.Char,15,userInfo.LastIP),
                                       GenerateInParam("@Body",  SqlDbType.NText, 0,userInfo.Body)
								   };
            string commandText1 = string.Format("update {0}UserDetails set RealName=@realname,Birthday=@birthday,Gender=@gender,IDCard=@idcard,RegionID=@regionid,Address=@address,LastTime=@lasttime,LastIp=@lastip,Body=@body where UserID=@userid", RDBSHelper.RDBSTablePre);

            RDBSHelper.ExecuteNonQuery(CommandType.Text, commandText1, parms1);
        }

        public void UpdatePartUser(PartUserInfo partUserInfo)
        {
            DbParameter[] parms = {
									   GenerateInParam("@username",SqlDbType.NChar,20,partUserInfo.UserName),
									   GenerateInParam("@email",SqlDbType.Char,50,partUserInfo.Email),
                                       GenerateInParam("@mobile",SqlDbType.Char,15,partUserInfo.Mobile),								   
									   GenerateInParam("@nickname",SqlDbType.NChar,20,partUserInfo.NickName),
                                       //GenerateInParam("@userrankid",SqlDbType.TinyInt,1,partUserInfo.UserRankID),
                                       GenerateInParam("@admingroupid",SqlDbType.TinyInt,1,partUserInfo.AdminGroupID),
									   //GenerateInParam("@avatar",SqlDbType.Char,40,partUserInfo.Avatar),
									  // GenerateInParam("@rankcredits",SqlDbType.Int,4,partUserInfo.RankCredits),
									   //GenerateInParam("@verifyemail",SqlDbType.TinyInt,1,partUserInfo.VerifyEmail),
									   //GenerateInParam("@verifymobile",SqlDbType.TinyInt,1,partUserInfo.VerifyMobile),
									   GenerateInParam("@state",SqlDbType.TinyInt,1,partUserInfo.State),
                                       GenerateInParam("@userid",SqlDbType.Int,4,partUserInfo.UserID)
								   };


            string commandText = string.Format("update {0}User SET UserName =@UserName,Email=@Email,Mobile=@Mobile,NickName=@NickName,AdminGroupID=@AdminGroupID,State=@State where UserID =@UserID ", RDBSHelper.RDBSTablePre);
            RDBSHelper.ExecuteNonQuery(CommandType.Text, commandText, parms);
        }

        public void UpdateUserDetail(UserDetailInfo userDetailInfo)
        {
            DbParameter[] parms1 = {
									   GenerateInParam("@userid",SqlDbType.Int,4,userDetailInfo.UserID),
                                       GenerateInParam("@realname",SqlDbType.NVarChar,10,userDetailInfo.RealName),
									   GenerateInParam("@birthday",SqlDbType.DateTime,8,userDetailInfo.Birthday),
									   GenerateInParam("@gender",SqlDbType.TinyInt,1,userDetailInfo.Gender),
                                       GenerateInParam("@idcard",SqlDbType.VarChar,18,userDetailInfo.IdCard),
									   GenerateInParam("@regionid",SqlDbType.SmallInt,2,userDetailInfo.RegionId),
									   GenerateInParam("@address",SqlDbType.NVarChar,150,userDetailInfo.Address),           
									   GenerateInParam("@lasttime",SqlDbType.DateTime,8,userDetailInfo.LastTime),
                                       GenerateInParam("@lastip",SqlDbType.Char,15,userDetailInfo.LastIP),
                                       GenerateInParam("@Body",  SqlDbType.NText, 0,userDetailInfo.Body)
								   };
            string commandText1 = string.Format("update {0}UserDetails set RealName=@realname,Birthday=@birthday,Gender=@gender,IDCard=@idcard,RegionID=@regionid,Address=@address,LastTime=@lasttime,LastIp=@lastip,Body=@body where UserID=@userid", RDBSHelper.RDBSTablePre);

            RDBSHelper.ExecuteNonQuery(CommandType.Text, commandText1, parms1);
        }

        /// <summary>
        /// 更新用户密码
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="password"></param>
        public void UpdateUserPasswordByUid(int userId, string password)
        {
            DbParameter[] parms = {
									   GenerateInParam("@UserID",SqlDbType.Int,4, userId),
									   GenerateInParam("@password",SqlDbType.Char,32, password)
								   };
            RDBSHelper.ExecuteNonQuery(CommandType.Text,
                                       string.Format("update [{0}User] set password = @password where UserID = @UserID", RDBSHelper.RDBSTablePre),
                                       parms);
        }
        #endregion


        #region 管理员

        public DataTable AdminGetUserList(int pageSize, int pageNumber, string condition, string sort)
        {

            bool noCondition = string.IsNullOrWhiteSpace(condition);
            string commandText;
            if (pageNumber == 1)
            {
                if (noCondition)
                    commandText = string.Format("SELECT TOP {0} {1} FROM [{2}User] ORDER BY {3}",
                                                pageSize,
                                                RDBSFields.PARTUSERS,
                                                RDBSHelper.RDBSTablePre,
                                                sort);

                else
                    commandText = string.Format("SELECT TOP {0} {1} FROM [{2}User] WHERE {4} ORDER BY {3}",
                                                pageSize,
                                                RDBSFields.PARTUSERS,
                                                RDBSHelper.RDBSTablePre,
                                                sort,
                                                condition);
            }
            else
            {
                if (noCondition)
                    commandText = string.Format("SELECT {0} FROM (SELECT TOP {3} ROW_NUMBER() OVER (ORDER BY {2}) AS [rowid],{0} FROM [{1}User]) AS [temp] WHERE [temp].[rowid] BETWEEN {4} AND {3}",
                                                RDBSFields.PARTUSERS,
                                                RDBSHelper.RDBSTablePre,
                                                sort,
                                                pageNumber * pageSize,
                                                (pageNumber - 1) * pageSize + 1);
                else
                    commandText = string.Format("SELECT {0} FROM (SELECT TOP {3} ROW_NUMBER() OVER (ORDER BY {2}) AS [rowid],{0} FROM [{1}User] WHERE {5}) AS [temp] WHERE [temp].[rowid] BETWEEN {4} AND {3}",
                                                RDBSFields.PARTUSERS,
                                                RDBSHelper.RDBSTablePre,
                                                sort,
                                                pageNumber * pageSize,
                                                (pageNumber - 1) * pageSize + 1,
                                                condition);
            }

            return RDBSHelper.ExecuteDataset(CommandType.Text, commandText).Tables[0];
        }

        public string AdminGetUserListCondition(string userName, string email, string mobile, int userRid, int adminGid)
        {
            StringBuilder condition = new StringBuilder();

            if (adminGid > 0)
                condition.AppendFormat(" AND [AdminGroupID] > 0 ");
            //if (!string.IsNullOrWhiteSpace(title))
            //    condition.AppendFormat(" AND [title] like '{0}%' ", title);

            return condition.Length > 0 ? condition.Remove(0, 4).ToString() : "";
        }

        public string AdminGetUserListSort(string sortColumn, string sortDirection)
        {
            if (string.IsNullOrWhiteSpace(sortColumn))
                sortColumn = "[UserID]";
            if (string.IsNullOrWhiteSpace(sortDirection))
                sortDirection = "DESC";

            return string.Format("{0} {1} ", sortColumn, sortDirection);
        }

        public int AdminGetUserCount(string condition)
        {

            string commandText;
            if (string.IsNullOrWhiteSpace(condition))
                commandText = string.Format("SELECT COUNT(UserID) FROM [{0}User]", RDBSHelper.RDBSTablePre);
            else
                commandText = string.Format("SELECT COUNT(UserID) FROM [{0}User] WHERE {1}", RDBSHelper.RDBSTablePre, condition);

            return TypeHelper.ObjectToInt(RDBSHelper.ExecuteScalar(CommandType.Text, commandText), 0);
        }

        public void DeleteUserById(int userID)
        {
            DbParameter[] param = {
                                    GenerateInParam("@userID",SqlDbType.Int, 4, userID)
                                   };
            string commandText = String.Format("DELETE FROM [{0}User] WHERE [userID]=@userID;",
                                                RDBSHelper.RDBSTablePre);
            RDBSHelper.ExecuteNonQuery(CommandType.Text, commandText, param);
        }

        //public int GetUserCountByAdminGid(int adminGid)
        //{
        //    throw new NotImplementedException();
        //}

        #endregion
    }
}
