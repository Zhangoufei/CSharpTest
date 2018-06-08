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
using System.Windows.Threading;

namespace TestProgram2.PublicControl
{
    /// <summary>
    /// VerifyFail.xaml 的交互逻辑
    /// </summary>
    public partial class VerifyFail : Window
    {
        public VerifyFail(string strTip)
        {
            InitializeComponent();

            this.Topmost = true;

            tbTip.Text = strTip;

            DispatcherTimer timer = new DispatcherTimer();
            timer.Tick += new EventHandler(timer_Tick);
            timer.Interval = TimeSpan.FromMilliseconds(5000);
            timer.Start();
        }
        void timer_Tick(object sender, EventArgs e)
        {
            DispatcherTimer ct = sender as DispatcherTimer;
            ct.Stop();
            this.Close();
        }
    }
}
