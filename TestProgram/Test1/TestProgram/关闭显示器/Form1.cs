using Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace 关闭显示器
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            User32DLL.SendMessage(this.Handle,(int) SysCommands.WM_SYSCOMMAND, (int)SysCommands.SC_MONITORPOWER,2);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            User32DLL.SendMessage(this.Handle, (int)SysCommands.WM_SYSCOMMAND, (int)SysCommands.SC_MONITORPOWER, -1);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            User32DLL.SendMessage(this.Handle, (int)SysCommands.WM_SYSCOMMAND, (int)SysCommands.SC_MONITORPOWER, 2);
        }
    }
}
