using Log;
using SerialUtil;
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
using System.Threading;
using System.Windows.Forms;
using SerialUtil.until;
using Timer = System.Windows.Forms.Timer;

namespace TestDevice
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private SerialClass SerialPort1 { get; set; }
        private SerialClass SerialPort2 { get; set; }
        /// <summary>
        /// 上位机初始化  
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            InitSerialPort(); //初始化串口

            //上位机自动发送数据遍历下位机
            ThreadPool.QueueUserWorkItem(Timer_Tick, 5);  //模拟5个设备
        }

        private void InitSerialPort()
        {
            SerialPort1 = new SerialClass(ConfigurationManager.AppSettings["COMUP"], 9600, Parity.None, 8, StopBits.One);
            SerialPort1.DataReceived += SerialPortDataReceived;
            SerialMessageHandler.CheckSuccess += SerialMessageHandler_CheckSuccess;

            try
            {
                SerialPort1.openPort();
            }
            catch (IOException ex)
            {
                Logger.Error(ex.Message);
            }
        }

        private void SerialMessageHandler_CheckSuccess(string obj)
        {
            if (obj.ToUpper() == "OK")
            {
                byte[] datat = CmdHelper.GenerateVerifyResultCmd(1, true);
                //给下位机 发送
                SerialPort1.SendData(datat, 0, datat.Length);  //发送成功标记  设置 这个设备为考试中状态
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
            InitSerialPort2();
        }

        private void Timer_Tick(object obj)
        {
            while (true)
            {
                SendData((int)obj);
                Thread.Sleep(2000);
            }
           
        }

        private int bytemp = 0;
        private void SendData(int temp)
        {
            bytemp++;
            if (bytemp > temp)
            {
                bytemp = 1;
            }
            byte[] bytete = new byte[]
              {
                0xaa,
                0xbb,
                (byte)bytemp,
                4,
                1,
                2,
                3,
                4,
                00,
                0x0e,
                0xcc,
                0xdd
              };

            SerialPort1.SendData(bytete, 0, bytete.Length);
        }

        private void InitSerialPort2()
        {
            SerialPort2 = new SerialClass(ConfigurationManager.AppSettings["COMDOWN"], 9600, Parity.None, 8, StopBits.One);
            SerialPort2.DataReceived += SerialPortDataReceived2;
            try
            {
                SerialPort2.openPort();
            }
            catch (IOException ex)
            {
                Logger.Error(ex.Message);
            }
        }


        private void SerialPortDataReceived2(Object sender, SerialDataReceivedEventArgs e, Byte[] bits)
        {
            try
            {
                SerialMessageHandler2.Handle(bits);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }
           
        }

        private void button3_Click(object sender, EventArgs e)
        {
           byte [] temp= CmdHelper.GenerateVerifyCmd(1,"41088219910715855X");

            SerialPort2.SendData(temp, 0, temp.Length);  //下位机给上位机发送验证信息
        }

    }


}
