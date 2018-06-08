using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using JAAJ.PoleMountedTransformer.Common;

namespace 学习测试2.Test
{
    public partial class Plc通用 : Form
    {
        public Plc通用()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var plc = PLCCommand2.GetInstance();
            bool tempReuslt = false;
            try
            {
                plc.SyncRead(PLCCommand2.GroupName.Main, (PLCCommand2.ItemName)int.Parse(textBox2.Text.Trim()), out tempReuslt);
            }
            catch (Exception)
            {
                MessageBox.Show(tempReuslt + "出现异常");
            }
            MessageBox.Show(tempReuslt+"结果");
        }
    }
}
