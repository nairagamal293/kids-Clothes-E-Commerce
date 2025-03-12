using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Ecomerce.Data;
using Ecomerce.Entities;

[Route("api/[controller]")]
[ApiController]
public class DiscountController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public DiscountController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult GetDiscounts()
    {
        return Ok(_context.Discounts.ToList());
    }

    [HttpPost]
    public IActionResult AddDiscount(Discount discount)
    {
        _context.Discounts.Add(discount);
        _context.SaveChanges();
        return CreatedAtAction(nameof(GetDiscounts), discount);
    }

    [HttpDelete("{id}")]
    public IActionResult RemoveDiscount(int id)
    {
        var discount = _context.Discounts.Find(id);
        if (discount == null) return NotFound();
        _context.Discounts.Remove(discount);
        _context.SaveChanges();
        return NoContent();
    }
}
