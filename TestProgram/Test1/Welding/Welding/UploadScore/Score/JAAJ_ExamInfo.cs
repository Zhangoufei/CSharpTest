using System;
using System.Collections.Generic;
using System.Text;
using JAAJ.Common;

namespace JAAJ.Model
{
    [Serializable]
    public class JAAJ_ExamInfo
    {
        #region Private Instances
        private int m_intiExamID = 0;
        private string m_strnvcBatchNO = "";
        private string m_strnvcExamName = "";
        private DateTime m_datdatExamDate = DateTime.Now;
        private string m_strnvcExamAdress = "";
        private int m_intiExamStatus = 0;
        private string m_strnvcMemo = "";
        private DateTime m_datdatUpdateDate = DateTime.Now;
        private bool m_blnblIsTemplate = false;

        private int m_intiUserID = 0;

        private string m_strnvcUserName = "";
        private string m_strnvcExamStatus = "";
        #endregion

        #region Public Properities

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
        /// 考试名称 
        /// </summary>
        [DataAttribute(FieldType.DBField)]
        public string nvcExamName
        {
            get
            {
                return m_strnvcExamName;
            }
            set
            {
                m_strnvcExamName = value;
            }
        }

        /// <summary>
        /// 考试日期 
        /// </summary>
        [DataAttribute(FieldType.DBField)]
        public DateTime datExamDate
        {
            get
            {
                return m_datdatExamDate;
            }
            set
            {
                m_datdatExamDate = value;
            }
        }

        /// <summary>
        /// 考试地点 
        /// </summary>
        [DataAttribute(FieldType.DBField)]
        public string nvcExamAdress
        {
            get
            {
                return m_strnvcExamAdress;
            }
            set
            {
                m_strnvcExamAdress = value;
            }
        }

        /// <summary>
        /// 考试状态 
        /// </summary>
        [DataAttribute(FieldType.DBField)]
        public int iExamStatus
        {
            get
            {
                return m_intiExamStatus;
            }
            set
            {
                m_intiExamStatus = value;
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
        public DateTime datUpdateDate
        {
            get
            {
                return m_datdatUpdateDate;
            }
            set
            {
                m_datdatUpdateDate = value;
            }
        }

        /// <summary>
        /// 更新日期 
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
        /// 登录账户 
        /// </summary>
        [DataAttribute(FieldType.DBField)]
        public string nvcUserName
        {
            get
            {
                return m_strnvcUserName;
            }
            set
            {
                m_strnvcUserName = value;
            }
        }
        /// <summary>
        /// 考试状态名称 
        /// </summary>
        [DataAttribute(FieldType.DBField)]
        public string nvcExamStatus
        {
            get
            {
                return m_strnvcExamStatus;
            }
            set
            {
                m_strnvcExamStatus = value;
            }
        }

        /// <summary>
        /// The 
        /// </summary>
        [DataAttribute(FieldType.DBField)]
        public bool blIsTemplate
        {
            get
            {
                return m_blnblIsTemplate;
            }
            set
            {
                m_blnblIsTemplate = value;
            }
        }

        #endregion


    }

}
