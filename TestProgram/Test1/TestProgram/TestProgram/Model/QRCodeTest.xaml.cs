using Common;
using JAAJ.DAL;
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
using System.Windows.Threading;

namespace TestProgram.Model
{
    /// <summary>
    /// QRCodeTest.xaml 的交互逻辑
    /// </summary>
    public partial class QRCodeTest : UserControl
    {
        public QRCodeTest()
        {
            InitializeComponent();

            DispatcherTimer timer = new DispatcherTimer();
            timer.Tick +=getFose;
            timer.Interval = TimeSpan.FromSeconds(2);
            timer.Start();
        }
        private void getFose(object sender, EventArgs e)
        {
            textBox2.Focus();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            string text1 = textBox1.Text.Trim();

            string key = "jieankeji";

            string temp = "919E2490C18FB58530A9D8248D9E93542287114CBFAC04E4";
            
            //加密
            textBox2.Text = CommonHelper.Encrypt(text1);

        }

        private void textBox2_TextChanged(object sender, TextChangedEventArgs e)
        {
           if (textBox2.Text.Length == 18)
            {
                textBox2.IsReadOnly = true;
                textBox.Text += "你好啊"+ textBox2.Text+"\r\n";
                textBox2.Text = "";
                textBox2.IsReadOnly = false;
            }
        }

        private void textBlock_TextInput(object sender, TextCompositionEventArgs e)
        {

        }
    }
}
