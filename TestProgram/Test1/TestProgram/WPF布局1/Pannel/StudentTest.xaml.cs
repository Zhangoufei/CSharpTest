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
using System.Windows.Shapes;
using WPF布局1.bLL;

namespace WPF布局1.Pannel
{
    /// <summary>
    /// StudentTest.xaml 的交互逻辑
    /// </summary>
    public partial class StudentTest : Window
    {
        public StudentTest()
        {
            InitializeComponent();
            stu = new Student();
            DataContext = stu;
            //Binding bingding = new Binding();
            //bingding.Source = stu;
            //bingding.Path = new PropertyPath("Name");
            //BindingOperations.SetBinding(textBoxName, TextBox.TextProperty, bingding);
        }
        Student stu;
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            stu.Name += "Name";
        }
    }
}
