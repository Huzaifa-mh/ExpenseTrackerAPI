using System.ComponentModel.DataAnnotations;

namespace ExpenseTrackerAPI.DTOs
{
    public class ExpenseDTO
    {
        [Required]
        [Range(0.01, double.MaxValue)]
        public decimal Amount { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [MaxLength(200)]
        public string? Description { get; set; }
    }
}
