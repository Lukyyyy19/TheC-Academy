using BudgetApp.Models;

namespace BudgetApp.Interfaces;

public interface ITransactionService
{
    public Task<bool> AddTransactionAsync(Transaction transaction);
    public Task<List<Transaction>> GetAllTransactions();
    public Task<Transaction> GetTransactionById(int id);
    public Task<bool> DeleteTransactionAsync(int id);
}