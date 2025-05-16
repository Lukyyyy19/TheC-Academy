using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using WebAppTracker.Models;

namespace WebAppTracker.Pages;

public class Update : PageModel
{
    private readonly IConfiguration _configuration;

    
    public Update(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public IActionResult OnGet()
    {
        return Page();
    }
    [BindProperty]
    public CalorieModel calorieModel { get; set; }
    public IActionResult OnPost(int id)
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
        {
            connection.Open();
            var tableCmd = connection.CreateCommand();
            tableCmd.CommandText =
                $@"UPDATE Calories
                    SET Date = '{calorieModel.Date}', Quantity = {calorieModel.Quantity}
                    WHERE Id = {id}";
            tableCmd.ExecuteNonQuery();
        }

        return RedirectToPage("./Index");
    }
}