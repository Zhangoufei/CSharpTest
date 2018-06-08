using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Welding
{
    /// <summary>
    /// 焊接类型
    /// </summary>
    public enum WeldingType:byte
    {
        /// <summary>
        /// CO2保护焊
        /// </summary>
        [Description("CO2保护焊")]
        CO2ProtectionWelding =0,

        /// <summary>
        /// 焊条电弧焊
        /// </summary>
        [Description("焊条电弧焊")]
        ShieldedMetalArcWelding=1,

        /// <summary>
        /// 氩弧焊
        /// </summary>
        [Description("氩弧焊")]
        ArgonArcWelding = 2,
    }
}
