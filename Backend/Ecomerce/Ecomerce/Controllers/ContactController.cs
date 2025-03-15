using Ecomerce.Data;
using Ecomerce.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class ContactController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public ContactController(ApplicationDbContext context)
    {
        _context = context;
    }

    // ✅ Save contact message in the database
    [HttpPost]
    public async Task<IActionResult> CreateContactMessage([FromBody] ContactMessage message)
    {
        if (message == null || string.IsNullOrWhiteSpace(message.Name) ||
            string.IsNullOrWhiteSpace(message.Email) || string.IsNullOrWhiteSpace(message.Message))
        {
            return BadRequest(new { Message = "All fields are required!" });
        }

        try
        {
            message.CreatedAt = DateTime.UtcNow;
            _context.ContactMessages.Add(message);
            await _context.SaveChangesAsync();

            return Ok(new { Id = message.Id, Message = "Contact message saved successfully." });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Message = "An unexpected error occurred.", Error = ex.Message });
        }
    }
}
