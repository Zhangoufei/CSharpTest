using Common;
using JaaJ.DAL;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TestProgram.Model
{
    /// <summary>
    /// ScreenParaMin.xaml 的交互逻辑
    /// </summary>
    public partial class ScreenParaMin : UserControl
    {
        public ScreenParaMin()
        {
            InitializeComponent();
            ReadPara();
        }

        ///读取配置文件
        public void ReadPara()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "\\RealityScores.xml";
            List<RealityScores> listRealityScores = CommonHelper.DeSerializeScoreFieldsSetting<List<RealityScores>>("RealityScores.xml");
            treeView.ItemsSource = listRealityScores;
        }
    }
}
