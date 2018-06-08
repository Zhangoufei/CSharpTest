using System;
using System.Collections.Generic;
using System.Text;
using JAAJ.Common;
using JAAJ.Model;
using System.Data.Common;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Net;
using System.Linq;
namespace UploadScore
{
    public class ScoreData
    {

        public static string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["strCon"].ToString();// "Data Source=WANGCHAO;Database=JAAJ;user id=sa;password=123456;Connect Timeout=30";

        #region 保存客户端分值 Save
        /// <summary>
        /// 保存客户端分值
        /// </summary>
        /// <param name="info">子项成绩</param>
        /// <param name="osisiJAAJ_SubItemScoreInfoList">子项试题成绩与试题步骤成绩</param>
        /// <returns>保存结果，成功或失败</returns>
        public static EnumSubmitResult Save(JAAJ_SubjectScoreInfo info, List<JAAJ_SubItemScoreInfo> osisiJAAJ_SubItemScoreInfoList)
        {
            int i = 0;
            string strStoreProcedure = "JAAJ_SubjectScore_Save";
            SqlDatabase odbDataBase = new SqlDatabase(connectionString);
            DbConnection conn = odbDataBase.CreateConnection();
            DbCommand odbCommand = odbDataBase.GetStoredProcCommand(strStoreProcedure);
            conn.Open();
            DbTransaction odtTran = conn.BeginTransaction();
            EnumSubmitResult enmResult = EnumSubmitResult.Failed;
            try
            {
                odbDataBase.AddInParameter(odbCommand, "@iSubjectScoreID", System.Data.DbType.Int32, info.iSubjectScoreID);
                odbDataBase.AddInParameter(odbCommand, "@nvcBatchNO", System.Data.DbType.String, info.nvcBatchNO);
                odbDataBase.AddInParameter(odbCommand, "@iExamineeID", System.Data.DbType.Int32, info.iExamineeID);
                odbDataBase.AddInParameter(odbCommand, "@iSubjectID", System.Data.DbType.Int32, info.iSubjectID);
                odbDataBase.AddInParameter(odbCommand, "@iSubjectItemID", System.Data.DbType.Int32, info.iSubjectItemID);
                odbDataBase.AddInParameter(odbCommand, "@decSubjectScore", System.Data.DbType.Decimal, info.decSubjectScore);
                odbDataBase.AddInParameter(odbCommand, "@datScoreDate", System.Data.DbType.DateTime, info.datScoreDate);
                odbDataBase.AddInParameter(odbCommand, "@nvcMemo", System.Data.DbType.String, info.nvcMemo);
                odbDataBase.AddOutParameter(odbCommand, "@result", System.Data.DbType.Int32, 8);
                odbDataBase.AddOutParameter(odbCommand, "@iSubjectScoreID2", System.Data.DbType.Int32, 8);
                odbDataBase.ExecuteNonQuery(odbCommand, odtTran);
                i = int.Parse(odbCommand.Parameters["@result"].Value.ToString());
                int iSubjectScoreID = (int)odbDataBase.GetParameterValue(odbCommand, "@iSubjectScoreID2");
                enmResult = (EnumSubmitResult)i;

                if (enmResult == EnumSubmitResult.Failed)
                {
                    odtTran.Rollback();
                    return enmResult = EnumSubmitResult.Failed;
                }

                //循环执行科目子项试题分数保存
                foreach (JAAJ_SubItemScoreInfo osisinfo in osisiJAAJ_SubItemScoreInfoList)
                {
                    strStoreProcedure = "JAAJ_SubItemScore_Save";
                    odbCommand = odbDataBase.GetStoredProcCommand(strStoreProcedure);
                    odbDataBase.AddInParameter(odbCommand, "@iSubItemScoreID", System.Data.DbType.Int32, osisinfo.iSubItemScoreID);
                    odbDataBase.AddInParameter(odbCommand, "@iSubjectScoreID", System.Data.DbType.Int32, iSubjectScoreID);
                    odbDataBase.AddInParameter(odbCommand, "@iExamineeID", System.Data.DbType.Int32, osisinfo.iExamineeID);
                    odbDataBase.AddInParameter(odbCommand, "@iTitleID", System.Data.DbType.Int32, osisinfo.iTitleID);
                    odbDataBase.AddInParameter(odbCommand, "@nvcTitleName", System.Data.DbType.String, osisinfo.nvcTitleName);
                    odbDataBase.AddInParameter(odbCommand, "@nvcDescription", System.Data.DbType.String, osisinfo.nvcDescription);
                    odbDataBase.AddInParameter(odbCommand, "@decSubjectItemScore", System.Data.DbType.Decimal, osisinfo.decSubjectItemScore);
                    odbDataBase.AddInParameter(odbCommand, "@nvcMemo", System.Data.DbType.String, osisinfo.nvcMemo);
                    odbDataBase.AddOutParameter(odbCommand, "@result", System.Data.DbType.Int32, 8);
                    odbDataBase.AddOutParameter(odbCommand, "@iSubItemScoreID2", System.Data.DbType.Int32, 8);
                    odbDataBase.ExecuteNonQuery(odbCommand, odtTran);
                    i = int.Parse(odbCommand.Parameters["@result"].Value.ToString());
                    int iSubItemScoreID = (int)odbDataBase.GetParameterValue(odbCommand, "@iSubItemScoreID2");
                    enmResult = (EnumSubmitResult)i;

                    if (enmResult == EnumSubmitResult.Failed)
                    {
                        odtTran.Rollback();
                        return enmResult = EnumSubmitResult.Failed;
                    }

                    //循环执行科目子项试题步骤分数保存
                    strStoreProcedure = "JAAJ_StepScore_Save";
                    foreach (JAAJ_StepScoreInfo ossiinfo in osisinfo.JAAJ_StepScoreInfoList)
                    {
                        odbCommand = odbDataBase.GetStoredProcCommand(strStoreProcedure);
                        odbDataBase.AddInParameter(odbCommand, "@iStepScoreID", System.Data.DbType.Int32, ossiinfo.iStepScoreID);
                        odbDataBase.AddInParameter(odbCommand, "@iSubItemScoreID", System.Data.DbType.Int32, iSubItemScoreID);
                        odbDataBase.AddInParameter(odbCommand, "@iStepID", System.Data.DbType.Int32, ossiinfo.iStepID);
                        odbDataBase.AddInParameter(odbCommand, "@nvcStepName", System.Data.DbType.String, ossiinfo.nvcStepName);
                        odbDataBase.AddInParameter(odbCommand, "@nvcDescription", System.Data.DbType.String, ossiinfo.nvcDescription);
                        odbDataBase.AddInParameter(odbCommand, "@decStepScore", System.Data.DbType.Decimal, ossiinfo.decStepScore);
                        odbDataBase.AddInParameter(odbCommand, "@nvcMemo", System.Data.DbType.String, ossiinfo.nvcMemo);
                        odbDataBase.AddOutParameter(odbCommand, "@result", System.Data.DbType.Int32, 8);
                        odbDataBase.ExecuteNonQuery(odbCommand, odtTran);
                        i = int.Parse(odbCommand.Parameters["@result"].Value.ToString());
                        enmResult = (EnumSubmitResult)i;
                        //出错不在往下执行，直接回滚
                        if (enmResult == EnumSubmitResult.Failed)//失败
                        {
                            odtTran.Rollback();
                            return enmResult = EnumSubmitResult.Failed;
                        }
                    }
                }
                odtTran.Commit();
            }
            catch
            {
                odtTran.Rollback();
                enmResult = EnumSubmitResult.Failed;
            }

            odbCommand.Dispose();
            odtTran.Dispose();
            conn.Close();
            return enmResult;
        }

        #endregion

        #region 获取考生信息实体 GetExamineeInfoByIDNum
        ///	<summary>
        ///	获取考生信息实体
        ///	</summary>
        ///	<param name="strIDNum">身份证号</param>
        ///	<param name="strBatchNo">批次号</param>
        ///	<returns>考生信息，JAAJ_ExamineeInfo</returns>
        public static JAAJ_ExamineeInfo GetExamineeInfoByIDNum(string strIDNum, string strBatchNo)
        {
            //string strSqlString = "SELECT * FROM JAAJ_Examinees WHERE nvcIDNum='" + strIDNum + "' AND CHARINDEX(nvcBatchNO,'" + strBatchNo + "')>0 ";
            string strSqlString = "SELECT * FROM JAAJ_Examinees WHERE nvcIDNum='" + strIDNum + "'";
            SqlDatabase odbDataBase = new SqlDatabase(connectionString);
            DbCommand odbCommand = odbDataBase.GetSqlStringCommand(strSqlString);
            JAAJ_ExamineeInfo oInfo = null;
            try
            {
                using (IDataReader reader = odbDataBase.ExecuteReader(odbCommand))
                {
                    if (reader.Read())
                    {
                        oInfo = new JAAJ_ExamineeInfo();
                        ModelHelper.LoadInfoData(oInfo, reader);
                        if (!oInfo.bSex)
                        {
                            oInfo.nvcSexName = PublicMethod.GetEnumShowName(CommonEnum.Sex.Man, (int)CommonEnum.Sex.Man);
                        }
                        else
                        {
                            oInfo.nvcSexName = PublicMethod.GetEnumShowName(CommonEnum.Sex.Woman, (int)CommonEnum.Sex.Woman);
                        }
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

        #region 获取当前考试信息
        /// <summary>
        /// 获取当前正在考试信息
        /// </summary>
        /// <returns>当前正在考试信息，JAAJ_ExamInfo</returns>
        public static JAAJ_ExamInfo GetCurrentExamInfo()
        {
            JAAJ_ExamInfo oInfo = null;
            try
            {
                string strSqlString = "SELECT * FROM JAAJ_Exams WHERE iExamStatus=0 ";
                SqlDatabase odbDataBase = new SqlDatabase(connectionString);
                DbCommand odbCommand = odbDataBase.GetSqlStringCommand(strSqlString);
                using (IDataReader reader = odbDataBase.ExecuteReader(odbCommand))
                {
                    if (reader.Read())
                    {
                        oInfo = new JAAJ_ExamInfo();
                        ModelHelper.LoadInfoData(oInfo, reader);
                    }
                }
            }
            catch (Exception oeException)
            {
                oInfo = null;
                //throw new Exception(oeException.Message, oeException);
            }
            return oInfo;
        }
        #endregion

        /// <summary>
        /// 获取本机IP
        /// </summary>
        /// <returns></returns>
        private static string GetLocalIP()
        {
            //return "172.17.173.1";
            //return "10.0.40.242";
            //return "192.168.1.18";

            String str;
            String Result = "";
            String hostName = Dns.GetHostName();
            IPAddress[] myIP = Dns.GetHostAddresses(hostName);
            foreach (IPAddress address in myIP)
            {
                str = address.ToString();
                bool blIPV4 = false;
                for (int i = 0; i < str.Length; i++)
                {
                    if (str[i] >= '0' && str[i] <= '9' || str[i] == '.')
                    {
                        blIPV4 = true;
                    }
                    else
                    {
                        blIPV4 = false;
                        break;
                    }

                }
                if (blIPV4)
                {
                    Result = str;
                }
            }
            return Result;
        }

        #region 获取科目子项实体 GetSubjectItemInfoByID
        ///	<summary>
        ///	获取科目子项实体
        ///	</summary>
        ///	<param name="iSubjectItemID">科目子项ID</param>
        ///	<returns>科目子项实体，JAAJ_SubjectItemInfo</returns>
        public static JAAJ_SubjectItemInfo GetSubjectItemInfoByID(int iSubjectItemID)
        {
            string strSqlString = "SELECT * FROM JAAJ_SubjectItems WHERE iSubjectItemID=" + iSubjectItemID + " ";
            SqlDatabase odbDataBase = new SqlDatabase(connectionString);
            DbCommand odbCommand = odbDataBase.GetSqlStringCommand(strSqlString);
            JAAJ_SubjectItemInfo oInfo = null;
            try
            {
                using (IDataReader reader = odbDataBase.ExecuteReader(odbCommand))
                {
                    if (reader.Read())
                    {
                        oInfo = new JAAJ_SubjectItemInfo();
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

        #region 获取设备信息实体 GetDeviceInfoByIP
        ///	<summary>
        ///	获取设备信息实体
        ///	</summary>
        ///	<param name="strIP">设备IP</param>
        ///	<returns>设备信息实体,JAAJ_DeviceInfo</returns>
        public static JAAJ_DeviceInfo GetDeviceInfoByIP(string strIP)
        {
            string strSqlString = "SELECT * FROM JAAJ_Devices WHERE nvcIP='" + strIP + "' ";
            SqlDatabase odbDataBase = new SqlDatabase(connectionString);
            DbCommand odbCommand = odbDataBase.GetSqlStringCommand(strSqlString);
            JAAJ_DeviceInfo oInfo = null;
            try
            {
                using (IDataReader reader = odbDataBase.ExecuteReader(odbCommand))
                {
                    if (reader.Read())
                    {
                        oInfo = new JAAJ_DeviceInfo();
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

        #region 根据考生ID获取考试过程实体 GetExamProceInfoByExamineeID
        /// <summary>
        /// 根据考生ID获取考试过程实体
        /// </summary>
        /// <param name="iExamineeID">考生ID，非考生号</param>
        /// <returns>考试过程实体，JAAJ_ExamProceInfo</returns>
        public static JAAJ_ExamProceInfo GetExamProceInfoByExamineeID(int iExamineeID)
        {
            string strIP = GetLocalIP();
            //string strSqlString = "SELECT * FROM JAAJ_ExamProcess WHERE iExamineeID=" + iExamineeID + " AND iSubjectItemID IN (SELECT iSubjectItemID FROM JAAJ_SubjectItems WHERE iDeviceID=(SELECT iDeviceTypeID FROM JAAJ_Devices WHERE nvcIP='" + strIP + "'))";
            string strSqlString = "SELECT * FROM JAAJ_ExamProcess WHERE iExamineeID={iExamineeID.ToString()} AND iDeviceID IN (select iDeviceID from JAAJ_Devices where nvcIP = '{strIP}')";
            SqlDatabase odbDataBase = new SqlDatabase(connectionString);
            DbCommand odbCommand = odbDataBase.GetSqlStringCommand(strSqlString);
            JAAJ_ExamProceInfo oInfo = null;
            try
            {
                using (IDataReader reader = odbDataBase.ExecuteReader(odbCommand))
                {
                    if (reader.Read())
                    {
                        oInfo = new JAAJ_ExamProceInfo();
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

        #region 更新设备状态为忙碌
        /// <summary>
        /// 更新设备状态为忙碌
        /// </summary>
        /// <param name="iStatus">0空闲，1忙碌</param>
        /// <returns></returns>
        public static EnumSubmitResult UpdateDeviceStatus(int iStatus)
        {
            EnumSubmitResult enmResult = EnumSubmitResult.Failed;
            string strIP = GetLocalIP();
            if (strIP != "")
            {
                JAAJ_DeviceInfo odiDeviceInfo = GetDeviceInfoByIP(strIP);
                if (odiDeviceInfo != null)
                {
                    string strSqlString = "Update JAAJ_Devices SET iDeviceStatus=" + iStatus + " WHERE iDeviceID=" + odiDeviceInfo.iDeviceID + " ";
                    SqlDatabase odbDataBase = new SqlDatabase(connectionString);
                    DbCommand odbCommand = odbDataBase.GetSqlStringCommand(strSqlString);
                    try
                    {
                        int iResult = odbDataBase.ExecuteNonQuery(odbCommand);
                        if (iResult > 0)
                        {
                            enmResult = EnumSubmitResult.Success;
                        }
                        else
                        {
                            enmResult = EnumSubmitResult.Failed;
                        }
                    }
                    catch (Exception oeException)
                    {
                        throw new Exception(oeException.Message, oeException);
                    }
                }
            }
            return enmResult;
        }
        #endregion

        #region 更新设备状态为空闲、插入已考试科目ID与子项ID、删除当前子项考试过程记录
        ///	<summary>
        ///	更新设备状态为空闲、插入已考试科目ID与子项ID、删除当前子项考试过程记录
        ///	</summary>
        ///	<returns>EnumSubmitResult</returns>
        public static EnumSubmitResult UpdateDeviceStatusCallListDeleteExamProcess(int iExamineeID)
        {
            EnumSubmitResult enmResult = EnumSubmitResult.Failed;
            string strIP = GetLocalIP();
            if (strIP != "")
            {
                JAAJ_DeviceInfo odiDeviceInfo = GetDeviceInfoByIP(strIP);

                JAAJ_ExamProceInfo oepiExamProceInfo = GetExamProceInfoByExamineeID(iExamineeID);
                if (oepiExamProceInfo != null)
                {

                    JAAJ_SubjectItemInfo osiiSubjectItemInfo = GetSubjectItemInfoByID(oepiExamProceInfo.iSubjectItemID);

                    if (odiDeviceInfo != null && osiiSubjectItemInfo != null)
                    {
                        string strStoreProcedure = "JAAJ_UpdateDeviceStatusCallListDeleteExamProcess";
                        SqlDatabase odbDataBase = new SqlDatabase(connectionString);
                        DbCommand odbCommand = odbDataBase.GetStoredProcCommand(strStoreProcedure);
                        odbDataBase.AddInParameter(odbCommand, "@iExamineeID", System.Data.DbType.Int32, iExamineeID);
                        odbDataBase.AddInParameter(odbCommand, "@iSubjectID", System.Data.DbType.Int32, osiiSubjectItemInfo.iSubjectID);
                        odbDataBase.AddInParameter(odbCommand, "@iSubjectItemID", System.Data.DbType.Int32, osiiSubjectItemInfo.iSubjectItemID);
                        odbDataBase.AddInParameter(odbCommand, "@iDeviceID", System.Data.DbType.Int32, odiDeviceInfo.iDeviceID);
                        odbDataBase.AddOutParameter(odbCommand, "@result", System.Data.DbType.Int32, 8);

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
                    }
                }
            }
            return enmResult;
        }
        #endregion

        #region 提醒老师复位硬件设备，保存提示信息
        ///	<summary>
        ///	提醒老师复位硬件设备，保存提示信息 
        ///	</summary>
        ///	<param name="strDescription">提示内容，需要描述清楚</param>
        ///	<returns>EnumSubmitResult</returns>
        public static EnumSubmitResult SaveDeviceResetTip(string strDescription)
        {
            JAAJ_DeviceResetTipInfo info = new JAAJ_DeviceResetTipInfo();
            info.iStatus = 0;
            info.nvcResetDescription = strDescription;

            string strStoreProcedure = "JAAJ_DeviceResetTip_Save";
            SqlDatabase odbDataBase = new SqlDatabase(connectionString);
            DbCommand odbCommand = odbDataBase.GetStoredProcCommand(strStoreProcedure);

            odbDataBase.AddInParameter(odbCommand, "@iResetID", System.Data.DbType.Int32, info.iResetID);
            odbDataBase.AddInParameter(odbCommand, "@nvcResetDescription", System.Data.DbType.String, info.nvcResetDescription);
            odbDataBase.AddInParameter(odbCommand, "@iStatus", System.Data.DbType.Int16, info.iStatus);
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

        #region 获取考生成绩明细
        /// <summary>
        /// 获取考生成绩明细
        /// </summary>
        /// <param name="strBatchNO">批次号</param>
        /// <param name="datExamDate">考试日期</param>
        /// <param name="iExamineeID">考生ID</param>
        /// <returns></returns>
        public static List<JAAJ_ScoreDetailInfo> GetListByCondition(string strBatchNO, string datExamDate, int iExamineeID)
        {
            string strStoreProcedure = "GetExamineeScoreDetail";
            SqlDatabase odbDataBase = new SqlDatabase(connectionString);
            DbCommand odbCommand = odbDataBase.GetStoredProcCommand(strStoreProcedure);
            List<JAAJ_ScoreDetailInfo> olstInfo = new List<JAAJ_ScoreDetailInfo>();
            JAAJ_ScoreDetailInfo oInfo = null;
            odbDataBase.AddInParameter(odbCommand, "@nvcBatchNo", System.Data.DbType.String, strBatchNO);
            odbDataBase.AddInParameter(odbCommand, "@datExamDate", System.Data.DbType.String, datExamDate);
            odbDataBase.AddInParameter(odbCommand, "@iExamineeID", System.Data.DbType.Int32, iExamineeID);

            try
            {
                using (IDataReader reader = odbDataBase.ExecuteReader(odbCommand))
                {
                    while (reader.Read())
                    {
                        oInfo = new JAAJ_ScoreDetailInfo();
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

        #region 根据子项成绩计算总成绩
        /// <summary>
        /// 获取该批次考生每个子项的成绩
        /// </summary>
        /// <param name="strBatchNO">选中批次号</param>
        /// <param name="strDate">考试日期</param>
        /// <returns></returns>
        public static List<JAAJ_SubjectScoreInfo> GetListWithTotalScore(string strSql, string strBatch)
        {
            StringBuilder strSqlString = new StringBuilder();
            strSqlString.Append("SELECT subjectScore.iExamineeID,nvcName,nvcExamineeGUID,examinee.nvcMemo AS ExamineeGUID, subjectScore.nvcBatchNO, nvcIDNum, subjects.nvcSubjectName,subjectScore.iSubjectID, SUM(decSubjectScore) AS sumScore , CONVERT(varchar(10),datScoreDate,23) AS datScoreDate,batch.nvcMemo AS batchGUID   ");
            strSqlString.Append(" FROM JAAJ_Examinees AS examinee, JAAJ_SubjectScores AS subjectScore, JAAJ_Subjects AS subjects ,JAAJ_SubjectItems AS subItems,JAAJ_Exams as Exam,JAAJ_Batchs as batch  ");
            //2015-06-17：增加 “subjectScore.iSubjectScoreID IN (SELECT MAX(iSubjectScoreID) AS iSubjectScoreID FROM JAAJ_SubjectScores GROUP BY isubjectItemId,iExamineeID) AND” 作用：用于取出当天最后一次成绩，用于规避如果考生当天多次考试，成绩计算重复计算
            strSqlString.Append(" WHERE subjectScore.iSubjectScoreID IN (SELECT MAX(iSubjectScoreID) AS iSubjectScoreID FROM JAAJ_SubjectScores GROUP BY isubjectItemId,iExamineeID) AND examinee.iExamineeID=subjectScore.iExamineeID  AND Exam.iExamStatus=0  AND subjects.iSubjectID=subjectScore.iSubjectID AND subItems.iSubjectItemID= subjectScore.iSubjectItemID AND CONVERT(varchar(10),datScoreDate,23)=CONVERT(varchar(10),Exam.datExamDate,23) AND subjectScore.nvcBatchNO=batch.nvcBatchNO ");
            if (!string.IsNullOrEmpty(strSql))
            {
                strSqlString.Append(strSql);
            }
            strSqlString.Append(" GROUP BY subjects.nvcSubjectName, subjectScore.iSubjectID,  subjectScore.iSubjectID, subjectScore.iExamineeID, examinee.nvcName, examinee.nvcExamineeGUID, subjectScore.nvcBatchNO,nvcIDNum,CONVERT(varchar(10),datScoreDate,23),examinee.nvcMemo,batch.nvcMemo ORDER BY subjectScore.nvcBatchNO DESC, subjectScore.iExamineeID ");
            string strFinalSql = strSqlString.ToString();
            SqlDatabase odbDataBase = new SqlDatabase(connectionString);
            DbCommand odbCommand = odbDataBase.GetSqlStringCommand(strFinalSql);
            List<JAAJ_SubjectScoreInfo> olstInfo = new List<JAAJ_SubjectScoreInfo>();
            JAAJ_SubjectScoreInfo oInfo = null;
            List<JAAJ_SubjectScoreInfo> ListInfo = new List<JAAJ_SubjectScoreInfo>();
            JAAJ_SubjectScoreInfo ScoreInfo = new JAAJ_SubjectScoreInfo();
            try
            {
                using (IDataReader reader = odbDataBase.ExecuteReader(odbCommand))
                {
                    while (reader.Read())
                    {
                        oInfo = new JAAJ_SubjectScoreInfo();
                        ModelHelper.LoadInfoData(oInfo, reader);
                        olstInfo.Add(oInfo);
                    }
                }
                //获取所考科目的得分权重
                List<JAAJ_ExamSubjectInfo> ListPersent = GetPersent(strBatch);
                //每个科目子项的成绩乘以对应的权重
                for (int i = 0; i < olstInfo.Count; i++)
                {
                    for (int j = 0; j < ListPersent.Count; j++)
                    {
                        if (olstInfo[i].iSubjectID == ListPersent[j].iSubjectID && olstInfo[i].datScoreDate == ListPersent[j].datExamDate)
                        {
                            olstInfo[i].decTotalScore += olstInfo[i].sumScore * ListPersent[j].decExamPersent;
                        }
                    }
                }
                //得到每个考生对应日期的考试总成绩
                var result = from p in olstInfo
                             group p by new { p.iExamineeID, p.nvcIDNum, p.nvcName, p.nvcBatchNO, p.datScoreDate, p.nvcExamineeGUID } into g
                             select new
                             {
                                 nvcExamineeGUID = g.Key.nvcExamineeGUID,
                                 nvcIDNum = g.Key.nvcIDNum,
                                 nvcBatchNO = g.Key.nvcBatchNO,
                                 nvcName = g.Key.nvcName,
                                 decTotalScore = g.Sum(p => p.decTotalScore),
                                 datScoreDate = g.Key.datScoreDate,
                                 //iSubjectScoreID = g.Select(p => p.iSubjectScoreID),
                                 iExamineeID = g.Key.iExamineeID
                             };
                ListInfo = new List<JAAJ_SubjectScoreInfo>();
                foreach (var r in result)
                {
                    ScoreInfo = new JAAJ_SubjectScoreInfo();
                    ScoreInfo.nvcExamineeGUID = r.nvcExamineeGUID.ToString();
                    ScoreInfo.nvcIDNum = r.nvcIDNum.ToString();
                    ScoreInfo.nvcBatchNO = r.nvcBatchNO.ToString();
                    ScoreInfo.nvcName = r.nvcName.ToString();
                    ScoreInfo.decTotalScore = r.decTotalScore;
                    ScoreInfo.datScoreDate = DateTime.Parse(r.datScoreDate.ToString());
                    ScoreInfo.iExamineeID = r.iExamineeID;
                    ListInfo.Add(ScoreInfo);
                }
            }
            catch (Exception oeException)
            {
                throw new Exception(oeException.Message, oeException);
            }

            return ListInfo;
        }

        /// <summary>
        /// 获取所考科目得分权重
        /// </summary>
        /// <param name="strBatchNO"></param>
        /// <param name="strDate"></param>
        /// <returns></returns>
        public static List<JAAJ_ExamSubjectInfo> GetPersent(string strBatchNO)
        {
            List<JAAJ_ExamSubjectInfo> m_oesJAAJ_ExamSubjectInfoList = new List<JAAJ_ExamSubjectInfo>();
            if (!string.IsNullOrEmpty(strBatchNO))
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("SELECT distinct esu.iExamSubjectID, esu.iSubjectID,CONVERT(varchar(10),exa.datExamDate,23) AS datExamDate,esu.decExamPersent ");
                strSql.Append(" FROM JAAJ_ExamSubjects AS  esu, JAAJ_Exams AS exa, JAAJ_SubjectScores AS sco ");
                strSql.Append(" WHERE  esu.iExamID=exa.iExamID AND CHARINDEX(sco.nvcBatchNO,exa.nvcBatchNO)>0  AND CONVERT(varchar(10),exa.datExamDate,23)=CONVERT(varchar(10),sco.datScoreDate,23) AND esu.iSubjectID=sco.iSubjectID ");
                strSql.Append(" AND sco.nvcBatchNO='" + strBatchNO + "'");

                string strFinalSql = strSql.ToString();
                SqlDatabase odbDataBase = new SqlDatabase(connectionString);
                DbCommand odbCommand = odbDataBase.GetSqlStringCommand(strFinalSql);
                JAAJ_ExamSubjectInfo oInfo = null;
                try
                {
                    using (IDataReader reader = odbDataBase.ExecuteReader(odbCommand))
                    {
                        while (reader.Read())
                        {
                            oInfo = new JAAJ_ExamSubjectInfo();
                            ModelHelper.LoadInfoData(oInfo, reader);
                            m_oesJAAJ_ExamSubjectInfoList.Add(oInfo);
                        }
                    }
                }
                catch (Exception oeException)
                {
                    throw new Exception(oeException.Message, oeException);
                }
            }
            else
            {
                m_oesJAAJ_ExamSubjectInfoList = null;
            }
            return m_oesJAAJ_ExamSubjectInfoList;
        }
        #endregion

        #region 自定义列表（无分页） GetListByCondition
        ///	<summary>
        ///	自定义列表（无分页）
        ///	</summary>
        ///	<param name="topn">前N条，为0时表示所有符合条件的</param>
        ///	<param name="condition">自定义条件，要以And开头</param>
        ///	<param name="orderby">排序，不需要Order by </param>
        ///	<returns>List<JAAJ_SubjectScoreInfo></returns>
        public static List<JAAJ_SubjectScoreInfo> GetListByCondition(int topn, string condition, string orderby)
        {
            string strSqlString = "SELECT ";
            if (topn > 0)
            {
                strSqlString += " TOP " + topn;
            }
            strSqlString += " * FROM JAAJ_SubjectScores WHERE 1=1"; if (condition != "")
            {
                strSqlString += condition;
            }
            if (orderby != "")
            {
                strSqlString += " ORDER BY " + orderby;
            }
            else
            {
                strSqlString += " ORDER BY iSubjectScoreID DESC";
            }

            SqlDatabase odbDataBase = new SqlDatabase(connectionString);
            DbCommand odbCommand = odbDataBase.GetSqlStringCommand(strSqlString);
            List<JAAJ_SubjectScoreInfo> olstInfo = new List<JAAJ_SubjectScoreInfo>();
            JAAJ_SubjectScoreInfo oInfo = null;
            try
            {
                using (IDataReader reader = odbDataBase.ExecuteReader(odbCommand))
                {
                    while (reader.Read())
                    {
                        oInfo = new JAAJ_SubjectScoreInfo();
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
    }
}
