using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProxyCommon
{
    public class Item
    {
        //步骤分值
        public decimal Score { get; set; }

        //步骤名称
        public string Name { get; set; }

        //序号
        public string SN { get; set; }

        //操作类别 0:无操作; 1:PLC; 2:微机保护; 3:读卡器;
        public string Operate { get; set; }

        //PLC的Group名称; 微机保护的遥信位;
        public string GroupName { get; set; }

        //PLC的Item名称; 微机保护遥信的Bit位
        public string ItemID { get; set; }

        //步骤正确操作值
        public string DataValue { get; set; }

        //读卡器地址
        public string ReaderAddress { get; set; }

        //RFID标签的EPC号
        public string EPC { get; set; }

        //标签是否应该存在
        public bool Exist { get; set; }

        public bool NeedReset { get; set; }

        public Item()
        { }
    }
}
