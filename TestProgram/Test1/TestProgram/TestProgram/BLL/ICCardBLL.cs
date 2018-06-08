using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestProgram.BLL
{
    public class ICCardBLL : ViewModelBase
    {

        private string examineeID;
        private string name;
        private string sex;
        private string nation;
        private string address;
        private string iDCode;

        private string titleName;

        public string BatchNO { get; set; }

        public string TitleName
        {
            get { return titleName; }
            set
            {
                titleName = value;
                RaisePropertyChanged("TitleName");
            }
        }

        public string ExamineeID
        {
            get { return examineeID; }
            set
            {
                examineeID = value;
                RaisePropertyChanged("ExamineeID");
            }
        }

        /// <summary>
        /// 姓名
        /// </summary>
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                RaisePropertyChanged("Name");
            }
        }

        /// <summary>
        /// 性别
        /// </summary>
        public string Sex
        {
            get { return sex; }
            set
            {
                sex = value;
                RaisePropertyChanged("Sex");
            }
        }

        /// <summary>
        /// 民族，护照识别时此项为空
        /// </summary>
        public string Nation
        {
            get { return nation; }
            set
            {
                nation = value;
                RaisePropertyChanged("Nation");
            }
        }

        /// <summary>
        /// 地址，在识别护照时导出的是国籍简码
        /// </summary>
        public string Address
        {
            get { return address; }
            set
            {
                address = value;
                RaisePropertyChanged("Address");
            }
        }

        /// <summary>
        /// 身份证件号
        /// </summary>
        public string IDCode
        {
            get { return iDCode; }
            set
            {
                iDCode = value;
                RaisePropertyChanged("IDCode");
            }
        }




    }
}
