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
    public TransactionsController(BudgetDbContext context, ICategoryService categoryService,ITransactionService transactionService)
    {
        _categoryService = categoryService;
        _transactionService = transactionService;
        _context = context;
    }

    // GET
    public IActionResult Index()
    {
        //ESTO ES COMO UN MODELO DE LO QUE VA  AVER EL USUARIO POR DEFECTO
        var transactionViewModel = new TransactionViewModel
        {
            Transaction = new Transaction
            {
                Description = "",
                Amount = 0,
                Date = DateTime.Now,
                CategoryId = 0
            },
            Categories = _categoryService.GetAllCategories().Result
        };
        return View(transactionViewModel);
    }

    public IActionResult Create()
    {
        return PartialView("Create",new TransactionViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Transaction")]TransactionViewModel newTransactionVM)
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
                return Json(new { success = true, message = "Transaction created successfully!" });
            }
        }
        else
        {
            var errores = ModelState
                .Where(ms => ms.Value.Errors.Count > 0)
                .Select(ms => new
                {
                    Campo = ms.Key,
                    Errores = ms.Value.Errors.Select(e => e.ErrorMessage).ToList()
                });
            TempData["ErrorMessage"] = "Failed to add transaction";
        }
        return View("Index");
    }
}