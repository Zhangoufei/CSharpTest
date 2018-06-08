using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Threading;

namespace TestProgram2.ViewModel
{
    class TipVerify
    {
        private static usercontrol.VerifyFail2 verify = new usercontrol.VerifyFail2 { Topmost = true };

        private static Action CallBack { set; get; }

        static TipVerify()
        {
            verify.IsVisibleChanged += Verify_IsVisibleChanged;
        }

        private static int sum = 0;
        public static void ShowTip(string tip, Action callBack = null)
        {

            verify.TipTest = tip;

            CallBack = callBack;

            verify.Show();

            verify.StartCount();
        }

        public static void ShowPauseTip(string tip, Action callBack = null)
        {
            DispatcherTimer timer = new DispatcherTimer()
            {
                Interval = TimeSpan.FromSeconds(3),
            };
            timer.Tick += (sender, e) =>
            {
                timer.Stop();
                verify.TipTest = tip;

                CallBack = callBack;

                verify.Show();

                verify.StartCount();
            };
            timer.Start();
        }


        public static void Dispose()
        {
            verify.Close();
        }

        private static void Verify_IsVisibleChanged(object sender, System.Windows.DependencyPropertyChangedEventArgs e)
        {
            if (!(bool)e.NewValue)
            {
                verify.StopCount();
                if (CallBack != null)
                {
                    CallBack.Invoke();
                }
            }
        }
    }
}
