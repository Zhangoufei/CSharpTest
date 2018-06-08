using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using ReaderB;

namespace HardWare
{
    public class RfidUtility
    {
        private int portOpenHandle = 0;

        public RfidUtility(byte readAdr)
        {
            this.ReadAdr = readAdr;
            listAddr.Add(readAdr);
        }


        #region 
        //写一个事件通知外面的
        public static event Action<Dictionary<string, string>> RfidValue;

        static void OnCallBack(Dictionary<string, string> rfidValue)
        {
            if (rfidValue != null)
            {
                RfidValue(rfidValue);
            }
        }

        //如果打开成功了，通知外面
        //写一个事件通知外面的
        public static event Action<bool> RfidOpenResult;
        static void OnRfidOpenReslt(bool result)
        {
            RfidOpenResult(result);
        }
        #endregion


        private byte ReadAdr = 0;
        /// <summary>
        /// Rfid读卡器打开 
        /// </summary>
        /// <param name="comPort">端口号</param>
        /// <param name="spanTimer">延迟时间.安装毫秒计算</param>
        public void OpenRfid(int comPort, int spanTimer)
        {
            var detectTimer = new System.Timers.Timer(spanTimer);  //默认6s
            detectTimer.Elapsed += (sender, args) =>
            {
                detectTimer.Dispose();
                ThreadPool.QueueUserWorkItem(openRf, comPort);  //等待一段时间再开启
            };
            detectTimer.Start();

        }
        private void openRf(object obj)
        {
            byte bytBaud = 0x5;

            int iResult = StaticClassReaderB.OpenComPort(int.Parse(obj.ToString()), ref ReadAdr, bytBaud, ref portOpenHandle);
            if (iResult == 0)
            {
                TimerSpanTick = false;
                OnRfidOpenReslt(true);
                Inventory(); //开始检测标签
            }
            OnRfidOpenReslt(false);
        }


        readonly List<byte> listAddr = new List<byte>();

        private bool addlist;
        /// <summary>
        /// 添加设备地址
        /// </summary>
        /// <param name="addr"></param>
        public void AddList(byte addr)
        {
            if (!listAddr.Contains(addr))
            {
                addlist = true;
                listAddr.Add(addr);  //如果不包含添加这个地址
                addlist = false;
            }
        }

        private object objectInvent = new object();
        /// <summary>
        /// 检测标签
        /// </summary>
        private bool Inventory()
        {
            bool result = false;
            while (bKeepDetection)
            {
                if (listAddr.Count > 0)
                {

                    for (int i = 0; i < listAddr.Count; i++)
                    {
                        GetEpcNumsByAdr(listAddr[i]);
                    }
                    Thread.Sleep(20);
                }
            }
            result = true;
            return result;
        }

        private static bool TimerSpanTick;
        /// <summary>
        /// 根据读卡器地址读取EPC标签
        /// </summary>
        /// <param name="bytAdr"></param>
        private void GetEpcNumsByAdr(byte bytAdr)
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

                if (TimerSpanEnable && !TimerSpanTick)
                {
                    TimerSpanTick = true;
                    InitDetectTimer(timerSpan);    //是否使用记时读取 
                }
                ProcessEPCNum(EPC, Totallen, CardNum, bytConAdr);
            }
        }

        private int TimerTick;
        private void InitDetectTimer(double timeSpan)
        {
            TimerTick = 0;
            var detectTimer = new System.Timers.Timer(timeSpan);  //默认6s
            detectTimer.Elapsed += (sender, args) =>
            {
                TimerTick++;
                bKeepDetection = false;   //while循环的开关

                detectTimer.Dispose();


                OnCallBack(detectedEPC);  //添加到事件中  添加一次 为后台计算成绩
                CloseRfid();  //关闭读卡器
            };
            detectTimer.AutoReset = true;
            detectTimer.Start();
        }

        /// <summary>
        /// 检测到的EPC标签
        /// </summary>
        private Dictionary<string, string> detectedEPC = new Dictionary<string, string>();
        /// <summary>
        /// 是否继续检测电子标签
        /// </summary>
        private bool bKeepDetection = true;
        /// <summary>
        /// 开始检测状态下未检测到标签的次数，以此参数来控制是否停止读取标签，默认阈值为10
        /// </summary>
        private int notDetectionEPC = 0;

        /// <summary>
        /// 检测状态;0:未开始检测;1:开始检测，以此参数来确认考生是否进入检测门范围；
        /// </summary>
        private int detectionState = 0;

        private static object _RFIDLock = new object();

        private double timerSpan;

        private Boolean DetectedTimerCreated { get; set; }

        private bool timerSpanEnable;

        public double TimerSpan
        {
            get
            {
                return timerSpan;
            }

            set
            {
                timerSpan = value;
            }
        }

        public bool TimerSpanEnable
        {
            get
            {
                return timerSpanEnable;
            }

            set
            {
                timerSpanEnable = value;
            }
        }



        /// <summary>
        /// 计算EPC号
        /// </summary>
        /// <param name="EPC"></param>
        /// <param name="Totallen"></param>
        /// <param name="CardNum"></param>
        /// <param name="bytConAdr"></param>
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
                    //如果是使用定时读取信息  在关闭时候触发事件
                    if (!TimerSpanEnable)
                    {
                        OnCallBack(detectedEPC);  //添加到事件中
                    }

                }
                iPos += iEPCLen + 1;
            }
        }

        /// <summary>
        /// 关闭设备
        /// </summary>
        public void CloseRfid()
        {
            listAddr.Clear();
            detectedEPC.Clear();
            StaticClassReaderB.CloseSpecComPort(portOpenHandle);
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
