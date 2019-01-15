using _02_BOL.Validations;
using System.ComponentModel.DataAnnotations;

namespace _02_BOL
{
    public class CarTypeModel
    {
        [Required, MinLength(2), MaxLength(50)]
        public string Make { get; set; }

        [Required, MaxLength(50)]
        public string Model { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public decimal DelayCharge { get; set; }

        [Required, CarYearValidation]
        public int Year { get; set; }

        [Required]
        public bool IsAutomatic { get; set; }
    }
}
