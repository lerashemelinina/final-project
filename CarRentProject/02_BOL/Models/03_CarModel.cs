using System.ComponentModel.DataAnnotations;

namespace _02_BOL
{
    public class CarModel
    {
        [Required]
        public CarTypeModel CarType { get; set; }

        [Required]
        public int Mileage { get; set; }

        [Required]
        public string Image { get; set; }

        [Required]
        public BranchModel Branch { get; set; }

        [Required]
        public bool IsForRent { get; set; }

        [Required]
        public int CarNumber { get; set; }

      
    }
}
