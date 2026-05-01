using Cherukuri_Spring26.Models;
using System.ComponentModel.DataAnnotations;

namespace Cherukuri_Spring26.Models
{
    public class Tenant : Person
    {

        public int? LeaseID { get; set; }

        [StringLength(100, MinimumLength = 2, ErrorMessage = "Employer Name must be between 2 and 100 characters.")]
        [Display(Name = "Employer Name")]
        public string EmployerName { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [Display(Name = "Monthly Income")]
        [Range(500.00, 10000.00, ErrorMessage = "Monthly Income must be between $500.00 and $10,000.00.")]
        public decimal MonthlyIncome { get; set; }

        public virtual LeaseAgreement LeaseAgreement { get; set; }
    }
}
