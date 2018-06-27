using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using BonSite.Core;

namespace BonSite.Web.Models
{
    public class VoteModel
    {
    }

    /// <summary>
    /// 投票结果详情
    /// </summary>
    public class VoteResultModel
    {

        public int Id { get; set; }

        public string Title { get; set; }

        public int State { get; set; }

        public int Type { get; set; }

        public List<VoteResultInfo> ResultsList { get; set; }
    }

}