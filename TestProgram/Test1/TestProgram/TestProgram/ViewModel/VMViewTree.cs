using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Xml;

namespace TestProgram.ViewModel
{
    class VMViewTree
    {

        public VMViewTree()
        {

            XmlDocument doc = new XmlDocument();
            doc.Load(AppDomain.CurrentDomain.BaseDirectory+ "\\Tests.xml");

            XmlDataProvider xdp = new XmlDataProvider();
            xdp.Document = doc;

            

        }

    }
}
