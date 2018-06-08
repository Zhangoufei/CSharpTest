using System;
using System.Collections.Generic;
using System.Text;

namespace JAAJ.Model
{
    [Serializable]
    public class JAAJ_SubItemScoreInfo
    {
        private int m_intiSubItemScoreID = 0;
        private int m_intiSubjectScoreID = 0;
        private int m_intiExamineeID = 0;
        private int m_intiTitleID = 0;
        private string m_strnvcTitleName = "";
        private string m_strnvcDescription = "";
        private decimal m_decdecSubjectItemScore = 0;
        private string m_strnvcMemo = "";

        private List<JAAJ_StepScoreInfo> m_ossiJAAJ_StepScoreInfoList = new List<JAAJ_StepScoreInfo>();

        /// <summary>
        /// 考生姓名
        /// </summary>
        private string m_nvcName = "";

        /// <summary>
        /// 证件号
        /// </summary>
        private string m_nvcIDNum = "";

        /// <summary>
        /// GUID编号
        /// </summary>
        private string m_nvcExamineeGUID = "";

        /// <summary>
        /// 科目名称
        /// </summary>
        private string m_nvcSubjectName = "";

        /// <summary>
        /// 科目子项名称
        /// </summary>
        private string m_nvcSubjectItemName = "";

        /// <summary>
        /// 试题名称
        /// </summary>
        private string m_nvcTitleName = "";

        private DateTime m_datScoreDate;


        /// <summary>
        /// 试题成绩ID 
        /// </summary>

        public int iSubItemScoreID
        {
            get { return m_intiSubItemScoreID; }
            set { m_intiSubItemScoreID = value; }
        }

        /// <summary>
        /// 科目成绩ID 
        /// </summary>

        public int iSubjectScoreID
        {
            get { return m_intiSubjectScoreID; }
            set { m_intiSubjectScoreID = value; }
        }

        /// <summary>
        /// 考生ID 
        /// </summary>

        public int iExamineeID
        {
            get { return m_intiExamineeID; }
            set { m_intiExamineeID = value; }
        }

        /// <summary>
        /// 考生姓名
        /// </summary>

        public string nvcName
        {
            get { return m_nvcName; }
            set { m_nvcName = value; }
        }

        /// <summary>
        /// 证件号
        /// </summary>

        public string nvcIDNum
        {
            get { return m_nvcIDNum; }
            set { m_nvcIDNum = value; }
        }

        /// <summary>
        /// 考生GUID
        /// </summary>

        public string nvcExamineeGUID
        {
            get { return m_nvcExamineeGUID; }
            set { m_nvcExamineeGUID = value; }
        }


        /// <summary>
        /// 科目名称
        /// </summary>

        public string nvcSubjectName
        {
            get { return m_nvcSubjectName; }
            set { m_nvcSubjectName = value; }
        }

        /// <summary>
        /// 科目子项名称
        /// </summary>

        public string nvcSubjectItemName
        {
            get { return m_nvcSubjectItemName; }
            set { m_nvcSubjectItemName = value; }
        }



        /// <summary>
        /// 试题ID 
        /// </summary>

        public int iTitleID
        {
            get { return m_intiTitleID; }
            set { m_intiTitleID = value; }
        }


        /// <summary>
        /// The 
        /// </summary>

        public string nvcDescription
        {
            get { return m_strnvcDescription; }
            set { m_strnvcDescription = value; }
        }

        /// <summary>
        /// 试题名称
        /// </summary>

        public string nvcTitleName
        {
            get { return m_nvcTitleName; }
            set { m_nvcTitleName = value; }
        }


        /// <summary>
        /// 分值 
        /// </summary>

        public decimal decSubjectItemScore
        {
            get { return m_decdecSubjectItemScore; }
            set { m_decdecSubjectItemScore = value; }
        }

        /// <summary>
        /// 备注 
        /// </summary>

        public string nvcMemo
        {
            get { return m_strnvcMemo; }
            set { m_strnvcMemo = value; }
        }

        /// <summary>
        /// 考试日期
        /// </summary>

        public DateTime datScoreDate
        {
            get { return m_datScoreDate; }
            set { m_datScoreDate = value; }
        }


        /// <summary>
        /// 试题成绩ID 
        /// </summary>

        public List<JAAJ_StepScoreInfo> JAAJ_StepScoreInfoList
        {
            get { return m_ossiJAAJ_StepScoreInfoList; }
            set { m_ossiJAAJ_StepScoreInfoList = value; }
        }
    }
}
