using System.Web;
using DrinksApp.Models;
using Newtonsoft.Json;
using RestSharp;

namespace DrinksApp;

public class DrinksService
{
    public List<Category> GetCategories()
    {
        var client = new RestClient("http://www.thecocktaildb.com/api/json/v1/1/");
        var request = new RestRequest("list.php?c=list");
        var response = client.ExecuteAsync(request);
        List<Category> categories = new();
        if (response.Result.StatusCode == System.Net.HttpStatusCode.OK)
        {
            string rawRespone = response.Result.Content;
            var serialize = JsonConvert.DeserializeObject<Categories>(rawRespone);

            categories = serialize.CategoriesList;
            return categories;
        }

        return categories;
    }

    public List<Drink> GetDrinks(string drinkCategory)
    {
        var client = new RestClient("http://www.thecocktaildb.com/api/json/v1/1/");
        var request = new RestRequest($"filter.php?c={HttpUtility.UrlEncode(drinkCategory)}");
        var response = client.ExecuteAsync(request);
        List<Drink> drinks = new();
        if (response.Result.StatusCode == System.Net.HttpStatusCode.OK)
        {
            string rawRespone = response.Result.Content;
            var serialize = JsonConvert.DeserializeObject<Drinks>(rawRespone);

            drinks = serialize.DrinksList;
            return drinks;
        }

        return drinks;
    }

    public List<Tuple<string,object>> GetDrinkDetails(string drink)
    {
        var client = new RestClient("http://www.thecocktaildb.com/api/json/v1/1/");
        var request = new RestRequest($"search.php?s={HttpUtility.UrlEncode(drink)}");
        var response = client.ExecuteAsync(request);
        List<Tuple<string,object>> prepList = new();
        if (response.Result.StatusCode == System.Net.HttpStatusCode.OK)
        {
            DrinkDetail dr = new();
            string rawRespone = response.Result.Content;
            var serialize = JsonConvert.DeserializeObject<DrinkDetailObject>(rawRespone);
            dr = serialize.DrinkDetailList[0];
            string formattedName = "";
            foreach (var propertyInfo in dr.GetType().GetProperties())
            {
                if (propertyInfo.Name.Contains("str"))
                {
                    formattedName = propertyInfo.Name.Substring(3);
                }

                if (!string.IsNullOrEmpty(propertyInfo.GetValue(dr)?.ToString()))
                {
                    prepList.Add(new Tuple<string, object>(formattedName, propertyInfo.GetValue(dr)));
                }
            }

            return prepList;
        }

        return prepList;
    }
}