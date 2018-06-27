using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BonSite.Core
{
    public class SpecialInfo
    {
        private int _specialid;//专题id
        private string _name = "";//名称
        private string _title = "";//标题
        private string _imgurl = "";//图片
        private string _logourl = "";//Logo
        private int _isout = 0;//是否为外部专题
        private string _outurl = "";//外部专题url
        private string _body = "";//专题介绍
        private int _displayorder = 0;//排序
        /// <summary>
        /// 专题id
        /// </summary>
        public int SpecialID
        {
            set { _specialid = value; }
            get { return _specialid; }
        }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name
        {
            set { _name = value; }
            get { return _name; }
        }
        /// <summary>
        /// 标题
        /// </summary>
        public string Title
        {
            set { _title = value; }
            get { return _title; }
        }
        /// <summary>
        /// 图片
        /// </summary>
        public string ImgUrl
        {
            set { _imgurl = value; }
            get { return _imgurl; }
        }
        /// <summary>
        /// Logo
        /// </summary>
        public string LogoUrl
        {
            set { _logourl = value; }
            get { return _logourl; }
        }
        /// <summary>
        /// 是否为外部专题
        /// </summary>
        public int IsOut
        {
            set { _isout = value; }
            get { return _isout; }
        }
        /// <summary>
        /// 外部专题url
        /// </summary>
        public string OutUrl
        {
            set { _outurl = value; }
            get { return _outurl; }
        }
        /// <summary>
        /// 专题介绍
        /// </summary>
        public string Body
        {
            set { _body = value; }
            get { return _body; }
        }
        /// <summary>
        /// 排序
        /// </summary>
        public int DisplayOrder
        {
            set { _displayorder = value; }
            get { return _displayorder; }
        }
    }
}
