using System.Dynamic;

namespace CyPhone.Common.UI
{
    public class AjaxResult
    {
        public AjaxResult()
        {
            Data = new ExpandoObject();
        }

        /// <summary>
        /// 操作结果类型
        /// </summary>
        public int State { get; set; } = ResultType.Error.GetHashCode();

        /// <summary>
        /// 提示消息
        /// </summary>
        public string Msg { get; set; } = "非法请求";

        /// <summary>
        /// 异常消息
        /// </summary>
        public string DebugMsg { get; set; }
        /// <summary>
        /// 获取 返回数据
        /// </summary>
        public dynamic Data { get; set; }
    }
    
    /// <summary>
    /// 表示 ajax 操作结果类型的枚举
    /// </summary>
    public enum ResultType
    {
        /// <summary>
        /// 异常结果类型
        /// </summary>
        Error = 0,
        /// <summary>
        /// 成功结果类型
        /// </summary>
        Success = 1,
        /// <summary>
        /// 消息结果类型
        /// </summary>
        Info = 2,
        /// <summary>
        /// 警告结果类型
        /// </summary>
        Warning = 3,
        /// <summary>
        /// 请求失败
        /// </summary>
        Fail=400,
        /// <summary>
        /// 请求超时
        /// </summary>
        Timeout = 504
    }
}
