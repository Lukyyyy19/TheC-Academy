using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using WebAppTracker.Models;

namespace WebAppTracker.Pages;

public class Delete : PageModel
{
    private readonly IConfiguration _configuration;
    [BindProperty]
    public CalorieModel calorieModel { get; set; }
    
    public Delete(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public void OnGet(int id)
    {
        calorieModel = GetRecord(id);
    }

    public CalorieModel GetRecord(int id)
    {
        using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
        {
            connection.Open();
            var tableCmd = connection.CreateCommand();
            tableCmd.CommandText = $"SELECT * FROM Calories WHERE Id = {id}";
            CalorieModel record = null;
            using (var reader = tableCmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    record = new CalorieModel
                    {
                        Id = reader.GetInt32(0),
                        Date = reader.GetDateTime(1),
                        Quantity = reader.GetInt32(2)
                    };
                }
            }

            return record;
        }
    }

    public IActionResult OnPost()
    {
        Console.WriteLine(calorieModel.Id);
        if(!ModelState.IsValid)
        {
            return Page();
        }
        using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
        {
            connection.Open();
            var tableCmd = connection.CreateCommand();
            tableCmd.CommandText = $"DELETE FROM Calories WHERE Id = {calorieModel.Id}";
            tableCmd.ExecuteNonQuery();
        }
        return RedirectToPage("./Index");
    }
}