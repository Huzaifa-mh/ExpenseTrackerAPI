using ExpenseTrackerAPI.Data;
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
        public async Task<IActionResult> GetExpense()
        {
            var expenses = await _context.Expenses.OrderBy(x => x.CreatedAt).Include(x=> x.Category).ToListAsync();
            return Ok(expenses);
        }

        [HttpGet]
        public async Task<IActionResult> GetExpenseById([FromBody] int id)
        {
            var expense = await _context.Expenses.FindAsync(id);
            if ( expense is null)
            {
                return NotFound();
            }
            return Ok(expense);
        }

        [HttpPost]
        public async Task<IActionResult<Expense>> CreateExpense([FromBody] Expense expense)
        
        { 
            
        }

    }
}
