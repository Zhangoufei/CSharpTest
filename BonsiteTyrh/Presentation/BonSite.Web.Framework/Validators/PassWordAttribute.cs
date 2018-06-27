using System;
using System.ComponentModel.DataAnnotations;

namespace BonSite.Web.Framework
{
    /// <summary>
    /// 密码验证属性
    /// </summary>
    public class PassWordAttribute : ValidationAttribute
    {
        public PassWordAttribute()
        {
            ErrorMessage = "密码为6到21位字母数字组合";
        }

        public override bool IsValid(object value)
        {
            if (value == null) return true;
            else return BonSite.Core.ValidateHelper.IsPassWord(value.ToString());

        }
    }
}
