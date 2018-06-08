using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace EnvCheck
{
    /// <summary>
    /// 环境检查管理器
    /// </summary>
    public class EnvInspectorManager
    {
        /// <summary>
        /// 检查器类型列表
        /// </summary>
        private Type[] InspectorTypeList { get; set; }
        /// <summary>
        /// 检查器列表
        /// </summary>
        private IEnumerable<IEnvCheck> InspectorList { get; set; }
        public EnvInspectorManager(IEnumerable<string> dependAssemblies)
        {
            //预加载依赖程序集
            if (dependAssemblies != null)
            {
                foreach (var dependAssembly in dependAssemblies)
                {
                    Assembly.Load(dependAssembly);
                }
            }
            
            LoadInspectorList();
        }
        
        /// <summary>
        /// 环境检查
        /// </summary>
        /// <returns>检查报告</returns>
        public IEnumerable<CheckResult> Check()
        {
            foreach (var envCheck in InspectorList)
            {
                string error;
                var result = envCheck.EnvCheck(out error);
                yield return new CheckResult
                {
                    Description = envCheck.Description,
                    Result = result,
                    Error = error
                };
            }
        }
        /// <summary>
        /// 加载检查器列表
        /// </summary>
        private void LoadInspectorList()
        {
            InspectorTypeList = AppDomain.CurrentDomain.GetAssemblies().SelectMany(a => a.GetTypes().Where(t => t.GetInterfaces().Contains(typeof(IEnvCheck)))).ToArray();
            
            InspectorList = from type in InspectorTypeList select Activator.CreateInstance(type) as IEnvCheck;
        }
    }
}