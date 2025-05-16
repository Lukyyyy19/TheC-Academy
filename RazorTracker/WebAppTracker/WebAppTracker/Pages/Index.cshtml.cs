using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using WebAppTracker.Models;

namespace WebAppTracker.Pages;

public class IndexModel : PageModel
{
    private readonly IConfiguration _configuration;
    public List<CalorieModel> Records { get; set; }

    public IndexModel(IConfiguration configuration)
    {
        this._configuration = configuration;
    }

    public void OnGet()
    {
        Records = GetAllRecords();
    }

    private List<CalorieModel> GetAllRecords()
    {
        using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
        {
            connection.Open();
            var tableCmd = connection.CreateCommand();
            tableCmd.CommandText = "SELECT * FROM Calories";
            var tableData = new List<CalorieModel>();
            using (var reader = tableCmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    var record = new CalorieModel
                    {
                        Id = reader.GetInt32(0),
                        Date = reader.GetDateTime(1),
                        Quantity = reader.GetInt32(2)
                    };
                    tableData.Add(record);
                }
            }

            return tableData;
        }
    }
}