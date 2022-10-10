using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineVehicleShowroom.Models
{
    public class VehicleViewModel
    {
        [Required(ErrorMessage ="Vehicle ID is required!")]
        [Display(Name = "Vehicle ID")]
        public int VehicleID { get; set; }

        [Display(Name = "Vehicle Name")]
        public string VehicleName { get; set; }

        [Display(Name = "Vehicle Model")]
        public string VehicleModel { get; set; }

        public double Cost { get; set; }

        [Required(ErrorMessage = "Please choose Vehicle Image!")]
        [Display(Name = "Vehicle Image")]
        public IFormFile VehiclePicture { get; set; }

        public string Description { get; set; }

        [Display(Name = "Total Stock")]
        public long TotalStock { get; set; }

        [Display(Name = "Dealer ID")]
        [Required(ErrorMessage = "Dealer ID is required!")]
        public int DealerID { get; set; }

        public double Rating { get; set; }
    }
}
