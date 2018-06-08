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
using GalaSoft.MvvmLight.Messaging;
using TestProgram2.usercontrol;
using TestProgram2.usercontrol2;
using TestProgram2.ViewModel;

namespace TestProgram2
{
    /// <summary>
    /// MainView.xaml 的交互逻辑
    /// </summary>
    public partial class MainView : Page
    {
        private DispatcherTimer timer;
        public MainView()
        {
            InitializeComponent();

            comboBox.Items.Add("2324");
            comboBox.Items.Add("2324");
            comboBox.Items.Add("2324");
            comboBox.Items.Add("2324");
            comboBox.Items.Add("2324");
            comboBox.Items.Add("2324");

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(2);
            timer.Tick += Timer_Tick; ;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            //if (!TipVerify.TipVerifyShow)
            //{
                // this.Width = System.Windows.SystemParameters.PrimaryScreenWidth;
                // this.Height = System.Windows.SystemParameters.PrimaryScreenHeight;
                textBox.Focus();
          //  }

        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            gridControl.Children.Clear();
            gridControl.Children.Add(new KeyboardUser());
        }

        private void butt1_Click(object sender, RoutedEventArgs e)
        {
            gridControl.Children.Clear();
            gridControl.Children.Add(new ListViewTest());
        }

        private void butt1_Copy_Click(object sender, RoutedEventArgs e)
        {
            gridControl.Children.Clear();
            gridControl.Children.Add(new DependencyPage());
        }

        private void butt2_Click(object sender, RoutedEventArgs e)
        {
            TipVerify.ShowTip("欢迎你", ShowWindow2);
        }
        private void butt3_Click(object sender, RoutedEventArgs e)
        {
            TipVerify.ShowPauseTip("欢迎你", ShowWindow2);
        }
        public void ShowWindow2()
        {
            // MessageBox.Show("欢迎你打开软件");
            gridControl.Children.Clear();
            gridControl.Children.Add(new KeyboardUser());
        }

        private void btnSetScreen_Click(object sender, RoutedEventArgs e)
        {
            gridControl.Children.Clear();
            gridControl.Children.Add(new UserScreenSetting());
        }

        private void btnSetBtton_Click(object sender, RoutedEventArgs e)
        {
            gridControl.Children.Clear();
            gridControl.Children.Add(new NumberTest());
        }

        private void btnShow_Click(object sender, RoutedEventArgs e)
        {

        }

        private void regesisterA_Click(object sender, RoutedEventArgs e)
        {
            gridControl.Children.Clear();
            gridControl.Children.Add(new RegisterView());

        }

        private void regesisterA_Copy_Click(object sender, RoutedEventArgs e)
        {
            Messenger.Default.Send<string>("弹出1", "GetRegister1");
        }
    }
}
