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
using System.Windows.Shapes;
using Test001.Common;

namespace Test001
{
    /// <summary>
    /// Messenger使用.xaml 的交互逻辑
    /// </summary>
    public partial class Messenger使用 : Page
    {
        public Messenger使用()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void button1_Copy_Click(object sender, RoutedEventArgs e)
        {
            Score socre = new Score();
            socre.Scores = "100";
            socre.SecondTitile = "考试成绩1";
            socre.Titile = "张三 考试成绩";
            XmlProcess.SerializeScoreFieldsSetting(socre);
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
