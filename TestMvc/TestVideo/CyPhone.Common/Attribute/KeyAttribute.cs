using System;

namespace CyPhone.Common.Attribute
{
    /// <summary>
    /// 用于标识主键
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class KeyAttribute : System.Attribute
    {
    }
}
