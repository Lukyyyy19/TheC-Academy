using BudgetApp.Data;
using BudgetApp.Interfaces;
using BudgetApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BudgetApp.Servicies;

public class CategoryService : ICategoryService
{
    private readonly BudgetDbContext _context;
    private ITransactionService _transactionService;

    public CategoryService(BudgetDbContext context)
    {
        _context = context;
    }

    public async Task<List<Category>> GetAllCategories()
    {
        var categories = await _context.Categories.ToListAsync();
        // categories.ForEach(category =>
        //     category.Transactions = _transactionService.GetAllTransactions().Result
        //         .Where(transaction => transaction.CategoryId == category.Id).ToList());
        return categories;
    }

    public async Task<Category> GetCategoryById(int id)
    {
        return await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<bool> AddCategoryAsync(Category category)
    {
        await _context.Categories.AddAsync(category);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteCategoryAsync(int id)
    {
        var category = await _context.Categories.FirstOrDefaultAsync(t => t.Id == id);
        if (category != null)
        {
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return true;
        }

        return false;
    }

    public async Task<bool> UpdateCategoryAsync(Category category)
    {
        var newCategory = await GetCategoryById(category.Id);
        if (newCategory == null) return false;
        newCategory.Name = category.Name;
        await _context.SaveChangesAsync();
        return true;
    }
}