using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace ProxyCommon
{
    public class WrittenItem 
    {
        public decimal Score { get; set; }

        public string Step { set; get; }
        public string Title { get; set; }

        public decimal Min { set; get; }

        public decimal Max { set; get; }

        public string Answer { get; set; }
        public string Describe { set; get; }

        string chooseAnswer;
        public string ChooseAnswer
        {
            get
            {
                return chooseAnswer;
            }
            set
            {
                chooseAnswer = value;
            }
        }
        public int Index { get; set; }
        public bool IsMultiple { get; set; }
        public string Image { get; set; }

        /// <summary>
        /// 0:普通选择题
        /// 1:图片识别题
        /// 2:图片选择题
        /// 3:视频选择题
        /// </summary>
        public string Type { get; set; }

        [XmlElement(ElementName = "Option")]
        public ObservableCollection<Option> Options { get; set; }

        public WrittenItem()
        {
            ChooseAnswer = string.Empty;
        }
    }
}
