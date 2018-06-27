using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


using BonSite.Core;
using BonSite.Services;
using BonSite.Web.Framework;
using BonSite.Core.Domain.Site;

namespace BonSite.Web.Admin.Models
{
    public class WeChatModel
    {
        public int id { get; set; }
        public string name { get; set; }
        public string appid { get; set; }
        public string secret { get; set; }
        public WeChatInfo[] WeChatList { get; set; }
        
    }
}