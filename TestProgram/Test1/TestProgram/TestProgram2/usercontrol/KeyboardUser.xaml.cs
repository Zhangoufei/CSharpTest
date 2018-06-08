using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TestProgram2.ViewModel;

namespace TestProgram2.usercontrol
{
    /// <summary>
    /// Keyboard.xaml 的交互逻辑
    /// </summary>
    public partial class KeyboardUser : UserControl
    {
        public KeyboardUser()
        {
            InitializeComponent();
            DataContext = new VMKeyboardUser();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            int temp = 0;
            int sum = 8;
            double mm = 0;
            Thread thread = new Thread(()=> {
                mm = sum / temp;
            });
            thread.Start();
           // MessageBox.Show(textBoxTest.textBox.Text + "  Counter.tbxwrite.Text;=" + Counter.tbxwrite.Text);
        }

        private void maitest()
        {

        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            Window1 win = new Window1();
            win.Show();
        }
    }
}
