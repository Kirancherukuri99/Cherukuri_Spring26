using Cherukuri_Spring26.Models;
using System.ComponentModel.DataAnnotations;

namespace Cherukuri_Spring26.Models
{
    public class LeaseAgreement
    {
        [Key]
        public int LeaseID { get; set; }

        public int? PropertyID { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [Display(Name = "Monthly Rent")]
        [Range(300.00, 50000.00, ErrorMessage = "Monthly Rent must be between $300.00 and $50,000.00.")]
        public decimal MonthlyRent { get; set; }

        public virtual Property Property { get; set; }
        public virtual ICollection<Tenant> Tenants { get; set; }

    }
}
