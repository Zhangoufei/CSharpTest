using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace ProxyCommon
{
    public class Option 
    {
        bool isChecked;

        [XmlAttribute]
        public string Serial { get; set; }

        [XmlAttribute]
        public string Title { get; set; }

        [XmlAttribute]
        public string Image { get; set; }

        public bool IsChecked
        {
            get {
                return isChecked;
            }
            set {
                isChecked = value;
            }
        }

        public bool IsMultiple { get; set; }

        public Option()
        {
            IsChecked = false;
        }
    }
}
