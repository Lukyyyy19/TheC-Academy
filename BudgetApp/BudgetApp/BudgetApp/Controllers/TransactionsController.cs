using BudgetApp.Data;
using BudgetApp.Interfaces;
using BudgetApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace BudgetApp.Controllers;

public class TransactionsController : Controller
{
    private readonly ICategoryService _categoryService;
    private readonly ITransactionService _transactionService;

    public TransactionsController(BudgetDbContext context, ICategoryService categoryService,
        ITransactionService transactionService)
    {
        _categoryService = categoryService;
        _transactionService = transactionService;
    }

    // GET
    public async Task<IActionResult> Index(string description, int categoryId)
    {
        ViewBag.Categories = await _categoryService.GetAllCategories();
        var category = await _categoryService.GetCategoryById(categoryId);
        var transactions = await _transactionService.GetTransactions(description, category);
        return View(transactions);
    }

    private async Task<List<Transaction>> GetTransactionsList()
    {
        ViewBag.Categories = await _categoryService.GetAllCategories();
        var transactions = await _transactionService.GetAllTransactions();
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

    public async Task<IActionResult> Edit(int id)
    {
        var transaction = await _transactionService.GetTransactionById(id);
        if (transaction != null)
        {
            var transactionViewModel = new TransactionViewModel
            {
                Transaction = transaction,
                Categories = _categoryService.GetAllCategories().Result
            };
            return PartialView(transactionViewModel);
        }

        TempData["ErrorMessage"] = "Transaction not found";
        return RedirectToAction("Index", await GetTransactionsList());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit([Bind("Transaction")] TransactionViewModel transactionViewModel)
    {
        ModelState.Remove("Categories");
        var transaction = transactionViewModel.Transaction;
        if (ModelState.IsValid)
        {
            if (await _transactionService.UpdateTransactionAsync(transaction))
            {
                TempData["SuccessMessage"] = "Transaction updated successfully!";
            }
            else
            {
                TempData["ErrorMessage"] = "Failed to update transaction";
            }
        }
        else
        {
            TempData["ErrorMessage"] = "Failed to update transaction";
        }

        return RedirectToAction("Index", await GetTransactionsList());
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

    [HttpPost, ActionName("Delete")]
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