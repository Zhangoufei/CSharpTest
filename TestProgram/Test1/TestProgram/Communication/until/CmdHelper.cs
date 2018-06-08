using SerialUtil;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace SerialUtil.until
{
    public static class CmdHelper
    {
        private static readonly byte[] Header = new byte[] { 0xaa, 0xbb, };
        private static readonly byte[] Ender = new byte[] { 0xcc, 0xdd };
        private static readonly Int32 LenOfDataLen = 1;
        private static readonly Int32 LenOfDataCheck = 2;
        static readonly ByteOrder ByteOrder = ByteOrder.BigEndian;

        public static byte[] GenerateQueryCmd(Int32 deviceId)
        {
            return GeneratePcCmd(deviceId, new byte[] {0x00}, 0, 1, Cmd.Query);
        }

        public static byte[] GenerateVerifyResultCmd(Int32 deviceId, Boolean verifySuccess)
        {
            var verifyData = verifySuccess ? new byte[] { 1 } : new byte[] { 0xff };

            return GeneratePcCmd(deviceId, verifyData, 0, verifyData.Length, Cmd.VerifyIdResult);
        }

        public static byte[] GenerateIdleCmd(Int32 deviceId, DeviceState state)
        {
            return GenerateDeviceCmd(deviceId, null, 0, 0, Cmd.Idle, state);
        }

        public static byte[] GenerateVerifyCmd(Int32 deviceId, string uid)
        {
            var uidBytes = UidHelper.ToBytes(uid);
            return GenerateDeviceCmd(deviceId, uidBytes, 0, uidBytes.Length, Cmd.VerifyId, DeviceState.Examming);
        }

        public static byte[] GenerateExamCommitCmd(Int32 deviceId, object answer)
        {
          
            string temp = "123412312341232210";
            byte[] cmdData = UidHelper.ToBytes(temp);


            return GenerateDeviceCmd(deviceId, cmdData, 0, cmdData.Length, Cmd.CommitExam, DeviceState.Validating);
        }

        /// <summary>
        /// 解析下位机命令
        /// </summary>
        /// <param name="response">命令数据</param>
        /// <param name="cmdInfo">解析后的命令信息</param>
        /// <param name="parsedHeadIndex">解析出的头索引</param>
        /// <param name="parsingIndex">解析进行到的位置的索引</param>
        /// <returns></returns>
        public static CmdParseResult ParseDeviceCmd(byte[] response, out CmdInfo cmdInfo, out Int32 parsedHeadIndex, out Int32 parsingIndex)
        {
            cmdInfo = null;
            parsingIndex = 0;

            var result = CmdParseResult.IncompleteCmd;

            var headerIndex = ByteHelper.LastIndexOf(response, Header);

            parsedHeadIndex = headerIndex;

            //判断命令是否完整
            var currentCmdLen = response.Length - headerIndex;

            //设备状态是否合法
            var offsetCmd = Header.Length + 1 + 1;
            var stateEnable = currentCmdLen >= offsetCmd;

            if (!stateEnable) goto Exit;

            parsingIndex = headerIndex + offsetCmd;

            var state = response[parsingIndex - 1];

            if (!Enum.IsDefined(typeof(DeviceState), state))
            {
                //设备状态不合法
                Debug.WriteLine("");
                result = CmdParseResult.Fail;
                goto Exit;
            }

            var offsetDataLen = Header.Length + 1 + 1 + 1;
            var cmdEnable = currentCmdLen >= offsetDataLen;

            if (!cmdEnable) goto Exit;

            var deviceId = response[Header.Length];

            parsingIndex = headerIndex + offsetDataLen;

            var cmd = response[parsingIndex - 1];
            if (!Enum.IsDefined(typeof(Cmd), cmd))
            {
                //命令号不合法
                Debug.WriteLine("");
                result = CmdParseResult.Fail;
                goto Exit;
            }

            var offsetData = Header.Length + 1 + 1 + 1 + 1;
            var dataLenEnable = currentCmdLen >= offsetData;

            //数据长度可用
            if (!dataLenEnable) goto Exit;

            var dataLen = response[headerIndex + offsetData - 1];

            var dataEnable = currentCmdLen >= offsetData + dataLen;
            //数据可用
            if (!dataEnable) goto Exit;

            var checkEnable = currentCmdLen >= offsetData + dataLen + LenOfDataCheck;

            if (!checkEnable) goto Exit;
            //判断校验
            parsingIndex = headerIndex + offsetData + dataLen;
            var check = ByteHelper.BytesToInt(response, parsingIndex, LenOfDataCheck, ByteOrder);
            var lenOfNeedCheck = 1 + 1 + 1 + 1 + dataLen;

            parsingIndex += LenOfDataCheck;
            int checkValue = GetDataCheck(response, headerIndex + Header.Length, lenOfNeedCheck);
            if (check != checkValue)
            {
                //校验不合法
                result = CmdParseResult.Fail;
                goto Exit;
            }

            var enderEnable = currentCmdLen >= offsetData + dataLen + LenOfDataCheck + Ender.Length;

            if (!enderEnable) goto Exit;

            //判断Ender是否正确
            if (!ByteHelper.SequenceEqual(response, parsingIndex, Ender, null))
            {
                result = CmdParseResult.Fail;
                parsingIndex += Ender.Length;
                goto Exit;
            }

            parsingIndex += Ender.Length;

            result = CmdParseResult.Ok;

            var data = new byte[dataLen];

            if (dataLen != 0)
            {
                Buffer.BlockCopy(response, headerIndex + offsetData, data, 0, dataLen);
            }

            cmdInfo = new CmdInfo
            {
                DeviceId = deviceId,
                State = (DeviceState)state,
                Cmd = (Cmd)cmd,
                Data = data
            };

            Exit:
            return result;
        }

        /// <summary>
        /// 解析上位机命令
        /// </summary>
        /// <param name="response">命令数据</param>
        /// <param name="cmdInfo">解析后的命令信息</param>
        /// <param name="parsedHeadIndex">解析出的头索引</param>
        /// <param name="parsingIndex">解析进行到的位置的索引</param>
        /// <returns></returns>
        public static CmdParseResult ParsePcCmd(byte[] response, out CmdInfo cmdInfo, out Int32 parsedHeadIndex, out Int32 parsingIndex)
        {
            cmdInfo = null;
            parsingIndex = 0;

            var result = CmdParseResult.IncompleteCmd;

            var headerIndex = ByteHelper.LastIndexOf(response, Header);

            parsedHeadIndex = headerIndex;

            //判断命令是否完整
            var currentCmdLen = response.Length - headerIndex;

            ////设备状态是否合法
            //var offsetCmd = Header.Length + 1 + 1;
            //var stateEnable = currentCmdLen >= offsetCmd;

            //if (!stateEnable) goto Exit;

            //parsingIndex = headerIndex + offsetCmd;

            //var state = response[parsingIndex - 1];

            //if (!Enum.IsDefined(typeof(DeviceState), state))
            //{
            //    //设备状态不合法
            //    Debug.WriteLine("");
            //    result = CmdParseResult.Fail;
            //    goto Exit;
            //}

            var offsetDataLen = Header.Length + 1 + 1;
            var cmdEnable = currentCmdLen >= offsetDataLen;

            if (!cmdEnable) goto Exit;

            var deviceId = response[Header.Length];

            parsingIndex = headerIndex + offsetDataLen;

            var cmd = response[parsingIndex - 1];

            if (!Enum.IsDefined(typeof(Cmd), cmd))
            {
                //命令号不合法
                Debug.WriteLine("");
                result = CmdParseResult.Fail;
                goto Exit;
            }


            var offsetData = Header.Length + 1 + 1 + 1;
            var dataLenEnable = currentCmdLen >= offsetData;

            //数据长度可用
            if (!dataLenEnable) goto Exit;

            var dataLen = response[headerIndex + offsetData - 1];

            var dataEnable = currentCmdLen >= offsetData + dataLen;
            //数据可用
            if (!dataEnable) goto Exit;

            var checkEnable = currentCmdLen >= offsetData + dataLen + LenOfDataCheck;

            if (!checkEnable) goto Exit;
            //判断校验
            parsingIndex = headerIndex + offsetData + dataLen;
            var check = ByteHelper.BytesToInt(response, parsingIndex, LenOfDataCheck, ByteOrder);
            var lenOfNeedCheck = 1 + 1 + 1 + dataLen;

            parsingIndex += LenOfDataCheck;

            if (check != GetDataCheck(response, headerIndex + Header.Length, lenOfNeedCheck))
            {
                //校验不合法
                result = CmdParseResult.Fail;
                goto Exit;
            }

            var enderEnable = currentCmdLen >= offsetData + dataLen + LenOfDataCheck + Ender.Length;

            if (!enderEnable) goto Exit;

            //判断Ender是否正确
            if (!ByteHelper.SequenceEqual(response, parsingIndex, Ender, null))
            {
                result = CmdParseResult.Fail;
                parsingIndex += Ender.Length;
                goto Exit;
            }

            parsingIndex += Ender.Length;

            result = CmdParseResult.Ok;

            var data = new byte[dataLen];

            if (dataLen != 0)
            {
                Buffer.BlockCopy(response, headerIndex + offsetData, data, 0, dataLen);
            }

            cmdInfo = new CmdInfo
            {
                DeviceId = deviceId,
                Cmd = (Cmd)cmd,
                Data = data
            };

            Exit:
            return result;
        }

        private static byte[] GeneratePcCmd(Int32 deviceId, byte[] cmdData, Int32 startIndex, Int32 len, Cmd cmd)
        {
            List<byte> result = new List<byte>();

            result.AddRange(Header);

            byte[] body = GetPcCmdBody(deviceId, cmdData, startIndex, len, cmd);

            result.AddRange(body);
            result.AddRange(Ender);

            return result.ToArray();
        }
        private static byte[] GenerateDeviceCmd(Int32 deviceId, byte[] cmdData, Int32 startIndex, Int32 len, Cmd cmd, DeviceState state)
        {
            List<byte> result = new List<byte>();

            result.AddRange(Header);

            byte[] body = GetDeviceCmdBody(deviceId, state, cmdData, startIndex, len, cmd);

            result.AddRange(body);
            result.AddRange(Ender);

            return result.ToArray();
        }
        static byte[] GetPcCmdBody(Int32 deviceId, byte[] cmdData, Int32 startIndex, Int32 len, Cmd cmd)
        {
            List<byte> result = new List<byte> { (byte)deviceId, (byte)cmd, };

            var dataLen = null == cmdData ? 0 : len;

            byte[] dataLenBts = ByteHelper.IntToBytes(dataLen, LenOfDataLen, ByteOrder);

            result.AddRange(dataLenBts);

            if (cmdData != null)
            {
                for (int i = startIndex; i < dataLen; i++)
                {
                    result.Add(cmdData[i]);
                }
            }

            Int32 dataCheck = GetDataCheck(result, 0, result.Count);

            byte[] checkBts = ByteHelper.IntToBytes(dataCheck, LenOfDataCheck, ByteOrder);

            result.AddRange(checkBts);

            return result.ToArray();
        }
        static byte[] GetDeviceCmdBody(Int32 deviceId, DeviceState state, byte[] cmdData, Int32 startIndex, Int32 len, Cmd cmd)
        {
            List<byte> result = new List<byte> { (byte)deviceId, (byte)state, (byte)cmd, };

            var dataLen = null == cmdData ? 0 : len;

            byte[] dataLenBts = ByteHelper.IntToBytes(dataLen, LenOfDataLen, ByteOrder);

            result.AddRange(dataLenBts);

            if (cmdData != null)
            {
                for (int i = startIndex; i < dataLen; i++)
                {
                    result.Add(cmdData[i]);
                }
            }

            Int32 dataCheck = GetDataCheck(result, 0, result.Count);

            byte[] checkBts = ByteHelper.IntToBytes(dataCheck, LenOfDataCheck, ByteOrder);

            result.AddRange(checkBts);

            return result.ToArray();
        }

        private static Int32 GetDataCheck(IList<byte> source, Int32 begin, Int32 length)
        {
            Int32 check = 0;

            for (Int32 i = begin; i < begin + length; i++)
            {
                check += source[i];
            }
            //check = ~check + 1;

            return check;
        }
    }
}
