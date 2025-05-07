using Newtonsoft.Json;

namespace DrinksApp.Models;

public class Drink
{
    public string idDrink { get; set; }
    public string strDrink { get; set; }
}

public class Drinks
{
    [JsonProperty("drinks")] public List<Drink> DrinksList { get; set; }
}