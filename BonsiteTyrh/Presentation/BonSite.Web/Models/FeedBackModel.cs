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
    public class FeedBackModel
    {
        public int FeedBackTypeId { get; set; }

        public string Tag { get; set; }

        public string LinkMan { get; set; }

        public string Tel { get; set; }

        public string Mobile { get; set; }

        public string Email { get; set; }

        public string Title { get; set; }

        public string Body { get; set; }

        public DateTime AddTime { get; set; }

        public string Reply { get; set; }

        public DateTime ReplyTime { get; set; }

        public int State { get; set; }

        public int IsOut { get; set; }

        public string Ip { get; set; }

        public string SearchKey { get; set; }
    }

    public class FeedBackListModel
    {
        public PageModel PageModel { get; set; }

        public DataTable FeedBackList { get; set; }

        public FeedBackTypeInfo FeedBackTypeInfo { get; set; }

        public FeedBackModel FeedBackModel { get; set; }
    }
}