using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace ProxyCommon
{
    public class Test
    {
        public string Name { get; set; }
        public decimal Score { get; set; }

        public decimal Min { set; get; }
        public decimal Max { set; get; }
        public string Description { get; set; }
        public string GroupName { get; set; }
        public WrittenItems WrittenItems { get; set; }

        public Items Items { get; set; }
        public Test()
        { }

    }
}
