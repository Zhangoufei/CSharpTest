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

namespace TestProgram2.usercontrol
{
    /// <summary>
    /// CalculatorControl.xaml 的交互逻辑
    /// </summary>
    public partial class CalculatorControl : UserControl
    {
        public CalculatorControl()
        {
            InitializeComponent();

            //cbUnit.Items.Add("G");
            //cbUnit.Items.Add("KG");
            //cbUnit.Items.Add("T");
        }

        ////添加text依赖属性
        //public static readonly DependencyProperty TextProperty =
        //    DependencyProperty.Register("Text", typeof(string)
        //        , typeof(userTextBox)
        //        , new PropertyMetadata("TextBoxValue1", new PropertyChangedCallback(OnTextChangeed))

        //        );

        //public string TextBoxValue1
        //{
        //    get { return (string)this.GetValue(TextProperty); }
        //    set { this.SetValue(TextProperty, value); }
        //}
        //static void OnTextChangeed(object sender, DependencyPropertyChangedEventArgs args)
        //{
        //    userTextBox source = (userTextBox)sender;
        //    source.textBox.Text = (string)args.NewValue;
        //}


        private void tbxwrite_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            try
            {
                //屏蔽中文输入和粘贴输入
                TextChange[] change = new TextChange[e.Changes.Count];
                e.Changes.CopyTo(change, 0);
                int offset = change[0].Offset;
                if (change[0].AddedLength > 0)
                {
                    double num = 0;
                    if (!Double.TryParse(textBox.Text, out num) && !textBox.Text.ToString().Contains("A") && !textBox.Text.ToString().Contains("C") && !textBox.Text.ToString().Contains("D"))
                    {
                        //textBox.Text = textBox.Text.Remove(offset, change[0].AddedLength);
                        textBox.Text = textBox.Text.Remove(change[0].RemovedLength, 1);
                        textBox.Select(offset, 0);
                    }
                }
            }
            catch (Exception ex)
            {
                // CommonHelper.Log("tbxwrite_TextChanged_2:" + ex.Message + "," + DateTime.Now);
                textBox.Text = "0";
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
        /// <summary>
        /// 确定按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void keyOk_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.Calculator.Visibility = Visibility.Hidden;
            }
            catch (Exception ex)
            {
            }
        }
        private void Calculator_LostFocus(object sender, RoutedEventArgs e)
        {
            this.Calculator.Visibility = Visibility.Hidden;
        }
        private void Calculator_GotFocus(object sender, RoutedEventArgs e)
        {
            this.Calculator.Visibility = Visibility.Visible;
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
                TextBox textValue = tbxwrite;
                Button bt = e.OriginalSource as Button;
                if (bt != null)
                {
                    string intext = bt.Content.ToString();
                    if (intext == "退格")
                    {
                        if (!string.IsNullOrEmpty(textValue.Text))
                        {
                            selectPos = textValue.SelectionStart;
                            if (selectPos != 0)
                            {
                                textValue.Text = textValue.Text.Substring(0, selectPos - 1) +
                                                textValue.Text.Substring(selectPos);

                                selectPos -= 1;
                                textValue.Focus();
                                textValue.Select(selectPos, 0);
                            }
                        }
                    }
                    else if (intext == "清除")
                    {
                        textValue.Clear();
                    }
                    else if (intext == "确定")
                    {
                        //不处理
                        //   WriteState = true;  //如果点击确认，说明确定输入，修复文本显示缺陷11.6
                    }
                    else
                    {
                        selectPos = textValue.SelectionStart;
                        if (textValue.Text.Length < TextMaxLenth)
                        {
                            if (intext == "AC" || intext == "DC")
                            {
                                if ((textValue.Text.ToString().Contains("A") || textValue.Text.ToString().Contains("C") || textValue.Text.ToString().Contains("D")))
                                {
                                    intext = "";
                                }
                            }
                            if (intext == ".")
                            {
                                if ((textValue.Text.ToString().Contains(".")))
                                {
                                    intext = "";
                                }
                            }
                            textValue.Text = textValue.Text.Substring(0, selectPos) + intext +
                                            textValue.Text.Substring(selectPos);
                            if (intext == "AC" || intext == "DC")
                            {
                                selectPos += 2;
                            }
                            else
                            {
                                selectPos += 1;
                            }
                            textValue.Focus();
                        }
                        textValue.Select(selectPos, 0);
                    }
                }
            }
            catch (Exception ex)
            {
                // CommonHelper.Log("LayoutRoot_Click:" + ex.Message + "," + DateTime.Now);
            }
        }
        ///// <summary>
        ///// 单位下拉框
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void cbUnit_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    ComboBox ss = sender as ComboBox;
        //    if (cbUnit.SelectedValue != null)
        //    {
             
        //    }
        //}
    }
}
