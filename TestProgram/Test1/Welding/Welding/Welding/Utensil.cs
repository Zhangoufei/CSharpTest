using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Welding
{
    /// <summary>
    /// 用具类型
    /// </summary>
    public enum Utensil
    {
        /// <summary>
        /// 焊接工作服
        /// </summary>
        [Description("焊接工作服")]
        WeldingWorkClothes,
        /// <summary>
        /// 焊接防护鞋
        /// </summary>
        [Description("焊接防护鞋")]
        WeldingProtectiveShoes,
        /// <summary>
        /// 长袖牛皮手套
        /// </summary>
        [Description("长袖牛皮手套")]
        LongSleeveLeatherGloves,
        /// <summary>
        /// 手持式焊接面罩
        /// </summary>
        [Description("手持式焊接面罩")]
        HandGeldWeldingMask,
        /// <summary>
        /// 自吸式防护口罩
        /// </summary>
        [Description("自吸式防护口罩")]
        SelfSuctionTypeRespirator,
        /// <summary>
        /// 短袖羊皮手套
        /// </summary>
        [Description("短袖羊皮手套")]
        ShortSleevedSheepskinGloves,
        /// <summary>
        /// 头戴式自动变光面罩
        /// </summary>
        [Description("头戴式自动变光面罩")]
        HeadMountedAutomaticLightChangingFaceMask,
    }
}
