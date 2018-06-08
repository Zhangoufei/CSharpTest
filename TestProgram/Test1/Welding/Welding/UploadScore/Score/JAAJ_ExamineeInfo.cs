using System;
using System.Collections.Generic;

using System.Text;

using JAAJ.Common;

namespace JAAJ.Model
{
    [Serializable]
    public class JAAJ_ExamineeInfo
    {
        #region Private Instances
        private int m_intiExamineeID = 0;
        private string m_strnvcExamineeGUID = "";
        private string m_strnvcName = "";
        private bool m_blnbSex = false;
        private string m_strnvcNation = "";
        private string m_strnvcCompanyName = "";
        private int m_intiCardType = 0;
        private string m_strnvcIDNum = "";
        private string m_strnvcPostCode = "";
        private string m_strnvcTelphone = "";
        private string m_strnvcAddress = "";
        private DateTime m_datdatBirthday = DateTime.Now;
        private DateTime m_datdatExamDate = DateTime.Now;
        private string m_strnvcBatchNO = "";
        private byte[] m_bytsimgPicture = null;
        private DateTime m_datdatIniCardDate = DateTime.Now;
        private int m_intiDisExamType = 0;
        private string m_strnvcClassGUID = "";
        private string m_strnvcClassName = "";
        private string m_strnvcDepGUID = "";
        private string m_strnvcDepName = "";
        private string m_strnvcMemo = "";
        private int m_intiExamType = 0;
        private int m_intiUserID = 0;
        #endregion

        #region Public Properities

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
        /// 考生GUID
        /// </summary>
        [DataAttribute(FieldType.DBField)]
        public string nvcExamineeGUID
        {
            get
            {
                return m_strnvcExamineeGUID;
            }
            set
            {
                m_strnvcExamineeGUID = value;
            }
        }

        /// <summary>
        /// 姓名 
        /// </summary>
        [DataAttribute(FieldType.DBField)]
        public string nvcName
        {
            get
            {
                return m_strnvcName;
            }
            set
            {
                m_strnvcName = value;
            }
        }

        /// <summary>
        /// 性别 
        /// </summary>
        [DataAttribute(FieldType.DBField)]
        public bool bSex
        {
            get
            {
                return m_blnbSex;
            }
            set
            {
                m_blnbSex = value;
            }
        }

        /// <summary>
        /// 民族 
        /// </summary>
        [DataAttribute(FieldType.DBField)]
        public string nvcNation
        {
            get
            {
                return m_strnvcNation;
            }
            set
            {
                m_strnvcNation = value;
            }
        }

        /// <summary>
        /// 单位名称 
        /// </summary>
        [DataAttribute(FieldType.DBField)]
        public string nvcCompanyName
        {
            get
            {
                return m_strnvcCompanyName;
            }
            set
            {
                m_strnvcCompanyName = value;
            }
        }

        /// <summary>
        /// 证件类型 
        /// </summary>
        [DataAttribute(FieldType.DBField)]
        public int iCardType
        {
            get
            {
                return m_intiCardType;
            }
            set
            {
                m_intiCardType = value;
            }
        }

        /// <summary>
        /// 证件号码 
        /// </summary>
        [DataAttribute(FieldType.DBField)]
        public string nvcIDNum
        {
            get
            {
                return m_strnvcIDNum;
            }
            set
            {
                m_strnvcIDNum = value;
            }
        }

        /// <summary>
        /// 邮编 
        /// </summary>
        [DataAttribute(FieldType.DBField)]
        public string nvcPostCode
        {
            get
            {
                return m_strnvcPostCode;
            }
            set
            {
                m_strnvcPostCode = value;
            }
        }

        /// <summary>
        /// 联系电话 
        /// </summary>
        [DataAttribute(FieldType.DBField)]
        public string nvcTelphone
        {
            get
            {
                return m_strnvcTelphone;
            }
            set
            {
                m_strnvcTelphone = value;
            }
        }

        /// <summary>
        /// 联系地址 
        /// </summary>
        [DataAttribute(FieldType.DBField)]
        public string nvcAddress
        {
            get
            {
                return m_strnvcAddress;
            }
            set
            {
                m_strnvcAddress = value;
            }
        }

        /// <summary>
        /// 出生日期 
        /// </summary>
        [DataAttribute(FieldType.DBField)]
        public DateTime datBirthday
        {
            get
            {
                return m_datdatBirthday;
            }
            set
            {
                m_datdatBirthday = value;
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
        /// 批次 
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
        /// 头像 
        /// </summary>
        [DataAttribute(FieldType.DBField)]
        public byte[] imgPicture
        {
            get
            {
                return m_bytsimgPicture;
            }
            set
            {
                m_bytsimgPicture = value;
            }
        }

        /// <summary>
        /// 特种初始领证日期 
        /// </summary>
        [DataAttribute(FieldType.DBField)]
        public DateTime datIniCardDate
        {
            get
            {
                return m_datdatIniCardDate;
            }
            set
            {
                m_datdatIniCardDate = value;
            }
        }

        /// <summary>
        /// 禁考标识 
        /// </summary>
        [DataAttribute(FieldType.DBField)]
        public int iDisExamType
        {
            get
            {
                return m_intiDisExamType;
            }
            set
            {
                m_intiDisExamType = value;
            }
        }

        /// <summary>
        /// 班级GUID 
        /// </summary>
        [DataAttribute(FieldType.DBField)]
        public string nvcClassGUID
        {
            get
            {
                return m_strnvcClassGUID;
            }
            set
            {
                m_strnvcClassGUID = value;
            }
        }

        /// <summary>
        /// 班级名称 
        /// </summary>
        [DataAttribute(FieldType.DBField)]
        public string nvcClassName
        {
            get
            {
                return m_strnvcClassName;
            }
            set
            {
                m_strnvcClassName = value;
            }
        }

        /// <summary>
        /// 培训机构GUID 
        /// </summary>
        [DataAttribute(FieldType.DBField)]
        public string nvcDepGUID
        {
            get
            {
                return m_strnvcDepGUID;
            }
            set
            {
                m_strnvcDepGUID = value;
            }
        }

        /// <summary>
        /// 培训机构名称 
        /// </summary>
        [DataAttribute(FieldType.DBField)]
        public string nvcDepName
        {
            get
            {
                return m_strnvcDepName;
            }
            set
            {
                m_strnvcDepName = value;
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
        /// 学员信息类别 
        /// </summary>
        [DataAttribute(FieldType.DBField)]
        public int iExamType
        {
            get
            {
                return m_intiExamType;
            }
            set
            {
                m_intiExamType = value;
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

        string m_strnvcCnName = "";

        /// <summary>
        /// 操作员名字 
        /// </summary>
        [DataAttribute(FieldType.DBField)]
        public string nvcCnName
        {
            get
            {
                return m_strnvcCnName;
            }
            set
            {
                m_strnvcCnName = value;
            }
        }


        string m_strnvcSex = "男";

        /// <summary>
        /// 性别名
        /// </summary>
        [DataAttribute(FieldType.DBField)]
        public string nvcSexName
        {
            get
            {
                return m_strnvcSex;
            }
            set
            {
                m_strnvcSex = value;
            }
        }
        #endregion


    }

}
