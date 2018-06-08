using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Collections.ObjectModel;

namespace JAAJ.PEAR.TestModel
{
    public class WrittenItems
    {
        private ObservableCollection<WrittenItem> itemList = new ObservableCollection<WrittenItem>();

        [XmlElement(ElementName = "WrittenItem")]
        public ObservableCollection<WrittenItem> ItemList
        {
            get { return itemList; }
            set { itemList = value; }
        }

        public WrittenItems() 
        { }
    }
}
