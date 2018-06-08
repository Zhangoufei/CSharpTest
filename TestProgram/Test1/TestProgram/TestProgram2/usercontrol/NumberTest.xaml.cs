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

namespace TestProgram2.usercontrol
{
    /// <summary>
    /// NumberTest.xaml 的交互逻辑
    /// </summary>
    public partial class NumberTest : UserControl
    {
        public NumberTest()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            CreateButton(10, 5);
        }

        private void CreateButton(int x, int y)
        {
            canvas1.Children.Clear();
            //四个方向的边距都是5
            double width = (this.canvas1.ActualWidth - (x + 1) * 5) / x;
            double height = (this.canvas1.ActualHeight - (y + 1) * 5) / y;

            for (int i = 0; i < x; i++)
            {
                //for (int j = 0; j < y; j++)
                //{
                    Button bt = new Button()
                    {
                        Width = width,
                        Height = height,
                        Content = i.ToString()
                    };

                    Canvas.SetTop(bt, y * height + 5);
                    Canvas.SetLeft(bt, i * width + 5);
                    //这两句很关键。按钮在Canvas中的定位与它自己的Left以及Top不是一个概念

                    canvas1.Children.Add(bt);
               // }
            }
        }

        private void CreateButton2(int x, int y)
        {
            canvas1.Children.Clear();
            //四个方向的边距都是5
            double width = (this.canvas1.ActualWidth - (x + 1) * 5) / x;
            double height = (this.canvas1.ActualHeight - (y + 1) * 5) / y;
            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    Button bt = new Button()
                    {
                        Width = width,
                        Height = height,
                        Content = i.ToString()
                    };

                    //  uniformGrid.(bt, j * height + 5);
                    //  Canvas.SetLeft(bt, i * width + 5);
                    //这两句很关键。按钮在Canvas中的定位与它自己的Left以及Top不是一个概念
                    uniformGrid.Children.Add(bt);
                }
            }
        }
        private void CreateButton2(int x)
        {
            canvas1.Children.Clear();
            //四个方向的边距都是5
            double width = (this.canvas1.ActualWidth - (x + 1) * 5) / x;
            double height = (this.canvas1.ActualHeight - (x + 1) * 5) / x;

            for (int i = 0; i < x; i++)
            {
                Button bt = new Button()
                {
                    Width = width,
                    Height = height,
                    Content = i.ToString()
                };
                uniformGrid.Children.Add(bt);
            }
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            CreateButton2(10);
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            PlateNumber palte = new PlateNumber() {Topmost=true};
            palte.Show();
        }
    }
}
