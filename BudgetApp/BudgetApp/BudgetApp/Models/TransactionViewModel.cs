using Microsoft.AspNetCore.Mvc.Rendering;

namespace BudgetApp.Models;

public class TransactionViewModel
{
    public Transaction Transaction { get; set; }
    public List<Category> Categories { get; set; }
}