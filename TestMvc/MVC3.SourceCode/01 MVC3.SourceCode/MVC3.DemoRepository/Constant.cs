
/*
 *
 * 创建人：李林峰
 * 
 * 时  间：2013-03-17
 *
 * 描  述：常量
 *
 */

using System.Configuration;

namespace MVC3.DemoRepository
{
    /// <summary>
    /// 常量类
    /// </summary>
    public class Constant
    {
        /// <summary>
        /// 连接字符串
        /// </summary>
        public static string CONNSTRING = ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
    }
}
