using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace ProxyCommon
{
    public class WrittenTest
    {
        public string Name { get; set; }
        public decimal Score { get; set; }
        public string Description { get; set; }
        public WrittenItems WrittenItems { get; set; }

        public WrittenTest()
        { }

    }
}
