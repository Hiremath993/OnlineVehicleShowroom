using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineVehicleShowroom.Entities.Business
{
    //Creating Sales table and its columns using Code first approach
    [Table("Sales")]
    public class Sales
    {
        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "Sales ID")]
        public int SalesID { get; set; }

        [Display(Name = "Vehicle ID")]
        [Required]
        public int VehicleID { get; set; }

        [Display(Name = "Customer ID")]
        [Required]
        public int CustomerID { get; set; }

        [Display(Name = "Showroom ID")]
        [Required]
        public int ShowroomID { get; set; }

        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18,2)")]
        public double Cost { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Order Date")]
        public DateTime OrderDate { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Delivery Date")]
        public DateTime DeliveryDate { get; set; }

        [MaxLength(50)]
        public string Remarks { get; set; }

    }
}
