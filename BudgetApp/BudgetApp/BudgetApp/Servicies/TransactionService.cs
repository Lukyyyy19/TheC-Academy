using BudgetApp.Data;
using BudgetApp.Interfaces;
using BudgetApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BudgetApp.Servicies;

public class TransactionService: ITransactionService
{
    private readonly BudgetDbContext _context;
    public TransactionService(BudgetDbContext context)
    {
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
        return await _context.Transactions.ToListAsync();
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
}