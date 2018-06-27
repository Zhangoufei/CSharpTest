using System;
using System.Data;
using System.Web.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

using BonSite.Core;
using BonSite.Services;
using BonSite.Web.Framework;
using System.Collections.Generic;

namespace BonSite.Web.Admin.Models
{
    public class ArticleClassModel
    {
        public int ArticleClassID { get; set; }

        public string ClassName { get; set; }

        public int ParentArticleClassID { get; set; }

        public int ClassType { get; set; }

        public int Target { get; set; }

        public int IsNav { get; set; }

        public int IsWeb { get; set; }

        public string WebUrl { get; set; }

        public int IsAdmin { get; set; }

        public int IsShowNews { get; set; }

        public string AdminUrl { get; set; }

        public int DisplayOrder { get; set; }

        public int IsOpen { get; set; }

        public string ListView { get; set; }

        public string ContentView { get; set; }

        public string Code { get; set; }

        public string ImgUrl { get; set; }

        public string Keyword { get; set; }

        public string Description { get; set; }
        public int IsClassBrand { get; set; }
        public string Subhead { get; set; }
    }

    public class ArticleClassListmodel
    {
        /// <summary>
        /// 数据列表
        /// </summary>
        public List<ArticleClassInfo> DataInfoList { get; set; }
        /// <summary>
        /// 分页对象
        /// </summary>
        public PageModel PageModel { get; set; }

        
    }
}