using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using BonSite.Core;
using BonSite.Services;
using BonSite.Web.Framework;

namespace BonSite.Web.Admin.Models
{
    public class SpecialModel
    {
        public int SpecialID { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string ImgUrl { get; set; }
        public string LogoUrl { get; set; }
        public int IsOut { get; set; }
        public string OutUrl { get; set; }
        public string Body { get; set; }
        public int DisplayOrder { get; set; }
    }

    public class SpecialListModel
    {
        public List<SpecialInfo> SpecialList { get; set; }
    }
}