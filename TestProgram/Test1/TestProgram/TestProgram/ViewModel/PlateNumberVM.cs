using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace TestProgram.ViewModel
{
    class PlateNumberVM : ViewModelBase
    {
        private RelayCommand<object> visiblyTest;

        public ICommand VisiblyTest {

            get {
                if (visiblyTest == null)
                {
                    visiblyTest = new RelayCommand<object>((para)=> TestVisibly(para));
                }
                return visiblyTest;
            }
        }
        public void TestVisibly(object obj)
        {
            Messenger.Default.Send<object>(obj,"TestVisible");
        }
    }
}
 