using BudgetApp.Data;
using BudgetApp.Interfaces;
using BudgetApp.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace BudgetApp.Controllers;

public class CategoryController:Controller
{
    private readonly ICategoryService _categoryService;
    private readonly ITransactionService _transactionService;
    public CategoryController(ICategoryService categoryService, ITransactionService transactionService)
    {
        _transactionService = transactionService;
        _categoryService = categoryService;
    }

    public async Task<IActionResult> Index()
    {
        return View(await GetCategoriesList());
    }
    private async Task<List<Category>> GetCategoriesList()
    {
        var categories = await _categoryService.GetAllCategories();
        foreach (var category in categories)
        {
            category.Transactions = await _transactionService.GetTransactions("",category);
        }
        return categories;
    }
    [HttpGet]
    public IActionResult Create()
    {
        ViewBag.Categories = _categoryService.GetAllCategories();
        return PartialView(new Category());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Name")] Category newCategory)
    {
        ModelState.Remove("Transactions");
        if (ModelState.IsValid)
        {
            var category = new Category
            {
                Name = newCategory.Name,
            };
            if (await _categoryService.AddCategoryAsync(category))
            {
                TempData["SuccessMessage"] = "Category created successfully!";
            }
        }
        else
        {
            TempData["ErrorMessage"] = "Failed to add category";
        }

        return View("Index", await GetCategoriesList());
    }
    
      public async Task<IActionResult> Edit(int id)
    {
        var category = await _categoryService.GetCategoryById(id);
        if (category != null)
        {
            return PartialView(category);
        }

        TempData["ErrorMessage"] = "Category not found";
        return RedirectToAction("Index", await GetCategoriesList());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit([Bind("Name,Id")] Category categoryViewModel)
    {
        ModelState.Remove("Transactions");
        if (ModelState.IsValid)
        {
            if (await _categoryService.UpdateCategoryAsync(categoryViewModel))
            {
                TempData["SuccessMessage"] = "Category updated successfully!";
            }
            else
            {
                TempData["ErrorMessage"] = "Failed to update category";
            }
        }
        else
        {
            TempData["ErrorMessage"] = "Failed to update category";
        }

        return RedirectToAction("Index", await GetCategoriesList());
    }

    public async Task<IActionResult> Delete(int? id)
    {
        int categoryId = id ?? 0;
        var category = await _categoryService.GetCategoryById(categoryId);
        if (category != null)
        {
            ModelState.Remove("Categories");
            var categoryViewModel = new Category
            {
                Name = category.Name
            };
            return PartialView(categoryViewModel);
        }

        TempData["ErrorMessage"] = "Category not found";
        return RedirectToAction("Index", await GetCategoriesList());
    }

    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> Delete(int id)
    {
        if (await _categoryService.DeleteCategoryAsync(id))
        {
            TempData["SuccessMessage"] = "Category deleted successfully!";
        }
        else
        {
            TempData["ErrorMessage"] = "Failed to delete category";
        }

        return RedirectToAction("Index", await GetCategoriesList());
    }
}