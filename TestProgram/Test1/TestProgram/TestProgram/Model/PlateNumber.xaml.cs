using GalaSoft.MvvmLight.Messaging;
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
using TestProgram.ViewModel;

namespace TestProgram.Model
{
    /// <summary>
    /// PlateNumber.xaml 的交互逻辑
    /// </summary>
    public partial class PlateNumber : UserControl
    {
        public PlateNumber()
        {
            InitializeComponent();
            DataContext = new PlateNumberVM();
            wrapPanel1.Visibility = Visibility.Hidden;
            this.Calculator.Visibility = Visibility.Hidden;
            Messenger.Default.Register<object>(this, "TestVisible", TestValue);
        }
        public void TestValue(object obj)
        {
            wrapPanel1.Visibility = Visibility.Visible;
        }


        // Using a DependencyProperty as the backing store for TextMaxLenth.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextMaxLenthProperty =
            DependencyProperty.Register("TextMaxLenth", typeof(int), typeof(MainWindow), new UIPropertyMetadata(10000));
        public int TextMaxLenth
        {
            get { return (int)GetValue(TextMaxLenthProperty); }
            set { SetValue(TextMaxLenthProperty, value); }
        }
        private int selectPos;
        /// <summary>
        /// 点击按钮输入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LayoutRoot_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Button bt = e.OriginalSource as Button;
                if (bt != null)
                {
                    string intext = bt.Content.ToString();
                    if (intext == "退格")
                    {
                        if (!string.IsNullOrEmpty(tbxwrite.Text))
                        {
                            selectPos = this.tbxwrite.SelectionStart;
                            if (selectPos != 0)
                            {
                                tbxwrite.Text = tbxwrite.Text.Substring(0, selectPos - 1) +
                                                tbxwrite.Text.Substring(selectPos);

                                selectPos -= 1;
                                tbxwrite.Focus();
                                tbxwrite.Select(selectPos, 0);
                            }
                        }
                    }
                    else if (intext == "清除")
                    {
                        tbxwrite.Clear();
                    }
                    else if (intext == "确定")
                    {
                        //不处理
                     //   WriteState = true;  //如果点击确认，说明确定输入，修复文本显示缺陷11.6
                    }
                    else
                    {
                        selectPos = this.tbxwrite.SelectionStart;
                        if (tbxwrite.Text.Length < TextMaxLenth)
                        {
                            if (intext == "AC" || intext == "DC")
                            {
                                if ((tbxwrite.Text.ToString().Contains("A") || tbxwrite.Text.ToString().Contains("C") || tbxwrite.Text.ToString().Contains("D")))
                                {
                                    intext = "";
                                }
                            }
                            if (intext == ".")
                            {
                                if ((tbxwrite.Text.ToString().Contains(".")))
                                {
                                    intext = "";
                                }
                            }
                            tbxwrite.Text = tbxwrite.Text.Substring(0, selectPos) + intext +
                                            tbxwrite.Text.Substring(selectPos);
                            if (intext == "AC" || intext == "DC")
                            {
                                selectPos += 2;
                            }
                            else
                            {
                                selectPos += 1;
                            }
                            tbxwrite.Focus();
                        }
                        tbxwrite.Select(selectPos, 0);
                    }
                }
            }
            catch (Exception ex)
            {
              //  CommonHelper.Log("LayoutRoot_Click:" + ex.Message + "," + DateTime.Now);
            }
        }

        private void tbxwrite_GotFocus(object sender, RoutedEventArgs e)
        {
            this.Calculator.Visibility = Visibility.Visible;
        }
        private void tbxwrite_LostFocus(object sender, RoutedEventArgs e)
        {
            this.Calculator.Visibility = Visibility.Hidden;
        }
    }
}
