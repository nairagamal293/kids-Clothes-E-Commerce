using Ecomerce.Data;
using Ecomerce.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Ecomerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PaymentController(ApplicationDbContext context)
        {
            _context = context;
        }

        // ✅ Process Payment
        [HttpPost("process")]
        public async Task<IActionResult> ProcessPayment(int orderId)
        {
            var order = await _context.Orders.FindAsync(orderId);

            if (order == null)
                return NotFound(new { message = "Order not found" });

            if (order.PaymentStatus == "Paid")
                return BadRequest(new { message = "Order is already paid" });

            // Simulate a successful payment
            order.PaymentStatus = "Paid";
            order.OrderStatus = "Processing";
            await _context.SaveChangesAsync();

            return Ok(new { message = "Payment successful", orderId = order.Id });
        }

        // ✅ Get Payment Status
        [HttpGet("status/{orderId}")]
        public async Task<IActionResult> GetPaymentStatus(int orderId)
        {
            var order = await _context.Orders.FindAsync(orderId);

            if (order == null)
                return NotFound(new { message = "Order not found" });

            return Ok(new { orderId = order.Id, paymentStatus = order.PaymentStatus });
        }

        // ✅ Refund Payment
        [HttpPost("refund/{orderId}")]
        public async Task<IActionResult> RefundPayment(int orderId)
        {
            var order = await _context.Orders.FindAsync(orderId);

            if (order == null)
                return NotFound(new { message = "Order not found" });

            if (order.PaymentStatus != "Paid")
                return BadRequest(new { message = "Order is not paid, cannot refund" });

            // Simulate a refund process
            order.PaymentStatus = "Refunded";
            order.OrderStatus = "Canceled";
            await _context.SaveChangesAsync();

            return Ok(new { message = "Payment refunded successfully", orderId = order.Id });
        }
    }
}
