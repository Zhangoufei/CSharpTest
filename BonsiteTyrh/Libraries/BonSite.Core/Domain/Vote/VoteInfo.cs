using System;
using System.Collections.Generic;


namespace BonSite.Core
{
    public class VoteInfo
    {
        private int _id;
        private string _title;
        private DateTime _startTime;
        private DateTime _endTime;
        private int _state;
        private int _type;

        /// <summary>
        /// ID
        /// </summary>
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }
        /// <summary>
        /// 标题
        /// </summary>
        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartTime
        {
            get { return _startTime; }
            set { _startTime = value; }
        }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndTime
        {
            get { return _endTime; }
            set { _endTime = value; }
        }
        /// <summary>
        /// 状态(0:关闭,1:开启)
        /// </summary>
        public int State
        {
            get { return _state; }
            set { _state = value; }
        }
        /// <summary>
        /// 类型(0:单选,1:多选)
        /// </summary>
        public int Type
        {
            get { return _type; }
            set { _type = value; }
        }

        public List<VoteResultInfo> ResultList
        {
            get;
            set;
        }
    }
}
