﻿using System;
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
using TestProgram2.PublicControl;
using TestProgram2.ViewModel;

namespace TestProgram2.usercontrol
{
    /// <summary>
    /// DependencyPage.xaml 的交互逻辑
    /// </summary>
    public partial class DependencyPage : UserControl
    {
        public DependencyPage()
        {
            InitializeComponent();
            DataContext = new DependencyVM();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            VerifyFail verify=new VerifyFail("欢迎");
            verify.Show();
        }
    }
}