using ServiceContractTest1;
using ServiceContractTest1ClientForm.Service1;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Windows.Forms;

namespace ServiceContractTest1ClientForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        

        ServiceContractTest1.IFirstService service = null;
        private void button1_Click(object sender, EventArgs e)
        {
            ///  FirstServiceClient servicer = new Service1.FirstServiceClient();
            // servicer.GetDataString("你好啊");
            //  NewMethod();   //连接服务端方式1  后台配置

            //连接服务端方式2  配置文件配置 
            InstanceContext callback = new InstanceContext(new callBack());  //这个this 就是重写的callback函数 也可以用一个类 来实现这个

            var client = new Service1.FirstServiceClient(callback, "NetTcpBinding_IFirstService");
            
            client.GetDataString("123");

        }

        class callBack : Service1.IFirstServiceCallback
        {
            public void NotifyClientMsg(string devStateInfo)
            {
                MessageBox.Show("服务端发送了消息" + devStateInfo);

            }

        }


        private void NewMethod()
        {
            InstanceContext callback = new InstanceContext(this);
            DuplexChannelFactory<ServiceContractTest1.IFirstService> channelFactory;

            NetTcpBinding bingding = new NetTcpBinding();
            bingding.Security.Mode = SecurityMode.None;
            Uri baseAddress = new Uri("net.tcp://localhost:8004/Service1");

            channelFactory = new DuplexChannelFactory<ServiceContractTest1.IFirstService>
                (callback, bingding, new EndpointAddress(baseAddress));

            service = channelFactory.CreateChannel();

            MessageBox.Show(service.GetDataString("你好啊"));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show(service.GetDataString(textBox1.Text.Trim()));
        }
    }
}
