using Cherukuri_Spring26.Models;
using System.ComponentModel.DataAnnotations;

namespace Cherukuri_Spring26.Models
{
    public class Owner : Person
    {
        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "License Number must be between 3 and 50 characters.")]
        [Display(Name = "License Number")]
        public string LicenseNumber { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Registered Date")]
        public DateTime RegisteredDate { get; set; }

        public virtual ICollection<PropertyOwner> PropertyOwners { get; set; }

    }
}
