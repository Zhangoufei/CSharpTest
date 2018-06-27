using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BonSite.Core
{
    /// <summary>
    /// 系统配置信息类
    /// </summary>
    [Serializable]
    public class SysConfigInfo : IConfigInfo
    {
        #region 主题设置
        private int _ismobile = 0;//是否支持移动访问
        private string _themename = "Default";//主题名称

        /// <summary>
        /// //电子班牌地址
        /// </summary>
        public string IpCard { set; get; }  

        /// <summary>
        /// 是否启用电子班牌
        /// </summary>
        public bool EnableIpCard { set; get; }

        /// <summary>
        /// 是否支持移动访问
        /// </summary>
        public int IsMobile
        {
            get { return _ismobile; }
            set { _ismobile = value; }
        }

        /// <summary>
        /// 主题名称
        /// </summary>
        public string ThemeName
        {
            get { return _themename; }
            set { _themename = value; }
        }

     

        #endregion
    }
}
