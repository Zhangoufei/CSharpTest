using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HardWare.CardReader;
using NUnit.Framework;
using ReaderB;

namespace HardWare
{
    [TestFixture]
    public class TestUHF
    {
        [Test]
        public void Test()
        {
            var serialPort = 3;
            byte deviceId = 1;
            byte baudRate = 5;
            var handle = 0;

            var result = StaticClassReaderB.OpenComPort(serialPort, ref deviceId, baudRate, ref handle);

            Console.WriteLine("open的返回值：{result.ToString()}");
            Console.WriteLine("设备id：{deviceId.ToString()}");
            Console.WriteLine("句柄：{handle.ToString()}");

            if (0 == result)
            {
                byte addrTID = 0;
                byte lenOfTID = 0;
                byte flagTID = 0;
                byte[] buffer = new byte[100];
                int dataLen = 0;
                int labelCount = 0;
                //读标签
                result = StaticClassReaderB.Inventory_G2(ref deviceId, addrTID, lenOfTID, flagTID, buffer, ref dataLen, ref labelCount, handle);

                Console.WriteLine("查询的返回值：{result.ToString()}");
                Console.WriteLine("labelCount：{labelCount.ToString()}");
                Console.WriteLine("dataLen：{dataLen.ToString()}");

                if (labelCount > 0)
                {
                    var pos = 0;
                    var data = buffer.Take(dataLen).ToArray();
                    var rawString = BitConverter.ToString(data).Replace("-", "");
                    for (int i = 0; i < labelCount; i++)
                    {
                        var len = data[pos];
                        var str = rawString.Substring(pos * 2 + 2, len * 2);
                        Console.WriteLine("标签：{str}");
                        pos += len + 1;
                    }
                }
            }
        }
        [Test]
        public void T2()
        {
            Console.WriteLine(Convert.ToString(1, 16).PadLeft(2, '0'));
        }
        [Test]
        public void T3()
        {
            byte deviceId = 1;
            if (UHFReader.Open(3, deviceId, 5))
            {
                Console.WriteLine("opened");
                foreach (var label in UHFReader.ReadLabel(deviceId))
                {
                    Console.WriteLine(label);
                }
                UHFReader.Close(deviceId);
            }
        }
    }
}
