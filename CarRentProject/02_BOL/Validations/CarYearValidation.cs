using System;
using System.ComponentModel.DataAnnotations;

namespace _02_BOL
{
    class CarYearValidation: ValidationAttribute
    {
        public override bool IsValid(Object value)
        {

         if (int.TryParse(value.ToString(), out int year))
            {
                if (year <= DateTime.Now.Year && ((DateTime.Now.Year - year) <= 5))
                    return true;
            }


            return false;
        }

    }
}
