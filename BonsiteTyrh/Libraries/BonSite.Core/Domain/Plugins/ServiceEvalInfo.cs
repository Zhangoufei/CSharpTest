using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BonSite.Core
{
    public class ServiceEvalInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Contact { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public string Courier { get; set; }
        public int EvalProduct { get; set; }
        public int EvalLogistics { get; set; }
        public int EvalService { get; set; }
        public string Body { get; set; }
        public DateTime CreateTime { get; set; }
        public int State { get; set; }
        public string WeChatName { get; set; }
        public string WeChatOpenId { get; set; }
    }
}
