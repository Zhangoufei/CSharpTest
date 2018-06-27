using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace BonSite.Core
{
    [Serializable]
    public class RouteConfigInfo : IConfigInfo
    {
        public List<map> maps { get; set; }
    }

    [Serializable]
    public class map
    {
        [XmlAttribute]
        public string name { get; set; }

        [XmlAttribute]
        public string url { get; set; }

        [XmlAttribute]
        public string controller { get; set; }

        [XmlAttribute]
        public string action { get; set; }

        public List<parames> parameters { get; set; }
    }

    [Serializable]
    public class parames
    {
        [XmlAttribute]
        public string name { get; set; }

        [XmlAttribute]
        public string value { get; set; }

        [XmlAttribute]
        public string constraint { get; set; }
    }

}
