using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SMK_Restaurant_API.Data;

namespace SMK_Restaurant_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private readonly AppDbContext _context;

        public MenuController(AppDbContext context)
        {
            _context = context;
        }

        // ✅ GET: Semua menu
        [HttpGet]
        [AllowAnonymous] // Boleh diakses tanpa login
        public async Task<IActionResult> GetAll()
        {
            var menus = await _context.Msmenu.ToListAsync();
            return Ok(menus);
        }

        [HttpGet("{id:int}")]
        [AllowAnonymous] // Boleh diakses tanpa login
        public async Task<IActionResult> GetById(int id)
        {
            var menu = await _context.Msmenu.FindAsync(id);
            return Ok(menu);
        }
    }
}
