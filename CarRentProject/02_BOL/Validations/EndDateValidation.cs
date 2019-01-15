using System;
using System.ComponentModel.DataAnnotations;

namespace _02_BOL
{
    class EndDateValidation: ValidationAttribute
    {
        public override bool IsValid(Object value)
        {
            if (DateTime.TryParse(value.ToString(), out DateTime endDate))
            {
               
                    return true;
            }

            return false;
        }
    }
}
