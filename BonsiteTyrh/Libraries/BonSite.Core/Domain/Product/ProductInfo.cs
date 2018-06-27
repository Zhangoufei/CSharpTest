using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BonSite.Core
{
    public class ProductInfo
    {
        private int _productid;//商品id
        private int _productclassid = 0;//商品分类ID
        private string _code = "";//商品编号
        private string _type = "";//商品类型
        private string _provider = "";//提供商
        private int _isshow = 0;//是否显示
        private int _istop = 0;//置顶
        private int _isbest = 0;//推荐
        private string _title = "";//标题
        private string _body = "";//正文
        private DateTime _addtime = DateTime.Now;//添加时间
        private DateTime _updatetime = DateTime.Now;//更新时间
        private string _imgurl = "";//小图
        private string _bigimgurl = "";//大图
        private string _digest = "";//摘要
        private string _keys = "";//关键字
        private int _adminid = 0;//管理员id
        private int _hits = 0;//浏览量
        private int _displayorder = 0;//排序
        private string _keyword = "";
        private string _description = "";
        /// <summary>
        /// 商品id
        /// </summary>
        public int ProductID
        {
            set { _productid = value; }
            get { return _productid; }
        }
        /// <summary>
        /// 商品分类ID
        /// </summary>
        public int ProductClassID
        {
            set { _productclassid = value; }
            get { return _productclassid; }
        }
        /// <summary>
        /// 商品编号
        /// </summary>
        public string Code
        {
            set { _code = value; }
            get { return _code; }
        }
        /// <summary>
        /// 类型
        /// </summary>
        public string Type
        {
            set { _type = value; }
            get { return _type; }
        }
        /// <summary>
        /// 供应商
        /// </summary>
        public string Provider
        {
            set { _provider = value; }
            get { return _provider; }
        }
        /// <summary>
        /// 是否显示
        /// </summary>
        public int IsShow
        {
            set { _isshow = value; }
            get { return _isshow; }
        }
        /// <summary>
        /// 置顶
        /// </summary>
        public int IsTop
        {
            set { _istop = value; }
            get { return _istop; }
        }
      
        /// <summary>
        /// 推荐
        /// </summary>
        public int IsBest
        {
            set { _isbest = value; }
            get { return _isbest; }
        }
        /// <summary>
        /// 标题
        /// </summary>
        public string Title
        {
            set { _title = value; }
            get { return _title; }
        }
        /// <summary>
        /// 正文
        /// </summary>
        public string Body
        {
            set { _body = value; }
            get { return _body; }
        }
        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime AddTime
        {
            set { _addtime = value; }
            get { return _addtime; }
        }
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateTime
        {
            set { _updatetime = value; }
            get { return _updatetime; }
        }
       
        /// <summary>
        /// 题图
        /// </summary>
        public string ImgUrl
        {
            set { _imgurl = value; }
            get { return _imgurl; }
        }
        /// <summary>
        /// Url
        /// </summary>
        public string BigImgUrl
        {
            set { _bigimgurl = value; }
            get { return _bigimgurl; }
        }
        /// <summary>
        /// 摘要
        /// </summary>
        public string Digest
        {
            set { _digest = value; }
            get { return _digest; }
        }
        /// <summary>
        /// 关键字
        /// </summary>
        public string Keys
        {
            set { _keys = value; }
            get { return _keys; }
        }       
        /// <summary>
        /// 管理员id
        /// </summary>
        public int AdminID
        {
            set { _adminid = value; }
            get { return _adminid; }
        }
        /// <summary>
        /// 浏览量
        /// </summary>
        public int Hits
        {
            set { _hits = value; }
            get { return _hits; }
        }
        /// <summary>
        /// 排序
        /// </summary>
        public int DisplayOrder
        {
            set { _displayorder = value; }
            get { return _displayorder; }
        }

        public string Keyword
        {
            set { _keyword = value; }
            get { return _keyword; }
        }

        public string Description
        {
            set { _description = value; }
            get { return _description; }
        }
    }
}
