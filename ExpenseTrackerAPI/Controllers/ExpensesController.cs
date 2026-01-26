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
            var expenses = await _context.Expenses.OrderByDescending(x => x.CreatedAt).Include(x=> x.Category).ToListAsync();
            return Ok(expenses);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetExpenseById(int id)
        {
            var expense = await _context.Expenses.Include(x => x.Category).FirstOrDefaultAsync(x => x.Id == id);
            if ( expense is null)
            {
                return NotFound();
            }
            return Ok(expense);
        }

        //[HttpPost]
        //public async Task<IActionResult<Expense>> CreateExpense([FromBody] Expense expense)
        
        //{ 
        //    if(!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //}

    }
}
