using System;
using System.Collections.Generic;
using System.Text;



namespace JAAJ.Model
{
    [Serializable]
    public class JAAJ_StepScoreInfo
    {
        #region Private Instances
        private int m_intiStepScoreID = 0;
        private int m_intiSubItemScoreID = 0;
        private int m_intiStepID = 0;

        private string m_strnvcDescription = "";

        private decimal m_decdecStepScore = 0;
        private string m_strnvcMemo = "";

        private string m_strExamineeName = "";
        private string m_strExamineeGUID = "";
        private string m_strExamineeIDNum = "";
        private string m_strSubjectName = "";
        private string m_strSubItemName = "";
        private string m_strTitleName = "";
        private string m_strStepName = "";
        private decimal m_decTitleScore = 0;
        #endregion

        #region Public Properities

        /// <summary>
        /// 试题步骤成绩ID 
        /// </summary>

        public int iStepScoreID
        {
            get
            {
                return m_intiStepScoreID;
            }
            set
            {
                m_intiStepScoreID = value;
            }
        }

        /// <summary>
        /// 试题成绩ID 
        /// </summary>

        public int iSubItemScoreID
        {
            get
            {
                return m_intiSubItemScoreID;
            }
            set
            {
                m_intiSubItemScoreID = value;
            }
        }

        /// <summary>
        /// 步骤ID 
        /// </summary>

        public int iStepID
        {
            get
            {
                return m_intiStepID;
            }
            set
            {
                m_intiStepID = value;
            }
        }

        /// <summary>
        /// 分值 
        /// </summary>

        public decimal decStepScore
        {
            get
            {
                return m_decdecStepScore;
            }
            set
            {
                m_decdecStepScore = value;
            }
        }

        /// <summary>
        /// 备注 
        /// </summary>

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
        /// 考生姓名
        /// </summary>

        public string nvcExamineeName
        {
            get
            {
                return m_strExamineeName;
            }
            set
            {
                m_strExamineeName = value;
            }
        }

        /// <summary>
        /// 考生GUID
        /// </summary>

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
        /// 考生身份证号
        /// </summary>

        public string nvcExamineeIDNum
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

        public string nvcSubItemName
        {
            get
            {
                return m_strSubItemName;
            }
            set
            {
                m_strSubItemName = value;
            }
        }

        /// <summary>
        /// 试题名称
        /// </summary>

        public string nvcTitleName
        {
            get
            {
                return m_strTitleName;
            }
            set
            {
                m_strTitleName = value;
            }
        }

        /// <summary>
        /// 步骤名称
        /// </summary>

        public string nvcStepName
        {
            get
            {
                return m_strStepName;
            }
            set
            {
                m_strStepName = value;
            }
        }
     

        /// <summary>
        /// The 
        /// </summary>

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
        /// 试题成绩
        /// </summary>

        public decimal decTitleScore
        {
            get
            {
                return m_decTitleScore;
            }
            set
            {
                m_decTitleScore = value;
            }
        }

        #endregion

    }

}
