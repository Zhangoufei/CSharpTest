using System;

namespace BonSite.Core
{
    /// <summary>
    ///  导航栏信息类
    /// </summary>
    public class NavInfo
    {
        private int _id;//编号
        private int _pid;//父编号
        private string _name;//名称
        private string _url;//网址
        private int _target;//打开目标
        private int _displayorder;//排序
        private string _weburl;
        private string _code;
        private string _imgurl;

        /// <summary>
        /// 编号
        /// </summary>
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }
        /// <summary>
        /// 父编号
        /// </summary>
        public int Pid
        {
            get { return _pid; }
            set { _pid = value; }
        }
        
        /// <summary>
        /// 名称
        /// </summary>
        public string Name
        {
            get { return _name; }
            set { _name = value.TrimEnd(); }
        }
        
        /// <summary>
        /// 网址
        /// </summary>
        public string Url
        {
            get { return _url; }
            set { _url = value.TrimEnd(); }
        }
        /// <summary>
        /// 打开目标
        /// </summary>
        public int Target
        {
            get { return _target; }
            set { _target = value; }
        }
        /// <summary>
        /// 排序
        /// </summary>
        public int DisplayOrder
        {
            get { return _displayorder; }
            set { _displayorder = value; }
        }

        public string WebUrl
        {
            get { return _weburl; }
            set { _weburl = value; }
        }

        public string Code
        {
            get { return _code; }
            set { _code = value; }
        }

        public string ImgUrl
        {
            get { return _imgurl; }
            set { _imgurl = value; }
        }
    }
}
