using AutoMapper;
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
        private readonly IMapper _mapper;

        public ReviewController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // ✅ GET: Semua review
        [HttpGet]
        [AllowAnonymous] // Boleh diakses tanpa login
        public async Task<IActionResult> GetAll()
        {
            var reviews = await _context.Review.Include(r => r.Member).ToListAsync();

            var reviewsDto = _mapper.Map<List<ReviewDto>>(reviews);

            return Ok(reviewsDto);
        }

        // ✅ GET: Review berdasarkan Menu
        [HttpGet("menu/{menuId}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetByMenu(int menuId)
        {
            var reviews = await _context.Review
                .Where(r => r.MenuID == menuId)
                .Include(r => r.Member)
                .ToListAsync();

            var reviewDto = _mapper.Map<List<ReviewDto>>(reviews);

            return Ok(reviewDto);
        }

        // ✅ POST: Tambah review
        [HttpPost]
        [RequestSizeLimit(10_000_000)]
        public async Task<IActionResult> Add([FromForm] ReviewRequest request)
        {

            if (request.Rating < 1 || request.Rating > 5)
                return BadRequest("Rating must be between 1 and 5.");

            var menu = await _context.Msmenu
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
                MenuID = request.MenuID,
                Rating = request.Rating,
                ReviewText = request.ReviewText,
                Photo = photoPath,
                CreatedAt = DateTime.Now
            };

            _context.Review.Add(review);
            await _context.SaveChangesAsync();

            return Ok(review);
        }

        // ✅ PUT: Edit review
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromForm] ReviewRequest request)
        {
            var review = await _context.Review.FindAsync(id);
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
            var review = await _context.Review.FindAsync(id);
            if (review == null) return NotFound();

            _context.Review.Remove(review);
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
