using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Ecomerce.Data;
using Ecomerce.Entities;

[Route("api/[controller]")]
[ApiController]
public class ShippingController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public ShippingController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet("{userId}")]
    public IActionResult GetShippingAddresses(int userId)
    {
        var addresses = _context.ShippingAddresses.Where(s => s.UserId == userId).ToList();
        return Ok(addresses);
    }

    [HttpPost]
    public IActionResult AddShippingAddress(ShippingAddress address)
    {
        _context.ShippingAddresses.Add(address);
        _context.SaveChanges();
        return CreatedAtAction(nameof(GetShippingAddresses), new { userId = address.UserId }, address);
    }

    [HttpDelete("{id}")]
    public IActionResult RemoveShippingAddress(int id)
    {
        var address = _context.ShippingAddresses.Find(id);
        if (address == null) return NotFound();
        _context.ShippingAddresses.Remove(address);
        _context.SaveChanges();
        return NoContent();
    }
}
