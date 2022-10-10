using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace OnlineVehicleShowroom.Entities.Auth
{
    //Declaring login page properties
    public class Login
    {
        [Required(ErrorMessage ="User Name is required!")]
        [EmailAddress]
        public string UserName { get; set; }

        [Required(ErrorMessage ="Password is required!")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
