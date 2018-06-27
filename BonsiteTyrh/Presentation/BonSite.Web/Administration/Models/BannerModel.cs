using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;

using BonSite.Core;
using BonSite.Services;
using BonSite.Web.Framework;

namespace BonSite.Web.Admin.Models
{

    public class BannerPositionListModel
    {
        public PageModel PageModel { get; set; }
        public List<BannerPositionInfo> BannerPositionList { get; set; }
    }

    public class BannerPositionModel
    {

        /// <summary>
        /// 标题
        /// </summary>
        [Required(ErrorMessage = "标题不能为空")]
        [StringLength(25, ErrorMessage = "标题长度不能大于25")]
        public string Title { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        [StringLength(75, ErrorMessage = "描述长度不能大于75")]
        public string Description { get; set; }
    }


    public class BannerModel
    {

        public BannerModel()
        {
            StartTime = EndTime = DateTime.Now;
            
        }

        public int BanPosId { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        [Required(ErrorMessage = "开始时间不能为空")]
        public DateTime StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        [Required(ErrorMessage = "结束时间不能为空")]
        [DateTimeNotLess("StartTime", "开始时间")]
        [DisplayName("结束时间")]
        public DateTime EndTime { get; set; }

        /// <summary>
        /// 是否显示
        /// </summary>
        public int IsShow { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        [StringLength(50, ErrorMessage = "标题长度不能大于50")]
        public string BannerTitle { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        [Required(ErrorMessage = "地址不能为空")]
        [StringLength(125, ErrorMessage = "地址长度不能大于125")]
        [Url]
        public string Url { get; set; }

        /// <summary>
        /// 图片
        /// </summary>
        [Required(ErrorMessage = "图片不能为空")]
        [StringLength(125, ErrorMessage = "图片长度不能大于125")]
        public string Img { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        [Required(ErrorMessage = "排序不能为空")]
        [DisplayName("排序")]
        public int DisplayOrder { get; set; }
    }

    /// <summary>
    /// banner列表模型类
    /// </summary>
    public class BannerListModel
    {
        public PageModel PageModel { get; set; }
        public DataTable BannerList { get; set; }
        public int BanPosId { get; set; }
    }
}