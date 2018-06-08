using SerialUtil;
using SerialUtil.Serial;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SerialUtil.until;

namespace TestDevice
{
    internal class SerialMessageHandler
    {
        public static readonly byte[] Header = { 0xaa, 0xbb };

        public static event Action<string> CheckSuccess;
        public static void Handle(byte[] msg)
        {
            if (msg != null)
            {
                Console.WriteLine(BitConverter.ToString(msg));

                SerialDataHandler.ReceiveData(msg, SerialParseProc);
            }
        }

        public static void CheckDeviceState(string temp)
        {
            CheckSuccess?.Invoke(temp);
        }


        private static CmdParseResult SerialParseProc(IList<byte> buffer, out Int32 parsedHeaderIndex,
            out Int32 parsingIndex)
        {
            parsedHeaderIndex = 0;
            parsingIndex = 0;
            CmdInfo cmdInfo;
            //接收下位机发送过来的数据  并解析为对象
            var parseResult = CmdHelper.ParseDeviceCmd(buffer.ToArray(), out cmdInfo, out parsedHeaderIndex, out parsingIndex);

            //接收到信息  //验证身份证信息
            if (cmdInfo.Cmd == Cmd.VerifyId && parseResult== CmdParseResult.Ok)
            {
                //发送成功标记
                CheckDeviceState("OK");
            }
            else
            {
                CheckDeviceState("Fail");
            }
            if (cmdInfo.Cmd == Cmd.CommitExam)
            {
                CheckDeviceState("CommitExam");
            }

            return parseResult;
        }
    }
}
