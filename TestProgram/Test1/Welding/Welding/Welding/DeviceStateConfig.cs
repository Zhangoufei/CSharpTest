using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using JAAJExamManagementSys;

namespace Welding
{
    class DeviceStateConfig
    {
        private static Dictionary<WeldingType, CheckConfig> CheckConfigs { get; set; }
        /// <summary>
        /// 加载配置
        /// </summary>
        public static void LoadConfig()
        {
            CheckConfigs = new Dictionary<WeldingType, CheckConfig>();

            var doc = new XmlDocument();

            doc.Load(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Check.xml"));

            if (doc.DocumentElement != null)
                foreach (XmlNode node in doc.DocumentElement.ChildNodes)
                {
                    var node1 = node["WeldingCheckConfig"];

                    if (node1 == null) continue;

                    var config = new CheckConfig
                    {
                        DressConfig = GetDressConfig(node["Dress"]),

                        WeldingCheckConfig = new WeldingCheckConfig
                        {
                            InspectionBeforeWelding = GetConfigHelper(node1, "InspectionBeforeWelding"),
                            SwitchOn = GetConfigHelper(node1, "SwitchOn"),
                            SwitchOff = GetConfigHelper(node1, "SwitchOff"),
                            CheckTrouble = GetConfigHelper(node1, "CheckTrouble"),
                            VirtualWeldingTotal = Decimal.Parse(node1.GetAttribute("VirtualWeldingTotal"))
                        }
                    };
                    CheckConfigs.Add((WeldingType)Enum.Parse(typeof(WeldingType), node.Name), config);
                }
        }
        /// <summary>
        /// 获取虚拟焊接成绩百分比
        /// </summary>
        /// <param name="weldingType"></param>
        /// <returns></returns>
        public static decimal GetVirtualWeldingScorePercent(WeldingType weldingType)
        {
            var total = CheckConfigs[weldingType].WeldingCheckConfig.VirtualWeldingTotal;

            if (weldingType == WeldingType.ArgonArcWelding)
            {
                return total / 90;
            }
            return total / 100;
        }
        /// <summary>
        /// 计算穿戴成绩
        /// </summary>
        /// <returns></returns>
        public static decimal CalcDressResult(
            Title dressReport,
            WeldingType weldingType,
            IEnumerable<Utensil> selectedUtensilList)
        {
            var config = CheckConfigs[weldingType].DressConfig;

            decimal total = 0;
            var configArray = config as DressItem[] ?? config.ToArray();//空间换时间

            Steps steps = new Steps();//加入计分报表
            dressReport.StepList.Add(steps);

            foreach (var utensil in selectedUtensilList)
            {
                var score = false;
                decimal stepScore = 0;

                foreach (var dressItem in configArray.Where(dressItem => dressItem.Utensil == utensil))
                {
                    score = true;//用具选择正确
                    stepScore = dressItem.Score;
                    break;
                }

                if (!score)//用具选择错误
                {
                    stepScore = -3;
                }

                //计分报表
                steps.StepList.Add(new Step
                {
                    StepName = EnumHelper.GetEnumDescription(utensil),
                    StepScore = stepScore
                });

                total += stepScore;
            }

            total = Math.Max(0, total);//不允许扣成负分

            dressReport.TitleScore = total;

            return total;
        }

        /// <summary>
        /// 计算焊前检查成绩
        /// </summary>
        /// <param name="steps"></param>
        /// <param name="weldingType"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        public static decimal CalcInspectionBeforeWeldingResult(Steps steps, WeldingType weldingType, DeviceState state)
        {
            var config = CheckConfigs[weldingType];
            return CalcWeldingResultHelper(steps, state, config.WeldingCheckConfig.InspectionBeforeWelding);
        }

        /// <summary>
        /// 计算推开关成绩
        /// </summary>
        /// <param name="steps"></param>
        /// <param name="weldingType"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        public static decimal CalcSwitchOnResult(Steps steps, WeldingType weldingType, DeviceState state)
        {
            var config = CheckConfigs[weldingType];
            return CalcWeldingResultHelper(steps, state, config.WeldingCheckConfig.SwitchOn);
        }

        /// <summary>
        /// 计算关开关成绩
        /// </summary>
        /// <param name="steps"></param>
        /// <param name="weldingType"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        public static decimal CalcSwitchOffResult(Steps steps, WeldingType weldingType, DeviceState state)
        {
            var config = CheckConfigs[weldingType];
            return CalcWeldingResultHelper(steps, state, config.WeldingCheckConfig.SwitchOff);
        }
        /// <summary>
        /// 计算检查隐患成绩
        /// </summary>
        /// <param name="steps"></param>
        /// <param name="weldingType"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        public static decimal CalcCheckTroubleResult(Steps steps, WeldingType weldingType, DeviceState state)
        {
            var config = CheckConfigs[weldingType];
            return CalcWeldingResultHelper(steps, state, config.WeldingCheckConfig.CheckTrouble);
        }
        /// <summary>
        /// 计算焊接子项分值辅助函数
        /// </summary>
        /// <param name="steps"></param>
        /// <param name="state"></param>
        /// <param name="checkItems"></param>
        /// <returns></returns>
        private static decimal CalcWeldingResultHelper(Steps steps, DeviceState state, IEnumerable<CheckItem> checkItems)
        {
            decimal total = 0;

            foreach (var checkItem in checkItems)
            {
                var score = ((checkItem.State & state) != 0) == checkItem.Enable;

                var value = score ? checkItem.Score : 0;

                steps.StepList.Add(new Step
                {
                    StepName = checkItem.Description,
                    StepScore = score ? checkItem.Score : 0,
                    StepDescription = score ? "是" : "否",
                });

                total += value;
            }
            return total;
        }

        private static IEnumerable<DressItem> GetDressConfig(XmlNode node)
        {
            return from XmlNode subNode in node.ChildNodes
                   let enableElem = subNode["Enable"]
                   let scoreElem = subNode["Score"]
                   select new DressItem()
                   {
                       Utensil = (Utensil)Enum.Parse(typeof(Utensil), subNode.Name),
                       Enable = enableElem != null && Boolean.Parse(enableElem.InnerText),
                       Score = Decimal.Parse(scoreElem.InnerText)
                   };
        }

        private static IEnumerable<CheckItem> GetConfigHelper(XmlNode node, string label)
        {
            var node1 = node[label];
            if (node1 != null)
                return from XmlNode subNode in node1.ChildNodes
                       let enableElem = subNode["Enable"]
                       let descriptionElem = subNode["Description"]
                       let scoreElem = subNode["Score"]
                       where descriptionElem != null
                       select new CheckItem
                       {
                           State = (DeviceState)Enum.Parse(typeof(DeviceState), subNode.Name),
                           Enable = enableElem != null && Boolean.Parse(enableElem.InnerText),
                           Description = descriptionElem.InnerText,
                           Score = Decimal.Parse(scoreElem.InnerText)
                       };
            return null;
        }

        class CheckConfig
        {
            public IEnumerable<DressItem> DressConfig { get; set; }
            public WeldingCheckConfig WeldingCheckConfig { get; set; }
        }
        /// <summary>
        /// 焊接检查配置
        /// </summary>
        class WeldingCheckConfig
        {
            /// <summary>
            /// 焊前检查项集合
            /// </summary>
            public IEnumerable<CheckItem> InspectionBeforeWelding { get; set; }
            /// <summary>
            /// 推开关项集合
            /// </summary>
            public IEnumerable<CheckItem> SwitchOn { get; set; }
            /// <summary>
            /// 关开关项集合
            /// </summary>
            public IEnumerable<CheckItem> SwitchOff { get; set; }
            /// <summary>
            /// 检查隐患集合
            /// </summary>
            public IEnumerable<CheckItem> CheckTrouble { get; set; }
            /// <summary>
            /// 虚拟焊接满分
            /// </summary>
            public decimal VirtualWeldingTotal { get; set; }
        }
        /// <summary>
        /// 穿戴项
        /// </summary>
        class DressItem
        {
            public Utensil Utensil { get; set; }
            public Boolean Enable { get; set; }
            public decimal Score { get; set; }
        }
        /// <summary>
        /// 检查项
        /// </summary>
        class CheckItem
        {
            public DeviceState State { get; set; }
            public Boolean Enable { get; set; }
            public string Description { get; set; }
            public decimal Score { get; set; }
        }
    }
}
