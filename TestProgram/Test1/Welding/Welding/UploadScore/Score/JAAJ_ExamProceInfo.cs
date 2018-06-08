using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JAAJ.Model
{
    [Serializable]
    public class JAAJ_ExamProceInfo
    {
        #region Private Instances
        private int m_intiExamProcessID = 0;
        private int m_intiExamineeID = 0;
        private int m_intiSubjectItemID = 0;
        #endregion

        #region Public Properities

        /// <summary>
        /// The 
        /// </summary>
        [DataAttribute(FieldType.DBField)]
        public int iExamProcessID
        {
            get
            {
                return m_intiExamProcessID;
            }
            set
            {
                m_intiExamProcessID = value;
            }
        }

        /// <summary>
        /// The 
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
        /// The 
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

        #endregion


    }

}
