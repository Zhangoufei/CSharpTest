using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BonSite.Core
{
    public class ShopInfo
    {
        private int _shopid;
        private string _shopname;
        private string _address;
        private string _tel;
        private string _fax;
        private string _shopimg;
        private string _position;
        private string _body;
        private string _area;
        private string _type;
        private int _orderid;
        private string _remark;

        public int ShopID
        {
            set { _shopid = value; }
            get { return _shopid; }
        }

        public string ShopName
        {
            set { _shopname = value; }
            get { return _shopname; }
        }

        public string Address
        {
            set { _address = value; }
            get { return _address; }
        }

        public string Tel
        {
            set { _tel = value; }
            get { return _tel; }
        }

        public string Fax
        {
            set { _fax = value; }
            get { return _fax; }
        }

        public string ShopImg
        {
            set { _shopimg = value; }
            get { return _shopimg; }
        }

        public string Position
        {
            set { _position = value; }
            get { return _position; }
        }

        public string Body
        {
            set { _body = value; }
            get { return _body; }
        }

        public string Area
        {
            set { _area = value; }
            get { return _area; }
        }

        public string Type
        {
            set { _type = value; }
            get { return _type; }
        }

        public int OrderID
        {
            set { _orderid = value; }
            get { return _orderid; }
        }

        public string Remark
        {
            set { _remark = value; }
            get { return _remark; }
        }


    }

   
}
