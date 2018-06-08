using System;
using System.Collections.Generic;
using System.Text;


using JAAJ.Common;

namespace JAAJ.Model
{
    [Serializable]
    public class JAAJ_SubjectItemInfo
    {
        #region Private Instances
        private int m_intiSubjectItemID = 0;
        private string m_strnvcSubjectItemName = "";
        private string m_strnvcSubjectItemShortName = "";
        private string m_strnvcDescription = "";
        private int m_intiSubjectID = 0;
        private int m_intiStatus = 0;
        private decimal m_decdecScore = 0;
        private int m_intiDeviceID = 0;
        private int m_intiTimeLength = 0;
        private string m_strnvcMemo = "";
        private string m_strxmlSubjectItemConfig = "";
        private DateTime m_datdatUpdateDate = DateTime.Now;
        private int m_intiUserID = 0;
        private string m_strnvcSubjectName = "";
        private string m_strnvcDeviceName = "";
        private string m_strnvcStatusName = "";
        private string m_strnvcUserName = "";
        private bool m_blnbJoinExam = true;
        private bool m_blnbRadom = true;
        #endregion

        #region Public Properities

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
        /// 科目子项名称 
        /// </summary>
        [DataAttribute(FieldType.DBField)]
        public string nvcSubjectItemName
        {
            get
            {
                return m_strnvcSubjectItemName;
            }
            set
            {
                m_strnvcSubjectItemName = value;
            }
        }
        /// <summary>
        /// 简称 
        /// </summary>
        [DataAttribute(FieldType.DBField)]
        public string nvcSubjectItemShortName
        {
            get
            {
                return m_strnvcSubjectItemShortName;
            }
            set
            {
                m_strnvcSubjectItemShortName = value;
            }
        }
        /// <summary>
        /// 简述 
        /// </summary>
        [DataAttribute(FieldType.DBField)]
        public string nvcDescription
        {
            get
            {
                return m_strnvcDescription;
            }
            set
            {
                m_strnvcDescription = value;
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
        /// 子项状态 
        /// </summary>
        [DataAttribute(FieldType.DBField)]
        public int iStatus
        {
            get
            {
                return m_intiStatus;
            }
            set
            {
                m_intiStatus = value;
            }
        }

        /// <summary>
        /// 状态名称 
        /// </summary>
        [DataAttribute(FieldType.DBField)]
        public string nvcStatusName
        {
            get
            {
                return m_strnvcStatusName;
            }
            set
            {
                m_strnvcStatusName = value;
            }
        }


        /// <summary>
        /// 总分值 
        /// </summary>
        [DataAttribute(FieldType.DBField)]
        public decimal decScore
        {
            get
            {
                return m_decdecScore;
            }
            set
            {
                m_decdecScore = value;
            }
        }

        /// <summary>
        /// 设备类别ID 
        /// </summary>
        [DataAttribute(FieldType.DBField)]
        public int iDeviceID
        {
            get
            {
                return m_intiDeviceID;
            }
            set
            {
                m_intiDeviceID = value;
            }
        }

        /// <summary>
        /// 设备类别名称
        /// </summary>
        [DataAttribute(FieldType.DBField)]
        public string nvcDeviceName
        {
            get
            {
                return m_strnvcDeviceName;
            }
            set
            {
                m_strnvcDeviceName = value;
            }
        }

        /// <summary>
        /// 考试时长 
        /// </summary>
        [DataAttribute(FieldType.DBField)]
        public int iTimeLength
        {
            get
            {
                return m_intiTimeLength;
            }
            set
            {
                m_intiTimeLength = value;
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
        /// XML明细 
        /// </summary>
        [DataAttribute(FieldType.DBField)]
        public string xmlSubjectItemConfig
        {
            get
            {
                return m_strxmlSubjectItemConfig;
            }
            set
            {
                m_strxmlSubjectItemConfig = value;
            }
        }

        /// <summary>
        /// 更新日期 
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
        /// 操作员名称
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
        /// 是否参与考试 
        /// </summary>
        [DataAttribute(FieldType.DBField)]
        public bool blJoinExam
        {
            get
            {
                return m_blnbJoinExam;
            }
            set
            {
                m_blnbJoinExam = value;
            }
        }

        /// <summary>
        /// 是否随机 
        /// </summary>
        [DataAttribute(FieldType.DBField)]
        public bool blRandom
        {
            get
            {
                return m_blnbRadom;
            }
            set
            {
                m_blnbRadom = value;
            }
        }

        #endregion


    }

}
