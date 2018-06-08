using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Common;
using LowVoltageTechnicianOperation.Common;
using 学习测试;

namespace 学习测试2
{
    public partial class PCA测试 : Form
    {
        public PCA测试()
        {
            InitializeComponent();
        }

        private static PLCCommand plcCommond;

        private void button4_Click(object sender, EventArgs e)
        {
            label2.Text = "";
           ThreadPool.QueueUserWorkItem(DeletetionEnd,1);

        }

        public void DeletetionEnd(object obj)
        {
            progressBar1.Value = 0;
            try
            {
                //发送开始考试
                plcCommond = PLCCommand.GetInstance();
                plcCommond.SyncWrite((PLCCommand.GroupName)obj, PLCCommand.ItemName.Set, 1);  //set写入1  指定科目1

                //开始检测
                plcCommond.SyncWrite((PLCCommand.GroupName)obj, PLCCommand.ItemName.DetectionStart, 1);  //DetectionStart写入1  开始检测
                bool declectResult2 = false;
                bool result2 = false;
                bool detectionEnd = false;

                int sumTimeOut = 0;

                while (!detectionEnd)
                {
                    try
                    {
                        sumTimeOut++;

                        progressBar1.Value = sumTimeOut;
                        if (sumTimeOut > 149)
                        {
                            progressBar1.Value = 150;
                            break;
                        }
                        bool result = false;
                        bool declectResult = plcCommond.SyncRead((PLCCommand.GroupName)obj, PLCCommand.ItemName.DetectionEnd, out result);
                        if (result)
                        {
                            progressBar1.Value = 150;
                            //读取结果值
                            declectResult2 = plcCommond.SyncRead((PLCCommand.GroupName)obj, PLCCommand.ItemName.Result, out result2);

                            detectionEnd = result;
                            break;
                        }

                        Thread.Sleep(100);
                    }
                    catch (Exception)
                    {
                        break;
                    }
                }
                if (result2)
                {
                    label2.Invoke(new Action(() =>
                    {
                        label2.Text = "当前科目检测结果正确";
                    }));
                }
                else
                {
                    label2.Invoke(new Action(() =>
                    {
                        label2.Text =  "当前科目检测结果错误";
                    }));
                }
            }
            catch (Exception)
            {
                label2.Invoke(new Action(() =>
                {
                    label2.Text = "当前科目检测结果错误";
                }));

            }
         
        }

        private void button5_Click(object sender, EventArgs e)
        {
            label2.Text = "";
            ThreadPool.QueueUserWorkItem(DeletetionEnd, 0);
        }


        private void button6_Click(object sender, EventArgs e)
        {
            label2.Text = "";
            ThreadPool.QueueUserWorkItem(DeletetionEnd, 2);
        }

        private void PCA测试_Load(object sender, EventArgs e)
        {

        }
    }
}
