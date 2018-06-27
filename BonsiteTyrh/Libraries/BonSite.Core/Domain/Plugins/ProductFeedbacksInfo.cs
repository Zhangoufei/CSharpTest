using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BonSite.Core
{
    public class ProductFeedbacksInfo
    {
        public int Id { get; set; }
        public string CityName { get; set; }
        public string ProductName { get; set; }
        public string ProductModel { get; set; }
        public string CustomerName { get; set; }
        public string Contact { get; set; }
        public string Address { get; set; }
        public string Body { get; set; }
        public int State { get; set; }
        public DateTime CreateTime { get; set; }
        public string WeChatName { get; set; }
        public string WeChatOpenId { get; set; }
        public string Imgs { get; set; }
    }
}
