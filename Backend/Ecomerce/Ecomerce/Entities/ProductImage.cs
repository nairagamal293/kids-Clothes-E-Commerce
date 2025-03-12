using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecomerce.Entities
{
    public class ProductImage
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int ProductId { get; set; }

        [ForeignKey("ProductId")]
        public Product Product { get; set; }

        [Required]
        public string ImagePath { get; set; } // ✅ Add this property
        public int DisplayOrder { get; set; }  // ✅ Add this
        public bool IsMainImage { get; set; } = false;  // ✅ Add this

    }
}
