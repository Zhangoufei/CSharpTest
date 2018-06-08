using System;
using System.Collections.Generic;
using System.Text;
using JAAJ.Model;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.Common;
using System.Data;
using Yorkg.Base;

namespace JAAJ.DAL
{
    public class JAAJ_UserData
    {

        #region 保存 Save
        ///	<summary>
        ///	保存
        ///	</summary>
        ///	<param name="JAAJ_UserInfo"></param>
        ///	<returns>EnumSubmitResult</returns>
        public static EnumSubmitResult Save(JAAJ_UserInfo info)
        {
            string strStoreProcedure = "JAAJ_User_Save";
            SqlDatabase odbDataBase = new SqlDatabase(SQLHelp.CONNECTINSTRING);
            DbCommand odbCommand = odbDataBase.GetStoredProcCommand(strStoreProcedure);

            odbDataBase.AddInParameter(odbCommand, "@iUserID", System.Data.DbType.Int32, info.iUserID);
            odbDataBase.AddInParameter(odbCommand, "@nvcUserName", System.Data.DbType.String, info.nvcUserName);
            odbDataBase.AddInParameter(odbCommand, "@nvcPassword", System.Data.DbType.String, info.nvcPassword);
            odbDataBase.AddInParameter(odbCommand, "@nvcRole", System.Data.DbType.String, info.nvcRole);
            odbDataBase.AddInParameter(odbCommand, "@nvcCnName", System.Data.DbType.String, info.nvcCnName);
            odbDataBase.AddInParameter(odbCommand, "@nvcContact", System.Data.DbType.String, info.nvcContact);
            odbDataBase.AddInParameter(odbCommand, "@nvcEmail", System.Data.DbType.String, info.nvcEmail);
            odbDataBase.AddInParameter(odbCommand, "@nvcAddress", System.Data.DbType.String, info.nvcAddress);
            odbDataBase.AddInParameter(odbCommand, "@nvcMemo", System.Data.DbType.String, info.nvcMemo);
            odbDataBase.AddOutParameter(odbCommand, "@result", System.Data.DbType.Int32, 8);
            EnumSubmitResult enmResult = EnumSubmitResult.Failed;
            using (IDbConnection connection = odbDataBase.CreateConnection())
            {
                connection.Open();
                try
                {
                    odbDataBase.ExecuteNonQuery(odbCommand);
                    int i = int.Parse(odbCommand.Parameters["@result"].Value.ToString());
                    enmResult = (EnumSubmitResult)i;
                }
                catch (Exception oeException)
                {
                    //throw new Exception(oeException.Message, oeException);
                }
                connection.Close();
            }
            odbCommand.Dispose();
            return enmResult;
        }

        #endregion

        #region 获取实体 GetInfoById
        ///	<summary>
        ///	获取实体
        ///	</summary>
        ///	<param name="iUserID"></param>
        ///	<returns>JAAJ_UserInfo</returns>
        public static JAAJ_UserInfo GetInfoById(int id)
        {
            string strSqlString = "SELECT * FROM JAAJ_Users WHERE iUserID=" + id;
            SqlDatabase odbDataBase = new SqlDatabase(SQLHelp.CONNECTINSTRING);
            DbCommand odbCommand = odbDataBase.GetSqlStringCommand(strSqlString);
            JAAJ_UserInfo oInfo = null;
            try
            {
                using (IDataReader reader = odbDataBase.ExecuteReader(odbCommand))
                {
                    if (reader.Read())
                    {
                        oInfo = new JAAJ_UserInfo();
                        ModelHelper.LoadInfoData(oInfo, reader);
                    }
                }
            }
            catch (Exception oeException)
            {
                throw new Exception(oeException.Message, oeException);
            }
            return oInfo;
        }
        #endregion

        #region 删除 DeleteInfoById
        ///	<summary>
        ///	删除
        ///	</summary>
        ///	<param name="id"></param>
        ///	<returns>EnumSubmitResult</returns>
        public static EnumSubmitResult DeleteInfoById(int id)
        {
            return JAAJ_UserData.DeleteInfoByIds(id.ToString());
        }
        #endregion

        #region 删除 DeleteInfoByIds
        ///	<summary>
        ///	删除
        ///	</summary>
        ///	<param name="ids"></param>
        ///	<returns>EnumSubmitResult</returns>
        public static EnumSubmitResult DeleteInfoByIds(string ids)
        {
            string strSqlString = "DELETE FROM JAAJ_Users WHERE iUserID in(" + ids + ")";
            SqlDatabase odbDataBase = new SqlDatabase(SQLHelp.CONNECTINSTRING);
            DbCommand odbCommand = odbDataBase.GetSqlStringCommand(strSqlString);

            EnumSubmitResult enmResult = EnumSubmitResult.Failed;
            try
            {
                odbDataBase.ExecuteNonQuery(odbCommand);
                enmResult = EnumSubmitResult.Success;
            }
            catch (Exception oeException)
            {
                throw new Exception(oeException.Message, oeException);
            }
            return enmResult;
        }
        #endregion

        #region 自定义列表（无分页） GetListByCondition
        ///	<summary>
        ///	自定义列表（无分页）
        ///	</summary>
        ///	<param name="topn">前N条，为0时表示所有符合条件的</param>
        ///	<param name="condition">自定义条件，要以And开头</param>
        ///	<param name="orderby">排序，不需要Order by </param>
        ///	<returns>List<JAAJ_UserInfo></returns>
        public static List<JAAJ_UserInfo> GetListByCondition(int topn, string condition, string orderby)
        {
            string strSqlString = "SELECT ";
            if (topn > 0)
            {
                strSqlString += " TOP " + topn;
            }
            strSqlString += " * FROM JAAJ_Users WHERE 1=1"; if (condition != "")
            {
                strSqlString += condition;
            }
            if (orderby != "")
            {
                strSqlString += " ORDER BY " + orderby;
            }
            else
            {
                strSqlString += " ORDER BY iUserID DESC";
            }

            SqlDatabase odbDataBase = new SqlDatabase(SQLHelp.CONNECTINSTRING);
            DbCommand odbCommand = odbDataBase.GetSqlStringCommand(strSqlString);
            List<JAAJ_UserInfo> olstInfo = new List<JAAJ_UserInfo>(); JAAJ_UserInfo oInfo = null;
            try
            {
                using (IDataReader reader = odbDataBase.ExecuteReader(odbCommand))
                {
                    while (reader.Read())
                    {
                        oInfo = new JAAJ_UserInfo();
                        ModelHelper.LoadInfoData(oInfo, reader);
                        olstInfo.Add(oInfo);
                    }
                }
            }
            catch (Exception oeException)
            {
                throw new Exception(oeException.Message, oeException);
            }
            return olstInfo;
        }
        #endregion

        #region 自定义列表（分页） GetListByPager
        ///	<summary>
        ///	自定义列表（分页）
        ///	</summary>
        ///	<returns>List<JAAJ_UserInfo></returns>
        public static List<JAAJ_UserInfo> GetListByPager(int pageNumber, int pageSize, string condition, string orderby, out int recordCount)
        {
            string strStoreProcedure = "JAAJ_User_GetList";
            SqlDatabase odbDataBase = new SqlDatabase(SQLHelp.CONNECTINSTRING);
            DbCommand odbCommand = odbDataBase.GetStoredProcCommand(strStoreProcedure);
            List<JAAJ_UserInfo> olstInfo = new List<JAAJ_UserInfo>(); JAAJ_UserInfo oInfo = null;
            odbDataBase.AddInParameter(odbCommand, "@pageNumber", System.Data.DbType.Int32, pageNumber);
            odbDataBase.AddInParameter(odbCommand, "@pageSize", System.Data.DbType.Int32, pageSize);
            odbDataBase.AddInParameter(odbCommand, "@condition", System.Data.DbType.String, condition);
            odbDataBase.AddInParameter(odbCommand, "@SortField", System.Data.DbType.String, orderby);
            odbDataBase.AddOutParameter(odbCommand, "@recordCount", System.Data.DbType.Int32, 0);
            try
            {
                using (IDataReader reader = odbDataBase.ExecuteReader(odbCommand))
                {
                    while (reader.Read())
                    {
                        oInfo = new JAAJ_UserInfo();
                        ModelHelper.LoadInfoData(oInfo, reader);
                        olstInfo.Add(oInfo);
                    }
                }
                recordCount = int.Parse(odbCommand.Parameters["@RecordCount"].Value.ToString());
            }
            catch (Exception oeException)
            {
                throw new Exception(oeException.Message, oeException);
            }
            return olstInfo;
        }
        #endregion


    }

}
