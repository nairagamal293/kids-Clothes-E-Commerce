using System;
using System.ComponentModel.DataAnnotations;

namespace Ecomerce.Entities
{
    public class Discount
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Code { get; set; }  // Promo Code

        [Required]
        [Range(0, 100)]
        public decimal Percentage { get; set; }  // Discount percentage

        public DateTime ExpiryDate { get; set; } // When the discount expires

        public bool IsActive { get; set; } = true;
    }
}
