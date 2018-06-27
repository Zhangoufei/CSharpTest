using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using BonSite.Core;
using BonSite.Web.Framework;

namespace BonSite.Web.Models
{
    public class JobModel
    {
    }


    public class JobListModel
    {
        public PageModel PageModel { get; set; }
        public DataTable JobList { get; set; }
        public string City { get; set; }
        public int ArticleClassID { get; set; }
        public ArticleClassInfo ArticleClassInfo { get; set; }
        public List<ArticleClassInfo> ClassPath { get; set; }

    }
}