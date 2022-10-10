using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineVehicleShowroom.Entities.Business
{
    //Creating Customer table and its columns using Code first approach
    [Table("Customer")]
    public class Customer
    {
        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "Customer ID")]
        public int CustomerID { get; set; }

        [StringLength(50, MinimumLength = 3)]
        [Display(Name = "Customer Name")]
        public string CustomerName { get; set; }

        [MaxLength(6)]
        public string Gender { get; set; }

        [MaxLength(10)]
        [Display(Name = "Contact No")]
        public string ContactNo { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [MaxLength(50)]
        public string Address { get; set; }

        [MaxLength(50)]
        public string City { get; set; }

        [MaxLength(50)]
        public string State { get; set; }

        public int Pincode { get; set; }


    }
}
