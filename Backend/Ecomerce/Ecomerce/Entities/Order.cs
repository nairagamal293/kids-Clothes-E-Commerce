using System;
using System.Collections.Generic;

namespace Ecomerce.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public int UserId { get; set; } // ✅ Foreign Key to User
        public User User { get; set; }

        public DateTime OrderDate { get; set; } = DateTime.UtcNow; // ✅ Auto-set order date

        public string OrderStatus { get; set; } = "Pending"; // ✅ Default status: Pending
        public string PaymentStatus { get; set; } = "Unpaid"; // ✅ Default: Unpaid

        public decimal TotalAmount { get; set; } // ✅ Total price of order

        public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>(); // ✅ List of Order Items
    }
}
