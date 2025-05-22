using BudgetApp.Models;

namespace BudgetApp.Interfaces;

public interface ITransactionService
{
    public Task<bool> AddTransactionAsync(Transaction transaction);
}