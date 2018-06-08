using SerialUtil;
using SerialUtil.Serial;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SerialUtil.until;

namespace 下位机模拟单片机数据
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
            var parseResult = CmdHelper.ParsePcCmd(buffer.ToArray(), out cmdInfo, out parsedHeaderIndex, out parsingIndex);

            //上位机查询 ，不做处理
            if (cmdInfo.Cmd == Cmd.Query)
            {

            }
            if (cmdInfo.Cmd == Cmd.VerifyIdResult)
            {
                CheckDeviceState("Examing");   //考试中
            }

            //接收到上位机发送的 信息了 验证结果  可以开始考试了
            if (parseResult == CmdParseResult.Ok)
            {
                //发送成功标记
                CheckDeviceState("OK");
            }
            else
            {
                CheckDeviceState("Fail");
            }

            return parseResult;
        }
    }
}
