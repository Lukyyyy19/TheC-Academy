using BudgetApp.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BudgetApp.Interfaces;

public interface ICategoryService
{
    Task<List<Category>> GetAllCategories();
    Task<Category> GetCategoryById(int id);
}