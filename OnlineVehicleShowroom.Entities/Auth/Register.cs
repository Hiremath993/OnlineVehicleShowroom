using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace OnlineVehicleShowroom.Entities.Auth
{
    //Declaring Register page properties
    public class Register
    {
        [Required(ErrorMessage = "EMail is required!")]
        [EmailAddress]
        public string EMail { get; set; }

        [Required(ErrorMessage = "Password is required!")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
