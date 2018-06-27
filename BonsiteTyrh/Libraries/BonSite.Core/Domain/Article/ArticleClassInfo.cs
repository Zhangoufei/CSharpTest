using System;

namespace BonSite.Core
{
    public class ArticleClassInfo
    {
        private int _articleclassid;//分类id
        private string _classname = "";//分类名称
        private int _parentarticleclassid = 0;//上级分类id
        private int _classtype = 2;//分类类型：0:链接,1文章单页,2文章列表,3图片列表,4自定义模板
        private int _target = 0;//是否新窗口打开
        private int _isnav = 0;//主导航
        private int _isweb = 0;//前台显示
        private string _weburl = "";//类型为连接时的地址
        private int _isadmin = 1;//是否管理后台显示
        private string _adminurl = "";//后台管理地址
        private int _displayorder = 0;//排序
        private int _isOpen = 0;//允许投稿
        private string _listView = "";//列表模板名称
        private string _contentView = "";//内容模板名称
        private string _code = "";//分类代码
        private string _imgurl = "";//分类图片
        private string _keyword = "";
        private string _description = "";
        private int _isclassbrand = 0;
        private string _subhead = "";
        private string _Auditor="";//操作人
        private int _IsShowNews;//栏目是否显示在首页


        /// <summary>
        /// 分类id
        /// </summary>
        public int ArticleClassID
        {
            set { _articleclassid = value; }
            get { return _articleclassid; }
        }
        /// <summary>
        /// 分类名称
        /// </summary>
        public string ClassName
        {
            set { _classname = value; }
            get { return _classname; }
        }
        /// <summary>
        /// 上级分类id
        /// </summary>
        public int ParentArticleClassID
        {
            set { _parentarticleclassid = value; }
            get { return _parentarticleclassid; }
        }
        /// <summary>
        /// 分类类型：0:链接,1文章单页,2文章列表,3图片列表,4自定义模板,-1管理菜单
        /// </summary>
        public int ClassType
        {
            set { _classtype = value; }
            get { return _classtype; }
        }
        /// <summary>
        /// 是否新窗口打开
        /// </summary>
        public int Target
        {
            set { _target = value; }
            get { return _target; }
        }
        /// <summary>
        /// 主导航
        /// </summary>
        public int IsNav
        {
            set { _isnav = value; }
            get { return _isnav; }
        }
        /// <summary>
        /// 前台显示
        /// </summary>
        public int IsWeb
        {
            set { _isweb = value; }
            get { return _isweb; }
        }
        /// <summary>
        /// 是否显示在首页
        /// </summary>
        public int IsShowNews
        {
            set { _IsShowNews = value; }
            get { return _IsShowNews; }
        }
        /// <summary>
        /// 类型为连接时的地址
        /// </summary>
        public string WebUrl
        {
            set { _weburl = value; }
            get { return _weburl; }
        }
        /// <summary>
        /// 是否管理后台显示
        /// </summary>
        public int IsAdmin
        {
            set { _isadmin = value; }
            get { return _isadmin; }
        }
        /// <summary>
        /// 后台管理地址
        /// </summary>
        public string AdminUrl
        {
            set { _adminurl = value; }
            get { return _adminurl; }
        }
        /// <summary>
        /// 排序
        /// </summary>
        public int DisplayOrder
        {
            set { _displayorder = value; }
            get { return _displayorder; }
        }
        /// <summary>
        /// 允许投稿
        /// </summary>
        public int IsOpen
        {
            set { _isOpen = value; }
            get { return _isOpen; }
        }
        /// <summary>
        /// 列表模板
        /// </summary>
        public string ListView
        {
            set { _listView = value;  }
            get { return _listView; }
        }

        /// <summary>
        /// 内容模板
        /// </summary>
        public string ContentView
        {
            set { _contentView = value; }
            get { return _contentView; }
        }

        /// <summary>
        /// 分类代码
        /// </summary>
        public string Code
        {
            set { _code = value; }
            get { return _code; }
        }

        /// <summary>
        /// 分类图片
        /// </summary>
        public string ImgUrl
        {
            set { _imgurl = value; }
            get { return _imgurl; }
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
        public int IsClassBrand
        {
            set { _isclassbrand = value; }
            get { return _isclassbrand; }
        }
        //副标题
        public string Subhead
        {
            set { _subhead = value;  }
            get { return _subhead;  }
        }
        public string Auditor
        {
            set { _Auditor = value; }
            get { return _Auditor; }
        }
    }
}
