using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Welding
{
    class EnumHelper
    {
        public static string GetEnumDescription(Enum enumValue)
        {
            string str = enumValue.ToString();
            System.Reflection.FieldInfo field = enumValue.GetType().GetField(str);
            object[] objs = field.GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute), false);
            if (objs.Length == 0) return str;
            var da = (System.ComponentModel.DescriptionAttribute)objs[0];
            return da.Description;
        }
    }
}
