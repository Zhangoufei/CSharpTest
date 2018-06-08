using System;
using System.Text;

namespace EnvCheck
{
    /// <summary>
    /// 检查项结果
    /// </summary>
    public class CheckResult
    {
        /// <summary>
        /// 检查结果
        /// </summary>
        public Boolean Result { get; set; }
        /// <summary>
        /// 错误信息
        /// </summary>
        public string Error { get; set; }
        /// <summary>
        /// 检查项描述信息
        /// </summary>
        public string Description { get; set; }
    }
}
