namespace Ecomerce.Entities
{
    public class CartItem
    {
        public int Id { get; set; }

        // ✅ Foreign key for Cart
        public int CartId { get; set; }
        public Cart Cart { get; set; }

        // ✅ Foreign key for Product
        public int ProductId { get; set; }
        public Product Product { get; set; }  // ✅ Ensure this navigation property exists

        // ✅ Foreign key for ProductSize
        public int ProductSizeId { get; set; }
        public ProductSize ProductSize { get; set; }

        public int Quantity { get; set; }
    }
}
