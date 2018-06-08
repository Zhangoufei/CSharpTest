using System;
using System.Windows;

namespace Welding.MyPage
{
    /// <summary>
    /// VerifyFail.xaml 的交互逻辑
    /// </summary>
    public partial class VerifyFail : Window
    {
        private System.Timers.Timer timer;

        public string TipText { get { return tbTip.Text; } set { tbTip.Text = value; } }
        
        public VerifyFail()
        {
            InitializeComponent();
            timer = new System.Timers.Timer();
            timer.Elapsed += new System.Timers.ElapsedEventHandler(timer_Elapsed);
            timer.Interval = 5000;
            //timer.AutoReset = false;
        }

        public void StartCount()
        {
            timer.Start();
        }

        public void StopCount()
        {
            timer.Stop();
        }

        void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            this.Dispatcher.BeginInvoke(new Action(Hide));
        }
    }
}
