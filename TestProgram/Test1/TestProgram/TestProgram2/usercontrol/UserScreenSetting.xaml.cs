using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using UserControl = System.Windows.Controls.UserControl;

namespace TestProgram2.usercontrol
{
    /// <summary>
    /// UserScreenSetting.xaml 的交互逻辑
    /// </summary>
    public partial class UserScreenSetting : UserControl
    {
        public UserScreenSetting()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 设置分变率
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //  ChangeResolution(1920, 1080, 1);

            WindowsApiUtils.ChangeResolution(1024,768,1);
        }
        /// <summary>
        /// 还原分辨率
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            ChangeResolution(1440, 900, 1);
        }
        // 改变分辨率
        public bool ChangeResolution(int width, int height, int screenNum)
        {
            bool result = false;

            Screen screen = null;

            Screen[] screenList = Screen.AllScreens;
            foreach (var verscreen in screenList)
            {
                if (1 == screenNum && !verscreen.Primary)
                {
                    screen = verscreen;
                    break;
                }
                if (0 == screenNum && verscreen.Primary)
                {
                    screen = verscreen;
                    break;
                }
            }


            // 初始化 DEVMODE结构
            DEVMODE devmode = new DEVMODE();
            devmode.dmDeviceName = "";
            devmode.dmFormName = new String(new char[32]);
            devmode.dmSize = (short)Marshal.SizeOf(devmode);

            IntPtr temp = new IntPtr();
            IntPtr temp2 = new IntPtr();


            if (0 != NativeMethods.EnumDisplaySettings(screen.DeviceName, NativeMethods.ENUM_CURRENT_SETTINGS, ref devmode))
            {
                int result2 = NativeMethods.ChangeDisplaySettingsEx(screen.DeviceName, ref devmode, temp, NativeMethods.CDS_TEST, temp2);

                devmode.dmPelsWidth = width;
                devmode.dmPelsHeight = height;
                result2 = NativeMethods.ChangeDisplaySettingsEx(screen.DeviceName, ref devmode, temp, NativeMethods.CDS_UPDATEREGISTRY, temp2);
            }


            //int screenWidth = screen.Bounds.Width;
            //int screenHeight = screen.Bounds.Height;
            //if (width == screenWidth || screenHeight == height) return true;
            //  初始化 DEVMODE结构
            //if (0 != NativeMethods.EnumDisplaySettings(screen.DeviceName, NativeMethods.ENUM_CURRENT_SETTINGS, ref devmode))
            //{


            //    // 改变屏幕分辨率
            //    int iRet = NativeMethods.ChangeDisplaySettings(ref devmode, NativeMethods.CDS_TEST);

            //    if (iRet == NativeMethods.DISP_CHANGE_FAILED)
            //    {
            //        // MessageBox.Show("不能执行你的请求", "信息");
            //        result = false;
            //    }
            //    else
            //    {
            //        devmode.dmPelsWidth = width;
            //        devmode.dmPelsHeight = height;

            //        iRet = NativeMethods.ChangeDisplaySettings(ref devmode, NativeMethods.CDS_UPDATEREGISTRY);
            //        switch (iRet)
            //        {
            //            // 成功改变
            //            case NativeMethods.DISP_CHANGE_SUCCESSFUL:
            //                {
            //                    result = true;
            //                    break;
            //                }
            //            case NativeMethods.DISP_CHANGE_RESTART:
            //                {
            //                    // MessageBox.Show("你需要重新启动电脑设置才能生效", "信息");
            //                    result = false;
            //                    break;
            //                }
            //            default:
            //                {
            //                    //  MessageBox.Show("改变屏幕分辨率失败", "信息");
            //                    result = false;
            //                    break;
            //                }
            //        }
            //    }
            //}
            return result;
        }


        // Win32 函数在托管环境下的声明
        public class NativeMethods
        {
            // 平台调用的申明
            [DllImport("user32.dll")]
            public static extern int EnumDisplaySettings(
             string deviceName, int modeNum, ref DEVMODE devMode);
            [DllImport("user32.dll")]
            public static extern int ChangeDisplaySettings(
               ref DEVMODE devMode, int flags);
            /// <summary>
            /// 设置屏幕分辨率
            /// </summary>
            /// <param name="lpszDeviceName"></param>
            /// <param name="lpDevMode"></param>
            /// <param name="hwnd"></param>
            /// <param name="dwflags"></param>
            /// <param name="lParam"></param>
            /// <returns></returns>
            [DllImport("user32.dll", SetLastError = true)]
            public static extern int ChangeDisplaySettingsEx(string lpszDeviceName, ref DEVMODE lpDevMode, IntPtr hwnd, uint dwflags, IntPtr lParam);


            // 控制改变屏幕分辨率的常量
            public const int ENUM_CURRENT_SETTINGS = -1;
            public const int CDS_UPDATEREGISTRY = 0x01;
            public const int CDS_TEST = 0x02;
            public const int DISP_CHANGE_SUCCESSFUL = 0;
            public const int DISP_CHANGE_RESTART = 1;
            public const int DISP_CHANGE_FAILED = -1;

            // 控制改变方向的常量定义
            public const int DMDO_DEFAULT = 0;
            public const int DMDO_90 = 1;
            public const int DMDO_180 = 2;
            public const int DMDO_270 = 3;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct DEVMODE
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string dmDeviceName;

            public short dmSpecVersion;
            public short dmDriverVersion;
            public short dmSize;
            public short dmDriverExtra;
            public int dmFields;
            public int dmPositionX;
            public int dmPositionY;
            public int dmDisplayOrientation;
            public int dmDisplayFixedOutput;
            public short dmColor;
            public short dmDuplex;
            public short dmYResolution;
            public short dmTTOption;
            public short dmCollate;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string dmFormName;

            public short dmLogPixels;
            public short dmBitsPerPel;
            public int dmPelsWidth;
            public int dmPelsHeight;
            public int dmDisplayFlags;
            public int dmDisplayFrequency;
            public int dmICMMethod;
            public int dmICMIntent;
            public int dmMediaType;
            public int dmDitherType;
            public int dmReserved1;
            public int dmReserved2;
            public int dmPanningWidth;
            public int dmPanningHeight;
        };
    }
}
