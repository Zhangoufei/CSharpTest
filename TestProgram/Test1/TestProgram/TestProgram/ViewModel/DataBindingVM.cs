using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace TestProgram.ViewModel
{
    class DataBindingVM : ViewModelBase
    {
        /// <summary>
        /// 绑定txt文本框值
        /// </summary>
        private string txtValue;
        public string TxtBinding {

            get { return txtValue; }
            set {

                if (txtValue != null)
                {
                    txtValue = value;
                    RaisePropertyChanged("TxtBinding");
                }
            }
        }
        //绑定button事件
        private ICommand _BtnClick;

    }
}
