using BonSite.Core.Domain.Site;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BonSite.Web.Admin.Models
{
    public class ClassManageModel
    {
        public int ClassID { get; set; }
        public string ClassName { get; set; }
    }
    public class ClassManageListModel
    {
        public ClassManageInfo[] ClassManageList { get; set; }
    }
}