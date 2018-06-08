using System;
using System.Runtime.Remoting.Messaging;
using System.Threading;
using System.Windows;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using JAAJ.Model;
using PutoutFire.Common;
using UploadScore;

namespace Welding
{
    public class LoginVM : ViewModelBase
    {
        private string examineeID;
        private string name;
        private string sex;
        private string nation;
        private string address;
        private string iDCode;

        private string tipMessage;
        private int iConnectionState;
        private IDCard IC = new IDCard();
        private Visibility skipButtonVisibility;

        public string BatchNO { get; set; }

        public string ExamineeID
        {
            get { return examineeID; }
            set
            {
                examineeID = value;
                RaisePropertyChanged("ExamineeID");
            }
        }

        /// <summary>
        /// 姓名
        /// </summary>
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                RaisePropertyChanged("Name");
            }
        }

        /// <summary>
        /// 性别
        /// </summary>
        public string Sex
        {
            get { return sex; }
            set
            {
                sex = value;
                RaisePropertyChanged("Sex");
            }
        }

        /// <summary>
        /// 民族，护照识别时此项为空
        /// </summary>
        public string Nation
        {
            get { return nation; }
            set
            {
                nation = value;
                RaisePropertyChanged("Nation");
            }
        }

        /// <summary>
        /// 地址，在识别护照时导出的是国籍简码
        /// </summary>
        public string Address
        {
            get { return address; }
            set
            {
                address = value;
                RaisePropertyChanged("Address");
            }
        }

        /// <summary>
        /// 身份证件号
        /// </summary>
        public string IDCode
        {
            get { return iDCode; }
            set
            {
                iDCode = value;
                RaisePropertyChanged("IDCode");
            }
        }

        public string TipMessage
        {
            get { return tipMessage; }
            set
            {
                tipMessage = value;
                RaisePropertyChanged("TipMessage");
            }
        }

        public Visibility SkipButtonVisibility
        {
            get { return skipButtonVisibility; }
            set
            {
                skipButtonVisibility = value; 
                RaisePropertyChanged(nameof(SkipButtonVisibility));
            }
        }

        private delegate IDCard MyDelegate();
        public LoginVM()
        {
            //MyDelegate md = new MyDelegate(ReadCard);
            //md.BeginInvoke(new AsyncCallback(CanExam), null);
        }

        public void Empty()
        {
            ExamineeID = String.Empty;
            Name = String.Empty;
            Sex = String.Empty;
            Nation = String.Empty;
            Address = String.Empty;
            IDCode = String.Empty;
        }

        private IDCard ReadCard()
        {
            try
            {
                iConnectionState = IC.InitComm();
                if (iConnectionState == 1)
                {
                    int iResult = 0;
                    while (iResult != 2)
                    {
                        iResult = IC.ReadCardContent();
                        Thread.Sleep(100);
                    }
                }
                else
                {
                    IC = null;
                }
            }
            catch (Exception ex)
            {
                IC = null;
                //LogImpl.Error(string.Format("{0}{2}{1}", ex.StackTrace, ex.Message, System.Environment.NewLine));
            }
            return IC;
        }
        private void CanExam(IAsyncResult result)
        {
            AsyncResult ar = (AsyncResult)result;
            MyDelegate md = (MyDelegate)ar.AsyncDelegate;
            IDCard IC = md.EndInvoke(result);
            if (IC == null)
            {
                Messenger.Default.Send<string>("          读卡器未连接", "ShowTip");
                return;
            }
            Name = IC.Name;
            Sex = IC.Sex;
            Nation = IC.Nation;
            Address = IC.Address;
            IDCode = IC.IDCode;

            #region 验证考试是否应该参与考试
            try
            {
                JAAJ_ExamineeInfo oeiJAAJ_ExamineeInfo = null;
                JAAJ_ExamInfo oeiJAAJ_ExamInfo = ScoreData.GetCurrentExamInfo();
                JAAJ_ExamProceInfo oepiJAAJ_ExamProceInfo = null;
                if (oeiJAAJ_ExamInfo != null)
                {
                    oeiJAAJ_ExamineeInfo = ScoreData.GetExamineeInfoByIDNum(IDCode, oeiJAAJ_ExamInfo.nvcBatchNO);
                    if (oeiJAAJ_ExamineeInfo != null)
                    {
                        ExamineeID = oeiJAAJ_ExamineeInfo.nvcExamineeGUID;
                        CommonHelper.examineeInfo = oeiJAAJ_ExamineeInfo;
                        oepiJAAJ_ExamProceInfo = ScoreData.GetExamProceInfoByExamineeID(oeiJAAJ_ExamineeInfo.iExamineeID);

                        if (oepiJAAJ_ExamProceInfo != null)
                        {
                            Messenger.Default.Send<string>("", "GoMainView");
                            return;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //LogImpl.Error(string.Format("{0}{2}{1}", ex.StackTrace, ex.Message, System.Environment.NewLine));
            }
            Messenger.Default.Send<string>("        未安排在此考试\n     请联系考务人员解决", "ShowWarning");
            #endregion
        }
    }
}
