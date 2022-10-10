using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace OnlineVehicleShowroom.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage ="EMail is required!")]
        [EmailAddress]
        public string EMail { get; set; }

        [Required(ErrorMessage ="Password is required!")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm Password is required!")]
        [DataType(DataType.Password)]
        [Display(Name ="Confirm Password")]
        [Compare("Password",ErrorMessage ="The password and confirmation password not match!")]
        public string ConfirmPassword { get; set; }
    }
}
