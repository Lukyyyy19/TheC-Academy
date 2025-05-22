using BudgetApp.Data;
using BudgetApp.Interfaces;
using BudgetApp.Models;

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
}