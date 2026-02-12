using ExpenseTrackerAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTrackerAPI.Data
{
    public class ExpenseDbContext(DbContextOptions<ExpenseDbContext> options) : DbContext(options)
    {
        public DbSet<Expense> Expenses => Set<Expense>();
        public DbSet<Category> Categories => Set<Category>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           base.OnModelCreating(modelBuilder);

            // ✅ Configure DateTime columns for PostgreSQL
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                foreach (var property in entityType.GetProperties())
                {
                    if (property.ClrType == typeof(DateTime) || property.ClrType == typeof(DateTime?))
                    {
                        property.SetColumnType("timestamp with time zone");
                    }
                }
            }

            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Food & Dining", ColorCode="#FF6384" },
                new Category { Id = 2, Name = "Transportation", ColorCode = "#36A2EB" },
                new Category { Id = 3, Name = "Entertainment", ColorCode = "#FFCE56" },
                new Category { Id = 4, Name = "Shopping", ColorCode = "#4BC0C0" },
                new Category { Id = 5, Name = "Bills & Utilities", ColorCode = "#9966FF" },
                new Category { Id = 6, Name = "Healthcare", ColorCode = "#FF9F40" },
                new Category { Id = 7, Name = "Others", ColorCode = "#C9CBCF" }
            );
        }
    }
    
}
