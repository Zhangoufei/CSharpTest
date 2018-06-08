using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JAAJExamManagementSys
{
    public class Step
    {
        private decimal m_decStepScore = 0;
        private string m_strStepName = "";
        private string m_strStepDescription = "";


        /// <summary>
        /// 必须有默认的构造函数
        /// </summary>
        public Step()
        { }
        public Step(decimal _decTitle, string _strTitleName, string _strTitleDescription)
        {
            this.m_decStepScore = _decTitle;
            this.m_strStepName = _strTitleName;
            this.m_strStepDescription = _strTitleDescription;
        }
        /// <summary>
        /// 步骤分数
        /// </summary>
        public decimal StepScore
        {
            get { return m_decStepScore; }
            set { m_decStepScore = value; }
        }
        /// <summary>
        /// 步骤名称
        /// </summary>
        public string StepName
        {
            get { return m_strStepName; }
            set { m_strStepName = value; }
        }
        /// <summary>
        /// 步骤描述
        /// </summary>
        public string StepDescription
        {
            get { return m_strStepDescription; }
            set { m_strStepDescription = value; }
        }
    }
}
