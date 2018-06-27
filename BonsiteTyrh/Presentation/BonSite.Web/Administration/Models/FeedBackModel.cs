using System;
using System.Data;
using System.ComponentModel;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using BonSite.Core;
using BonSite.Services;
using BonSite.Web.Framework;

namespace BonSite.Web.Admin.Models
{

    public class FeedBackTypeListModel 
    {
        //public PageModel PageModel { get; set; }

        public List<FeedBackTypeInfo> FeedBackTypeList { get; set; }
    }

    public class FeedBackTypeModel
    {
        public int FeedBackTypeId { get; set; }
        public string TypeName { get; set; }
        public int IsShowList { get; set; }
        public string Body { get; set; }
        public string Tags { get; set; }
    }


    public class FeedBackListModel
    {
        public PageModel PageModel { get; set; }

        public int FeedBackTypeId { get; set; }

        public DataTable FeedBackList { get; set; }
    }

    public class FeedBackModel 
    {
        public int Id { get; set; }

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
}