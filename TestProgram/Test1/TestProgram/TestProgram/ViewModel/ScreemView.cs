using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using JAAJ.PEAR.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TestProgram.Model;

namespace TestProgram.ViewModel
{
    public class ScreemView : ViewModelBase
    {
        public ScreemView()
        {
            bool result = Camera_Process.startCamera(0);

            result = Camera_Process.startCamera(1);
        }

        private RelayCommand<object> submitOperationCommand;

        public ICommand SubmitOperationCommand
        {
            get
            {
                if (submitOperationCommand == null)
                {
                    submitOperationCommand = new RelayCommand<object>(x => Sub(x));
                }
                return submitOperationCommand;
            }
        }

        private RelayCommand screenCatpure;
        /// <summary>
        /// 图片采集事件 
        /// </summary>
        public ICommand ScreenCatpure
        {
            get {
                if (screenCatpure == null)
                {
                    screenCatpure = new RelayCommand(() => ScreenCap());
                }
                return screenCatpure;
            }
        }

        public RelayCommand screenPara;

        public ICommand ScreenPara
        {
            get {
                if (screenPara == null)
                {
                    screenPara = new RelayCommand(()=> ScreenParamater());
                }
                return screenPara;
            }
        }

        /// <summary>
        /// 执行图片采集配置处理
        /// </summary>
        public void ScreenParamater() {
            Messenger.Default.Send<object>("", "ShowScreenPara");
        }

        public void ScreenCap()
        {
            try
            {
                Process process = new Process();
                process.StartInfo.FileName = "platForm.exe";
                process.StartInfo.UseShellExecute = true;
                process.Start();
            }
            catch (Exception)
            {
                Messenger.Default.Send<object>("启动程序错误", "Sub");
            }
           
        }
        public void Sub(object obj)
        {
            int temp = int.Parse(obj.ToString()) - 1;
            double getValue = Camera_Process.matchAndJumpNext(0, temp, false);


            temp = int.Parse(obj.ToString()) - 1;
            double getValue2 = Camera_Process.matchAndJumpNext(1, temp, false);

            Messenger.Default.Send<object>(getValue + "第二个评分 " + getValue2, "Sub");
        }

        //读取配置文件


    }
}
