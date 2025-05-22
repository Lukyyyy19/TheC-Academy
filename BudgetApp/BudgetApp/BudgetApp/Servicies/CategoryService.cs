using BudgetApp.Data;
using BudgetApp.Interfaces;
using BudgetApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BudgetApp.Servicies;

public class CategoryService:ICategoryService
{
    private readonly BudgetDbContext _context;
    public CategoryService(BudgetDbContext context)
    {
        _context = context;
    }
    
    public async Task<List<Category>> GetAllCategories()
    {
        return await _context.Categories.ToListAsync();
    }

    public async Task<Category> GetCategoryById(int id)
    {
        return await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
    }
}