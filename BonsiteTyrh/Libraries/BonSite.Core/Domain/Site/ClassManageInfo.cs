using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BonSite.Core.Domain.Site
{
    public class ClassManageInfo
    {
        //班号
        private int _ClassID;
        //班名
        private string _ClassName;
        /// <summary>
        /// 班号
        /// </summary>
        public int ClassID
        {
            get { return _ClassID; }
            set { _ClassID = value; }
        }
        /// <summary>
        ///班名
        /// </summary>
        public string ClassName
        {
            get { return _ClassName; }
            set { _ClassName = value; }
        }
    }
}
