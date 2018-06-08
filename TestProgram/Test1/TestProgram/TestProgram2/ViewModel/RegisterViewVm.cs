using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using GalaSoft.MvvmLight.Messaging;

namespace TestProgram2.ViewModel
{
    class RegisterViewVm
    {


        private ICommand btnClick;


        public ICommand BtnClick
        {
            get
            {
                if (btnClick == null)
                {
                    btnClick = new RelayCommand(() =>
                    {
                        Messenger.Default.Send<string>("弹出1", "GetRegister1");
                    });
                }
                return btnClick;
            }
        }
        private ICommand btnClick2;


        public ICommand BtnClick2
        {
            get
            {
                if (btnClick2 == null)
                {
                    btnClick2 = new RelayCommand(() =>
                    {
                        Messenger.Default.Send<string>("弹出2", "GetRegister2");
                    });
                }
                return btnClick2;
            }
        }

    }
}
