using Ecomerce.Entities;  // ✅ Add this

namespace Ecomerce.Entities
{
    public class ProductSize
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }  // ✅ Ensure Product is recognized
        public string Size { get; set; }
        public decimal Price { get; set; }

        public decimal BulkDiscountPercentage { get; set; }  // ✅ Add this

    }
}
