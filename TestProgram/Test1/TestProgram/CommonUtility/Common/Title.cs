using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace ProxyCommon
{
    public class Title
    {
        private decimal m_decTitleScore = 0;
        private string m_strTitleName = "";
        private string m_strTitleDescription = "";
        private List<Steps> osStepList = new List<Steps>();

        ///<summary>
        /// 必须有默认的构造函数
        ///</summary>
        public Title()
        { }

        public Title(decimal _decTitle, string _strTitleName, string _strTitleDescription)
        {
            this.m_decTitleScore = _decTitle;
            this.m_strTitleName = _strTitleName;
            this.m_strTitleDescription = _strTitleDescription;
        }
        /// <summary>
        /// 试题总分
        /// </summary>
        public decimal TitleScore
        {
            get { return m_decTitleScore; }
            set { m_decTitleScore = value; }
        }
        /// <summary>
        /// 试题名称
        /// </summary>
        public string TitleName
        {
            get { return m_strTitleName; }
            set { m_strTitleName = value; }
        }
        /// <summary>
        /// 试题描述
        /// </summary>
        public string TitleDescription
        {
            get { return m_strTitleDescription; }
            set { m_strTitleDescription = value; }
        }
        /// <summary>
        /// 步骤成绩列表
        /// </summary>
        [XmlElement(ElementName = "Steps")]
        public List<Steps> StepList
        {
            get { return osStepList; }
            set { osStepList = value; }
        }
    }
}
