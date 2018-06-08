using HardWare;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace 学习测试
{
    public class Program
    {
        [STAThread]
        public static void Main(string[] agers)
        {

            Untity.AllocConsole();
            Shell.WriteLine("注意：启动程序...");
            //Shell.WriteLine("/tWritten by Oyi319");
            //Shell.WriteLine("/tBlog: http://blog.csdn.com/oyi319");
            //Shell.WriteLine("{0}：{1}", "警告", "这是一条警告信息。");
            //Shell.WriteLine("{0}：{1}", "错误", "这是一条错误信息！");
            //Shell.WriteLine("{0}：{1}", "注意", "这是一条需要的注意信息。");
            Shell.WriteLine("");

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Test1());
        }
    }
}
