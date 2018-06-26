using System;

namespace CyPhone.Common.UI
{
    public class PagerOptions
    {
        public PagerOptions()
        {
            CurrentPage = 1;
            PageSize = 20;
            MaxSize = 5;
            TotalItem = 0;
        }

        /// <summary>
        /// 当前页
        /// </summary>
        public int CurrentPage { get; set; }

        /// <summary>
        /// 每页条数
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// 总页数
        /// </summary>
        public int PageCount
        {
            get { return (int)Math.Ceiling(Convert.ToDecimal(TotalItem) / Convert.ToDecimal(PageSize)); }
        }

        /// <summary>
        /// 页码显示个数
        /// </summary>
        public int MaxSize { get; set; }

        /// <summary>
        /// 记录总数
        /// </summary>
        public int TotalItem { get; set; }

        /// <summary>
        /// 数据源
        /// </summary>
        public dynamic Data { get; set; }

        /// <summary>
        /// 搜索项
        /// </summary>
        public string SearchText { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// 备用字段
        /// </summary>
        public string StrSpareField { get; set; }

        /// <summary>
        /// 备用字段2
        /// </summary>
        public string StrSpareField2 { get; set; }
    }
}
