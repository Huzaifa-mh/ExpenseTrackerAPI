using ExpenseTrackerAPI.Data;
using ExpenseTrackerAPI.DTOs;
using ExpenseTrackerAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTrackerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class ExpensesController(ExpenseDbContext context) : ControllerBase
    {
        private readonly ExpenseDbContext _context = context;

        [HttpGet]
        public async Task<IActionResult> GetExpense([FromQuery] DateTime? startDate, DateTime? endDate)
        {
            var query = _context.Expenses.Include(x=> x.Category).AsQueryable();
            if(startDate.HasValue)
            {
                query = query.Where(x => x.Date >= startDate.Value);
            }
            if(endDate.HasValue)
            {
                query = query.Where(x => x.Date <= endDate);
            }

            var expense = await query.OrderByDescending(x => x.CreatedAt).ToListAsync();

            var expenseDto = expense.Select(c => new ExpenseResponseDTO
            {
                Id = c.Id,
                Amount = c.Amount,
                Date = c.Date,
                Description = c.Description,
                CreatedAt = c.CreatedAt,
                CategoryId = c.CategoryId,
                CategoryName = c.Category.Name,
                CategoryColor = c.Category.ColorCode
            }).ToList();
            return Ok(expenseDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetExpenseById(int id)
        {
            var expense = await _context.Expenses.Include(x => x.Category).FirstOrDefaultAsync(x => x.Id == id);
            if ( expense is null)
            {
                return NotFound();
            }
            var expenseDto =new ExpenseResponseDTO
            {
                Id = expense.Id,
                Amount = expense.Amount,
                Date = expense.Date,
                Description = expense.Description,
                CreatedAt = expense.CreatedAt,
                CategoryId = expense.CategoryId,
                CategoryName = expense.Category.Name,
                CategoryColor = expense.Category.ColorCode
            };
            return Ok(expenseDto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateExpense([FromBody] ExpenseDTO expenseDto)

        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var expense = new Expense
            {
                Amount = expenseDto.Amount,
                CategoryId = expenseDto.CategoryId,
                Date = expenseDto.Date,
                Description = expenseDto.Description,
                CreatedAt = DateTime.UtcNow
            };

            _context.Expenses.Add(expense);
            await _context.SaveChangesAsync();

            await _context.Entry(expense).Reference(e => e.Category).LoadAsync();

            var response = new ExpenseResponseDTO
            {
                Id = expense.Id,
                Amount = expense.Amount,
                Date = expense.Date,
                Description = expense.Description,
                CreatedAt = expense.CreatedAt,
                CategoryId = expense.CategoryId,
                CategoryName = expense.Category.Name,
                CategoryColor = expense.Category.ColorCode
            };

            return CreatedAtAction(nameof(GetExpenseById), new {id = expense.Id}, response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExpense(int id)
        {
            var expense = await _context.Expenses.FindAsync(id);
            if(expense is null)
            {
                return NotFound();
            }
            _context.Expenses.Remove(expense);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
