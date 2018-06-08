using System;
using System.Collections.Generic;
using System.Linq;
using Communication;
using Communication.Serial;
using Log;

namespace Welding
{
    class SerialMessageHandler
    {
        private static readonly byte[] Header = { 0x7f, 0x80 };
        //private static byte[] Ender = { 0x81, 0x82 };
        public static event Action<DeviceState> DeviceStateChanged;

        public static void Handle(byte[] msg)
        {
            if (msg != null)
            {
                Logger.Debug("msg不为空");
                SerialDataHandler.ReceiveData(msg, SerialParseProc);
            }
            else
            {
                Logger.Debug("msg为空");
            }
        }

        private static CmdParseResult SerialParseProc(IList<byte> buffer, out Int32 parsedHeaderIndex, out Int32 parsingIndex)
        {
            parsedHeaderIndex = 0;
            parsingIndex = 0;

            CmdParseResult result = CmdParseResult.IncompleteCmd;

            var index = BytesHelper.IndexOf(buffer, Header);
            if (buffer.Count > 0)
            {
                Logger.Debug("inderx:" + index);
                for (int i = 0; i < buffer.Count; i++)
                {
                    Logger.Debug("buffer:" + buffer[i].ToString());
                }
            }

            if (index >= 0)
            {
                parsedHeaderIndex = index;

                Boolean completeCmd = (buffer.Count - index) >= (Header.Length + 4);
                //协议数据是完整的
                if (completeCmd)
                {
                    parsingIndex = index + Header.Length + 4;

                    var checkIndex = parsingIndex - 2;

                    var checkValid = buffer[checkIndex] == 0x81 && buffer[checkIndex + 1] == 0x82;
                    Logger.Debug(buffer[checkIndex].ToString() + "@@@" + buffer[checkIndex + 1]);
                    if (checkValid)
                    {
                        result = CmdParseResult.Ok;
                        var msg = "检测到有效的串口数据:{Environment.NewLine}{BitConverter.ToString(buffer.ToArray())}";

                        Logger.Debug(msg);

                        var state = GetDeviceState(buffer, checkIndex - 2);

                        OnDeviceStateChanged(state);
                    }
                    else
                    {
                        result = CmdParseResult.Fail;

                        var msg = "检测到无效的串口数据:{Environment.NewLine}{BitConverter.ToString(buffer.ToArray())}";

                        Logger.Debug(msg);
                    }

                }
                else
                {
                    Logger.Debug("协议是不完整的");
                }
            }
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
    }
}
