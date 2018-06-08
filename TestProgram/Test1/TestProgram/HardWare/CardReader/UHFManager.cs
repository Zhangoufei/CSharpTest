using System;
using System.Collections.Generic;
using System.Configuration;
using System.Windows.Threading;

namespace HardWare.CardReader
{
    public static class UHFManager
    {
        public static event Action<IEnumerable<string>> LabelDetected;
        private static DispatcherTimer DetectTimer { get; set; }
        private static UHFArgs UhfArgs { get; set; }
        private static IEnumerable<string> LabelCache { get; set; }
        static UHFManager()
        {
            ResetLabelCache();

            UhfArgs = new UHFArgs
            {
                DeviceId = 0,
                BaudRateCode = 5,
                SerialPort = Int32.Parse(ConfigurationManager.AppSettings["UHFCOM"]),
            };
            DetectTimer  = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(200)
            };
            DetectTimer.Tick += DetectTimerTick;
        }

        private static void DetectTimerTick(object sender, EventArgs e)
        {
            //如果打开端口成功，启动检测事件。检测考试人员穿戴。
            var labels = UHFReader.ReadLabel(UhfArgs.DeviceId);

            //LabelCache = LabelCache.Union(labels);

            //OnLabelDetected(LabelCache);
            OnLabelDetected(labels);
        }

        public static Boolean StartDetect()
        {
            ResetLabelCache();
            //Log.Logger.Debug("开始检测装备");
            if (UHFReader.Open(UhfArgs.SerialPort, UhfArgs.DeviceId, UhfArgs.BaudRateCode))
            {
              //  Log.Logger.Debug("开始检测装备，端口打开成功");
                DetectTimer.Start();
                return true;
            }
            return false;
        }

        public static void StopDetect()
        {
            DetectTimer.Stop();

            UHFReader.Close(UhfArgs.DeviceId);
        }

        static void OnLabelDetected(IEnumerable<string> labels)
        {
            var cache = LabelDetected;

            if (cache != null)
            {
                cache(labels);
            }
        }

        static void ResetLabelCache()
        {
            LabelCache = new List<string>();
        }
    }
}
