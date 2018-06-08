using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace TestProgram2.usercontrol
{
    /// <summary>
    /// ListViewTest.xaml 的交互逻辑
    /// </summary>
    public partial class ListViewTest : UserControl
    {
        private ListViewTestVM listViewTest = null;
        public ListViewTest()
        {
            InitializeComponent();
            DataContext = listViewTest = new ListViewTestVM();
        }

        private void 添加_Click(object sender, RoutedEventArgs e)
        {
            listViewTest.CmdList.Add(new CmdInfo()
            {
                DeviceId = 555,
                Data = new byte[]
                {
                    23,
                    0x0c,
                    0x06
                }
            });
        }
    }

    class ListViewTestVM
    {
        private ObservableCollection<CmdInfo> cmdList;
        public ObservableCollection<CmdInfo> CmdList { get { return cmdList; } }
        public ListViewTestVM()
        {
            cmdList = new ObservableCollection<CmdInfo>();

            cmdList.Add(new CmdInfo()
            {
                DeviceId = 123,
                Data = new byte[]
                {
                    231,
                    0x0c,
                    0x06
                }
            });
            cmdList.Add(new CmdInfo()
            {
                DeviceId = 1553,
                Data = new byte[]
               {
                    213,
                    0x0c,
                    0x06
               }
            });
        }


    }


    public class CmdInfo 
    {
        public Int32 DeviceId { get; set; }
        public byte[] Data { get; set; }

    }


}
