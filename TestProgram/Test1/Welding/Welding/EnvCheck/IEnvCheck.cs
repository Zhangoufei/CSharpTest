using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EnvCheck
{
    /// <summary>
    /// 检查环境
    /// </summary>
    public interface IEnvCheck
    {
        /// <summary>
        /// 检查项说明
        /// </summary>
        string Description { get; }
        /// <summary>
        /// 检查环境项
        /// </summary>
        /// <param name="error">错误信息</param>
        /// <returns>环境是否完好</returns>
        Boolean EnvCheck(out string error);
    }
}
