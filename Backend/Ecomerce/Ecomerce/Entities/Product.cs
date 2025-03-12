using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecomerce.Entities
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        // Foreign Key for Category
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public Category Category { get; set; }

        // Collection of Product Sizes
        public List<ProductSize> ProductSizes { get; set; }

        // Stock Quantity
        public int Quantity { get; set; }
        public string SKU { get; set; } 
        public bool IsFeatured { get; set; } = false; 
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow; 

        // List of Product Images
        public List<ProductImage> ProductImages { get; set; }
    }
}



