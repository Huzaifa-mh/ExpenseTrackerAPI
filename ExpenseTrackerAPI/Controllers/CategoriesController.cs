using ExpenseTrackerAPI.Data;
using ExpenseTrackerAPI.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTrackerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController(ExpenseDbContext context) : ControllerBase
    {
        private readonly ExpenseDbContext _context = context;

        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            var categories = await _context.Categories.ToListAsync();
            
            var categoriesDto = categories.Select(c => new CategoryDTO
            {
                Id = c.Id,
                Name = c.Name,
                ColorCode = c.ColorCode
            }).ToList();

            return Ok(categoriesDto);
        }
    }
}
