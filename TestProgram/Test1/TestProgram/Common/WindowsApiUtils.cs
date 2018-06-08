using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace Common
{
    /// <summary>
    /// windows api  函数调用 
    /// </summary>
    public class WindowsApiUtils
    {
        /// <summary>
        /// 关闭显示器 
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="Msg"></param>
        /// <param name="wParam"></param>
        /// <param name="lParam"></param>
        /// <returns></returns>
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        /// <summary>
        /// 屏幕分辨率 自定义设置 2017-8-10  
        /// </summary>
        /// <param name="width">要设置的宽度</param>
        /// <param name="height">要设置的高度</param>
        /// <param name="screenNum">要设置的显示器 主屏幕为 0，另一个屏幕为 1 暂时只支持两个屏幕设置</param>
        /// <returns>设置成功 true，失败false</returns>
        public static bool ChangeResolution(int width, int height, int screenNum)
        {
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

            int screenWidth = screen.Bounds.Width;
            int screenHeight = screen.Bounds.Height;
            if (width == screenWidth || screenHeight == height) return true;

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
                if (result2 == 0)
                {
                    return true;
                }
            }
            return false;
        }
    }
    #region 屏幕分辨率设置 参数 开始
    /// <summary>
    /// 
    /// </summary>
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
    #endregion  屏幕分辨率设置 参数 结束
}
