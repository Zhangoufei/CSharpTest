using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace ProxyCommon
{
    public class Items
    {
        private List<Item> itemList = new List<Item>();

        [XmlElement(ElementName = "Item")]
        public List<Item> ItemList
        {
            get { return itemList; }
            set { itemList = value; }
        }

        public Items() 
        { }
    }
}
