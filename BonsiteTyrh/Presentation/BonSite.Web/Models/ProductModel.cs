using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

using BonSite.Core;
using BonSite.Services;
using BonSite.Web.Framework;

namespace BonSite.Web.Models
{
    public class ProductModel
    {
        public ProductInfo ProductInfo { get; set; }
        public ArticleClassInfo ArticleClassInfo { get; set; }
        public List<ArticleClassInfo> ClassPath { get; set; }
    }

    /// <summary>
    /// 列表Model
    /// </summary>
    public class ProductListModel
    {
        public PageModel PageModel { get; set; }
        public DataTable ProductList { get; set; }
        public int ArticleClassID { get; set; }
        public ArticleClassInfo ArticleClassInfo { get; set; }
        public List<ProductClassInfo> ClassPath { get; set; }
    }

}