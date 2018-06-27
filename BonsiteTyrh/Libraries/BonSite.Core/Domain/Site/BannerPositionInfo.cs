using System;

namespace BonSite.Core
{
    public class BannerPositionInfo
    {
        private int _banposid;
        private string _title;
        private string _description;

        /// <summary>
        /// 分类ID
        /// </summary>
        public int BanPosId
        {
            get { return _banposid; }
            set { _banposid = value; }
        }

        /// <summary>
        /// 名称
        /// </summary>
        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }

        /// <summary>
        /// 说明
        /// </summary>
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }
    }
}
