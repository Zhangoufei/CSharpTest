using System;

namespace Simulation
{
    [Flags]
    enum DeviceState
    {
        /// <summary>
        /// 电源线
        /// </summary>
        PowerLine = 0x8000,
        /// <summary>
        /// 二次线
        /// </summary>
        SecondaryLine=0x4000,
        /// <summary>
        /// 电源开关
        /// </summary>
        PowerSwitch=0x2000,
        /// <summary>
        /// 排气阀
        /// </summary>
        ExhaustValve=0x1000,
        /// <summary>
        /// 氧气瓶阀
        /// </summary>
        OxygenBottleValve=0x800,
        /// <summary>
        /// 乙炔瓶阀门
        /// </summary>
        AcetyleneBottleValve=0x400,
        /// <summary>
        /// 二氧化碳瓶阀门
        /// </summary>
        CarbonDioxideBottleValve=0x200,
        /// <summary>
        /// 氩气瓶阀门
        /// </summary>
        ArgonGasCylinderValve=0x100,
        /// <summary>
        /// 油桶
        /// </summary>
        OilDrum=0x80,
        /// <summary>
        /// 水桶
        /// </summary>
        Bucket=0x40
    }
}
