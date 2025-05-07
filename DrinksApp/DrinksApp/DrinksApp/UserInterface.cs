using Spectre.Console;

namespace DrinksApp;

public class UserInterface
{
    DrinksService drinksService = new DrinksService();

    public void DisplayDrinksCategorys()
    {
        var categories = drinksService.GetCategories();
        var category = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Select a [green]category[/]:")
                .PageSize(categories.Count)
                .AddChoices(categories.Select(c => c.strCategory).ToList())
        );
        DisplayDrinks(category);
    }

    private void DisplayDrinks(string category)
    {
        var drinks = drinksService.GetDrinks(category);
        var drinksList = drinks.Select(c => c.strDrink).ToList();
        drinksList.Add("[red]Go Back[/]");
        var drink = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Select a [green]drink[/]:")
                .PageSize(10)
                .AddChoices(drinksList)
        );
        if (drink == "[red]Go Back[/]")
        {
            DisplayDrinksCategorys();
            return;
        }
        DisplayDrinkDetails(drink);
    }

    private void DisplayDrinkDetails(string drink)
    {
        var drinkDetails = drinksService.GetDrinkDetails(drink);
        string category = "";
        Table table = new Table();
        table.AddColumn(drink).AddColumn("");
        foreach (var detail in drinkDetails)
        {
            table.AddRow(detail.Item1, detail.Item2.ToString());
            if (detail.Item1 == "Category")
            {
                category = detail.Item2.ToString();
            }
        }
        table.ShowRowSeparators();
        AnsiConsole.Write(table);
        AnsiConsole.MarkupLine("[green]Press any key to go back...[/]");
        Console.ReadKey();
        Console.Clear();
       DisplayDrinks(category);
    }
}