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

namespace TestProgram2.usercontrol
{
    /// <summary>
    /// VerifyFail.xaml 的交互逻辑
    /// </summary>
    public partial class VerifyFail2 : Window
    {
        private System.Timers.Timer timer;
        public VerifyFail2()
        {
            InitializeComponent();

            timer = new System.Timers.Timer();
            timer.Interval = 5000;
            timer.Elapsed += Timer_Elapsed;
        }

        public void StartCount()
        {
            timer.Start();
        }

        public void StopCount()
        {
            timer.Stop();
        }


        private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            Dispatcher.BeginInvoke(new Action(Hide));
        }

        public string TipTest {
            get { return tHeader.Text; }
            set { tHeader.Text = value; }
        }
    }
}
