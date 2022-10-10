using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static System.Net.Mime.MediaTypeNames;
using Microsoft.AspNetCore.Http;

namespace OnlineVehicleShowroom.Entities.Business
{
    //Creating Vehicle table and its columns using Code first approach
    [Table("Vehicle")]
    public class Vehicle
    {
        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "Vehicle ID")]
        public int VehicleID { get; set; }

        [StringLength(50)]
        [Display(Name = "Vehicle Name")]
        public string VehicleName { get; set; }

        [StringLength(50)]
        [Display(Name = "Vehicle Model")]
        public string VehicleModel { get; set; }

        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18,2)")]
        public double Cost { get; set; }

        public string VehicleImage { get;  set; }

        [MaxLength(50)]
        public string Description { get; set; }

        public long TotalStock { get; set; }

        [Display(Name = "Dealer ID")]
        [Required]
        public int DealerID { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public double Rating { get; set; }


    }
}
