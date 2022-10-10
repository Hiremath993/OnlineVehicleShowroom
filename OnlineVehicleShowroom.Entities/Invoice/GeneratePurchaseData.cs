using OnlineVehicleShowroom.Entities.Business;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace OnlineVehicleShowroom.Entities.Invoice
{
    //Creating Generate Purchase Data table and its columns using Code first approach
    [Table("GeneratePurchaseData")]
    public class GeneratePurchaseData
    {
        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "Purchase ID")]
        public int PurchaseID { get; set; }

        [Display(Name = "Customer ID")]
        [Required]
        public int CustomerID { get; set; }

        [StringLength(50, MinimumLength = 3)]
        [Display(Name = "Customer Name")]
        public string CustomerName { get; set; }

        [Display(Name = "Sales ID")]
        [Required]
        public int SalesID { get; set; }

        [Display(Name = "Vehicle ID")]
        [Required]
        public int VehicleID { get; set; }

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
