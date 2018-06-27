using System;

namespace BonSite.Core
{
    /// <summary>
    /// 文章类
    /// </summary>
    public class ArticleInfo
    {
        private int _articleid;//文章id
        private int _articleclassid = 0;//文章分类ID
        private int _specialid = 0;//所属专题
        private int _displaytype = 0;//显示方式0：文章，1连接
        private int _isshow = 0;//是否显示
        private int _istop = 0;//置顶
        private int _ishome = 0;//首页推荐
        private int _isbest = 0;//推荐
        private string _title = "";//标题
        private string _body = "";//正文
        private DateTime _addtime = DateTime.Now;//添加时间
        private DateTime _updatetime = DateTime.Now;//更新时间
        private string _author = "";//作者
        private string _comeform = "";//来自
        private string _imgurl = "";//题图
        private string _url = "";//Url
        private string _digest = "";//摘要
        private string _keys = "";//关键字
        private int _userid = 0;//用户id
        private int _adminid = 0;//管理员id
        private int _hits = 0;//浏览量
        private string _keyword = "";
        private string _description = "";
        private string _informType = "";//发布类型
        private DateTime _endTime = DateTime.Now;//通知结束时间
        private string _informGroup = "";//通知分组
        private int _isclassbrand = 0;//班牌新闻
        private int _pushstatus = 0;//推送状态
        private string _microVideo = "";//微视频
        private int _ApprovalStatus = 0;//文章审核状态
        private int _Praise = 0;//点赞数
        private string _Auditor = "";//审核人
        /// <summary>
        /// 文章id
        /// </summary>
        public int ArticleID
        {
            set { _articleid = value; }
            get { return _articleid; }
        }
        /// <summary>
        /// 文章分类ID
        /// </summary>
        public int ArticleClassID
        {
            set { _articleclassid = value; }
            get { return _articleclassid; }
        }
        /// <summary>
        /// 所属专题
        /// </summary>
        public int SpecialID
        {
            set { _specialid = value; }
            get { return _specialid; }
        }
        /// <summary>
        /// 显示方式0：文章，1连接，2单页模板
        /// </summary>
        public int DisplayType
        {
            set { _displaytype = value; }
            get { return _displaytype; }
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
        /// 首页推荐
        /// </summary>
        public int IsHome
        {
            set { _ishome = value; }
            get { return _ishome; }
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
        /// 作者
        /// </summary>
        public string Author
        {
            set { _author = value; }
            get { return _author; }
        }
        /// <summary>
        /// 来自
        /// </summary>
        public string ComeForm
        {
            set { _comeform = value; }
            get { return _comeform; }
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
        public string Url
        {
            set { _url = value; }
            get { return _url; }
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
        /// 用户id
        /// </summary>
        public int UserID
        {
            set { _userid = value; }
            get { return _userid; }
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
        //发布类型
        public string InformType
        {
            set { _informType = value; }
            get { return _informType; }
        }
        //通知结束时间
        public DateTime EndTime
        {
            set { _endTime = value; }
            get { return _endTime; }
        }


        //通知分组
        public string InformGroup
        {
            set { _informGroup = value; }
            get { return _informGroup; }
        }

        public int IsClassBrand
        {
            set { _isclassbrand = value; }
            get { return _isclassbrand; }
        }
        public int PushStatus
        {
            set { _pushstatus = value; }
            get { return _pushstatus; }
        }
        public class InformGroupItem
        {
            public bool isSel { get; set; }
            public int GroupID { get; set; }
            public string GroupName { get; set; }
        }
        //微视频
        public string MicroVideo
        {
            set { _microVideo = value; }
            get { return _microVideo; }
        }
        public int ApprovalStatus
        {
            set { _ApprovalStatus = value; }
            get { return _ApprovalStatus; }
        }
        public int Praise
        {
            set { _Praise = value; }
            get { return _Praise; }
        }
        public string Auditor
        {
            set { _Auditor = value; }
            get { return _Auditor; }
        }

    }
}
