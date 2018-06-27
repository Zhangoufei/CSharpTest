using System;
using System.Web;
using System.Data;
using System.Drawing;
using System.Web.Mvc;
using System.Drawing.Text;
using System.ComponentModel;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using BonSite.Core;

namespace BonSite.Web.Admin.Models
{
    /// <summary>
    /// 站点模型类
    /// </summary>
    public class SetModel
    {
        public string SiteName { get; set; }
        [AllowHtml]
        public string SiteUrl { get; set; }
        public string SiteTitle { get; set; }
        public string SEOKeyword { get; set; }
        public string SEODescription { get; set; }
        public string ICP { get; set; }
        public string Script { get; set; }
        public int IsLicensed { get; set; }
        public int ShowSEO { get; set; }
    }

    public class UploadModel
    {
        [Required(ErrorMessage = "图片类型不能为空")]
        public string UploadImgType { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "图片大小必须大于0")]
        [DisplayName("图片大小")]
        public int UploadImgSize { get; set; }

        [Required(ErrorMessage = "附件类型不能为空")]
        public string UploadAttType { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "附件大小必须大于0")]
        [DisplayName("附件大小")]
        public int UploadAttSize { get; set; }


        [Range(0, 2, ErrorMessage = "请选择正确的水印类型")]
        [DisplayName("水印类型")]
        public int WatermarkType { get; set; }

        [Range(0, 100, ErrorMessage = "水印质量只能位于0到100")]
        [DisplayName("水印质量")]
        public int WatermarkQuality { get; set; }

        [Range(1, 9, ErrorMessage = "请选择正确的水印位置")]
        [DisplayName("水印位置")]
        public int WatermarkPosition { get; set; }

        public string WatermarkImg { get; set; }

        [Range(1, 10, ErrorMessage = "水印图片透明度必须位于1到10之间")]
        [DisplayName("水印图片透明度")]
        public int WatermarkImgOpacity { get; set; }

        public string WatermarkText { get; set; }

        public string WatermarkTextFont { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "水印文字大小必须大于0")]
        [DisplayName("水印文字大小")]
        public int WatermarkTextSize { get; set; }

        /// <summary>
        /// 题图缩略图大小
        /// </summary>
        [Required(ErrorMessage = "请输入内容")]
        public string ArticleImgThumbSize { get; set; }

        /// <summary>
        /// 友情链接缩略图大小
        /// </summary>
        [Required(ErrorMessage = "请输入内容")]
        public string FriendLinkThumbSize { get; set; }

        /// <summary>
        /// 栏目管理缩略图大小
        /// </summary>
        [Required(ErrorMessage = "请输入内容")]
        public string ArticleClassThumbSize { get; set; }

        /// <summary>
        /// 专题缩略图大小
        /// </summary>
        [Required(ErrorMessage = "请输入内容")]
        public string SpecialImgThumbSize { get; set; }

        /// <summary>
        /// 用户头像缩略图大小
        /// </summary>
        [Required(ErrorMessage = "请输入内容")]
        public string UserAvatarThumbSize { get; set; }

        /// <summary>
        /// 用户等级头像缩略图大小
        /// </summary>
        [Required(ErrorMessage = "请输入内容")]
        public string UserRankAvatarThumbSize { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errorList = new List<ValidationResult>();

            if (WatermarkType == 1 && string.IsNullOrWhiteSpace(WatermarkText))
                errorList.Add(new ValidationResult("水印文字不能为空!", new string[] { "WatermarkText" }));
            else if (WatermarkType == 2 && string.IsNullOrWhiteSpace(WatermarkImg))
                errorList.Add(new ValidationResult("水印图片不能为空!", new string[] { "WatermarkImg" }));


            return errorList;
        }
    }
}