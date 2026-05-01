using System.ComponentModel.DataAnnotations;

namespace Cherukuri_Spring26.Models
{
    public class Person
    {
        [Key]
        public int PersonID { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "First Name must be between 2 and 50 characters.")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Last Name must be between 2 and 50 characters.")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [StringLength(15, MinimumLength = 7, ErrorMessage = "Phone must be between 7 and 15 characters.")]
        [Phone]
        [Display(Name = "Phone Number")]
        public string Phone { get; set; }

        [StringLength(100, MinimumLength = 5, ErrorMessage = "Email must be between 5 and 100 characters.")]
        [EmailAddress]
        [Required]
        [Display(Name = "Email Address")]
        public string Email { get; set; }

        
        public string? UserID { get; set; }

    }
}