using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace _02_BOL
{
    class EmailValidation: ValidationAttribute
    {
        public override bool IsValid(Object value)
        {

            const string validEmailPattern = @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|"
                                             + @"([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)"
                                             + @"@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$";

            return new Regex(validEmailPattern, RegexOptions.IgnoreCase).IsMatch(value.ToString());

        }
    }
}
