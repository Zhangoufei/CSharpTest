using JAAJ.PEAR.Common;
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
using System.Windows.Threading;

namespace Test001
{
    /// <summary>
    /// 倒计时.xaml 的交互逻辑
    /// </summary>
    public partial class 倒计时 : Window
    {
        public 倒计时()
        {
            InitializeComponent();
            InitClock();
        }

        private ProcessCount pCount ;
        private DispatcherTimer timer;
        /// <summary>
        /// 初始化倒计时
        /// </summary>
        private void InitClock()
        {
            pCount = new ProcessCount(40);
            tbSecond.Text = pCount.GetSecond();

            timer = new DispatcherTimer();
            timer.Tick += new EventHandler(timer_Tick);
            timer.Interval = TimeSpan.FromMilliseconds(1000);
            timer.Start();
        }

        private void timer_Tick(object sender, EventArgs args)
        {
            if (pCount.ProcessCountDown())
            {
                tbSecond.Text = pCount.GetSecond();
            }
            else
            {
                timer.Stop();
                this.Close();
               // CommonHelper.SpeechToPrompt("40秒没有开始考试");
             //   CommonHelper.mainWindow.Content = new Login();
            }
        }
    }


}
