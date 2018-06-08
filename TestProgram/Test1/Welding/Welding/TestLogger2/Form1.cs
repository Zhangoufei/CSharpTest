using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using log4net;
using Log;

namespace TestLogger2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Logger.Debug("我是Debug");
            Logger.Info("我是Info");
            Logger.Warn("我是Warn");
            Logger.Error("我是Error");
            Logger.Fatal("我是Fatal");
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
