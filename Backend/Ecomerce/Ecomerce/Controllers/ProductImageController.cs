using Ecomerce.Data;
using Ecomerce.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Ecomerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductImageController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public ProductImageController(ApplicationDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }

        // ✅ Get all product images
        [HttpGet]
        public async Task<IActionResult> GetProductImages()
        {
            var images = await _context.ProductImages.ToListAsync();
            return Ok(images);
        }

        // ✅ Upload an image
        [HttpPost("upload")]
        public async Task<IActionResult> UploadProductImage(int productId, IFormFile imageFile, int displayOrder, bool isMainImage)
        {
            if (imageFile == null || imageFile.Length == 0)
            {
                return BadRequest("No image file provided.");
            }

            try
            {
                // ✅ Ensure the upload folder exists
                var uploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "products");
                if (!Directory.Exists(uploadFolder))
                {
                    Directory.CreateDirectory(uploadFolder);
                }

                // ✅ Generate unique file name
                var fileName = $"{Guid.NewGuid()}_{Path.GetFileName(imageFile.FileName)}";
                var filePath = Path.Combine(uploadFolder, fileName);

                // ✅ Save file
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(stream);
                }

                // ✅ Save file path to database
                var productImage = new ProductImage
                {
                    ProductId = productId,
                    ImagePath = $"/uploads/products/{fileName}",  // ✅ Relative path
                    DisplayOrder = displayOrder,
                    IsMainImage = isMainImage
                };

                _context.ProductImages.Add(productImage);
                await _context.SaveChangesAsync();

                return Ok(new { Message = "Image uploaded successfully!", productImage });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }


        // ✅ Delete an image
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductImage(int id)
        {
            var image = await _context.ProductImages.FindAsync(id);
            if (image == null)
                return NotFound("Image not found.");

            string imagePath = Path.Combine(_hostEnvironment.WebRootPath, image.ImagePath.TrimStart('/'));

            if (System.IO.File.Exists(imagePath))
                System.IO.File.Delete(imagePath);

            _context.ProductImages.Remove(image);
            await _context.SaveChangesAsync();

            return Ok("Image deleted successfully.");
        }

        // ✅ Update an image (change DisplayOrder or IsMainImage)
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProductImage(int id, [FromBody] ProductImage updatedImage)
        {
            var existingImage = await _context.ProductImages.FindAsync(id);
            if (existingImage == null)
                return NotFound("Image not found.");

            existingImage.DisplayOrder = updatedImage.DisplayOrder;
            existingImage.IsMainImage = updatedImage.IsMainImage;

            await _context.SaveChangesAsync();

            return Ok("Image updated successfully.");
        }
    }
}
