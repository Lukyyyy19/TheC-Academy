namespace BudgetApp.Models;

public class Category
{
    int Id { get; set; }
    string Name { get; set; }
    ICollection<Transaction> Transactions { get; }
}