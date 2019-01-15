using System;
using System.ComponentModel.DataAnnotations;

namespace _02_BOL.Validations
{
    class PriceValidation: ValidationAttribute
    {
        public override bool IsValid(Object value)
        {

            if (int.TryParse(value.ToString(), out int num))
            {
                if (num>0)
                    return true;
            }

            return false;
        }
    }
}
