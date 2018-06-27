using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BonSite.Core.Domain.Site
{
    /// <summary>
    /// 微信公众号管理
    /// </summary>
    public class WeChatInfo
    {
        private int _id;
        private string _name;
        private string _appid;
        private string _secret;

        //编号
        public int id
        {
            get { return _id; }
            set { _id = value; }
        }
        //名字
        public string name
        {
            get { return _name; }
            set { _name = value; }
        }

        public string appid
        {
            get { return _appid; }
            set { _appid = value; }
        }

        public string secret
        {
            get { return _secret; }
            set { _secret = value; }
        }
    }
}
