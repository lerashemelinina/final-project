using System;
using System.ComponentModel.DataAnnotations;

namespace _02_BOL
{
    class AgeValidation: ValidationAttribute
    {
        public override bool IsValid(Object value)
        {

            if (DateTime.TryParse(value.ToString(), out DateTime date))
            {
                int currentYear = DateTime.Now.Year;

                if (((currentYear - date.Year) >= 18) && ((currentYear - date.Year) <= 75))
                    return true; 

            }
            return false;

        }
    }
}
