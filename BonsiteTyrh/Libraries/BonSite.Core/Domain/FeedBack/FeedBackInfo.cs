using System;

namespace BonSite.Core
{
    public class FeedBackInfo
    {
        private int _id;
        private int _feedbacktypeid;
        private string _tag;
        private string _linkman;
        private string _tel;
        private string _mobile;
        private string _email;
        private string _title;
        private string _body;
        private DateTime _addtime;
        private string _replay;
        private DateTime _replaytime;
        private int _state;
        private int _isout;
        private string _ip;
        private string _searchkey;

        /// <summary>
        /// ID
        /// </summary>
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }
        /// <summary>
        /// 分类ID
        /// </summary>
        public int FeedBackTypeId
        {
            get { return _feedbacktypeid; }
            set { _feedbacktypeid = value; }
        }
        /// <summary>
        /// 小类型
        /// </summary>
        public string Tag
        {
            get { return _tag; }
            set { _tag = value; }
        }
        /// <summary>
        /// 联系人
        /// </summary>
        public string LinkMan
        {
            get { return _linkman; }
            set { _linkman = value; }
        }
        /// <summary>
        /// 电话
        /// </summary>
        public string Tel
        {
            get { return _tel; }
            set { _tel = value; }
        }
        /// <summary>
        /// 手机
        /// </summary>
        public string Mobile
        {
            get { return _mobile; }
            set { _mobile = value; }
        }
        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }
        /// <summary>
        /// 主题
        /// </summary>
        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }
        /// <summary>
        /// 正文
        /// </summary>
        public string Body
        {
            get { return _body; }
            set { _body = value; }
        }
        /// <summary>
        /// 反馈时间
        /// </summary>
        public DateTime AddTime
        {
            get { return _addtime; }
            set { _addtime = value; }
        }
        /// <summary>
        /// 回复
        /// </summary>
        public string Reply
        {
            get { return _replay; }
            set { _replay = value; }
        }
        /// <summary>
        /// 回复时间
        /// </summary>
        public DateTime ReplyTime
        {
            get { return _replaytime; }
            set { _replaytime = value; }
        }
        /// <summary>
        /// 状态（0：未回复,1:已回复）
        /// </summary>
        public int State
        {
            get { return _state; }
            set { _state = value; }
        }
        /// <summary>
        /// 是否外部显示(0:不显示,1:显示)
        /// </summary>
        public int IsOut
        {
            get { return _isout; }
            set { _isout = value; }
        }
        /// <summary>
        /// IP
        /// </summary>
        public string Ip
        {
            get { return _ip; }
            set { _ip = value; }
        }
        /// <summary>
        /// 查询码
        /// </summary>
        public string SearchKey
        {
            get { return _searchkey; }
            set { _searchkey = value; }
        }


    }
}
