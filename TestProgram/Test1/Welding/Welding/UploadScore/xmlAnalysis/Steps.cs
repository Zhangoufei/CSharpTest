using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace JAAJExamManagementSys
{
    public class Steps
    {
        List<Step> osStepList = new List<Step>();

        [XmlElement(ElementName = "Step")]
        public List<Step> StepList
        {
            get { return osStepList; }
            set { osStepList = value; }
        }
    }
}
