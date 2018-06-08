using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace JAAJ.PEAR.TestModel
{
    public class WrittenTests
    {
        [XmlElement(ElementName = "WrittenTest")]
        public List<WrittenTest> TestList { get; set; }
    }
}
