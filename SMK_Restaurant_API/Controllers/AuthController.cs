using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SMK_Restaurant_API.Data;
using SMK_Restaurant_API.Services;

namespace SMK_Restaurant_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly JwtService _jwt;

        public AuthController(AppDbContext context, JwtService jwt)
        {
            _context = context;
            _jwt = jwt;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest("Email and Password are required.");

            var user = await _context.Msmember
                .FirstOrDefaultAsync(u => u.Email == request.Email && u.Password == request.Password);

            if (user == null) return Unauthorized("Invalid credentials");

            var token = _jwt.GenerateToken(user);
            return Ok(new { token });
        }

    }
}
