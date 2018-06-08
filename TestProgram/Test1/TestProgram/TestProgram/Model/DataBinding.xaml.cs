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
using TestProgram.ViewModel;

namespace TestProgram.Model
{
    /// <summary>
    /// DataBinding.xaml 的交互逻辑
    /// </summary>
    public partial class DataBinding : UserControl
    {
        public DataBinding()
        {
            InitializeComponent();

            DataContext = new DataBindingVM();   //将数据绑定到 view上面
        }
    }
}
