using BudgetApp.Data;
using BudgetApp.Interfaces;
using BudgetApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace BudgetApp.Controllers;

public class TransactionsController : Controller
{
    private readonly BudgetDbContext _context;
    private readonly ICategoryService _categoryService;
    public TransactionsController(BudgetDbContext context, ICategoryService categoryService)
    {
        _categoryService = categoryService;
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
        Console.WriteLine("asdnafkanfanf");
        return PartialView("Create");
    }

    [HttpPost]
    public async Task<IActionResult> Create(TransactionViewModel newTransactionVM)
    {
        var newTransaction = newTransactionVM.Transaction;
        var transaction = new Transaction
        {
            Description = newTransaction.Description,
            Amount = newTransaction.Amount,
            Date = newTransaction.Date,
            CategoryId = newTransaction.CategoryId
        };
        Console.WriteLine("La categoria es: "+transaction.CategoryId);
        
        await _context.Transactions.AddAsync(transaction);
        await _context.SaveChangesAsync();
        return View("Index");
    }
}