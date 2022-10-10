using OnlineVehicleShowroom.Entities.Business;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace OnlineVehicleShowroom.Entities.Invoice
{
    //Creating Generate Bill table and its columns using Code first approach
    [Table("GenerateBill")]
    public class GenerateBill
    {
        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "Order ID")]
        public int OrderID { get; set; }

        [Display(Name = "Sales ID")]
        [Required]
        public int SalesID { get; set; }


        [Display(Name = "Vehicle ID")]
        [Required]
        public int VehicleID { get; set; }


        [StringLength(50, MinimumLength = 3)]
        [Display(Name = "Customer Name")]
        public string CustomerName { get; set; }
 

        [Display(Name = "Showroom Name")]
        [Required]
        public string ShowroomName { get; set; }


        [DataType(DataType.Date)]
        [Display(Name = "Order Date")]
        public DateTime OrderDate { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Delivery Date")]
        public DateTime DeliveryDate { get; set; }

        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18,2)")]
        public double Cost { get; set; }
    }
}
