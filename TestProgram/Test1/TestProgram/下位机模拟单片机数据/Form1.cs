using Log;
using SerialUtil.Serial;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SerialUtil.until;
using HD.DBHelper;

namespace 下位机模拟单片机数据
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
         
        }

        private SerialClass serialPort { get; set; }

        private void button1_Click(object sender, EventArgs e)
        {
            InitSerialPort();
            button1.Enabled = false;
        }

        private void InitSerialPort()
        {
            serialPort = new SerialClass(ConfigurationManager.AppSettings["COM"], 115200, Parity.None, 8, StopBits.One);
            serialPort.DataReceived += SerialPortDataReceived;
            SerialMessageHandler.CheckSuccess += SerialMessageHandler_CheckSuccess;
            try
            {
                serialPort.openPort();
            }
            catch (IOException ex)
            {
                Logger.Error(ex.Message);
            }
        }

        private void SerialMessageHandler_CheckSuccess(string obj)
        {
            if (obj.ToLower() == "examing")
            {
                MessageBox.Show("开始考试");
            }
        }

        private void SerialPortDataReceived(Object sender, SerialDataReceivedEventArgs e, Byte[] bits)
        {
            try
            {
                SerialMessageHandler.Handle(bits);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            int deviceID = int.Parse(textBox2.Text.Trim());
            string cardID = txtCardNum.Text.Trim();
            byte[] temp = CmdHelper.GenerateVerifyCmd(deviceID, cardID);

            serialPort.SendData(temp,0,temp.Length);   //发送验证信息
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            StringBuilder buiderBuilder=new StringBuilder();
            buiderBuilder.Append("select  nvcName as 姓名, nvcIDNum as 身份证 from  JAAJ_Examinees  ");
            DataSet ds = DbHelperSQL.Query(buiderBuilder.ToString());
            dataGridView1.DataSource = ds.Tables[0];
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int deviceID = int.Parse(textBox2.Text.Trim());
            string cardID = txtCardNum.Text.Trim();
           
            byte[] temp = CmdHelper.GenerateExamCommitCmd(deviceID, null);

            for (int i = 0; i < 3; i++)
            {
                serialPort.SendData(temp, 0, temp.Length);   //提交考试
            }
          
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string temp = txtSearch.Text.Trim();
            StringBuilder buiderBuilder = new StringBuilder();
            buiderBuilder.Append("select  nvcName as 姓名, nvcIDNum as 身份证 from  JAAJ_Examinees  where nvcName like '%"+temp+"%' ");
            DataSet ds = DbHelperSQL.Query(buiderBuilder.ToString());
            dataGridView1.DataSource = ds.Tables[0];
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                txtCardNum.Text = dataGridView1.SelectedRows[0].Cells["身份证"].Value.ToString();
            }
        }
    }
}
