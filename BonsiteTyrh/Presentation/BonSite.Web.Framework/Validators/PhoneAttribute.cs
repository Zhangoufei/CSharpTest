using System;
using System.ComponentModel.DataAnnotations;

namespace BonSite.Web.Framework
{
    /// <summary>
    /// 固话号验证属性
    /// </summary>
    public class PhoneAttribute : ValidationAttribute
    {
        public PhoneAttribute()
        {
            ErrorMessage = "不是有效的固话号";
        }

        public override bool IsValid(object value)
        {
            if (value == null) return true;
            else return BonSite.Core.ValidateHelper.IsPhone(value.ToString());

        }
    }
}
