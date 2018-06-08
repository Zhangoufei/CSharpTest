using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Bonsit.Core
{
    public class DataBs
    {
        private  Srategy _rdbs = null;//关系型数据库

      //  private static object _locker = new object();//锁对象
      //  private static bool _enablednosql = false;//是否启用非关系型数据库


        public DataBs()
        {
           // _enablednosql = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "BonSite.NOSQLStrategy.*.dll", SearchOption.TopDirectoryOnly).Length > 0;
            try
            {
                string[] fileNameList = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "Bonsit.RDBSStrategy.*.dll", SearchOption.TopDirectoryOnly);

                string temp = fileNameList[0].Substring(fileNameList[0].LastIndexOf("RDBSStrategy.") + 13);
                _rdbs = (Srategy)Activator.CreateInstance(Type.GetType(string.Format("Bonsit.RDBSStrategy.{0}.RDBSStrategy, Bonsit.RDBSStrategy.{0}", temp.Replace(".dll", "")),
                                                                                            false,
                                                                                            true));
            }
            catch
            {
                throw new Exception("创建\"关系数据库策略对象\"失败，可能存在的原因：未将\"关系数据库策略程序集\"添加到bin目录中；将多个\"关系数据库策略程序集\"添加到bin目录中；\"关系数据库策略程序集\"文件名不符合\"BonSite.RDBSStrategy.{策略名称}.dll\"格式");
            }
        }

        /// <summary>
        /// 关系型数据库
        /// </summary>
        public  Srategy RDBS
        {
            get { return _rdbs; }
        }


    }
}
