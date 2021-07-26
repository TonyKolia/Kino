using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KinoApp.Models
{
    public class RegisterViewModel
    {
        [Required]
        [MaxLength(15, ErrorMessage = "Username cannot be longer than 15 characters")]
        public string Username { get; set; }

        [DataType(DataType.EmailAddress)]
        [Required]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [Required]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Required]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The passwords do not match")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "Date of birth")]
        public DateTime? BirthDate { get; set; }
    }
}
