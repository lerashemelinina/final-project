using System;
using System.ComponentModel.DataAnnotations;

namespace _02_BOL
{
    class StartDateValidation: ValidationAttribute
    {
        public override bool IsValid(Object value)
        {
         
            if (DateTime.TryParse(value.ToString(), out DateTime startDate))
            {
                if (startDate > DateTime.Now)
                    return true;
            }

            return false;
        }
    }
}
