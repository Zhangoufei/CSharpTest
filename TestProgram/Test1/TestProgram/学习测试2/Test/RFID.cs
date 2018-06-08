using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using HardWare;

namespace 学习测试2.Test
{
    public partial class RFID : Form
    {
        public RFID()
        {
            InitializeComponent();
        }
        RfidUtility unity = null;
        private bool RegisterEnver = false;
        private void btnOpen_Click(object sender, EventArgs e)
        {
            tbxValue.Text = "";

            byte comport = byte.Parse(ConfigurationManager.AppSettings["rfid"].ToString());
            unity = new RfidUtility(comport);

            unity.TimerSpan = 8000;
            unity.TimerSpanEnable = false;

            if (!RegisterEnver)
            {
                RegisterEnver = true;
                RfidUtility.RfidValue += RfidUtility_RfidValue;
                RfidUtility.RfidOpenResult += RfidUtility_RfidOpenResult;
            }
            unity.OpenRfid(int.Parse(btnComport.Text.Trim()),1);  //打开串口 开始读卡 使用默认地址
        }

        private void RfidUtility_RfidOpenResult(bool obj)
        {
            if (obj)
            {
                if (btnOpen.IsHandleCreated)
                {
                    btnOpen.Invoke(new Action(() =>
                    {
                        btnOpen.Enabled = false;
                    }));
                }
            }

        }

        private void RfidUtility_RfidValue(Dictionary<string, string> obj)
        {
            if (obj != null)
            {

                foreach (var temp in obj)
                {
                    if (tbxValue.IsHandleCreated)
                    {
                        tbxValue.Invoke(new Action(() =>
                        {
                            tbxValue.AppendText(temp.Key + ":" + temp.Value + "\r\n");
                        }));
                    }
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {

            unity.CloseRfid();
            btnOpen.Enabled = true;

            btn1.Enabled = true;
            btn2.Enabled = true;
            btn3.Enabled = true;
            btn4.Enabled = true;
        }

        private void btn1_Click(object sender, EventArgs e)
        {
            btn1.Enabled = false;
            unity.AddList(byte.Parse(textBox1.Text.Trim()));
        }

        private void btn2_Click(object sender, EventArgs e)
        {
            btn2.Enabled = false;
            unity.AddList(byte.Parse(textBox2.Text.Trim()));
        }

        private void btn3_Click(object sender, EventArgs e)
        {
            btn3.Enabled = false;
            unity.AddList(byte.Parse(textBox3.Text.Trim()));
        }

        private void btn4_Click(object sender, EventArgs e)
        {
            btn4.Enabled = false;
            unity.AddList(byte.Parse(textBox4.Text.Trim()));
        }
    }
}
