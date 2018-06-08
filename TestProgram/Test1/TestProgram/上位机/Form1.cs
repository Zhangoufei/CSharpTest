using SerialUtil.Serial;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using Log;
using TestDevice;

namespace 上位机
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private ObservableCollection<DeviceInfo> deviceList;   //设备信息

        private SerialClass serialPort { get; set; }
        private void button1_Click(object sender, EventArgs e)
        {
            deviceList = new ObservableCollection<DeviceInfo>();
            setDeviceInfo(5);  //初始化5个设备
            InitSerialPort();
            //上位机自动发送数据遍历下位机
            ThreadPool.QueueUserWorkItem(Timer_Tick, deviceList);  //模拟5个设备
            button1.Enabled = false;
        }

        private void setDeviceInfo(int deviceNum)
        {
            for (int i = 0; i < deviceNum; i++)
            {
                deviceList.Add(new DeviceInfo()
                {
                    DeviceId = i+1,
                    State = DeviceState.Validating,
                    UserId = null
                });
            }

        }

        private void InitSerialPort()
        {
            serialPort = new SerialClass(ConfigurationManager.AppSettings["COM"], 9600, Parity.None, 8, StopBits.One);
            serialPort.DataReceived += SerialPort_DataReceived;
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

        private void Timer_Tick(object obj)
        {
            while (true)
            {
                SendData((ObservableCollection<DeviceInfo>)obj);
                Thread.Sleep(2000);
            }
        }

        /// <summary>
        /// 遍历 设备 轮询 查询 设备状态
        /// </summary>
        /// <param name="temp"></param>
        private void SendData(ObservableCollection<DeviceInfo> temp)
        {
            foreach (var VARIABLE in temp)
            {
                if (VARIABLE.State != DeviceState.Unused)   //如果不能为不能考试就遍历
                {
                    byte[] queryBytes = CmdHelper.GenerateQueryCmd(VARIABLE.DeviceId);   //查询命令
                    serialPort.SendData(queryBytes, 0, queryBytes.Length);
                }
            }
        }


        private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e, byte[] bits)
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

        private void SerialMessageHandler_CheckSuccess(string obj)
        {
            if (obj.ToUpper() == "OK")
            {
                byte[] datat = CmdHelper.GenerateVerifyResultCmd(deviceList[1].DeviceId, true);
                //给下位机 发送
                serialPort.SendData(datat, 0, datat.Length);  //发送成功标记  设置 这个设备为考试中状态

                deviceList[1].State=DeviceState.Unused;

                MessageBox.Show("设备号 1 开始考试");

            }
            else if (obj.ToLower() == "commitexam")
            {
                deviceList[1].State = DeviceState.Validating;
                MessageBox.Show("完成考试");
            }
            //如果是通用应答不做处理 ....
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form2 form=new Form2();
            form.Show();
        }
    }
}
