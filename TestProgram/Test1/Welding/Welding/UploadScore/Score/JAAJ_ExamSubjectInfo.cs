using System;
using System.Collections.Generic;
using System.Text;


namespace JAAJ.Model
{
    [Serializable]
    public class JAAJ_ExamSubjectInfo
    {
        #region Private Instances
        private int m_intiExamSubjectID = 0;
        private int m_intiSubjectID = 0;
        private int m_intiExamID = 0;
        private decimal m_decdecExamPersent = 0;
        private string m_strnvcSubjectItemIDs = "";
        private int m_intiTitleNumber = 0;
        private int m_intiTeacherID = 0;
        private string m_strnvcSubjectItemShortNames = "";
        private string m_strnvcMemo = "";
        private int m_intiUserID = 0;
        private string m_strnvcSubjectName = "";
        private string m_strnvcShortName = "";
        private string m_strnvcSubjectPersent = "";
        private DateTime m_datExamDate;
        private string m_strOperate = "移除";
        #endregion

        #region Public Properities

        /// <summary>
        /// 考试安排科目明细表ID 
        /// </summary>
        [DataAttribute(FieldType.DBField)]
        public int iExamSubjectID
        {
            get
            {
                return m_intiExamSubjectID;
            }
            set
            {
                m_intiExamSubjectID = value;
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
        /// 考试ID 
        /// </summary>
        [DataAttribute(FieldType.DBField)]
        public int iExamID
        {
            get
            {
                return m_intiExamID;
            }
            set
            {
                m_intiExamID = value;
            }
        }

        /// <summary>
        /// 科目分值权重 
        /// </summary>
        [DataAttribute(FieldType.DBField)]
        public decimal decExamPersent
        {
            get
            {
                return m_decdecExamPersent;
            }
            set
            {
                m_decdecExamPersent = value;
            }
        }

        /// <summary>
        /// 考试子项ID组合 
        /// </summary>
        [DataAttribute(FieldType.DBField)]
        public string nvcSubjectItemIDs
        {
            get
            {
                return m_strnvcSubjectItemIDs;
            }
            set
            {
                m_strnvcSubjectItemIDs = value;
            }
        }

        /// <summary>
        /// 考题数量 
        /// </summary>
        [DataAttribute(FieldType.DBField)]
        public int iTitleNumber
        {
            get
            {
                return m_intiTitleNumber;
            }
            set
            {
                m_intiTitleNumber = value;
            }
        }

        /// <summary>
        /// 监考老师ID 
        /// </summary>
        [DataAttribute(FieldType.DBField)]
        public int iTeacherID
        {
            get
            {
                return m_intiTeacherID;
            }
            set
            {
                m_intiTeacherID = value;
            }
        }
        /// <summary>
        /// 科目子项简称组合 
        /// </summary>
        [DataAttribute(FieldType.DBField)]
        public string nvcSubjectItemShortNames
        {
            get
            {
                return m_strnvcSubjectItemShortNames;
            }
            set
            {
                m_strnvcSubjectItemShortNames = value;
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
        /// 操作员ID 
        /// </summary>
        [DataAttribute(FieldType.DBField)]
        public int iUserID
        {
            get
            {
                return m_intiUserID;
            }
            set
            {
                m_intiUserID = value;
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
                return m_strnvcSubjectName;
            }
            set
            {
                m_strnvcSubjectName = value;
            }
        }

        /// <summary>
        /// 科目简称 
        /// </summary>
        [DataAttribute(FieldType.DBField)]
        public string nvcShortName
        {
            get
            {
                return m_strnvcShortName;
            }
            set
            {
                m_strnvcShortName = value;
            }
        }
        /// <summary>
        /// 科目权重 
        /// </summary>
        [DataAttribute(FieldType.DBField)]
        public string nvcSubjectPersent
        {
            get
            {
                return m_strnvcSubjectPersent;
            }
            set
            {
                m_strnvcSubjectPersent = value;
            }
        }
         
        /// <summary>
        /// 考试日期日期 
        /// </summary>
        [DataAttribute(FieldType.DBField)]
        public DateTime datExamDate
        {
            get
            {
                return m_datExamDate;
            }
            set
            {
                m_datExamDate = value;
            }
        }


        /// <summary>
        /// 操作
        /// </summary>
        [DataAttribute(FieldType.DBField)]
        public string Operate
        {
            get
            {
                return m_strOperate;
            }
            set
            {
                m_strOperate = value;
            }
        }
        #endregion


    }

}
