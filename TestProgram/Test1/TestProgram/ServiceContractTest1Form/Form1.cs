using ServiceContractTest1.implements;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Windows.Forms;

namespace ServiceContractTest1Form
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        ServiceHost host = null;
        ServiceHost host2 = null;
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                host = new ServiceHost(typeof(Service));
                host.Open();
                MessageBox.Show("服务1 已启动");

                //启用另一个服务
                host2 = new ServiceHost(typeof(FirstService));
                host2.Open();
                MessageBox.Show("服务2 已启动");
            }
            catch (Exception ee)
            {
                MessageBox.Show("服务异常"+ee.StackTrace);
            }
          
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (FirstService.callBack != null)
            {
                FirstService.callBack.NotifyClientMsg("通知消息:" + textBox1.Text.Trim());
            }
          
        }
    }
}
