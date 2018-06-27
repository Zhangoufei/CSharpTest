using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using BonSite.Web.Framework;

namespace BonSite.Web.Admin.Models
{
    public class ServiceEvalModel
    {
    }

    public class ServiceEvalListModel
    {
        public PageModel PageModel { get; set; }

        public DataTable DataList { get; set; }

    }
}