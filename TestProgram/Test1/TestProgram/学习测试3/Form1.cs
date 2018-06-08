using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace 学习测试3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            Dictionary<int, string> dicName = new Dictionary<int, string>();

            string path = Application.StartupPath + "\\file";

            if (Directory.Exists(path))
            {
                var str = Directory.GetFiles(path);
                int sumName = 0;
                foreach (var vtem in str)
                {
                    if (vtem.Contains(".xml"))
                    {
                        sumName++;
                        string temp = vtem.Substring(vtem.LastIndexOf("\\") + 1).Replace(".xml", "");
                        dicName.Add(sumName,temp);
                    }
                }

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int height = Screen.PrimaryScreen.Bounds.Height;

            int weight = Screen.PrimaryScreen.Bounds.Width;

            MessageBox.Show("当前屏幕 分辨率 高度为:"+height+"\r\t 宽度为:"+weight);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Process.Start("C:\\Program Files\\Common Files\\microsoft shared\\ink\\TabTip.exe");
        }
    }
}
