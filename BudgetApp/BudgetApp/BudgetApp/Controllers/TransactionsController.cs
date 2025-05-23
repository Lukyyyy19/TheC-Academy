using BudgetApp.Data;
using BudgetApp.Interfaces;
using BudgetApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace BudgetApp.Controllers;

public class TransactionsController : Controller
{
    private readonly BudgetDbContext _context;
    private readonly ICategoryService _categoryService;
    private readonly ITransactionService _transactionService;

    public TransactionsController(BudgetDbContext context, ICategoryService categoryService,
        ITransactionService transactionService)
    {
        _categoryService = categoryService;
        _transactionService = transactionService;
        _context = context;
    }

    // GET
    public async Task<IActionResult> Index()
    {
        var transactions = await GetTransactionsList();
        return View(transactions);
    }

    private async Task<List<Transaction>> GetTransactionsList()
    {
        var transactions = await _transactionService.GetAllTransactions();
        transactions.ForEach(transaction =>
            transaction.Category = _categoryService.GetCategoryById(transaction.CategoryId).Result);
        return transactions;
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        var transactionViewModel = new TransactionViewModel
        {
            Transaction = new Transaction
            {
                Description = "",
                Amount = 0,
                Date = DateTime.Now,
                CategoryId = 0
            },
            Categories = await _categoryService.GetAllCategories()
        };
        return PartialView(transactionViewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Transaction")] TransactionViewModel newTransactionVM)
    {
        ModelState.Remove("Categories");
        var newTransaction = newTransactionVM.Transaction;
        if (ModelState.IsValid)
        {
            var transaction = new Transaction
            {
                Description = newTransaction.Description,
                Amount = newTransaction.Amount,
                Date = newTransaction.Date,
                CategoryId = newTransaction.CategoryId
            };
            if (await _transactionService.AddTransactionAsync(transaction))
            {
                TempData["SuccessMessage"] = "Transaction created successfully!";
            }
        }
        else
        {
            TempData["ErrorMessage"] = "Failed to add transaction";
        }

        return View("Index", await GetTransactionsList());
    }

    public async Task<IActionResult> Delete(int? id)
    {
        int transactionId = id ?? 0;
        var transaction = await _transactionService.GetTransactionById(transactionId);
        if (transaction != null)
        {
            ModelState.Remove("Categories");
            var transactionViewModel = new TransactionViewModel
            {
                Transaction = transaction,
            };
            return PartialView(transactionViewModel);
        }

        TempData["ErrorMessage"] = "Transaction not found";
        return RedirectToAction("Index", await GetTransactionsList());
    }
    [HttpPost,ActionName("Delete")]
    public async Task<IActionResult> Delete(int id)
    {
        if (await _transactionService.DeleteTransactionAsync(id))
        {
            TempData["SuccessMessage"] = "Transaction deleted successfully!";
        }
        else
        {
            TempData["ErrorMessage"] = "Failed to delete transaction";
        }

        return RedirectToAction("Index", await GetTransactionsList());
    }
}