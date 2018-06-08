using System;
using System.Windows.Controls;
using System.Windows.Threading;
using log4net.Core;
using Log;
using PutoutFire.Common;
using UploadScore;

namespace Welding.MyPage
{
    /// <summary>
    /// TestResult.xaml 的交互逻辑
    /// </summary>
    public partial class TestResult : Page
    {
        public TestResult()
        {
            InitializeComponent();
            
            DispatcherTimer timer = new DispatcherTimer();
            timer.Tick += new EventHandler(timer_Tick);
            timer.Interval = TimeSpan.FromMilliseconds(10000);
            timer.Start();
            CommonHelper.SpeechToPrompt("考试结束");
        }
        public void aa(object sender ,EventArgs e)
        {
            App.Current.Shutdown();
        }
        
        void timer_Tick(object sender, EventArgs e)
        {
            try
            {
                DispatcherTimer ct = sender as DispatcherTimer;
                ct.Stop();

                ScoreData.UpdateDeviceStatusCallListDeleteExamProcess(CommonHelper.examineeInfo.iExamineeID);
                CommonHelper.mainWindow.Content = new Login();
            }
            catch (Exception ex)
            {
                Logger.Error(string.Format("{0}{2}{1}", ex.StackTrace, ex.Message, System.Environment.NewLine));
            }
        }
    }
}
