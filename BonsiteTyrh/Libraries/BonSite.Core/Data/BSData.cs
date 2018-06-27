using System;
using System.IO;

namespace BonSite.Core
{
    /// <summary>
    /// BonSIte数据管理类
    /// </summary>
    public class BSData
    {
        private static IRDBSStrategy _rdbs = null;//关系型数据库

        private static object _locker = new object();//锁对象
        private static bool _enablednosql = false;//是否启用非关系型数据库
        

        static BSData()
        {
            _enablednosql = Directory.GetFiles(System.Web.HttpRuntime.BinDirectory, "BonSite.NOSQLStrategy.*.dll", SearchOption.TopDirectoryOnly).Length > 0;
            try
            {
                string[] fileNameList = Directory.GetFiles(System.Web.HttpRuntime.BinDirectory, "BonSite.RDBSStrategy.*.dll", SearchOption.TopDirectoryOnly);
                _rdbs = (IRDBSStrategy)Activator.CreateInstance(Type.GetType(string.Format("BonSite.RDBSStrategy.{0}.RDBSStrategy, BonSite.RDBSStrategy.{0}", fileNameList[0].Substring(fileNameList[0].LastIndexOf("RDBSStrategy.") + 13).Replace(".dll", "")),
                                                                                            false,
                                                                                            true));
            }
            catch


            {
                throw new BSException("创建\"关系数据库策略对象\"失败，可能存在的原因：未将\"关系数据库策略程序集\"添加到bin目录中；将多个\"关系数据库策略程序集\"添加到bin目录中；\"关系数据库策略程序集\"文件名不符合\"BonSite.RDBSStrategy.{策略名称}.dll\"格式");
            }
        }

        /// <summary>
        /// 关系型数据库
        /// </summary>
        public static IRDBSStrategy RDBS
        {
            get { return _rdbs; }
        }

        
    }
}
