using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using EnvCheck;

namespace HardWare.EnvCheck
{
    /// <summary>
    /// UHF环境检查
    /// </summary>
    public class UHFEnvInspector:IEnvCheck
    {
        public string Description { get { return "UHF动态库依赖项"; } }
        public bool EnvCheck(out string error)
        {
            var dllPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Basic.dll");

            if (File.Exists(dllPath))
            {
                error = String.Empty;
                return true;
            }
            error = $"{dllPath} 不存在!";
            return false;
        }
    }
}
