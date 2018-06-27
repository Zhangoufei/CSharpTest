using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BonSite.Web.Models
{
    public class userResult
    {
        /// <summary>
        /// 执行状态
        /// </summary>
        public string retCode { get; set; }

        /// <summary>
        /// 执行结果信息
        /// </summary>
        public string retMsg { get; set; }

        ///// <summary>
        ///// 失效时间
        ///// </summary>
        //public string retData { get; set; }
        public u_users retData { get; set; }
    }
}