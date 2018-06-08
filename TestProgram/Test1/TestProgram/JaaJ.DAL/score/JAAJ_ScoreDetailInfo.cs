using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms.VisualStyles;

namespace JAAJ.Model
{
    [Serializable]
    public class JAAJ_ScoreDetailInfo
    {
        #region Private Instances

        private string m_strnvcTitleName = "";
        private string m_strnvcDescription = "";

        #endregion

        #region Public Properities

        /// <summary>
        /// 成绩名称
        /// </summary>
        public string nvcTitleName
        {
            get
            {
                return m_strnvcTitleName;
            }
            set
            {
                m_strnvcTitleName = value;
            }
        }


        /// <summary>
        /// 成绩描述
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

        #endregion
    }

}
