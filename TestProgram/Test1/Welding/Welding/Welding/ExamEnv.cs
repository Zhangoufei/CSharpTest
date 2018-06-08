using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Threading;
using HardWare.CardReader;
using Log;

namespace Welding
{
    class ExamEnv
    {
        private UtensilDictionary UtensilDictionary { get; set; }
        /// <summary>
        /// 用具字典
        /// </summary>
        private Dictionary<WeldingType, IEnumerable<Utensil>> UtensilDictionary2 { get; set; }
        /// <summary>
        /// 用户选择的用具列表
        /// </summary>
        private IEnumerable<Utensil> SelectedUtensilList { get; set; }
        /// <summary>
        /// 随机种子
        /// </summary>
        private Random RandomSeed { get; set; }
        /// <summary>
        /// 焊接种类集合
        /// </summary>
        private Array WeldingTypeArray { get; set; }
        /// <summary>
        /// 场景进程
        /// </summary>
        private Process WeldingSceneProcess { get; set; }
        /// <summary>
        /// 场景退出的回调处理程序
        /// </summary>
        private Action SceneExitedCallBack { get; set; }
        /// <summary>
        /// 当前焊接类型
        /// </summary>
        public WeldingType CurrentWeldingType { get; set; }
        private DeviceState CurrentDeviceState { get; set; }

        public ExamEnv()
        {
            InitUtensils();

            UtensilDictionary = new UtensilDictionary();

            WeldingTypeArray = Enum.GetValues(typeof(WeldingType));

            RandomSeed = new Random();

            UHFManager.LabelDetected += UHFManagerLabelDetected;

            SerialMessageHandler.DeviceStateChanged += DeviceStateChanged;
        }
        /// <summary>
        /// 重置
        /// </summary>
        public void Reset()
        {
            ScoreCalculator.Init();

            CurrentDeviceState = DeviceState.None;
        }

        public void PrintSelectedUtensil()
        {
            if (SelectedUtensilList == null)
            {
                Logger.Info("穿戴为空");
                return;
            }

            foreach (var utensil in SelectedUtensilList)
            {
                Logger.Info(utensil.ToString());
            }
        }

        /// <summary>
        /// 启动焊接场景
        /// </summary>
        public void StartWeldingScene(Action onSceneExited)
        {
            SceneExitedCallBack = onSceneExited;

            var sceneDir = ConfigurationManager.AppSettings["sceneDir"];

            var startInfo = new ProcessStartInfo("VirtualWeldingA0.exe")
            //var startInfo = new ProcessStartInfo("notepad.exe")
            {
                WorkingDirectory = sceneDir,
                //0-二保焊，1-电弧焊，2-氩弧焊
                Arguments = ((byte)CurrentWeldingType).ToString(),
            };

            WeldingSceneProcess = Process.Start(startInfo);

            if (WeldingSceneProcess != null)
            {
                WeldingSceneProcess.EnableRaisingEvents = true;
                WeldingSceneProcess.Exited += WeldingSceneProcessExited;
            }
        }
        /// <summary>
        /// 穿戴完成
        /// </summary>
        public void DressComplete()
        {
            StopDetectUtensils();
            var score = ScoreCalculator.CalcDressResult(CurrentWeldingType, SelectedUtensilList);

            Logger.Debug("穿戴成绩:{score.ToString(CultureInfo.InvariantCulture)}");
        }
        /// <summary>
        /// 焊前检查完成
        /// </summary>
        public void InspectionBeforeWeldingComplete()
        {
            var score = ScoreCalculator.CalcInspectionBeforeWeldingResult(CurrentWeldingType, CurrentDeviceState);

            Logger.Debug("焊前检查成绩:{score.ToString(CultureInfo.InvariantCulture)}");
        }
        /// <summary>
        /// 推开关完成
        /// </summary>
        public void SwitchOnComplete()
        {
            var score = ScoreCalculator.CalcSwitchOnResult(CurrentWeldingType, CurrentDeviceState);

            Logger.Debug(CurrentDeviceState.ToString());
            Logger.Debug("推开关成绩:{score.ToString(CultureInfo.InvariantCulture)}");
        }
        /// <summary>
        /// 焊接完成
        /// </summary>
        public void WeldingComplete()
        {
            var iniPath = Path.Combine(ConfigurationManager.AppSettings["sceneDir"], @"weld\config\Score.ini");
            var score = ScoreCalculator.CalcVirtualWeldingResult(CurrentWeldingType, iniPath);

            Logger.Debug("焊接成绩:{score.ToString(CultureInfo.InvariantCulture)}");
        }
        /// <summary>
        /// 关开关完成
        /// </summary>
        public void SwitchOffComplete()
        {
            var score = ScoreCalculator.CalcSwitchOffResult(CurrentWeldingType, CurrentDeviceState);

            Logger.Debug("关开关成绩:{score.ToString(CultureInfo.InvariantCulture)}");
        }
        /// <summary>
        /// 排查隐患完成
        /// </summary>
        public void CheckTroubleComplete()
        {
            var score = ScoreCalculator.CalcCheckTroubleResult(CurrentWeldingType, CurrentDeviceState);
            Logger.Debug("排查隐患成绩:{score.ToString(CultureInfo.InvariantCulture)}");
        }

        private void DeviceStateChanged(DeviceState state)
        {
            CurrentDeviceState = state;
        }

        private void WeldingSceneProcessExited(object sender, EventArgs e)
        {
            //焊接场景退出
            if (SceneExitedCallBack != null)
            {
                SceneExitedCallBack.Invoke();
            }
        }

        private void UHFManagerLabelDetected(IEnumerable<string> labelList)
        {
            SelectedUtensilList = (from string label in labelList select UtensilDictionary.LookUp(label)).
                Where(utensil => utensil.HasValue).Select(utensil => utensil.Value);
        }

        /// <summary>
        /// 随机生成焊接类型
        /// </summary>
        public void RandomWelding()
        {
            CurrentWeldingType = (WeldingType)WeldingTypeArray.GetValue(RandomSeed.Next(WeldingTypeArray.Length));
        }

        /// <summary>
        /// 开始检测用具穿戴
        /// </summary>
        public Boolean StartDetectUtensils()
        {
            return UHFManager.StartDetect();
        }
        /// <summary>
        /// 停止检测用具穿戴
        /// </summary>
        public void StopDetectUtensils()
        {
            UHFManager.StopDetect();
        }

        /// <summary>
        /// 初始化用具
        /// </summary>
        private void InitUtensils()
        {
            UtensilDictionary2 = new Dictionary<WeldingType, IEnumerable<Utensil>>();

            var utensil1 = from string utensil in Properties.Settings.Default.Utensil1
                           select (Utensil)Enum.Parse(typeof(Utensil), utensil);
            var utensil2 = from string utensil in Properties.Settings.Default.Utensil2
                           select (Utensil)Enum.Parse(typeof(Utensil), utensil);
            var utensil3 = from string utensil in Properties.Settings.Default.Utensil3
                           select (Utensil)Enum.Parse(typeof(Utensil), utensil);

            UtensilDictionary2.Add(WeldingType.ShieldedMetalArcWelding, utensil1);
            UtensilDictionary2.Add(WeldingType.CO2ProtectionWelding, utensil2);
            UtensilDictionary2.Add(WeldingType.ArgonArcWelding, utensil3);
        }
    }
}
