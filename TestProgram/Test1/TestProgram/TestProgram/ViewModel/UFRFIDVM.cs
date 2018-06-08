using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using HardWare.CardReader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestProgram.ViewModel
{
    class UFRFIDVM : ViewModelBase
    {
        private string title;
        public string TitleCommond
        {
            get
            {
                return title;
            }
            set
            {
                title = value;
                RaisePropertyChanged("TitleCommond");
            }
        }

        private string testVisiblity;

        public string TestVisiblity
        {
            get
            {
                return testVisiblity;
            }
            set
            {
                testVisiblity = value;
                RaisePropertyChanged("TestVisiblity");
            }
        }

        private string testBingding;

        public string TestBingding
        {
            get
            {
                return testBingding;
            }
            set
            {
                testBingding = value;
                RaisePropertyChanged("TestBingding");
            }
        }

        public UFRFIDVM()
        {
            //初始化RFID串口
            UHFManager.StartDetect();
            UHFManager.LabelDetected += UHFManagerLabelDetected;
            TitleCommond = "RFID";
        }
        private void UHFManagerLabelDetected(IEnumerable<string> labelList)
        {
            if (labelList.Any())
            {
                foreach (var item in labelList)
                {
                    TestBingding = item+"\r\n";
                }
            }
        }

    }
}
