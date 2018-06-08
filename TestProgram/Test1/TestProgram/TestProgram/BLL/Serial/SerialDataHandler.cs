using System;
using System.Collections.Generic;

namespace TestProgram.BLL.Serial
{
    public delegate CmdParseResult SerialParseFunc(IList<byte> buffer, out Int32 parsedHeaderIndex, out Int32 parsingIndex);

    public static class SerialDataHandler
    {
        private static readonly List<byte> ReadBuffer = new List<byte>();

        private static readonly object SyncRoot = new object();

        //public static event Action<object> CmdParsed;

        public static void ReceiveData(byte[] data, SerialParseFunc serialParseFunc)
        {
            lock (SyncRoot)
            {
               // string temp = SerialClass.byteToHexStr(data);

                ReadBuffer.AddRange(data);
                
                ParseCmd(serialParseFunc);
                ClearBuffer();
                //if (ParseCmd(out cmdInfo, serialParseFunc))
                //{
                //    //OnCmdParsed(cmdInfo);
                //}
            }
        }

        public static void ClearBuffer()
        {
            lock (SyncRoot)
            {
                ReadBuffer.Clear();
            }
        }

        /// <summary>
        /// 解析命令
        /// </summary>
        /// <returns>是否解析出一条正确命令</returns>
        static Boolean ParseCmd(SerialParseFunc serialParseFunc)
        {
            Int32 parsedHeaderIndex;
            Int32 parsingIndex;

            var parseResult = serialParseFunc(ReadBuffer.ToArray(), out parsedHeaderIndex, out parsingIndex);

            Boolean result = false;

            switch (parseResult)
            {
                case CmdParseResult.Fail:
                    //清除解析后的数据
                    if (parsingIndex > 0)
                    {
                        ReadBuffer.RemoveRange(0, parsingIndex);
                    }
                    break;
                case CmdParseResult.IncompleteCmd:
                    //清除协议头之前的数据,加快解析
                    if (parsedHeaderIndex > 0)
                    {
                        ReadBuffer.RemoveRange(0, parsedHeaderIndex);
                    }
                    break;
                case CmdParseResult.Ok:
                    result = true;
                    //清除解析后的数据
                    if (parsingIndex > 0)
                    {
                        ReadBuffer.RemoveRange(0, parsingIndex);
                    }
                    break;
            }

            return result;
        }

        //static void OnCmdParsed(object cmdInfo)
        //{
        //    var cache = CmdParsed;

        //    if (cache != null)
        //    {
        //        cache(cmdInfo);
        //    }
        //}

    }
}
