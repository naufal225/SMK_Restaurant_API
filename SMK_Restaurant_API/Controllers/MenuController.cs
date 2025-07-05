using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SMK_Restaurant_API.Data;
using SMK_Restaurant_API.Dto;
using SMK_Restaurant_API.Models;
using System;

namespace SMK_Restaurant_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public MenuController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // ✅ GET: Semua menu
        [HttpGet]
        [AllowAnonymous] // Boleh diakses tanpa login
        public async Task<IActionResult> GetAll()
        {
            var menus = await _context.Msmenu.ToListAsync();

            var menusDto = _mapper.Map<List<MenuDto>>(menus);

            return Ok(menusDto);
        }

        [HttpGet("{id:int}")]
        [AllowAnonymous] // Boleh diakses tanpa login
        public async Task<IActionResult> GetById(int id)
        {
            var menu = await _context.Msmenu.FindAsync(id);

            if(menu == null)
            {
                return NotFound();
            }

            var menuDto = _mapper.Map<MenuDto>(menu);
            return Ok(menuDto);
        }
    }
}
