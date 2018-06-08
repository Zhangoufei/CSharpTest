using System;
using System.Collections.Generic;
using System.IO.Ports;
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
using Communication;
using Communication.Serial;

namespace Simulation
{
    /// <summary>
    /// SimulationPage.xaml 的交互逻辑
    /// </summary>
    public partial class SimulationPage : Page
    {
        SerialClass serialPort = new SerialClass("COM3", 9600, Parity.None, 8, StopBits.One);
        public SimulationPage()
        {
            InitializeComponent();
        }

        public Boolean OpenSerilPort()
        {
            return serialPort.openPort();
        }

        private void OnCheckChange(object sender, RoutedEventArgs e)
        {
            var data = GetData(GetCurrentState());

            serialPort.SendData(data, 0, data.Length);
        }

        private DeviceState GetCurrentState()
        {
            return Grid1.Children.Cast<CheckBox>().Where(box => box.IsChecked.HasValue && box.IsChecked.Value).
                Aggregate((DeviceState)0, (current, box) => current | (DeviceState)Enum.Parse(typeof(DeviceState), (string)box.Tag));
        }

        private byte[] GetData(DeviceState state)
        {
            var data = BytesHelper.IntToBytes((Int32)state, 2, DataType.BigEndian);

            return new byte[] { 0x7f, 0x80, data[0], data[1],0x81, 0x82 };
        }
    }
}
