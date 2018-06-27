
using System;
using System.Data;
using System.Web.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

using BonSite.Core;
using BonSite.Services;
using BonSite.Web.Framework;
using System.Collections.Generic;

namespace BonSite.Web.Admin.Models
{
    public class ArticleModel
    {
        /// <summary>
        /// 标题
        /// </summary>
        [DisplayName("标题")]
        [Required(ErrorMessage="文章不能为空")]
        [StringLength(200,ErrorMessage="文章标题不能大于200")]
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


        public string Keyword { get; set; }

        public string Description { get; set; }

        public string InformType { get; set; }

        public DateTime EndTime { get; set; }

        public string InformGroup { get; set; }
        public int IsClassBrand { get; set; }

        public int PushStatus { get; set; }

        public List<BonSite.Core.ArticleInfo.InformGroupItem> InformGroupList { get; set; }
        public string MicroVideo { get; set; }
        public int ApprovalStatus { get; set; }
        public int Praise { get; set; }
        public string Auditor { get; set; }
    }

    public class ArticleListModel
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
        public int ArticleClassID { get; set; }
        /// <summary>
        /// 新闻标题
        /// </summary>
        public string ArticleTitle { get; set; }

        /// <summary>
        /// 当前分类
        /// </summary>
        public ArticleClassInfo ClassInfo { get; set; }

        /// <summary>
        /// 数据列表
        /// </summary>
        public List<ArticleInfo> DataInfoList { get; set; }

       
    }


    public class Tag
    {
        public int id { get; set; }
        public string name { get; set; }
        public int count { get; set; }

    }


}