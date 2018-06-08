using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestView.ViewModel
{
    public class FormViewModel : ViewModelBase
    {

        public string test;

        public string Test
        {
            get { return test; }
            set
            {
                test = value;
                RaisePropertyChanged("Test");
            }
        }

    }
}
