using BudgetApp.Models;

namespace BudgetApp.Interfaces;

public interface ITransactionService
{
    public Task<bool> AddTransactionAsync(Transaction transaction);
    public Task<List<Transaction>> GetAllTransactions();
    public Task<List<Transaction>> GetTransactions(string description);
    public Task<List<Transaction>> GetTransactions(Category category);
    public Task<List<Transaction>> GetTransactions(int amount);
    
    public Task<Transaction> GetTransactionById(int id);
    public Task<bool> DeleteTransactionAsync(int id);
    public Task<bool> UpdateTransactionAsync(Transaction transaction);
}