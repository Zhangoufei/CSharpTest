using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace 学习测试.基础学习
{
    public partial class MultipleScreen : Form
    {
        int screenNo = 1;
        public MultipleScreen()
        {
            InitializeComponent();

            if (Screen.AllScreens.Length > 1)
            {
                Left = Screen.AllScreens[screenNo].Bounds.Left;
                Top = Screen.AllScreens[screenNo].Bounds.Top;

                Width = Screen.AllScreens[screenNo].Bounds.Width;
                Height = Screen.AllScreens[screenNo].Bounds.Height;
            }


        }
    }
}
