using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using Welding.Annotations;

namespace Welding.MyPage
{
    /// <summary>
    /// DressPage.xaml 的交互逻辑
    /// </summary>
    public partial class DressPage : Page
    {
        public DressPage()
        {
            InitializeComponent();

            DataContext = new DressViewModel();
        }
        public DressViewModel Model { get { return (DressViewModel) DataContext; } }
 
        /// <summary>
        /// 禁用按钮1秒钟
        /// </summary>
        public void DisableNextButtonFor1S()
        {
            Button1.IsEnabled = false;

            ThreadPool.QueueUserWorkItem(state =>
            {
                Thread.Sleep(1000);

                Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() => Button1.IsEnabled = true));
            });
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.ParentWindow().StepToInspectionPage();
        }
    }

    public class DressViewModel:INotifyPropertyChanged
    {
        private string weldingDescription;

        public string WeldingDescription
        {
            get { return weldingDescription; }
            set
            {
                weldingDescription = value;
                OnPropertyChanged(nameof(WeldingDescription));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
