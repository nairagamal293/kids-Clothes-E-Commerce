using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Ecomerce.Data;
using Ecomerce.Entities;

[Route("api/[controller]")]
[ApiController]
public class ReviewController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public ReviewController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet("{productId}")]
    public IActionResult GetReviews(int productId)
    {
        var reviews = _context.Reviews.Where(r => r.ProductId == productId).ToList();
        return Ok(reviews);
    }

    [HttpPost]
    public IActionResult AddReview(Review review)
    {
        _context.Reviews.Add(review);
        _context.SaveChanges();
        return CreatedAtAction(nameof(GetReviews), new { productId = review.ProductId }, review);
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteReview(int id)
    {
        var review = _context.Reviews.Find(id);
        if (review == null) return NotFound();
        _context.Reviews.Remove(review);
        _context.SaveChanges();
        return NoContent();
    }
}
