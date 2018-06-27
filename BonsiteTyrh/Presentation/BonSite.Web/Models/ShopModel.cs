using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using BonSite.Core;
using BonSite.Web.Framework;

namespace BonSite.Web.Models
{
    public class ShopModel
    {

    }

    public class ShopListModel
    {
        public PageModel PageModel { get; set; }
        public DataTable ShopList { get; set; }
        public string Area { get; set; }
        public int ArticleClassID { get; set; }
        public ArticleClassInfo ArticleClassInfo { get; set; }
        public List<ArticleClassInfo> ClassPath { get; set; }

    }
}