using Ecomerce.Data;
using Ecomerce.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

[Route("api/contact")]
[ApiController]
public class ContactController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public ContactController(ApplicationDbContext context)
    {
        _context = context;
    }

    // POST: api/contact
    [HttpPost]
    public async Task<IActionResult> CreateContactMessage([FromBody] ContactMessage message)
    {
        if (message == null)
        {
            return BadRequest("Invalid message data.");
        }

        try
        {
            message.CreatedAt = DateTime.UtcNow; // Ensure CreatedAt is set

            _context.ContactMessages.Add(message);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "Contact message saved successfully." });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Message = "An unexpected error occurred.", Error = ex.Message });
        }
    }

}
