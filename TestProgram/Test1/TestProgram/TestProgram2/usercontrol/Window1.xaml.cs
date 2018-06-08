using AForge.Video.DirectShow;
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
    /// Window1.xaml 的交互逻辑
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
            StartCamera();
        }


        public void StartCamera()
        {
            player.BackgroundImage = null;

            var device = CameraHelper.GetDevice();

            if (device == null)
            {
               // TipWindow.ShowTip("请检查摄像头连接!");
                return;
            }

            player.VideoSource = device;
            player.Start();
        }
    }
    class CameraHelper
    {
        private static readonly FilterInfoCollection VideoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);

        public static VideoCaptureDevice GetDevice()
        {
            return VideoDevices.Count > 0 ? new VideoCaptureDevice(VideoDevices[0].MonikerString) : null;
        }
    }
}
