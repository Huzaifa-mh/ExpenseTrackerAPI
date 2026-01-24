using System.ComponentModel.DataAnnotations;

namespace ExpenseTrackerAPI.Models
{
    public class Category
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string? Name { get; set; } = string.Empty;
        [MaxLength(7)]
        public string? ColorCode { get; set; } = "#000000";

        public ICollection<Expense>? Expenses { get; set; } = new List<Expense>();
    }
}
