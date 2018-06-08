using System;
using System.Collections.Generic;
using System.Text;


using JAAJ.Common;


namespace JAAJ.Model
{
    [Serializable]
    public class JAAJ_DeviceInfo
    {
        #region Private Instances
        private int m_intiDeviceID = 0;
        private string m_strnvcDeviceName = "";
        private string m_strnvcDescription = "";
        private int m_intiDeviceTypeID = 0;
        private int m_intiExamnationRoomID = 0;
        private int m_intiDeviceStatus = 0;
        private string m_strnvcDeviceStatus = "";
        private string m_strnvcIP = "";
        private string m_strnvcMemo = "";
        private DateTime m_datdatUpdateDate = DateTime.Now;
        private int m_intiUserID = 0;
        private string m_strnvcDeviceTypeName="";
        private string m_strnvcUserName = "";
        #endregion

        #region Public Properities

        /// <summary>
        /// 设备ID 
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
        /// 设备名称 
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
        /// 设备简述 
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
        /// The 
        /// </summary>
        [DataAttribute(FieldType.DBField)]
        public int iExamnationRoomID
        {
            get
            {
                return m_intiExamnationRoomID;
            }
            set
            {
                m_intiExamnationRoomID = value;
            }
        }

        /// <summary>
        /// 类别ID 
        /// </summary>
        [DataAttribute(FieldType.DBField)]
        public int iDeviceTypeID
        {
            get
            {
                return m_intiDeviceTypeID;
            }
            set
            {
                m_intiDeviceTypeID = value;
            }
        }

        /// <summary>
        /// 类别名称 
        /// </summary>
        [DataAttribute(FieldType.DBField)]
        public string nvcDeviceTypeName
        {
            get
            {
                return m_strnvcDeviceTypeName;
            }
            set
            {
                m_strnvcDeviceTypeName = value;
            }
        }



        /// <summary>
        /// 设备状态 
        /// </summary>
        [DataAttribute(FieldType.DBField)]
        public int iDeviceStatus
        {
            get
            {
                return m_intiDeviceStatus;
            }
            set
            {
                m_intiDeviceStatus = value;
            }
        }
        
        /// <summary>
        /// 设备状态名称 
        /// </summary>
        [DataAttribute(FieldType.DBField)]
        public string nvcDeviceStatusName
        {
            get
            {
                return m_strnvcDeviceStatus;
            }
            set
            {
                m_strnvcDeviceStatus = value;
            }
        }


        /// <summary>
        /// IP地址 
        /// </summary>
        [DataAttribute(FieldType.DBField)]
        public string nvcIP
        {
            get
            {
                return m_strnvcIP;
            }
            set
            {
                m_strnvcIP = value;
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




        #endregion


    }

}
