using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineVehicleShowroom.Entities.Business
{
    //Creating Showroom table and its columns using Code first approach
    [Table("Showroom")]
    public class Showroom
    {
        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "Showroom ID")]
        public int ShowroomID { get; set; }

        [StringLength(50, MinimumLength = 3)]
        [Display(Name = "Showroom Name")]
        public string Name { get; set; }

        [Display(Name = "Dealer ID")]
        [Required]
        public int DealerID { get; set; }

        [StringLength(50, MinimumLength = 3)]
        [Display(Name = "Owner Name")]
        public string OwnerName { get; set; }

        [MaxLength(10)]
        [Display(Name = "Contact No")]
        public string ContactNo { get; set; }

        [StringLength(50, MinimumLength = 3)]
        public string Address { get; set; }

        [StringLength(50, MinimumLength = 3)]
        public string City { get; set; }

        [StringLength(50, MinimumLength = 3)]
        public string State { get; set; }

        public int Pincode { get; set; }


    }
}
