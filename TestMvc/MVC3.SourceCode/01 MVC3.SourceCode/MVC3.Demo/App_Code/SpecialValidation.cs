using System;
using System.ComponentModel.DataAnnotations;

namespace MVC3.Demo.App_Code
{
    public class SpecialValidation : RegularExpressionAttribute
    {
        public SpecialValidation() : base(@"^[0-5]*$") { }

        public override string FormatErrorMessage(string name)
        {
            return String.Format("{0}在0-5之间", name);
        }
    }
}