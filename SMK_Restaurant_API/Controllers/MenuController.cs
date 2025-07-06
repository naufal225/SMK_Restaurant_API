using AutoMapper;
using AutoMapper.Execution;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SMK_Restaurant_API.Data;
using SMK_Restaurant_API.Dto;
using SMK_Restaurant_API.Models;
using System;
using System.Security.Claims;

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
        public async Task<IActionResult> GetAll()
        {
            var memberId = User.FindFirst(ClaimTypes.Name)?.Value;
            if (memberId == null) return Unauthorized();

            var menus = await _context.Msmenu.ToListAsync();

            var menusDto = _mapper.Map<List<MenuDto>>(menus);

            var reviewedMenuIds = await _context.Review
                .Where(r => r.MemberID == memberId)
                .Select(r => r.MenuID)
                .ToListAsync();

            var orderedMenuIds = await _context.DetailOrder
                .Where(d => _context.Headerorder
                    .Any(h => h.OrderID == d.OrderID && h.MemberID == memberId))
                .Select(d => d.MenuID)
                .Distinct()
                .ToListAsync();

            foreach (var menuDto in menusDto)
            {
                menuDto.IsReviewed = reviewedMenuIds.Contains(menuDto.MenuID);
                menuDto.IsOrdered = orderedMenuIds.Contains(menuDto.MenuID);
            }

            return Ok(menusDto);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var memberId = User.FindFirst(ClaimTypes.Name)?.Value;
            if (memberId == null) return Unauthorized();

            var menu = await _context.Msmenu.FindAsync(id);

            if(menu == null)
            {
                return NotFound();
            }

            var menuDto = _mapper.Map<MenuDto>(menu);

            var reviewedMenuIds = await _context.Review
                .Where(r => r.MemberID == memberId)
                .Select(r => r.MenuID)
                .ToListAsync();

            var orderedMenuIds = await _context.DetailOrder
                .Where(d => _context.Headerorder
                    .Any(h => h.OrderID == d.OrderID && h.MemberID == memberId))
                .Select(d => d.MenuID)
                .Distinct()
                .ToListAsync();
            
            menuDto.IsReviewed = reviewedMenuIds.Contains(menuDto.MenuID);
            menuDto.IsOrdered = orderedMenuIds.Contains(menuDto.MenuID);
            

            return Ok(menuDto);
        }
    }
}
