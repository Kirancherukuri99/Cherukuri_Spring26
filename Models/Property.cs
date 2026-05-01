using Cherukuri_Spring26.Models;
using System.ComponentModel.DataAnnotations;

namespace Cherukuri_Spring26.Models
{
    public class Property
    {
        [Key]
        public int PropertyID { get; set; }

        [Required]
        [StringLength(200, MinimumLength = 5, ErrorMessage = "Address must be between 5 and 200 characters.")]
        [Display(Name = "Street Address")]
        public string Address { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "City must be between 2 and 100 characters.")]
        public string City { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "State must be between 2 and 50 characters.")]
        public string State { get; set; }

        [Required]
        [StringLength(10, MinimumLength = 5, ErrorMessage = "Zip Code must be between 5 and 10 characters.")]
        [Display(Name = "Zip Code")]
        public string ZipCode { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [Display(Name = "Listing Price")]
        [Range(100.00, 10000.00, ErrorMessage = "Listing Price must be between $100.00 and $10,000.00.")]
        public decimal ListingPrice { get; set; }


        public virtual ICollection<PropertyOwner> PropertyOwners { get; set; }
        public virtual ICollection<LeaseAgreement> LeaseAgreements { get; set; }
    }
}

