using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using GalaSoft.MvvmLight.Messaging;
using TestProgram2.ViewModel;

namespace TestProgram2.usercontrol2
{
    /// <summary>
    /// RegisterView.xaml 的交互逻辑
    /// </summary>
    public partial class RegisterView : UserControl
    {
        public RegisterView()
        {
            InitializeComponent();
            DataContext = new RegisterViewVm();

            Messenger.Default.Register<string>(this, "GetRegister1", GetRegister1);
        }

        private void GetRegister1(string temp)
        {
            MessageBox.Show("注册" + temp);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
           
            Messenger.Default.Register<string>(this, "GetRegister2", GetRegister2);
        }
        private void GetRegister2(string temp)
        {
            MessageBox.Show("注册2" + temp);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Messenger.Reset();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Messenger.Default.Register<string>(this, "GetRegister1", GetRegister1);
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
        }
    }
}
