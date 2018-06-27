using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using BonSite.Web.Framework;

namespace BonSite.Web.Admin.Models
{
    public class ShopModel
    {
        public int ShopID { get; set; }
        public string ShopName { get; set; }
        public string Address { get; set; }
        public string Tel { get; set; }
        public  string Fax { get; set; }
        public string ShopImg { get; set; }
        public string Position { get; set; }
        public string Body { get; set; }
        public string Area { get; set; }
        public string Type { get; set; }
        public int OrderID { get; set; }
        public string Remark { get; set; }
    }

    public class ShopListModel
    {
        public PageModel PageModel { get; set; }

        public DataTable DataList { get; set; }
        
    }
}