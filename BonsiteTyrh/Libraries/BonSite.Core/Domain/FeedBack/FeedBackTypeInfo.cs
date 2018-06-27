using System;

namespace BonSite.Core
{
    public class FeedBackTypeInfo
    {
        private int _feedbacktypeid;
        private string _typename;
        private int _isshowlist;
        private string _body;
        private string _tags;

        /// <summary>
        /// 反馈ID
        /// </summary>
        public int FeedBackTypeId
        {
            get { return _feedbacktypeid; }
            set { _feedbacktypeid = value; }
        }
        /// <summary>
        /// 反馈名称
        /// </summary>
        public string TypeName
        {
            get { return _typename; }
            set { _typename = value; }
        }
        /// <summary>
        /// 是否显示回复列表(0:不显示,1:显示)
        /// </summary>
        public int IsShowList
        {
            get { return _isshowlist; }
            set { _isshowlist = value; }
        }
        /// <summary>
        /// 说明
        /// </summary>
        public string Body
        {
            get { return _body; }
            set { _body = value; }
        }

        /// <summary>
        /// 小分类，各个中间以半角逗号分割
        /// </summary>
        public string Tags
        {
            get { return _tags; }
            set { _tags = value; }
        }
    }
}
