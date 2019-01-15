using System.ComponentModel.DataAnnotations;

namespace _02_BOL
{
    public class BranchModel
    {
        [Required, MaxLength(100)]
        public string Adress { get; set; }

        [Required]
        public int Latitude { get; set; }

        [Required]
        public int Lingitude { get; set; }

        [Required, MaxLength(50)]
        public string BranchName { get; set; }
    }
}
