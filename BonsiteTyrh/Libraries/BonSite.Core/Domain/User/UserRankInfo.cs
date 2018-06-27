using System;

namespace BonSite.Core
{
    /// <summary>
    /// 用户等级信息类
    /// </summary>
    public class UserRankInfo
    {
        private int _userrankid;//用户等级id
        private int _issystem;//是否是系统等级
        private string _rankname;//用户等级标题
        private string _avatar;//用户等级头像
        private int _limitdays;//限制天数

        ///<summary>
        ///用户等级id
        ///</summary>
        public int UserRankID
        {
            get { return _userrankid; }
            set { _userrankid = value; }
        }
        ///<summary>
        ///是否是系统等级
        ///</summary>
        public int IsSystem
        {
            get { return _issystem; }
            set { _issystem = value; }
        }
        ///<summary>
        ///用户等级标题
        ///</summary>
        public string RankName
        {
            get { return _rankname; }
            set { _rankname = value.TrimEnd(); }
        }
        /// <summary>
        /// 用户等级头像
        /// </summary>
        public string Avatar
        {
            get { return _avatar; }
            set { _avatar = value.TrimEnd(); }
        }
        
        /// <summary>
        /// 限制天数
        /// </summary>
        public int LimitDays
        {
            get { return _limitdays; }
            set { _limitdays = value; }
        }
    }
}
