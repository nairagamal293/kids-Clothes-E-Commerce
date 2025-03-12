using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Ecomerce.Data;
using Ecomerce.Entities;
using Microsoft.AspNetCore.Authorization;

[Route("api/[controller]")]
[ApiController]
public class WishlistController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public WishlistController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet("{userId}")]
    public IActionResult GetWishlist(int userId)
    {
        var wishlist = _context.Wishlists.Where(w => w.UserId == userId).ToList();
        return Ok(wishlist);
    }

    [HttpPost]
    public IActionResult AddToWishlist(Wishlist wishlist)
    {
        _context.Wishlists.Add(wishlist);
        _context.SaveChanges();
        return Ok(wishlist);
    }

    [HttpDelete("{id}")]
    public IActionResult RemoveFromWishlist(int id)
    {
        var wishlistItem = _context.Wishlists.Find(id);
        if (wishlistItem == null) return NotFound();
        _context.Wishlists.Remove(wishlistItem);
        _context.SaveChanges();
        return NoContent();
    }
}
