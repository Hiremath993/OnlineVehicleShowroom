using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineVehicleShowroom.Entities.Business
{
    //Creating Dealer table and its columns using Code first approach
    [Table("Dealer")]
    public class Dealer
    {
        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "Dealer ID")]
        public int DealerID { get; set; }

        [StringLength(50, MinimumLength = 3)]
        [Display(Name = "Dealer Name")]
        public string DealerName { get; set; }

        [StringLength(50, MinimumLength = 3)]
        [Display(Name = "Company Name")]
        public string CompanyName { get; set; }

        [MaxLength(10)]
        [Display(Name = "Contact No")]
        public string ContactNo { get; set; }

        [MaxLength(50)]
        public string Address { get; set; }

        [MaxLength(50)]
        public string City { get; set; }

        [MaxLength(50)]
        public string State { get; set; }

        public int Pincode { get; set; }



    }
}
