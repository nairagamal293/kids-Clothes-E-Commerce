namespace Ecomerce.Entities
{
    public class OrderItem
    {
        public int Id { get; set; }

        // Foreign key for Order
        public int OrderId { get; set; }
        public Order Order { get; set; }

        // Foreign key for Product
        public int ProductId { get; set; }
        public Product Product { get; set; }

        // Foreign key for ProductSize
        public int ProductSizeId { get; set; }
        public ProductSize ProductSize { get; set; }

        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
