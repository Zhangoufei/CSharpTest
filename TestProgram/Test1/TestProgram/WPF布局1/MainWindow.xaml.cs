using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WPF布局1.Pannel;
using WPF布局1.布局测试;

namespace WPF布局1
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
           

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Content = new Page1();

            //Content = new Page2();

            //Content = new UniformGrid布局();
            // Content = new 布局综合使用1();

            //  Content = new GridTest1();
            //  Content = new StackPanelTest1();
            Content = new TestVideo();


            //StudentTest test = new StudentTest();
            //test.Show();

            SliderTest1 test2= new SliderTest1();
           // test2.Show();

            StudentTest2 test3 = new StudentTest2();
           // test3.Show();
        }
    }
}
