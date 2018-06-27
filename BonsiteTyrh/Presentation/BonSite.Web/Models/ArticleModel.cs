using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

using BonSite.Core;
using BonSite.Services;
using BonSite.Web.Framework;

namespace BonSite.Web.Models
{
    /// <summary>
    /// 文章模型
    /// </summary>
    public class ArticleModel
    {
        public ArticleInfo ArticleInfo { get; set; }
        public ArticleClassInfo ArticleClassInfo { get; set; }
        public List<ArticleClassInfo> ClssPath { get; set; }
    }

    public class SearchListModel
    {
        public string key { get; set; }
        public PageModel PageModel { get; set; }
        public DataTable ArticleList { get; set; }
    }

    /// <summary>
    /// 专题列表
    /// </summary>
    public class SpecialListModel
    {
        public PageModel PageModel { get; set; }
        public DataTable ArticleList { get; set; }
        public int SpecialId { get; set; }
        public SpecialInfo SpecialInfo { get; set; }
        
    }

    /// <summary>
    /// 列表Model
    /// </summary>
    public class ArticleListModel
    {
        public PageModel PageModel { get; set; }
        public DataTable ArticleList { get; set; }
        public int ArticleClassID { get; set; }
        public ArticleClassInfo ArticleClassInfo { get; set; }
        public List<ArticleClassInfo> ClssPath { get; set; }
    }

    /// <summary>
    /// 投稿Model
    /// </summary>
    public class DeliverModel
    {
        /// <summary>
        /// 标题
        /// </summary>
        [DisplayName("标题")]
        [Required(ErrorMessage = "文章不能为空")]
        [StringLength(200, ErrorMessage = "文章标题不能大于200")]
        public string Title { get; set; }

        [DisplayName("分类")]
        public int ArticleClassID { get; set; }

        [DisplayName("所属专题")]
        public int SpecialID { get; set; }

        [DisplayName("显示方式")]
        public int DisplayType { get; set; }

        [DisplayName("审核")]
        public int IsShow { get; set; }

        [DisplayName("置顶")]
        public int IsTop { get; set; }

        [DisplayName("推荐首页")]
        public int IsHome { get; set; }

        [DisplayName("推荐")]
        public int IsBest { get; set; }

        [DisplayName("正文")]
        public string Body { get; set; }

        [DisplayName("新加时间")]
        public DateTime AddTime { get; set; }

        [DisplayName("更新时间")]
        public DateTime UpdateTime { get; set; }

        [DisplayName("作者")]
        public string Author { get; set; }

        [DisplayName("来源")]
        public string ComeForm { get; set; }

        [DisplayName("题图")]
        public string ImgUrl { get; set; }

        /// <summary>
        /// 外部地址
        /// </summary>
        [DisplayName("外部地址")]
        public string Url { get; set; }

        [DisplayName("摘要")]
        public string Digest { get; set; }

        [DisplayName("关键字")]
        public string Keys { get; set; }

        [DisplayName("用户ID")]
        public int UserID { get; set; }

        [DisplayName("管理员ID")]
        public int AdminID { get; set; }

        [DisplayName("浏览量")]
        public int Hits { get; set; }

    }


}