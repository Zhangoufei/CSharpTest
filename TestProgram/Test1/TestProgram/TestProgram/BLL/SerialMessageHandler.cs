using System;
using System.Collections.Generic;
using System.Linq;
using TestProgram;
using TestProgram.BLL.Serial;

namespace Welding
{
    public class SerialMessageHandler
    {
        private static readonly byte[] Header = { 0x7f, 0x8f };
        //private static byte[] Ender = { 0x81, 0x82 };
        public static event Action<DeviceState> DeviceStateChanged;

        public static void Handle(byte[] msg)
        {
            if (msg != null)
            {
                // Logger.Debug("msg不为空");
                SerialDataHandler.ReceiveData(msg, SerialParseProc);
            }
            else
            {
                //  Logger.Debug("msg为空");
            }
        }

      

        private static CmdParseResult SerialParseProc(IList<byte> buffer, out Int32 parsedHeaderIndex, out Int32 parsingIndex)
        {
            parsedHeaderIndex = 0;
            parsingIndex = 0;

            var temp = buffer.ToList();

            //解析数据 //至少要包含头（1字节）+长度（1字节）+校验（1字节
            while (temp.Count >= 10)
            {
                int index = temp.IndexOf(Header[0]);
                if (index >0 && temp[index] == Header[0] && temp[index+9] == Header[1])
                {
                    if (temp[1] == 0x01)
                    {
                        One = true;
                    }
                    else
                    {
                        One = false;
                    }
                    if (temp[2] == 0x01)
                    {
                        Two = true;
                    }
                    else
                    {
                        Two = false;
                    }
                    if (temp[3] == 0x01)
                    {
                        Three = true;
                    }
                    else
                    {
                        Three = false;
                    }
                    if (temp[4] == 0x01)
                    {
                        Four = true;
                    }
                    else
                    {
                        Four = false;
                    }
                    if (temp[5] == 0x01)
                    {
                        Five = true;
                    }
                    else
                    {
                        Five = false;
                    }
                    if (temp[6] == 0x01)
                    {
                        Six = true;
                    }
                    else
                    {
                        Six = false;
                    }
                    if (temp[7] == 0x01)
                    {
                        Seven = true;
                    }
                    else
                    {
                        Seven = false;
                    }
                    if (temp[8] == 0x01)
                    {
                        Eight = true;
                    }
                    else
                    {
                        Eight = false;
                    }
                    break;
                }
                else
                {
                    temp.RemoveRange(0, temp.IndexOf(Header[0])+10);  //丢弃一部分数据，重新判断
                }
            }
            CmdParseResult result = CmdParseResult.IncompleteCmd;
            return result;
        }

        private static DeviceState GetDeviceState(IList<byte> buffer, Int32 index)
        {
            var state = BytesHelper.BytesToInt(buffer, index, 2, DataType.BigEndian);

            return (DeviceState)state;
        }

        static void OnDeviceStateChanged(DeviceState state)
        {
            var cache = DeviceStateChanged;

            if (cache != null)
            {
                cache.Invoke(state);
            }
        }

        private static bool one;

        private static bool two;

        private static bool three;

        private static bool four;

        private static bool five;

        private static bool six;

        private static bool seven;


        private static bool eight;

        public static bool Eight
        {
            get
            {
                return eight;
            }

            set
            {
                eight = value;
            }
        }

        public static bool One
        {
            get
            {
                return one;
            }

            set
            {
                one = value;
            }
        }

        public static bool Two
        {
            get
            {
                return two;
            }

            set
            {
                two = value;
            }
        }

        public static bool Three
        {
            get
            {
                return three;
            }

            set
            {
                three = value;
            }
        }

        public static bool Four
        {
            get
            {
                return four;
            }

            set
            {
                four = value;
            }
        }

        public static bool Five
        {
            get
            {
                return five;
            }

            set
            {
                five = value;
            }
        }

        public static bool Six
        {
            get
            {
                return six;
            }

            set
            {
                six = value;
            }
        }

        public static bool Seven
        {
            get
            {
                return seven;
            }

            set
            {
                seven = value;
            }
        }

    }



    [Flags]
    public enum DeviceState
    {
        None = 0x0,
        /// <summary>
        /// 电源线
        /// </summary>
        PowerLine = 0x8000,
        /// <summary>
        /// 二次线
        /// </summary>
        SecondaryLine = 0x4000,
        /// <summary>
        /// 电源开关
        /// </summary>
        PowerSwitch = 0x2000,
        /// <summary>
        /// 排气阀
        /// </summary>
        ExhaustValve = 0x1000,
        /// <summary>
        /// 氧气瓶阀
        /// </summary>
        OxygenBottleValve = 0x800,
        /// <summary>
        /// 乙炔瓶阀门
        /// </summary>
        AcetyleneBottleValve = 0x400,
        /// <summary>
        /// 二氧化碳瓶阀门
        /// </summary>
        CarbonDioxideBottleValve = 0x200,
        /// <summary>
        /// 氩气瓶阀门
        /// </summary>
        ArgonGasCylinderValve = 0x100,
        /// <summary>
        /// 油桶
        /// </summary>
        OilDrum = 0x80,
        /// <summary>
        /// 水桶
        /// </summary>
        Bucket = 0x40
    }
}
