using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;


using BonSite.Core;
using BonSite.Services;
using BonSite.Web.Framework;
using BonSite.Web.Admin.Models;

namespace BonSite.Web.Admin.Models
{
    public class VoteListModel
    {
        public PageModel PageModel { get; set; }

        public DataTable VoteList { get; set; }
    }

    public class VoteModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public int State { get; set; }

        public int Type { get; set; }

    }

    public class VoteResultListModel {

        public List<VoteResultInfo> ResultsList { get; set; }
    }

    public class VoteResultModel
    {
        public int Id { get; set; }

        public int VoteId { get; set; }

        public int DisplayOrder { get; set; }

        public string Result { get; set; }

        public int Count { get; set; }
    }
}