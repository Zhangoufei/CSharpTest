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
using System.Windows.Threading;

namespace TestProgram2
{
    /// <summary>
    /// VerifyFail.xaml 的交互逻辑
    /// </summary>
    public partial class PlateNumber : Window
    {
        public PlateNumber()
        {
            InitializeComponent();

            //DispatcherTimer timer = new DispatcherTimer();
            //timer.Interval = TimeSpan.FromSeconds(2);
            //timer.Tick += Timer_Tick;
            //timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            DispatcherTimer timer =(DispatcherTimer) sender;
            timer.Stop();

           // CreateButton(3,4);
        }

        private void CreateButton(int x, int y)
        {
            canvas1.Children.Clear();
            //四个方向的边距都是5
            double width = (this.canvas1.ActualWidth - (x + 1) * 5) / x;
            double height = (this.canvas1.ActualHeight - (y + 1) * 5) / y;


            int sumNumber = 0;

            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    sumNumber++;
                    Button bt = new Button()
                    {
                        Width = width,
                        Height = height,
                        Content = sumNumber.ToString(),
                    };
                    bt.Click += Bt_Click;

                    Canvas.SetTop(bt, j * height + 5);
                    Canvas.SetLeft(bt, i * width + 5);
                    //这两句很关键。按钮在Canvas中的定位与它自己的Left以及Top不是一个概念

                    canvas1.Children.Add(bt);
                }
            }

        }

        private void Bt_Click(object sender, RoutedEventArgs e)
        {
            Button but = (Button)sender;

            string temp = but.Content.ToString();

            //TipWindow.ShowTip(temp);
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            CreateButton(3, 3);
        }
    }
}
