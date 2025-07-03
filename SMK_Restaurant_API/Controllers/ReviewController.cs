using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SMK_Restaurant_API.Data;
using SMK_Restaurant_API.Dto;
using SMK_Restaurant_API.Models;

namespace SMK_Restaurant_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ReviewController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ReviewController(AppDbContext context)
        {
            _context = context;
        }

        // ✅ GET: Semua review
        [HttpGet]
        [AllowAnonymous] // Boleh diakses tanpa login
        public async Task<IActionResult> GetAll()
        {
            var reviews = await _context.Reviews.ToListAsync();
            return Ok(reviews);
        }

        // ✅ GET: Review berdasarkan Menu
        [HttpGet("menu/{menuId}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetByMenu(int menuId)
        {
            var reviews = await _context.Reviews
                .Where(r => r.MenuID == menuId)
                .ToListAsync();

            return Ok(reviews);
        }

        // ✅ POST: Tambah review
        [HttpPost]
        [RequestSizeLimit(10_000_000)]
        public async Task<IActionResult> Add([FromBody] ReviewRequest request)
        {
            if (request.Rating < 1 || request.Rating > 5)
                return BadRequest("Rating must be between 1 and 5.");

            var order = await _context.Headerorders
                .FirstOrDefaultAsync(o => o.OrderID == request.OrderID);

            if (order == null)
                return NotFound("Order not found.");

            var menu = await _context.Msmenus
                .FirstOrDefaultAsync(m => m.MenuID == request.MenuID);

            if (menu == null)
                return NotFound("Menu not found.");

            string photoPath = string.Empty;

            if (request.Photo != null)
            {
                if (!IsValidImage(request.Photo))
                    return BadRequest("Invalid image. Must be .jpg, .jpeg, .png and max 2MB.");

                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(request.Photo.FileName);
                var savePath = Path.Combine("wwwroot/images", fileName);

                using (var stream = new FileStream(savePath, FileMode.Create))
                {
                    await request.Photo.CopyToAsync(stream);
                }

                photoPath = "/images/" + fileName;
            }

            var review = new Review
            {
                OrderID = request.OrderID,
                MenuID = request.MenuID,
                Rating = request.Rating,
                ReviewText = request.ReviewText,
                Photo = photoPath,
                CreatedAt = DateTime.Now
            };

            _context.Reviews.Add(review);
            await _context.SaveChangesAsync();

            return Ok(review);
        }

        // ✅ PUT: Edit review
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ReviewRequest request)
        {
            var review = await _context.Reviews.FindAsync(id);
            if (review == null) return NotFound();

            string photoPath = review.Photo;

            if (request.Photo != null)
            {
                if (!IsValidImage(request.Photo))
                    return BadRequest("Invalid image. Must be .jpg, .jpeg, .png and max 2MB.");

                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(request.Photo.FileName);
                var savePath = Path.Combine("wwwroot/images", fileName);

                using (var stream = new FileStream(savePath, FileMode.Create))
                {
                    await request.Photo.CopyToAsync(stream);
                }

                photoPath = "/images/" + fileName;
            }

            review.Rating = request.Rating;
            review.ReviewText = request.ReviewText;
            review.Photo = photoPath;

            await _context.SaveChangesAsync();
            return Ok(review);
        }

        // ✅ DELETE: Hapus review
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var review = await _context.Reviews.FindAsync(id);
            if (review == null) return NotFound();

            _context.Reviews.Remove(review);
            await _context.SaveChangesAsync();
            return Ok("Review deleted.");
        }

        private bool IsValidImage(IFormFile file)
        {
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
            var maxFileSize = 2 * 1024 * 1024; // 2MB

            var extension = Path.GetExtension(file.FileName).ToLower();

            return file.Length <= maxFileSize && allowedExtensions.Contains(extension);
        }


    }
}
