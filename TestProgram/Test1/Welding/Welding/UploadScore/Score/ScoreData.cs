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

        #region ����ͻ��˷�ֵ Save
        /// <summary>
        /// ����ͻ��˷�ֵ
        /// </summary>
        /// <param name="info">����ɼ�</param>
        /// <param name="osisiJAAJ_SubItemScoreInfoList">��������ɼ������ⲽ��ɼ�</param>
        /// <returns>���������ɹ���ʧ��</returns>
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

                //ѭ��ִ�п�Ŀ���������������
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

                    //ѭ��ִ�п�Ŀ�������ⲽ���������
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
                        //����������ִ�У�ֱ�ӻع�
                        if (enmResult == EnumSubmitResult.Failed)//ʧ��
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

        #region ��ȡ������Ϣʵ�� GetExamineeInfoByIDNum
        ///	<summary>
        ///	��ȡ������Ϣʵ��
        ///	</summary>
        ///	<param name="strIDNum">���֤��</param>
        ///	<param name="strBatchNo">���κ�</param>
        ///	<returns>������Ϣ��JAAJ_ExamineeInfo</returns>
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

        #region ��ȡ��ǰ������Ϣ
        /// <summary>
        /// ��ȡ��ǰ���ڿ�����Ϣ
        /// </summary>
        /// <returns>��ǰ���ڿ�����Ϣ��JAAJ_ExamInfo</returns>
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
        /// ��ȡ����IP
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

        #region ��ȡ��Ŀ����ʵ�� GetSubjectItemInfoByID
        ///	<summary>
        ///	��ȡ��Ŀ����ʵ��
        ///	</summary>
        ///	<param name="iSubjectItemID">��Ŀ����ID</param>
        ///	<returns>��Ŀ����ʵ�壬JAAJ_SubjectItemInfo</returns>
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

        #region ��ȡ�豸��Ϣʵ�� GetDeviceInfoByIP
        ///	<summary>
        ///	��ȡ�豸��Ϣʵ��
        ///	</summary>
        ///	<param name="strIP">�豸IP</param>
        ///	<returns>�豸��Ϣʵ��,JAAJ_DeviceInfo</returns>
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

        #region ���ݿ���ID��ȡ���Թ���ʵ�� GetExamProceInfoByExamineeID
        /// <summary>
        /// ���ݿ���ID��ȡ���Թ���ʵ��
        /// </summary>
        /// <param name="iExamineeID">����ID���ǿ�����</param>
        /// <returns>���Թ���ʵ�壬JAAJ_ExamProceInfo</returns>
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

        #region �����豸״̬Ϊæµ
        /// <summary>
        /// �����豸״̬Ϊæµ
        /// </summary>
        /// <param name="iStatus">0���У�1æµ</param>
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

        #region �����豸״̬Ϊ���С������ѿ��Կ�ĿID������ID��ɾ����ǰ����Թ��̼�¼
        ///	<summary>
        ///	�����豸״̬Ϊ���С������ѿ��Կ�ĿID������ID��ɾ����ǰ����Թ��̼�¼
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

        #region ������ʦ��λӲ���豸��������ʾ��Ϣ
        ///	<summary>
        ///	������ʦ��λӲ���豸��������ʾ��Ϣ 
        ///	</summary>
        ///	<param name="strDescription">��ʾ���ݣ���Ҫ�������</param>
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

        #region ��ȡ�����ɼ���ϸ
        /// <summary>
        /// ��ȡ�����ɼ���ϸ
        /// </summary>
        /// <param name="strBatchNO">���κ�</param>
        /// <param name="datExamDate">��������</param>
        /// <param name="iExamineeID">����ID</param>
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

        #region ��������ɼ������ܳɼ�
        /// <summary>
        /// ��ȡ�����ο���ÿ������ĳɼ�
        /// </summary>
        /// <param name="strBatchNO">ѡ�����κ�</param>
        /// <param name="strDate">��������</param>
        /// <returns></returns>
        public static List<JAAJ_SubjectScoreInfo> GetListWithTotalScore(string strSql, string strBatch)
        {
            StringBuilder strSqlString = new StringBuilder();
            strSqlString.Append("SELECT subjectScore.iExamineeID,nvcName,nvcExamineeGUID,examinee.nvcMemo AS ExamineeGUID, subjectScore.nvcBatchNO, nvcIDNum, subjects.nvcSubjectName,subjectScore.iSubjectID, SUM(decSubjectScore) AS sumScore , CONVERT(varchar(10),datScoreDate,23) AS datScoreDate,batch.nvcMemo AS batchGUID   ");
            strSqlString.Append(" FROM JAAJ_Examinees AS examinee, JAAJ_SubjectScores AS subjectScore, JAAJ_Subjects AS subjects ,JAAJ_SubjectItems AS subItems,JAAJ_Exams as Exam,JAAJ_Batchs as batch  ");
            //2015-06-17������ ��subjectScore.iSubjectScoreID IN (SELECT MAX(iSubjectScoreID) AS iSubjectScoreID FROM JAAJ_SubjectScores GROUP BY isubjectItemId,iExamineeID) AND�� ���ã�����ȡ���������һ�γɼ������ڹ��������������ο��ԣ��ɼ������ظ�����
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
                //��ȡ������Ŀ�ĵ÷�Ȩ��
                List<JAAJ_ExamSubjectInfo> ListPersent = GetPersent(strBatch);
                //ÿ����Ŀ����ĳɼ����Զ�Ӧ��Ȩ��
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
                //�õ�ÿ��������Ӧ���ڵĿ����ܳɼ�
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
        /// ��ȡ������Ŀ�÷�Ȩ��
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

        #region �Զ����б��޷�ҳ�� GetListByCondition
        ///	<summary>
        ///	�Զ����б��޷�ҳ��
        ///	</summary>
        ///	<param name="topn">ǰN����Ϊ0ʱ��ʾ���з���������</param>
        ///	<param name="condition">�Զ���������Ҫ��And��ͷ</param>
        ///	<param name="orderby">���򣬲���ҪOrder by </param>
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
