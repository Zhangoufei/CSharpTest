using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace TestProgram
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        private int exceptionCount;
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            Current.DispatcherUnhandledException += Application_DispatcherUnhandledException;
        }

        private void Application_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            //"I'm sorry, the current application occured some issues.." + 
            if (exceptionCount > 0) { exceptionCount = 0; e.Handled = true; return; }
           // MessageWindow.Show(MessageLevel.Error, e.Exception.Message);
            exceptionCount++;
            e.Handled = true;
        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Exception ex = e.ExceptionObject as Exception;
           // MessageWindow.Show(MessageLevel.Error, ResourceHelper.GetRsByKey("LOC_Error_00002") + e.ExceptionObject);
        }
    }

}
