using System.ComponentModel.DataAnnotations;

namespace Cherukuri_Spring26.Models
{
    public class PropertyOwner
    {
        [Key]
        public int PropertyOwnerID { get; set; }

        public int? PropertyID { get; set; }

        public int? OwnerID { get; set; }

        public virtual Property Property { get; set; }
        public virtual Owner Owner { get; set; }
    }
}
