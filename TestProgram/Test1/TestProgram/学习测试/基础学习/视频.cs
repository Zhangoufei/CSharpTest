using AForge.Video.DirectShow;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace 学习测试.基础学习
{
    public partial class 视频 : Form
    {
        public 视频()
        {
            InitializeComponent();
        }
        FilterInfoCollection videoDevices;
        VideoCaptureDevice videoSource;
        public int selectedDeviceIndex = 0;
        private void button1_Click(object sender, EventArgs e)
        {
            videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            selectedDeviceIndex =0 ;
            videoSource = new VideoCaptureDevice(videoDevices[selectedDeviceIndex].MonikerString);//连接摄像头。
            videoSource.VideoResolution = videoSource.VideoCapabilities[selectedDeviceIndex];
            videoSourcePlayer1.VideoSource = videoSource;
            // set NewFrame event handler
            videoSourcePlayer1.Start();

            videoSourcePlayer1.Start();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (videoSource == null)
                return;
            Bitmap bitmap = videoSourcePlayer1.GetCurrentVideoFrame();
            string fileName = "54250.jpg";//DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss-ff") + ".jpg";
            bitmap.Save(@"E:\" + fileName, ImageFormat.Jpeg);
            bitmap.Dispose();
        }
    }
}
