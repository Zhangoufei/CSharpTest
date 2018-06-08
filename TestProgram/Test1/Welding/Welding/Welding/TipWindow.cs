using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Welding.MyPage;

namespace Welding
{
    class TipWindow
    {
        static VerifyFail tipWindow = new VerifyFail {Topmost = true};

        private static Action CallBack { get; set; }

        static TipWindow()
        {
            tipWindow.IsVisibleChanged += OnVisibleChanged;
        }

        public static void ShowTip(string tip, Action callback = null)
        {
            tipWindow.TipText = tip;
            
            CallBack = callback;

            tipWindow.Show();

            tipWindow.StartCount();
        }

        public static void Dispose()
        {
            tipWindow.Close();
        }

        private static void OnVisibleChanged(object sender, System.Windows.DependencyPropertyChangedEventArgs e)
        {
            if (!((Boolean)e.NewValue))//隐藏窗口
            {
                tipWindow.StopCount();

                if (CallBack != null)
                {
                    CallBack.Invoke();
                }

            }
        }
    }
}
