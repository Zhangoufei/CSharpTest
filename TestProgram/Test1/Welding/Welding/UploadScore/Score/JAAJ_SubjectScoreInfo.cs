using System;
using System.Collections.Generic;
using System.Text;
using JAAJ.Common;
using UploadScore;



namespace JAAJ.Model
{
    [Serializable]
    public class JAAJ_SubjectScoreInfo
    {
        #region Private Instances
        private int m_intiSubjectScoreID = 0;
        private string m_strnvcBatchNO = "";
        private int m_intiExamineeID = 0;
        private int m_intiSubjectID = 0;
        private int m_intiSubjectItemID = 0;
        private decimal m_decdecSubjectScore = 0;
        private DateTime m_datdatScoreDate = GeneralData.NullDateTime;
        private string m_strnvcMemo = "";

        private string m_strExamineeGUID = "";
        private string m_strExamineeName = "";
        private string m_strExamineeIDNum = "";
        private string m_strSubjectName = "";
        private string m_strItemName = "";

        private decimal m_decSubItemScore = 0;


        private decimal m_decTotalScore = 0;

        private decimal m_decsumScore = 0;
        #endregion

        #region Public Properities

        /// <summary>
        /// 科目成绩ID 
        /// </summary>
        [DataAttribute(FieldType.DBField)]
        public int iSubjectScoreID
        {
            get
            {
                return m_intiSubjectScoreID;
            }
            set
            {
                m_intiSubjectScoreID = value;
            }
        }
        /// <summary>
        /// 批次号 
        /// </summary>
        [DataAttribute(FieldType.DBField)]
        public string nvcBatchNO
        {
            get
            {
                return m_strnvcBatchNO;
            }
            set
            {
                m_strnvcBatchNO = value;
            }
        }

        /// <summary>
        /// 考生ID 
        /// </summary>
        [DataAttribute(FieldType.DBField)]
        public int iExamineeID
        {
            get
            {
                return m_intiExamineeID;
            }
            set
            {
                m_intiExamineeID = value;
            }
        }

        /// <summary>
        /// 科目ID 
        /// </summary>
        [DataAttribute(FieldType.DBField)]
        public int iSubjectID
        {
            get
            {
                return m_intiSubjectID;
            }
            set
            {
                m_intiSubjectID = value;
            }
        }

        /// <summary>
        /// 科目子项ID 
        /// </summary>
        [DataAttribute(FieldType.DBField)]
        public int iSubjectItemID
        {
            get
            {
                return m_intiSubjectItemID;
            }
            set
            {
                m_intiSubjectItemID = value;
            }
        }

        /// <summary>
        /// 子项成绩 
        /// </summary>
        [DataAttribute(FieldType.DBField)]
        public decimal decSubjectScore
        {
            get
            {
                return m_decdecSubjectScore;
            }
            set
            {
                m_decdecSubjectScore = value;
            }
        }

        /// <summary>
        /// 成绩日期 
        /// </summary>
        [DataAttribute(FieldType.DBField)]
        public DateTime datScoreDate
        {
            get
            {
                return m_datdatScoreDate;
            }
            set
            {
                m_datdatScoreDate = value;
            }
        }

        /// <summary>
        /// 备注 
        /// </summary>
        [DataAttribute(FieldType.DBField)]
        public string nvcMemo
        {
            get
            {
                return m_strnvcMemo;
            }
            set
            {
                m_strnvcMemo = value;
            }
        }

        /// <summary>
        /// 考生号
        /// </summary>
        [DataAttribute(FieldType.DBField)]
        public string nvcExamineeGUID
        {
            get
            {
                return m_strExamineeGUID;
            }
            set
            {
                m_strExamineeGUID = value;
            }
        }

        /// <summary>
        /// 考生姓名
        /// </summary>
        [DataAttribute(FieldType.DBField)]
        public string nvcName
        {
            get
            {
                return m_strExamineeName; ;
            }
            set
            {
                m_strExamineeName = value;
            }
        }

        /// <summary>
        /// 考生身份证号
        /// </summary>
        [DataAttribute(FieldType.DBField)]
        public string nvcIDNum
        {
            get
            {
                return m_strExamineeIDNum;
            }
            set
            {
                m_strExamineeIDNum = value;
            }
        }

        /// <summary>
        /// 科目名称
        /// </summary>
        [DataAttribute(FieldType.DBField)]
        public string nvcSubjectName
        {
            get
            {
                return m_strSubjectName;
            }
            set
            {
                m_strSubjectName = value;
            }
        }

        /// <summary>
        /// 子项名称
        /// </summary>
        [DataAttribute(FieldType.DBField)]
        public string nvcSubjectItemName
        {
            get
            {
                return m_strItemName;
            }
            set
            {
                m_strItemName = value;
            }
        }


        /// <summary>
        /// 总分
        /// </summary>
        [DataAttribute(FieldType.DBField)]
        public decimal decTotalScore
        {
            get
            {
                return m_decTotalScore;
            }
            set
            {
                m_decTotalScore = value;
            }
        }

        /// <summary>
        /// 子项成绩之和
        /// </summary>
        [DataAttribute(FieldType.DBField)]
        public decimal sumScore
        {
            get
            {
                return m_decsumScore;
            }
            set
            {
                m_decsumScore = value;
            }
        }
        #endregion
    }

}
