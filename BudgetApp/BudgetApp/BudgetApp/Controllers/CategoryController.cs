using BudgetApp.Data;
using BudgetApp.Interfaces;
using BudgetApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BudgetApp.Controllers;

public class CategoryController
{
    private readonly ICategoryService _categoryService;
    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }
}