using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SerialUtil.until
{
    public enum DeviceState:byte
    {
        Validating,   //空闲中
        Examming,       //验证身份中 考试中
        Unused,         //考试中 不能使用这个设备
    }
}
