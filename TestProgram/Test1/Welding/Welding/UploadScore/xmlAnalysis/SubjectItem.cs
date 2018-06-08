using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace JAAJExamManagementSys
{
    public class SubjectItem
    {
        private decimal m_decSubjectItemScore = 0;
        private List<Titles> m_otTitleList = new List<Titles>();

        /// <summary>
        /// 必须有默认的构造函数
        /// </summary>
        public SubjectItem()
        {

        }

        public SubjectItem(decimal _decSubjectItemScore)
        {
            this.m_decSubjectItemScore = _decSubjectItemScore;
        }


        /// <summary>
        /// 子项总分
        /// </summary>
        public decimal SubjectItemScore
        {
            get { return m_decSubjectItemScore; }
            set { m_decSubjectItemScore = value; }
        }
        /// <summary>
        /// 试题成绩列表
        /// </summary>
        [XmlElement(ElementName = "Titles")]
        public List<Titles> TitleList
        {
            get { return m_otTitleList; }
            set { m_otTitleList = value; }
        }
    }
}
