using System;
using System.IO;
using System.Threading;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;


namespace Welding.MyPage
{
    /// <summary>
    /// Login.xaml 的交互逻辑
    /// </summary>
    public partial class Login : System.Windows.Controls.Page
    {
      //  private LoginVM loginVM;

        private ImageSource DftHeadSource { get; }
        /// <summary>
        /// 可以读;表示读卡器环境正常
        /// </summary>
        public Boolean CanDetectId { get; set; }
        public Login()
        {
            InitializeComponent();

          //  IDCardReader.IdCardDetected += IdCardDetected;

            //loginVM = new LoginVM
            //{
            //    SkipButtonVisibility = AppEnv.DebugMode ? Visibility.Visible : Visibility.Hidden,
            //};
            //this.DataContext = loginVM;

        }

        //public void StartDetect()
        //{
        //    if (CanDetectId)
        //    {
        //        if (!IDCardReader.Reading && !IDCardReader.Read())//没在读且添加读取任务失败
        //        {
        //            Logger.Error("无法添加读取身份证任务!");
        //        }
        //    }
        //}

        //private void IdCardDetected(HardWare.CardReader.IDCard idCard)
        //{
        //    Logger.Debug("开始读卡");
        //    if (idCard != null)
        //    {
        //        loginVM.Name = idCard.Name;
        //        loginVM.Sex = idCard.Sex;
        //        loginVM.Nation = idCard.Nation;
        //        loginVM.Address = idCard.Address;
        //        loginVM.IDCode = idCard.IDCode;

        //        Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() =>
        //        {
        //            imgPhoto.Source = GetMemoryBmp($"{AppDomain.CurrentDomain.BaseDirectory}{"/zp.bmp"}");
        //        }));

        //        Thread.Sleep(2000);

        //        string examineeId;

        //        //TODO  暂注释 （查询数据库判断是否有可以考试）
        //      //  var canExam = CanExam(idCard.IDCode, out examineeId);

        //        var canExam = true;

        //        if (canExam)
        //        {
        //            //loginVM.ExamineeID = examineeId;
        //            loginVM.ExamineeID = String.Empty;//考生号

        //            Dispatcher.Invoke(DispatcherPriority.Normal,
        //                new Action(() => this.ParentWindow().StepToExamIntroductionPage()));
        //        }
        //        else
        //        {
        //            Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() =>
        //            {
        //                TipWindow.ShowTip("未安排在此考试\n请联系考务人员解决", ClearPage);
        //            }));

        //            StartDetect();
        //        }
        //    }
        //}

        //private Boolean CanExam(string idCardNo, out string examineeId)
        //{
        //    JAAJ_ExamInfo oeiJAAJ_ExamInfo = ScoreData.GetCurrentExamInfo();
        //    if (oeiJAAJ_ExamInfo != null)
        //    {
        //        var oeiJAAJ_ExamineeInfo = ScoreData.GetExamineeInfoByIDNum(idCardNo, oeiJAAJ_ExamInfo.nvcBatchNO);
        //        if (oeiJAAJ_ExamineeInfo != null)
        //        {
        //            examineeId = oeiJAAJ_ExamineeInfo.nvcExamineeGUID;
        //            CommonHelper.examineeInfo = oeiJAAJ_ExamineeInfo;
        //            var oepiJAAJ_ExamProceInfo = ScoreData.GetExamProceInfoByExamineeID(oeiJAAJ_ExamineeInfo.iExamineeID);

        //            if (oepiJAAJ_ExamProceInfo != null)
        //            {
        //                return true;
        //            }
        //        }
        //    }
        //    examineeId = string.Empty;
        //    return false;
        //}

        private void ClearPage()
        {
            //DataContext = loginVM = new LoginVM();
        }

        /// <summary>
        /// 将本地图片读取到内存中并返回BitmapImage对象，以免对本地文件的引用造成其他操作不可访问
        /// </summary>
        /// <param name="strPath"></param>
        /// <returns></returns>
        private BitmapImage GetMemoryBmp(string strPath)
        {
            if (!File.Exists(strPath))
            {
                return new BitmapImage();
            }
            BinaryReader binReader = new BinaryReader(File.Open(strPath, FileMode.Open));
            FileInfo fileInfo = new FileInfo(strPath);
            byte[] bytBmp = binReader.ReadBytes((int)fileInfo.Length);
            binReader.Close();

            BitmapImage bitImg = new BitmapImage();
            bitImg.BeginInit();
            bitImg.StreamSource = new MemoryStream(bytBmp);
            bitImg.EndInit();
            return bitImg;
        }

        private void SkipClick(object sender, RoutedEventArgs e)
        {
          //  IDCardReader.CanRead = false;//跳出读循环
         //   this.ParentWindow().StepToExamIntroductionPage();
        }
    }
}
