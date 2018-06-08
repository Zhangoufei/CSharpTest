using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JAAJExamManagementSys;
using Log;
using PutoutFire.Common;
using UploadScore;

namespace Welding
{
    class ScoreCalculator
    {
        private static BaseInfo ExamResultReport { get; set; }
        private static SubjectItem SubjectItem { get; set; }
        private static Title DressPart { get; set; }
        private static Title WeldingPart { get; set; }
        private static Title CheckTroublePart { get; set; }
        private static Steps WeldingSteps { get; set; }

        public static void Init()
        {
            ExamResultReport = new BaseInfo();
            SubjectItem = new SubjectItem(0);
            Titles parts = new Titles();
            DressPart = new Title(0, "焊接防护用品的选择和使用", "");
            WeldingPart = new Title(0, "安全操作技术", "");
            CheckTroublePart = new Title(0, "作业现场安全隐患排除", "");

            ExamResultReport.SubjectItemList.Add(SubjectItem);
            SubjectItem.TitleList.Add(parts);
            parts.TitleList.Add(DressPart);
            parts.TitleList.Add(WeldingPart);
            parts.TitleList.Add(CheckTroublePart);
        }
        /// <summary>
        /// 计算总成绩并提交
        /// </summary>
        /// <returns></returns>
        public static decimal CalcResultAndCommit()
        {
            SubjectItem.SubjectItemScore = DressPart.TitleScore + WeldingPart.TitleScore + CheckTroublePart.TitleScore;

            Common.SaveBillFieldsSetting(ExamResultReport);

            if (CommonHelper.examineeInfo != null)
            {
                new CommonHelper().SaveScore(CommonHelper.examineeInfo.nvcBatchNO, CommonHelper.examineeInfo.iExamineeID);

                ScoreData.UpdateDeviceStatusCallListDeleteExamProcess(CommonHelper.examineeInfo.iExamineeID);
            }

            return SubjectItem.SubjectItemScore;
        }
        /// <summary>
        /// 计算穿戴成绩
        /// </summary>
        /// <param name="dressReport"></param>
        /// <param name="weldingType"></param>
        /// <param name="selectedUtensilList"></param>
        /// <returns></returns>
        public static decimal CalcDressResult(
            WeldingType weldingType,
            IEnumerable<Utensil> selectedUtensilList)
        {
            var userDress = selectedUtensilList == null ? new Utensil[0] :
                (selectedUtensilList as Utensil[] ?? selectedUtensilList.ToArray());
            return DeviceStateConfig.CalcDressResult(DressPart, weldingType, userDress);
        }

        /// <summary>
        /// 计算穿戴成绩
        /// </summary>
        /// <returns></returns>
        //public static decimal CalcDressResult(
        //    Dictionary<WeldingType, IEnumerable<Utensil>> utensilDictionary,
        //    WeldingType currentWeldingType,
        //    IEnumerable<Utensil> selectedUtensilList, decimal weight)
        //{
        //    var answer = utensilDictionary[currentWeldingType];

        //    var answerArray = answer as Utensil[] ?? answer.ToArray();
        //    var userAnswerArray = selectedUtensilList == null ? new Utensil[0]:
        //        (selectedUtensilList as Utensil[] ?? selectedUtensilList.ToArray());

        //    //求正确答案和用户答案的交集:得分项
        //    var intersect = answerArray.Intersect(userAnswerArray);

        //    //求用户答案和正确答案的差集:扣分项
        //    var except = userAnswerArray.Except(answerArray);

        //    var intersectArray = intersect as Utensil[] ?? intersect.ToArray();
        //    var exceptArray = except as Utensil[] ?? except.ToArray();

        //    DressPart.TitleScore = Math.Max(0, intersectArray.Length - exceptArray.Length)*weight;

        //    //记录分步信息
        //    Steps steps = new Steps();
        //    DressPart.StepList.Add(steps);

        //    foreach (var utensil in intersectArray)
        //    {
        //        steps.StepList.Add(new Step
        //        {
        //            StepName = EnumHelper.GetEnumDescription(utensil),
        //            StepScore = weight
        //        });
        //    }

        //    foreach (var utensil in exceptArray)
        //    {
        //        steps.StepList.Add(new Step
        //        {
        //            StepName = EnumHelper.GetEnumDescription(utensil),
        //            StepScore = -weight
        //        });
        //    }
        //    return DressPart.TitleScore;
        //}

        /// <summary>
        /// 计算焊前检查结果
        /// </summary>
        /// <returns></returns>
        public static decimal CalcInspectionBeforeWeldingResult(WeldingType weldingType, DeviceState state)
        {
            WeldingSteps = new Steps();

            WeldingPart.StepList.Add(WeldingSteps);

            var score = DeviceStateConfig.CalcInspectionBeforeWeldingResult(WeldingSteps, weldingType, state);

            WeldingPart.TitleScore += score;

            return score;
        }

        /// <summary>
        /// 计算推开关成绩
        /// </summary>
        /// <param name="weldingType"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        public static decimal CalcSwitchOnResult(WeldingType weldingType, DeviceState state)
        {
            var score = DeviceStateConfig.CalcSwitchOnResult(WeldingSteps, weldingType, state);

            WeldingPart.TitleScore += score;

            return score;
        }

        /// <summary>
        /// 计算虚拟焊接成绩
        /// </summary>
        /// <param name="weldingType"></param>
        /// <param name="iniPath"></param>
        /// <returns></returns>
        public static decimal CalcVirtualWeldingResult(WeldingType weldingType, string iniPath)
        {
            var percent = DeviceStateConfig.GetVirtualWeldingScorePercent(weldingType);

            var score = weldingType == WeldingType.ArgonArcWelding ?
                ArgonArcWeldingScoreParser.CalcScore(WeldingSteps, iniPath, percent) :
                WeldingScoreParser.CalcScore(WeldingSteps, iniPath, percent);

            WeldingPart.TitleScore += score;

            return score;
        }

        /// <summary>
        /// 计算关开关成绩
        /// </summary>
        /// <param name="weldingType"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        public static decimal CalcSwitchOffResult(WeldingType weldingType, DeviceState state)
        {
            var score = DeviceStateConfig.CalcSwitchOffResult(WeldingSteps, weldingType, state);

            WeldingPart.TitleScore += score;

            return score;
        }
        /// <summary>
        /// 计算检查隐患成绩
        /// </summary>
        /// <param name="weldingType"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        public static decimal CalcCheckTroubleResult(WeldingType weldingType, DeviceState state)
        {
            var steps = new Steps();

            CheckTroublePart.StepList.Add(steps);

            var score = DeviceStateConfig.CalcCheckTroubleResult(steps, weldingType, state);

            CheckTroublePart.TitleScore += score;

            return score;
        }
    }
    /// <summary>
    /// 分值项
    /// </summary>
    class ScoreItem
    {
        /// <summary>
        /// 权重
        /// </summary>
        public decimal Weight { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 得分
        /// </summary>
        public decimal Score { get; set; }
    }
    /// <summary>
    /// 氩弧焊分值解析器
    /// </summary>
    static class ArgonArcWeldingScoreParser
    {
        private static ScoreItem Offset { get; set; }
        private static ScoreItem Speed { get; set; }
        private static ScoreItem WorkAngle { get; set; }
        private static ScoreItem MoveAngle { get; set; }
        private static ScoreItem Nozzle { get; set; }
        private static ScoreItem TigWire { get; set; }

        static ArgonArcWeldingScoreParser()
        {
            Offset = new ScoreItem
            {
                Description = "位置偏移",
                Weight = 35,
            };
            Speed = new ScoreItem
            {
                Description = "焊接速度",
                Weight = 15,
            };
            WorkAngle = new ScoreItem
            {
                Description = "工作角度",
                Weight = 10,
            };
            MoveAngle = new ScoreItem
            {
                Description = "移动速度",
                Weight = 10,
            };
            Nozzle = new ScoreItem
            {
                Description = "喷嘴高度",
                Weight = 10,
            };
            TigWire = new ScoreItem
            {
                Description = "填丝角度",
                Weight = 10,
            };
        }

        public static decimal CalcScore(Steps steps, string iniPath, decimal percent)
        {
            var ini = new IniFile(iniPath);

            Offset.Score = Decimal.Parse(ini.IniReadValue("OffsetScore", "value"));
            Speed.Score = Decimal.Parse(ini.IniReadValue("SpeedScore", "value"));
            WorkAngle.Score = Decimal.Parse(ini.IniReadValue("WorkAngleScore", "value"));
            MoveAngle.Score = Decimal.Parse(ini.IniReadValue("MoveAngleScore", "value"));
            Nozzle.Score = Decimal.Parse(ini.IniReadValue("NozzleScore", "value"));
            TigWire.Score = Decimal.Parse(ini.IniReadValue("TigWireScore", "value"));

            Offset.Score = ScoreHelper.MapScore(Offset.Score, percent);
            Speed.Score = ScoreHelper.MapScore(Speed.Score, percent);
            WorkAngle.Score = ScoreHelper.MapScore(WorkAngle.Score, percent);
            MoveAngle.Score = ScoreHelper.MapScore(MoveAngle.Score, percent);
            Nozzle.Score = ScoreHelper.MapScore(Nozzle.Score, percent);
            TigWire.Score = ScoreHelper.MapScore(TigWire.Score, percent);

            steps.StepList.Add(new Step
            {
                StepName = Offset.Description,
                StepScore = Offset.Score,
            });
            steps.StepList.Add(new Step
            {
                StepName = Speed.Description,
                StepScore = Speed.Score,
            });
            steps.StepList.Add(new Step
            {
                StepName = WorkAngle.Description,
                StepScore = WorkAngle.Score,
            });
            steps.StepList.Add(new Step
            {
                StepName = Offset.Description,
                StepScore = Offset.Score,
            });
            steps.StepList.Add(new Step
            {
                StepName = MoveAngle.Description,
                StepScore = MoveAngle.Score,
            });
            steps.StepList.Add(new Step
            {
                StepName = Nozzle.Description,
                StepScore = Nozzle.Score,
            });
            steps.StepList.Add(new Step
            {
                StepName = TigWire.Description,
                StepScore = TigWire.Score,
            });

            return Offset.Score + Speed.Score + WorkAngle.Score +
                MoveAngle.Score + Nozzle.Score + TigWire.Score;
        }
    }
    static class WeldingScoreParser
    {
        private static ScoreItem Offset { get; set; }
        private static ScoreItem Speed { get; set; }
        private static ScoreItem WorkAngle { get; set; }
        private static ScoreItem MoveAngle { get; set; }
        private static ScoreItem Nozzle { get; set; }
        //private static ScoreItem TigWire { get; set; }

        static WeldingScoreParser()
        {
            Offset = new ScoreItem
            {
                Description = "位置偏移",
                Weight = 50,
            };
            Speed = new ScoreItem
            {
                Description = "焊接速度",
                Weight = 20,
            };
            WorkAngle = new ScoreItem
            {
                Description = "工作角度",
                Weight = 10,
            };
            MoveAngle = new ScoreItem
            {
                Description = "移动速度",
                Weight = 10,
            };
            Nozzle = new ScoreItem
            {
                Description = "喷嘴高度",
                Weight = 10,
            };
            //TigWire = new ScoreItem
            //{
            //    Description = "填丝角度",
            //    Weight = 10,
            //};
        }

        public static decimal CalcScore(Steps steps, string iniPath, decimal percent)
        {
            var ini = new IniFile(iniPath);

            try
            {
                Offset.Score = Decimal.Parse(ini.IniReadValue("OffsetScore", "value"));
                Speed.Score = Decimal.Parse(ini.IniReadValue("SpeedScore", "value"));
                WorkAngle.Score = Decimal.Parse(ini.IniReadValue("WorkAngleScore", "value"));
                MoveAngle.Score = Decimal.Parse(ini.IniReadValue("MoveAngleScore", "value"));
                Nozzle.Score = Decimal.Parse(ini.IniReadValue("NozzleScore", "value"));
            }
            catch (Exception ex)
            {
                Logger.Error("{ex.GetType().ToString()} {ex.Message} iniPath:{iniPath} ReadValue:{ini.IniReadValue(OffsetScore+, value)}");
            }
            
            //TigWire.Score = Decimal.Parse(ini.IniReadValue("TigWireScore", "value"));

            Offset.Score = ScoreHelper.MapScore(Offset.Score, percent);
            Speed.Score = ScoreHelper.MapScore(Speed.Score, percent);
            WorkAngle.Score = ScoreHelper.MapScore(WorkAngle.Score, percent);
            MoveAngle.Score = ScoreHelper.MapScore(MoveAngle.Score, percent);
            Nozzle.Score = ScoreHelper.MapScore(Nozzle.Score, percent);
            //TigWire.Score = ScoreHelper.MapScore(TigWire.Score, percent);

            steps.StepList.Add(new Step
            {
                StepName = Offset.Description,
                StepScore = Offset.Score,
            });
            steps.StepList.Add(new Step
            {
                StepName = Speed.Description,
                StepScore = Speed.Score,
            });
            steps.StepList.Add(new Step
            {
                StepName = WorkAngle.Description,
                StepScore = WorkAngle.Score,
            });
            steps.StepList.Add(new Step
            {
                StepName = Offset.Description,
                StepScore = Offset.Score,
            });
            steps.StepList.Add(new Step
            {
                StepName = MoveAngle.Description,
                StepScore = MoveAngle.Score,
            });
            steps.StepList.Add(new Step
            {
                StepName = Nozzle.Description,
                StepScore = Nozzle.Score,
            });

            return Offset.Score + Speed.Score + WorkAngle.Score +
                   MoveAngle.Score + Nozzle.Score;// + TigWire.Score;
        }
    }

    static class ScoreHelper
    {
        /// <summary>
        /// 根据满分不同转换成绩
        /// </summary>
        /// <param name="score"></param>
        /// <param name="percent"></param>
        /// <returns></returns>
        public static decimal MapScore(decimal score, decimal percent)
        {
            return score * percent;
        }
    }
}
