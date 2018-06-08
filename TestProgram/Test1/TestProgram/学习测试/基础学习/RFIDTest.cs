using Common;
using ReaderB;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace 学习测试.基础学习
{
    public partial class RFIDTest : Form
    {
        public RFIDTest()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
        private int portOpenHandle = 0;
        private bool bNeedKeep = true;
        private int iportAdr = 0;
        private bool bKeepDetection = true;
        private static object _RFIDLock = new object();
        private static object _RDSLock = new object();
        /// <summary>
        /// 开始检测状态下未检测到标签的次数，以此参数来控制是否停止读取标签，默认阈值为10
        /// </summary>
        private int notDetectionEPC = 0;
        /// <summary>
        /// 检测状态;0:未开始检测;1:开始检测，以此参数来确认考生是否进入检测门范围；
        /// </summary>
        private int detectionState = 0;
        /// <summary>
        /// 检测到的EPC标签
        /// </summary>
        private Dictionary<string, string> detectedEPC = new Dictionary<string, string>();
        /// <summary>
        /// 初始化读卡器
        /// </summary>
        private void InitReader()
        {
            iportAdr = int.Parse(System.Configuration.ConfigurationManager.AppSettings["DoorAdr"]);
            byte bytComAdr = 0x02;
            byte bytBaud = 0x5;
            try
            {
                Thread thread = new Thread(() =>
                {
                    int iResult = StaticClassReaderB.OpenComPort(iportAdr, ref bytComAdr, bytBaud, ref portOpenHandle);
                    if (iResult == 0)
                    {
                        Inventory();
                    }
                });
                thread.IsBackground = true;
                thread.Start();
            }
            catch (Exception ex)
            {
                LogImpl.Error(string.Format("{0}{2}{1}", ex.StackTrace, ex.Message, System.Environment.NewLine));
            }
        }

        /// <summary>
        /// 检测标签
        /// </summary>
        private void Inventory()
        {
            while (bKeepDetection)
            {
                GetEPCNumsByAdr(0x02);
                GetEPCNumsByAdr(0x03);
                GetEPCNumsByAdr(0x04);
                GetEPCNumsByAdr(0x06);
            }
            StaticClassReaderB.CloseSpecComPort(portOpenHandle);
        }


        /// <summary>
        /// 根据读卡器地址读取EPC标签
        /// </summary>
        /// <param name="bytAdr"></param>
        private void GetEPCNumsByAdr(byte bytAdr)
        {
            int CardNum = 0;
            int Totallen = 0;
            byte[] EPC = new byte[100];

            byte bytConAdr = bytAdr;
            byte AdrTID = 0;
            byte LenTID = 0;
            byte TIDFlag = 0;
            int iResult = StaticClassReaderB.Inventory_G2(ref bytConAdr, AdrTID, LenTID, TIDFlag, EPC, ref Totallen, ref CardNum, portOpenHandle);
            if (CardNum > 0)
            {
                ProcessEPCNum(EPC, Totallen, CardNum, bytConAdr);
            }
            else
            {
                if (detectionState == 1)
                {
                    notDetectionEPC++;
                    if (notDetectionEPC > 100)
                    {
                        bKeepDetection = false;
                    }
                }
            }
        }

        private void GetEPCNumsForOperate(byte bytAdr)
        {
            int CardNum = 0;
            int Totallen = 0;
            byte[] EPC = new byte[100];

            byte bytConAdr = bytAdr;
            byte AdrTID = 0;
            byte LenTID = 0;
            byte TIDFlag = 0;
            int iResult = StaticClassReaderB.Inventory_G2(ref bytConAdr, AdrTID, LenTID, TIDFlag, EPC, ref Totallen, ref CardNum, portOpenHandle);
            if (iResult == 0x30)
                return;
            if (!bNeedKeep)
            {
                lock (_RFIDLock)
                {
                    var list = detectedEPC.Where(p => p.Value == bytAdr.ToString()).Select(p => p.Key).ToList();
                    for (int i = 0; i < list.Count(); i++)
                    {
                        detectedEPC.Remove(list[i]);
                    }
                }
            }
            if (CardNum > 0)
            {
                ProcessEPCNum(EPC, Totallen, CardNum, bytConAdr);
            }
        }

        private void ProcessEPCNum(byte[] EPC, int Totallen, int CardNum, byte bytConAdr)
        {
            notDetectionEPC = 0;
            detectionState = 1;
            int iEPCLen = 0;
            int iPos = 0;

            byte[] arryEPC = EPC.Take(Totallen).ToArray();
            string strEPC = ByteArrayToHexString(arryEPC);
            for (int i = 0; i < CardNum; i++)
            {
                iEPCLen = arryEPC[iPos];
                string strCurentEPC = strEPC.Substring(iPos * 2 + 2, iEPCLen * 2);
                lock (_RFIDLock)
                {
                    if (!detectedEPC.ContainsKey(strCurentEPC))
                    {
                        detectedEPC.Add(strCurentEPC, bytConAdr.ToString());
                    }
                    else
                    {
                        detectedEPC[strCurentEPC] = bytConAdr.ToString();
                    }
                }
                iPos += iEPCLen + 1;
            }
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
                sb.Append(Convert.ToString(b, 16).PadLeft(2, '0'));
            return sb.ToString().ToUpper();
        }

    }
}
