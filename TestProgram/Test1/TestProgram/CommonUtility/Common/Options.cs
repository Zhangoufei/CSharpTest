using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace ProxyCommon
{
    public class Options
    {
        private List<Option> optionList = new List<Option>();

        [XmlElement(ElementName = "Option")]
        public List<Option> OptionList
        {
            get { return optionList; }
            set { optionList = value; }
        }

        public Options()
        { }
    }
}
