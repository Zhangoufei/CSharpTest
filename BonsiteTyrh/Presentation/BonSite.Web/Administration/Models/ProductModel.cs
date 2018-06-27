using System;
using System.Data;
using System.Web.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

using BonSite.Core;
using BonSite.Services;
using BonSite.Web.Framework;

namespace BonSite.Web.Admin.Models
{
    public class ProductModel
    {
        /// <summary>
        /// 标题
        /// </summary>
        [DisplayName("标题")]
        [Required(ErrorMessage = "产品不能为空")]
        [StringLength(200, ErrorMessage = "产品标题不能大于200")]
        public string Title { get; set; }

        [DisplayName("分类")]
        public int ProductClassID { get; set; }

        [DisplayName("商品编号")]
        public string Code { get; set; }

        [DisplayName("类型")]
        public string Type { get; set; }

        [DisplayName("供应商")]
        public string Provider { get; set; }

        [DisplayName("审核")]
        public int IsShow { get; set; }

        [DisplayName("置顶")]
        public int IsTop { get; set; }
        
        [DisplayName("推荐")]
        public int IsBest { get; set; }

        [DisplayName("正文")]
        public string Body { get; set; }

        [DisplayName("新加时间")]
        public DateTime AddTime { get; set; }

        [DisplayName("更新时间")]
        public DateTime UpdateTime { get; set; }

        [DisplayName("题图")]
        public string ImgUrl { get; set; }

        [DisplayName("大图")]
        public string BigImgUrl { get; set; }


        [DisplayName("摘要")]
        public string Digest { get; set; }

        [DisplayName("关键字")]
        public string Keys { get; set; }

        [DisplayName("管理员ID")]
        public int AdminID { get; set; }

        [DisplayName("浏览量")]
        public int Hits { get; set; }

        [DisplayName("排序")]
        public int DisplayOrder { get; set; }


        public string Keyword { get; set; }

        public string Description { get; set; }
    }


    public class ProductListModel
    {
        /// <summary>
        /// 分页对象
        /// </summary>
        public PageModel PageModel { get; set; }

        /// <summary>
        /// 数据列表
        /// </summary>
        public DataTable DataList { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public string SortColumn { get; set; }
        /// <summary>
        /// 排序方向
        /// </summary>
        public string SortDirection { get; set; }
        /// <summary>
        /// 新闻类型ID
        /// </summary>
        public int ProductClassID { get; set; }
        /// <summary>
        /// 新闻标题
        /// </summary>
        public string ProductTitle { get; set; }

        /// <summary>
        /// 当前分类
        /// </summary>
        public ArticleClassInfo ClassInfo { get; set; }


    }
}