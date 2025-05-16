using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.SqlServer.Storage.Internal;
using WebAppTracker.Models;

namespace WebAppTracker.Pages;

public class Create : PageModel
{
    private readonly IConfiguration _configuration;

    public Create(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    public IActionResult OnGet()
    {
        return Page();
    }
    
    [BindProperty]
    public CalorieModel CalorieModel { get; set; }

    public IActionResult OnPost()
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
                $@"INSERT INTO Calories(Date,Quantity)
                    VALUES ('{CalorieModel.Date}', {CalorieModel.Quantity})";
            tableCmd.ExecuteNonQuery();
        }

        return RedirectToPage("./Index");
    }
}