using BudgetApp.Data;
using BudgetApp.Interfaces;
using BudgetApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BudgetApp.Servicies;

public class TransactionService : ITransactionService
{
    private readonly BudgetDbContext _context;
    private readonly ICategoryService _categoryService;

    public TransactionService(BudgetDbContext context, ICategoryService categoryService)
    {
        _categoryService = categoryService;
        _context = context;
    }

    public async Task<bool> AddTransactionAsync(Transaction transaction)
    {
        await _context.Transactions.AddAsync(transaction);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<List<Transaction>> GetAllTransactions()
    {
        var transactions = await _context.Transactions.ToListAsync();
        transactions.ForEach(transaction =>
            transaction.Category = _categoryService.GetCategoryById(transaction.CategoryId).Result);
        return transactions;
    }

    public async Task<List<Transaction>> GetTransactions(string description, Category? category = null)
    {
        if (description == null)
        {
            description = "";
        }

        var allTransactions = await GetAllTransactions();
        if (category != null && category.Id != 0)
        {
            allTransactions = allTransactions.Where(t => t.Category == category).ToList();
        }
        return allTransactions.Where(t =>
            t.Description.Contains(description)
        ).ToList();
    }

    public async Task<List<Transaction>> GetTransactions(Category category)
    {
        var allTransactions = await GetAllTransactions();
        return allTransactions.Where(t => t.Category == category).ToList();
    }

    public async Task<List<Transaction>> GetTransactions(int amount)
    {
        var allTransactions = await GetAllTransactions();
        return allTransactions.Where(t => t.Amount.ToString().Contains(amount.ToString())).ToList();
    }

    public async Task<Transaction> GetTransactionById(int id)
    {
        return await _context.Transactions.FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task<bool> DeleteTransactionAsync(int id)
    {
        var transaction = await _context.Transactions.FirstOrDefaultAsync(t => t.Id == id);
        if (transaction != null)
        {
            _context.Transactions.Remove(transaction);
            await _context.SaveChangesAsync();
            return true;
        }

        return false;
    }

    public async Task<bool> UpdateTransactionAsync(Transaction transaction)
    {
        var newTransaction = await GetTransactionById(transaction.Id);
        if (newTransaction == null) return false;
        newTransaction.Description = transaction.Description;
        newTransaction.Amount = transaction.Amount;
        newTransaction.Date = transaction.Date;
        newTransaction.CategoryId = transaction.CategoryId;
        //_context.Transactions.Update(transaction);
        await _context.SaveChangesAsync();
        return true;
    }
}