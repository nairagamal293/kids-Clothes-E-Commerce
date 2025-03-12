using Ecomerce.Data;
using Ecomerce.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class CartController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public CartController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: api/cart
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CartItem>>> GetCartItems()
    {
        return await _context.CartItems.Include(c => c.Product).ToListAsync();
    }

    // POST: api/cart
    [HttpPost]
    public async Task<ActionResult<CartItem>> AddToCart(CartItem cartItem)
    {
        var product = await _context.Products.FindAsync(cartItem.ProductId);
        if (product == null)
        {
            return NotFound("Product not found.");
        }

        var existingCartItem = await _context.CartItems
            .FirstOrDefaultAsync(c => c.ProductId == cartItem.ProductId);

        if (existingCartItem != null)
        {
            existingCartItem.Quantity += cartItem.Quantity;
        }
        else
        {
            _context.CartItems.Add(cartItem);
        }

        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetCartItems), cartItem);
    }

    // DELETE: api/cart/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> RemoveFromCart(int id)
    {
        var cartItem = await _context.CartItems.FindAsync(id);
        if (cartItem == null)
        {
            return NotFound();
        }

        _context.CartItems.Remove(cartItem);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
