using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using TestProgram2.usercontrol;

namespace TestProgram2.ViewModel
{
    class VMKeyboardUser : ViewModelBase
    {
        private RelayCommand<object> submitOperationCommand;
        public ICommand SubmitOperationCommand
        {
            get
            {
                if (submitOperationCommand == null)
                {
                    submitOperationCommand = new RelayCommand<object>(x => Sub(x));
                }
                return submitOperationCommand;
            }
        }
        public void Sub(object obj)
        {

        }



        private string tTestComputorValue;

        /// <summary>
        /// 姓名
        /// </summary>
        public string TTestComputorValue
        {
            get { return tTestComputorValue; }
            set
            {
                tTestComputorValue = value;
                RaisePropertyChanged("TTestComputorValue");
            }
        }
    }
}
