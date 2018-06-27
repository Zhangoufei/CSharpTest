using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BonSite.Core
{
    public class ProductClassInfo
    {
        private int _productclassid;
        private string _productclassname = "";
        private int _parentproductclassid = 0;
        private int _displayorder;

        public int ProductClassID
        {
            set { _productclassid = value; }
            get { return _productclassid; }
        }

        public string ProductClassName
        {
            set { _productclassname = value; }
            get { return _productclassname; }
        }

        public int ParentProductClassID
        {
            set { _parentproductclassid = value; }
            get { return _parentproductclassid; }
        }

        public int DisplayOrder
        {
            set { _displayorder = value; }
            get { return _displayorder; }
        }
    }
}
