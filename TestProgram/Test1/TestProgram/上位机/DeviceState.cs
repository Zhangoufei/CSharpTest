using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace 上位机
{
    public enum DeviceState : byte
    {
        Validating,
        Examming,
        Unused,
    }


    public class DeviceInfo
    {
        public Int32 DeviceId { get; set; }
        public DeviceState State { get; set; }
        public string UserId { get; set; }
    }
}
