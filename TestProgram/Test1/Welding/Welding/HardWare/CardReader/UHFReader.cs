using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using ReaderB;

namespace HardWare.CardReader
{
    public class UHFReader
    {
        private static byte[] buffer = new byte[300];
        private static Dictionary<Int32, SerialMapValue> dictionary = new Dictionary<int,SerialMapValue>(); 
        /// <summary>
        /// 串口号映射到多台设备的映射信息
        /// </summary>
        class SerialMapValue
        {
            public List<Int32> DeviceIdList { get; set; }
            public Int32 Handle { get; set; }
            public byte BaudRateCode { get; set; }
        }
        public static Boolean Open(Int32 serialPort, byte deviceId, byte baudRateCode)
        {
            Int32 handle = 0;

            var result = StaticClassReaderB.OpenComPort(serialPort, ref deviceId, baudRateCode, ref handle);
            Log.Logger.Debug("serialPort:"+ serialPort+ "deviceId:"+ deviceId+ "baudRateCode:"+ baudRateCode+ "handle:"+ handle);
            DictionaryAdd(serialPort, deviceId, baudRateCode, handle);

            var success = result == 0;

            if (!success)
            {
                var msg = $"UHF端口打开失败! {Environment.NewLine}serialPort:[{serialPort.ToString()}] deviceId:[{deviceId.ToString()}]"
                          + $" baudRateCode:[{baudRateCode.ToString()}] OpenResult:[{result.ToString()}]";
                Log.Logger.Error(msg);
            }

            return success;
        }

        public static List<string> ReadLabel(byte deviceId)
        {
            var labels = new List<string>();

            byte addrTID = 0;
            byte lenOfTID = 0;
            byte flagTID = 0;
            int dataLen = 0;
            int labelCount = 0;

            var value = GetMapItemById(deviceId);

            if (value == null)
            {
                throw new InvalidDataException("无法根据UHF读卡器id获取到相应的值");
            }

            Int32 handle = value.Handle;

            //读标签
            StaticClassReaderB.Inventory_G2(ref deviceId, addrTID, lenOfTID, flagTID, buffer, ref dataLen, ref labelCount, handle);
            //假如buffer太小，则增大buffer
            while (dataLen > buffer.Length)
            {
                buffer = new byte[dataLen];

                StaticClassReaderB.Inventory_G2(ref deviceId, addrTID, lenOfTID, flagTID, buffer, ref dataLen, ref labelCount, handle);
            }
            if (labelCount > 0)
            {
                var pos = 0;
                
                for (int i = 0; i < labelCount; i++)
                {
                    var len = buffer[pos++];
                    labels.Add(BitConverter.ToString(buffer, pos, len).Replace("-", ""));
                    pos += len;
                }
            }
            return labels;
        }

        public static void Close(byte deviceId)
        {
            var value = GetMapItemById(deviceId);

            if (value != null)
            {
                StaticClassReaderB.CloseSpecComPort(value.Handle);

                foreach (var key in dictionary.Keys.Where(key => dictionary[key] == value))
                {
                    dictionary.Remove(key);break;
                }
            }
        }

        public static void Bind(byte deviceId, Int32 serialPort)
        {
            var value = dictionary[serialPort];

            if (value != null)
            {
                if (!value.DeviceIdList.Contains(deviceId))
                {
                    value.DeviceIdList.Add(deviceId);
                }
            }
        }

        static void DictionaryAdd(Int32 serialPort, byte deviceId, byte baudRateCode, Int32 handle)
        {
            dictionary.Add(serialPort, new SerialMapValue
            {
                BaudRateCode = baudRateCode,
                DeviceIdList = new List<int>() { deviceId},
                Handle = handle
            });
        }

        static SerialMapValue GetMapItemById(Int32 deviceId)
        {
            return dictionary.Values.FirstOrDefault(value => value.DeviceIdList.Contains(deviceId));
        }
    }
}
