using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace JAAJ.PEAR.TestModel
{
    public class Test
    {
        public string Name { get; set; }
        public decimal Score { get; set; }
        public string Description { get; set; }
        public string GroupName { get; set; }
        public WrittenItems WrittenItems { get; set; }

        public Test()
        { }

    }
}
