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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Test001
{
    /// <summary>
    /// 播放视频.xaml 的交互逻辑
    /// </summary>
    public partial class 播放视频 : Page
    {
        public 播放视频()
        {
            InitializeComponent();

            mediaElement.Source =new Uri(AppDomain.CurrentDomain.BaseDirectory + "Image\\" + "运行转检修.mp4");
            mediaElement.Play();
        }
    }
}
