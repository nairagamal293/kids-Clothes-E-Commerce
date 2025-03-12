using Ecomerce.Entities;

namespace Ecomerce.Entities

{
    public class Cart
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;  // ✅ Add this

        public List<CartItem> CartItems { get; set; } = new List<CartItem>();  // ✅ Ensure initialization
    }


}
