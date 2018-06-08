using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;

namespace JAAJ.PEAR.TestModel
{
    public class WrittenItem : ObservableObject
    {
        public decimal Score { get; set; }
        public string Title { get; set; }
        public string Answer { get; set; }

        string chooseAnswer;
        public string ChooseAnswer {
            get {
                return chooseAnswer;
            }
            set {
                chooseAnswer = value;
                RaisePropertyChanged("ChooseAnswer");
            }
        }
        public int Index{get; set;}
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
