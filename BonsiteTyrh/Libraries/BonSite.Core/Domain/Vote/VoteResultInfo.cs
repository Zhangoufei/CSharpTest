using System;

namespace BonSite.Core
{
    public class VoteResultInfo
    {
        private int _id;
        private int _voteid;
        private int _displayorder;
        private string _result;
        private int _count;

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
        public int VoteId
        {
            get { return _voteid; }
            set { _voteid = value; }
        }
        /// <summary>
        /// 排序ID
        /// </summary>
        public int DisplayOrder
        {
            get { return _displayorder; }
            set { _displayorder = value; }
        }
        /// <summary>
        /// 选项内容
        /// </summary>
        public string Result
        {
            get { return _result; }
            set { _result = value; }
        }
        /// <summary>
        /// 计数
        /// </summary>
        public int Count
        {
            get { return _count; }
            set { _count = value; }        
        }

    }
}
