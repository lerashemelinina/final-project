using System;
using System.ComponentModel.DataAnnotations;

namespace _02_BOL
{
    public class OrderModel
    {
        [Required]
        public DateTime StartRent { get; set; }

        [Required]
        public DateTime EndRent { get; set; }

        
        public DateTime ReturnDate { get; set; }

        [Required]
        public UserModel User { get; set; }


        [Required]
        public CarModel Car { get; set; }

    }
}
