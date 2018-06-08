using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TestDevice;

namespace 上位机
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            SerialMessageHandler.CheckSuccess += SerialMessageHandler_CheckSuccess;
        }


        private void SerialMessageHandler_CheckSuccess(string obj)
        {
            if (obj.ToUpper() == "OK")
            {
                MessageBox.Show("设备号 1 开始考试");

            }
            else if (obj.ToLower() == "commitexam")
            {
                MessageBox.Show("完成考试");
            }
            //如果是通用应答不做处理 ....
        }
    }
}
