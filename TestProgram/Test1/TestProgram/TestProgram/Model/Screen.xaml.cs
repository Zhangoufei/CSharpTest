using GalaSoft.MvvmLight.Messaging;
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
using TestProgram.ViewModel;

namespace TestProgram.Model
{
    /// <summary>
    /// Screen.xaml 的交互逻辑
    /// </summary>
    public partial class Screen : UserControl
    {
        ScreemView screen;
        public Screen()
        {
            InitializeComponent();
            screen = new ScreemView();
            DataContext = screen;
            Messenger.Default.Register<object>(this, "ShowScreenPara", ShowScreenPara);
            Messenger.Default.Register<object>(this, "Sub", ShowSub);
           // Messenger.Default.Register<object>(this, "Sub", ShowSub);
        }
        public void ShowSub(object temp)
        {
            MessageBox.Show("你的评分当前为:"+temp.ToString());
        }
        public void ShowScreenPara(object temp)
        {
            this.Content = new ScreenParaMin();
        }
    }
}
