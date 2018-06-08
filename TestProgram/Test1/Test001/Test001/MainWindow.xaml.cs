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
using System.Windows.Threading;
using Welding.MyPage;

namespace Test001
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Window1 wind = new Window1();
            Content = wind;

            播放视频 mm = new 播放视频();
           // Content = mm;

            Messenger使用 mss = new Messenger使用();
          //  Content = mss;

        }

        private Window1 winTrans { get; set; }
        private void button_Click(object sender, RoutedEventArgs e)
        {
            Content = winTrans;

            Test2 test = new Test2
            {
                Id = "2",
                Age = 2
            };
            MessageBox.Show(test.Id);

            Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() => showMessage()));

            while (true)
            {
                int i = 10;
                if (i == 10)
                {
                    MessageBox.Show("HelloWorld");
                    break;
                }
            }

        }


        private void showMessage()
        {
            MessageBox.Show("hello world 112");   
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
          //  contentControl1.Content = new UserControl();
          
        }
    }
    class Test2
    {

        private string id;
        private int age;

        public string Id
        {
            get
            {
                return id;
            }

            set
            {
                id = value;
            }
        }

        public int Age
        {
            get
            {
                return age;
            }

            set
            {
                age = value;
            }
        }
    }
}
