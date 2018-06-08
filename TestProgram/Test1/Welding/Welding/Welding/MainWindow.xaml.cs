using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using Communication.Serial;
using EnvCheck;
using HardWare.CardReader;
using Log;
using PutoutFire.Common;
using Welding.MyPage;

namespace Welding
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private Login LoginPage { get; set; }
        private ExamIntroductionPage IntroductionPage { get; set; }
        private DressPage DressPage { get; set; }
        /// <summary>
        /// 焊前检查界面
        /// </summary>
        private InspectionBeforeWeldingPage InspectionBeforeWeldingPage { get; set; }
        private SwitchOnPage SwitchOnPage { get; set; }
        private SwitchOffPage SwitchOffPage { get; set; }
        private CheckTroublePage CheckTroublePage { get; set; }

        private TestResult ExamCompletePage { get; set; }

        private ExamEnv ExamEnv { get; set; }

        private SerialClass SerialPort { get; set; }

        private WindowInteropHelper WindowInteropHelper { get; set; }
        
        public MainWindow()
        {
            InitializeComponent();

            InitPages();

            InitSerialPort();

            Content = LoginPage;

            ResetIdCardReader();

            SetMaxWindow();

            ExamEnv = new ExamEnv();

            WindowInteropHelper = new WindowInteropHelper(this);

            DeviceStateConfig.LoadConfig();
            
            this.Loaded += MainWindowLoaded;
            Closed += MainWindowClosed;
        }

        /// <summary>
        /// 跳到考试介绍界面
        /// </summary>
        public void StepToExamIntroductionPage()
        {
            CommonHelper.SpeechToPrompt("提示：本项考试包括三个科目：科目一：劳动防护用品的选用. 科目二：安全操作技术. 科目三：作业现场安全隐患排除. 考试限时60分钟，规定时间内未完成，系统会自动提交考试。请于40秒内点击“开始考试”按钮，超时系统会自动返回刷卡界面。");
            Content = IntroductionPage;
        }

        /// <summary>
        /// 跳转到穿戴界面
        /// </summary>
        public void StepToDressPage()
        {
            CommonHelper.SpeechToPrompt("请根据焊接类型从工具柜中检查挑选正确的安全用具，并穿戴，操作完成后点击下一步。");

            ExamEnv.Reset();
            ExamEnv.RandomWelding();

            DressPage.Model.WeldingDescription = EnumHelper.GetEnumDescription(ExamEnv.CurrentWeldingType);//将随机产生的焊接类型赋值给穿戴界面
            
            Content = DressPage;
            ExamEnv.StartDetectUtensils();

            DressPage.DisableNextButtonFor1S();
        }

        /// <summary>
        /// 跳转到焊前检查界面
        /// </summary>
        public void StepToInspectionPage()
        {
            CommonHelper.SpeechToPrompt("请检查电源线、二次线是否完好，完成后点击下一步。");

            ExamEnv.DressComplete();
            
            ExamEnv.PrintSelectedUtensil();

            Content = InspectionBeforeWeldingPage;
        }

        /// <summary>
        /// 跳转到推开关界面
        /// </summary>
        public void StepToSwitchOnPage()
        {
            CommonHelper.SpeechToPrompt("打开焊接相关的开关，完成后点击就绪按钮。");

            ExamEnv.InspectionBeforeWeldingComplete();

            Content = SwitchOnPage;
        }

        /// <summary>
        /// 跳转到关开关界面
        /// </summary>
        private void StepToSwitchOffPage()
        {
            CommonHelper.SpeechToPrompt("关闭焊接相关的所有开关，完成后点击下一步。");
            
            ExamEnv.WeldingComplete();

            Content = SwitchOffPage;
        }

        /// <summary>
        /// 跳转到检查隐患界面
        /// </summary>
        public void StepToCheckTroublePage()
        {
            CommonHelper.SpeechToPrompt("请检查安全隐患，完成后点击提交。");

            ExamEnv.SwitchOffComplete();

            Content = CheckTroublePage;
        }

        /// <summary>
        /// 启动焊接场景
        /// </summary>
        public void StartWeldingScene()
        {
            ExamEnv.SwitchOnComplete();

            //WindowHelper.DeactiveWindow(this);
            DisableUI();

            Topmost = false;
            
            ExamEnv.StartWeldingScene(() => Dispatcher.Invoke(DispatcherPriority.Normal, new Action(
                () =>
                {
                    //var iniPath = @"E:\Code\1010\VirtualWelding-A0-2016.8.30\bin - 副本\Release\weld\config\Score.ini";
                    //MessageBox.Show(ScoreCalculator.CalcResult(ExamEnv.CurrentWeldingType, iniPath).ToString(CultureInfo.InvariantCulture));
                    try
                    {
                        Topmost = true;

                        StepToSwitchOffPage();

                        EnableUI();
                    }
                    catch (Exception ex)
                    {
                        Logger.Error(ex.Message);
                    }
                    
                })));
        }

        public void Commit()
        {
            ExamEnv.CheckTroubleComplete();

            var score = ScoreCalculator.CalcResultAndCommit();

            Logger.Info("得分:{score.ToString(CultureInfo.InvariantCulture)}");

            //TODO
            StepToExamCompletePage();
            //Reset();
        }

        private void StepToExamCompletePage()
        {
            Content = new TestResult();
        }

        private void DisableUI()
        {
            WindowHelper.NoActive(WindowInteropHelper.Handle);
            IsEnabled = false;
        }

        private void EnableUI()
        {
            WindowHelper.Active(WindowInteropHelper.Handle);
            IsEnabled = true;
        }

        private void MainWindowClosed(object sender, EventArgs e)
        {
            TipWindow.Dispose();
        }

        private void Reset()
        {
            CommonHelper.examineeInfo = null;

            ExamEnv.Reset();

            Content = LoginPage;

            ResetIdCardReader();
        }

        private void ResetIdCardReader()
        {
            if (!IDCardReader.CanRead)
            {
                IDCardReader.CanRead = true;
            }

            if (!LoginPage.CanDetectId)
            {
                LoginPage.CanDetectId = IDCardReader.Open();
                
                if (!LoginPage.CanDetectId)
                {
                    TipWindow.ShowTip("读卡器未连接");
                }
            }

            if (LoginPage.CanDetectId)
            {
                LoginPage.StartDetect();
            }
        }

        private void InitSerialPort()
        {
            SerialPort = new SerialClass(ConfigurationManager.AppSettings["COM"], 9600, Parity.None, 8, StopBits.One);
            SerialPort.DataReceived += SerialPortDataReceived;

            try
            {
                
               bool open =  SerialPort.openPort();
                if (open == true)
                {
                    Logger.Debug("打开"+ ConfigurationManager.AppSettings["COM"] +"成功");
                }
                else
                {
                    Logger.Debug("打开" + ConfigurationManager.AppSettings["COM"] + "失败");
                }
            }
            catch (IOException ex)
            {
                Logger.Error(ex.Message);
            }
        }

        private void SerialPortDataReceived(object sender, SerialDataReceivedEventArgs e, byte[] bits)
        {
            SerialMessageHandler.Handle(bits);
        }

        private void MainWindowLoaded(object sender, RoutedEventArgs e)
        {
            EnvCheck();
        }

        private void EnvCheck()
        {
            var envInspectorMgr = new EnvInspectorManager(new[] {"HardWare"});
            var result = envInspectorMgr.Check();

            foreach (var checkResult in result)
            {
                Trace.Write(checkResult.Description);
                Trace.Write("\t");
                Trace.Write(checkResult.Result?"成功":"失败:{checkResult.Error}");
                Trace.WriteLine(String.Empty);
            }
        }

        private void InitPages()
        {
            LoginPage = new Login();

            IntroductionPage = new ExamIntroductionPage();

            DressPage = new DressPage();

            InspectionBeforeWeldingPage = new InspectionBeforeWeldingPage();

            SwitchOnPage = new SwitchOnPage();

            SwitchOffPage = new SwitchOffPage();

            CheckTroublePage = new CheckTroublePage();
        }
        private void SetMaxWindow()
        {
            Width = SystemParameters.PrimaryScreenWidth;
            Height = SystemParameters.PrimaryScreenHeight;

            if (!AppEnv.DebugMode)
            {
                this.AllowsTransparency = true;
                this.WindowStyle = System.Windows.WindowStyle.None;
                this.ResizeMode = System.Windows.ResizeMode.NoResize;
                this.Topmost = true;
            }
        }
    }

    class WindowHelper
    {
        private const int GWL_EXSTYLE = -20;
        private const int WS_EX_NOACTIVATE = 0x08000000;

        [DllImport("user32.dll")]
        private static extern IntPtr SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        [DllImport("user32.dll")]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);
        /// <summary>
        /// 禁用焦点
        /// </summary>
        /// <param name="hWnd"></param>
        public static void NoActive(IntPtr hWnd)
        {
            SetWindowLong(hWnd, GWL_EXSTYLE,
                GetWindowLong(hWnd, GWL_EXSTYLE) | WS_EX_NOACTIVATE);
        }
        /// <summary>
        /// 启用焦点
        /// </summary>
        /// <param name="hWnd"></param>
        public static void Active(IntPtr hWnd)
        {
            SetWindowLong(hWnd, GWL_EXSTYLE,
                GetWindowLong(hWnd, GWL_EXSTYLE) & ~WS_EX_NOACTIVATE);
        }
    }
}
