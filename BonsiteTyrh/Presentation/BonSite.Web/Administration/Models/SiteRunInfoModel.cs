using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BonSite.Web.Admin.Models
{
    public class SiteRunInfoModel
    {
        

        /// <summary>
        /// 在线用户数量
        /// </summary>
        public int OnlineUserCount { get; set; }
        /// <summary>
        /// 在线游客数量
        /// </summary>
        public int OnlineGuestCount { get; set; }
        /// <summary>
        /// 在线会员数量
        /// </summary>
        public int OnlineMemberCount { get; set; }

        /// <summary>
        /// 商城版本
        /// </summary>
        public string SiteVersion { get; set; }
        /// <summary>
        /// .net版本
        /// </summary>
        public string NetVersion { get; set; }
        /// <summary>
        /// 操作系统版本
        /// </summary>
        public string OSVersion { get; set; }
        /// <summary>
        /// 服务器运行时间
        /// </summary>
        public string TickCount { get; set; }
        /// <summary>
        /// CPU数量
        /// </summary>
        public string ProcessorCount { get; set; }
        /// <summary>
        /// 内存数
        /// </summary>
        public string WorkingSet { get; set; }

        /// <summary>
        /// 缓存数量
        /// </summary>
        public int WorkingTick { get; set; }
    }
}