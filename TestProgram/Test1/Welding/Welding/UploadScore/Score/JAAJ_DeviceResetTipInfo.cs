using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JAAJ.Model
{
    [Serializable]
    public class JAAJ_DeviceResetTipInfo
    {
        #region Private Instances
        private int m_intiResetID = 0;
        private string m_strnvcResetDescription = "";
        private int m_intiStatus = 0;
        #endregion

        #region Public Properities

        /// <summary>
        /// The 
        /// </summary>
        [DataAttribute(FieldType.DBField)]
        public int iResetID
        {
            get
            {
                return m_intiResetID;
            }
            set
            {
                m_intiResetID = value;
            }
        }

        /// <summary>
        /// The 
        /// </summary>
        [DataAttribute(FieldType.DBField)]
        public string nvcResetDescription
        {
            get
            {
                return m_strnvcResetDescription;
            }
            set
            {
                m_strnvcResetDescription = value;
            }
        }

        /// <summary>
        /// The 
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

        #endregion


    }

}
