using System.ComponentModel.DataAnnotations;

namespace WebAppTracker.Models;

public class CalorieModel
{
    public int Id {get; set;}
    [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}",ApplyFormatInEditMode = true)]
    public DateTime Date {get; set;}
    public int Quantity {get; set;}
}