using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace JAAJExamManagementSys
{
    public class BaseInfo
    {
        List<SubjectItem> osiSubjectItemList = new List<SubjectItem>();

        [XmlElement(ElementName = "SubjectItem")]
        public List<SubjectItem> SubjectItemList
        {
            get { return osiSubjectItemList; }
            set { osiSubjectItemList = value; }
        }
    }
}
