using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace ProxyCommon
{
    public class Tests
    {
        List<Test> testList = new List<Test>();

        [XmlElement(ElementName = "Test")]
        public List<Test> TestList
        {
            get { return testList; }
            set { testList = value; }
        }
    }
}
