using System;
using System.Collections.Generic;
using System.Configuration;
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
using TestProgram.BLL.Serial;
using Welding;

namespace TestProgram.Model
{
    /// <summary>
    /// SerialTestView.xaml 的交互逻辑
    /// </summary>
    public partial class SerialTestView : UserControl
    {
        public SerialTestView()
        {
            InitializeComponent();
        }
        SerialClass serial = null;
        private void button_Click(object sender, RoutedEventArgs e)
        {
            // serial = new SerialClass();
            serial = new SerialClass(ConfigurationManager.AppSettings["COM"], 9600, Parity.None, 8, StopBits.One);
            //serial.BaudRate = 9600;
            //serial.PortName = ConfigurationManager.AppSettings["COM"];
            serial.DataReceived += SerialPortDataReceived;
            bool result = serial.openPort();
        }



        private void SerialPortDataReceived(object sender, SerialDataReceivedEventArgs e, byte[] bits)
        {
            SerialMessageHandler.Handle(bits);
            Dispatcher.Invoke(new Action(() =>
            {
                listBox.Items.Add(SerialMessageHandler.Eight + "\r\n");

                listBox.ScrollIntoView(listBox.Items.Count);

                if (SerialMessageHandler.One)
                {
                    panelValue1.Background = Brushes.Green;
                }
                else
                {
                    panelValue1.Background = Brushes.Red;
                }
                if (SerialMessageHandler.Two)
                {
                    panelValue2.Background = Brushes.Green;
                }
                else
                {
                    panelValue2.Background = Brushes.Red;
                }
                if (SerialMessageHandler.Three)
                {
                    panelValue3.Background = Brushes.Green;
                }
                else
                {
                    panelValue3.Background = Brushes.Red;
                }
                if (SerialMessageHandler.Four)
                {
                    panelValue4.Background = Brushes.Green;
                }
                else
                {
                    panelValue4.Background = Brushes.Red;
                }
                if (SerialMessageHandler.Five)
                {
                    panelValue5.Background = Brushes.Green;
                }
                else
                {
                    panelValue5.Background = Brushes.Red;
                }
                if (SerialMessageHandler.Six)
                {
                    panelValue6.Background = Brushes.Green;
                }
                else
                {
                    panelValue6.Background = Brushes.Red;
                }
                if (SerialMessageHandler.Seven)
                {
                    panelValue7.Background = Brushes.Green;
                }
                else
                {
                    panelValue7.Background = Brushes.Red;
                }

                if (SerialMessageHandler.Eight)
                {
                    panelValue8.Background = Brushes.Green;
                }
                else
                {
                    panelValue8.Background = Brushes.Red;
                }
            }
            ));
        }
    }
}
