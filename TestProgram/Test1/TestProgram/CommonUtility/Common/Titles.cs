using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace ProxyCommon
{
    public class Titles
    {
        List<Title> m_otTitleList = new List<Title>();

        [XmlElement(ElementName = "Title")]
        public List<Title> TitleList
        {
            get { return m_otTitleList; }
            set { m_otTitleList = value; }
        }
    }
}
