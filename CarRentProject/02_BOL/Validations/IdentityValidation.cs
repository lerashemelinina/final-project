using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace _02_BOL
{
    class IdentityValidation: ValidationAttribute
    {
        public override bool IsValid(Object value)
        {
            int sum = 0;
            List<int> temp = new List<int>();
           

            if (int.TryParse(value.ToString(), out int num))
            {
                if (value.ToString().Length > 9)
                {

                }
                else if (value.ToString().Length <= 9)
                {
                    if (value.ToString().Length % 2 == 0)
                        temp.Add(0);

                    foreach (char item in value.ToString())
                    {
                        temp.Add(Convert.ToInt32(new string(item, 1)));
                    }


                    for (int i = 0; i < temp.Count; i++)
                    {
                        if (i % 2 == 1)
                            temp[i] = temp[i] * 2;

                        if (temp[i] > 9)
                            temp[i] = temp[i] % 10 + 1;

                        sum = sum + temp[i];
                    }

                
                }

                return sum % 10 == 0;

            }
            else
            {
                return false;
            }

        }
    }
}
