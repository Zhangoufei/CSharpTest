using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using Log;

namespace Welding
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        private void App_OnStartup(object sender, StartupEventArgs e)
        {
            if (e.Args.Length > 0)
            {
                Logger.DebugMode = AppEnv.DebugMode = string.Compare(e.Args[0], "Debug", StringComparison.InvariantCultureIgnoreCase) == 0;
            }
        }
    }

    class AppEnv
    {
        public static Boolean DebugMode { get; set; } 
    }
}
