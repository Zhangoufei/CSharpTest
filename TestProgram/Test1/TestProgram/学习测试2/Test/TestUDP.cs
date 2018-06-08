using Example485;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace 学习测试2.Test
{
    public partial class TestUDP : Form
    {
        public TestUDP()
        {
            InitializeComponent();
        }
        private UdpCommunication dataTransmitter;
        private void button1_Click(object sender, EventArgs e)
        {
            EnableDataReceive(true);
        }

        private void TestUDP_Load(object sender, EventArgs e)
        {
            dataTransmitter = new UdpCommunication();
            string error;
            if (!dataTransmitter.Bind("127.0.0.1", out error))
            {
                MessageBox.Show("绑定查询端口失败");
            }
        }
        void SetDeviceEp(IPEndPoint ipEndPoint)
        {
            dataTransmitter.RemoteEP = ipEndPoint;
        }
        void EnableDataReceive(Boolean enable)
        {
            if (enable)
            {
                dataTransmitter.DataReceived += new Action<byte[]>(OnDataReceived);
            }
            else
            {
                dataTransmitter.DataReceived -= new Action<byte[]>(OnDataReceived);
            }
        }

        void OnDataReceived(byte[] obj)
        {
            if (obj != null)
            {
                MessageBox.Show(ByteArrayToHexString(obj));
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            byte [] bytete=new byte[5];
            for (int i = 0; i < bytete.Length; i++)
            {
                bytete[i] =byte.Parse(i + 5+"");
            }
            //发送验证结果
            SetDeviceEp((IPEndPoint)dataTransmitter.LocalEndPoint);
            dataTransmitter.Send(bytete);
        }

        /// <summary>
        /// byte数组转换为16进制字符串
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private string ByteArrayToHexString(byte[] data)
        {
            StringBuilder sb = new StringBuilder(data.Length * 3);
            foreach (byte b in data)
                sb.Append(Convert.ToString(b, 16));
            return sb.ToString().ToUpper();
        }
    }
}
