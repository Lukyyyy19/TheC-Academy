using System.ComponentModel.DataAnnotations;

namespace BudgetApp.Models;

public class Transaction
{
    public int Id { get; set; }
    public string Description { get; set; }
    [DataType(DataType.Currency)] public decimal Amount { get; set; }
    [DataType(DataType.Date)] public DateTime Date { get; set; }
    
    public Category Category { get; set; }
    public int CategoryId { get; set; }
}