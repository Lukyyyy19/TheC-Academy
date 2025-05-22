using BudgetApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BudgetApp.Data;

public class BudgetDbContext:DbContext
{
    public DbSet<Transaction> Transactions => Set<Transaction>();
    public DbSet<Category> Categories => Set<Category>();
    
    public BudgetDbContext(DbContextOptions<BudgetDbContext> options):base(options){}
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>()
            .HasMany(e=>e.Transactions)
            .WithOne(e=>e.Category)
            .HasForeignKey(e=>e.CategoryId)
            .HasPrincipalKey(e=>e.Id);
    }
}