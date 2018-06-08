using GalaSoft.MvvmLight.Messaging;
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
using TestProgram.BLL.CommonUnity;
using TestProgram.Model;
using 学习测试.GRID__;

namespace TestProgram
{

    /// <summary>
    /// MainView.xaml 的交互逻辑
    /// </summary>
    public partial class MainView : Page
    {

        public MainView()
        {
            InitializeComponent();
            // CommonHelper.mainView.Content = this;
        }
        private void button_Click(object sender, RoutedEventArgs e)
        {
            gridControl.Children.Clear();
            gridControl.Children.Add(new Screen());
        }

        private void button_Copy_Click(object sender, RoutedEventArgs e)
        {
            gridControl.Children.Clear();
            gridControl.Children.Add(new TestView2());
        }

        private void button_Copy1_Click(object sender, RoutedEventArgs e)
        {
            gridControl.Children.Clear();
            gridControl.Children.Add(new SerialTestView());
        }

        private void buttonUFID_Click(object sender, RoutedEventArgs e)
        {
            gridControl.Children.Clear();
            gridControl.Children.Add(new UFRFID());
        }

        private void Linq2_Click(object sender, RoutedEventArgs e)
        {
            gridControl.Children.Clear();
            gridControl.Children.Add(new LinqTest2());
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            gridControl.Children.Clear();
            gridControl.Children.Add(new PlateNumber());
        }

        private void btnQrCode_Click(object sender, RoutedEventArgs e)
        {
            gridControl.Children.Clear();
            gridControl.Children.Add(new QRCodeTest());
        }

        private void dataGridBing_Click(object sender, RoutedEventArgs e)
        {
            gridControl.Children.Clear();
            gridControl.Children.Add(new DataGridTest());
        }

        private void bingdingData_Click(object sender, RoutedEventArgs e)
        {

        }

        private void bingdingSpeak_Click(object sender, RoutedEventArgs e)
        {
            gridControl.Children.Clear();
            gridControl.Children.Add(new SpeakTest());
        }

        private void bingdingTree_Click(object sender, RoutedEventArgs e)
        {
            gridControl.Children.Clear();
            gridControl.Children.Add(new ViewTree());
        }

        private void ICCard_Click(object sender, RoutedEventArgs e)
        {
            gridControl.Children.Clear();
            gridControl.Children.Add(new ICCardMode());
        }

        private void PicturePhoto_Click(object sender, RoutedEventArgs e)
        {
            gridControl.Children.Clear();
            gridControl.Children.Add(new PhotoPicture());
        }

        private void ScoreSubmit_Click(object sender, RoutedEventArgs e)
        {
            gridControl.Children.Clear();
            gridControl.Children.Add(new SocreSubmit());
        }

        private void ScoreSubmit_Copy_Click(object sender, RoutedEventArgs e)
        {
            int i = 9;

            int mm = i/0;
            ThreadPool.QueueUserWorkItem(TestThread);

        }

        private void TestThread(object obj)
        {
            int i = 9;

            int mm = i / 0;
        }
    }
}
