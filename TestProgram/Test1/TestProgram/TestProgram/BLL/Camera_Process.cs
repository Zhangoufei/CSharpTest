using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Runtime.InteropServices;//这是用到DllImport时候要引入的包

namespace JAAJ.PEAR.Common
{
    /// <summary>
    /// 相机扑捉验证类
    /// </summary>
    class Camera_Process
    {
        /// <summary>
        ///	相机初始化接口
        /// </summary>
        /// <param name="port">参数：启动相机索引取值范围0-n(相机总个数n+1)</param>
        /// <returns></returns>
        [DllImport("ColorDistinguish.dll", EntryPoint = "startCamera", CharSet = CharSet.Auto, SetLastError = false, CallingConvention = CallingConvention.Cdecl)]
        public extern static bool startCamera(int number);

        /// <summary>
        /// 匹配当前步骤接口：
        /// </summary>
        /// <param name="port">相机索引 从0 开始计算</param>
        /// <param name="stepIndex" >步骤 从0开始</param>
        /// <param name="showWindow">是否显示采集视图窗口</param>
        /// <returns>返回值:-2 - 相机设备无法正常运行
        /// -1 - 所有步骤匹配完毕
        /// 0 - 当前步骤匹配失败
        /// 1 - 当前步骤匹配成功
        ///</returns>
        [DllImport("ColorDistinguish.dll", EntryPoint = "matchAndJumpNext", CharSet = CharSet.Auto, SetLastError = false, CallingConvention = CallingConvention.Cdecl)]
        public extern static int matchAndJumpNext(int port,int stepIndex, bool showWindow);

        /// <summary>
        /// 拆除当前步骤接口：
        /// </summary>
        /// <param name="port">相机索引 从0 开始计算</param>
        /// <param name="stepIndex" >步骤 从0开始</param>
        /// <param name="showWindow">是否显示采集视图窗口</param>
        /// <returns>返回值:-2 - 相机设备无法正常运行
        /// -1 - 所有步骤匹配完毕
        /// 0 - 当前步骤匹配失败
        /// 1 - 当前步骤匹配成功
        ///</returns>
        [DllImport("ColorDistinguish.dll", EntryPoint = "removeAndJumpPrev", CharSet = CharSet.Auto, SetLastError = false, CallingConvention = CallingConvention.Cdecl)]
        public extern static int removeAndJumpPrev(int port, bool showWindow);

    }

}
