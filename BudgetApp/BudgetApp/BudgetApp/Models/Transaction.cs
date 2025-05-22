using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BudgetApp.Models;

public class Transaction
{
    public int Id { get; set; }
    public string Description { get; set; }

    [DataType(DataType.Currency), Required, Range(0.01,
         double.MaxValue, ErrorMessage = "Amount must be greater than zero.")]
    public decimal Amount { get; set; }

    [DataType(DataType.Date), Required] public DateTime Date { get; set; }
    [JsonIgnore]
    public Category? Category { get; set; }
    [Required] public int CategoryId { get; set; }
}