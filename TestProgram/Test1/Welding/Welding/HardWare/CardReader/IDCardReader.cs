using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Log;

namespace HardWare.CardReader
{
    public class IDCardReader
    {
        static IDCard wrapper = new IDCard();

        public static event Action<IDCard> IdCardDetected;
        public static  Boolean Reading { get; set; }
        public static Boolean CanRead { get; set; }
        public static Boolean Open()
        {
            var value = wrapper.InitComm();
            var result = value == 1;

            if (!result)
            {
                Logger.Error($"身份证读卡器初始化失败:[{value.ToString()}]");
            }

            return result;
        }

        public static Boolean Read()
        {
            if (CanRead && !Reading)//可以读且没在读
            {
                Reading = true;
                ThreadPool.QueueUserWorkItem(ReadInternel);
                return true;//读取任务已成功添加
            }
            return false;//无法添加读取任务
        }

        public static Boolean Close()
        {
            return wrapper.CloseComm() == 1;
        }

        private static void ReadInternel(object state)
        {
            while (CanRead)
            {
                if (wrapper.ReadCardContent() != 2)
                {
                    Thread.Sleep(300);
                }
                else
                {
                    OnIdCardDetected();
                    break;
                }
            }
            Reading = false;//读取完成
        }

        private static void OnIdCardDetected()
        {
            var cache = IdCardDetected;

            if (cache != null)
            {
                cache(wrapper);
            }
        }
    }
}
