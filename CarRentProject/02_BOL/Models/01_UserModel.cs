using System;
using System.ComponentModel.DataAnnotations;

namespace _02_BOL
{
    public class UserModel
    {
        [Required, MinLength(2), MaxLength(40)]
        public string FullName { get; set; }

        [Required, IdentityValidation, MinLength(9), MaxLength(9)]
        public string IdentityNumber { get; set; }

        [Required, MinLength(6), MaxLength(20)]
        public string UserName { get; set; }

        [AgeValidation]
        public DateTime? BirthDate { get; set; }

        [Required]
        public bool IsMale { get; set; }

        [Required, EmailValidation]
        public string Email { get; set; }

        [Required, MinLength(6)]
        public string Password { get; set; }


        public string UserRole { get; set; }


        public string Image { get; set; }
    }
}

