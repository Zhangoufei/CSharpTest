using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BonSite.Core
{
    public class AdminMenuInfo
    {
        private int _id;//ID
        private int _articleClassId;//分类ID
        private int _userId;//用户ID

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
        public int ArticleClassID
        {
            get { return _articleClassId; }
            set { _articleClassId = value; }
        }
        /// <summary>
        /// 用户ID
        /// </summary>
        public int UserId
        {
            get { return _userId; }
            set { _userId = value; }
        }

    }
}
