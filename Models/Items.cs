using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication3.Models
{
    public class Items
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ItemId { get; set; }
        [Required]
        public string ItemCode { get; set; }

        [Required]
        public string ItemTitle { get; set; }

        public string Description { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Sale Price must be greater than 0.")]
        public decimal SalePrice { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Purchase Price must be greater than 0.")]
        public decimal PurchasePrice { get; set; }

        public int LocationId { get; set; }

        public bool IsActive { get; set; }

        public Locations Location { get; set; }
    
}
}
