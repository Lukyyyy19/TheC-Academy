using Manager;
using Microsoft.EntityFrameworkCore;
using Models;
using Spectre.Console;
using Microsoft.Extensions.Configuration;

List<MenuOptions> menuOptions = new List<MenuOptions>()
{
    new MenuOptions() { id = 1, option = "Manage Stacks" },
    new MenuOptions() { id = 2, option = "Manage FlashCards" },
    new MenuOptions() { id = 3, option = "Study" },
    /// new MenuOptions() { id = 4, option = "View all stacks" },
    new MenuOptions() { id = 0, option = "Exit" }
};

var dbManager = new DataBaseManager("Server=(localdb)\\MSSQLLocalDB");
int currentStackId = 0;
bool running = true;

while (running)
{
    Console.Clear();
    var confirmation = AnsiConsole.Prompt(
        new SelectionPrompt<MenuOptions>()
            .Title("Select an option")
            .AddChoices(menuOptions)
            .UseConverter(x => x.option));
    switch (confirmation.id)
    {
        case 0:
            running = false;
            break;
        case 1:
            StackSection();
            break;
    }
}

void StackSection()
{
    while (true)
    {
        Console.Clear();
        var stacks = dbManager.GetStacks();
        var stackSelected = AnsiConsole.Prompt(new SelectionPrompt<Stack>()
            .Title("Select a stack")
            .AddChoices(stacks)
            .AddChoices(new Stack()
            {
                id = -1,
                name = "Go back"
            }).UseConverter(x => x.name));
        currentStackId = stackSelected.id;
        if (stackSelected.id == -1) return;
        var cards = dbManager.GetCards(stackSelected.id);
        Console.Clear();
        var table = new Table();
        table.Border = TableBorder.Rounded;
        table.Title = new TableTitle($"Stack: {stackSelected.name}");
        table.AddColumns("Id");
        table.AddColumn("Question");
        table.AddColumn("Answer");
        foreach (var card in cards)
        {
            table.AddRow($"{card.id}", $"[bold yellow]{card.question}[/]", $"[bold green]{card.answer}[/]");
        }

        AnsiConsole.Write(table);
        var menuOptions = new List<MenuOptions>()
        {
            new MenuOptions() { id = 1, option = "Add card" },
            new MenuOptions() { id = 2, option = "Edit card" },
            new MenuOptions() { id = 3, option = "Delete card" },
            new MenuOptions() { id = 0, option = "Go back" }
        };

        var confirmation = AnsiConsole.Prompt(
            new SelectionPrompt<MenuOptions>()
                .Title("Select an option")
                .AddChoices(menuOptions)
                .UseConverter(x => x.option));
        switch (confirmation.id)
        {
            case 0:
                break;
            case 1:
                AddCard();
                return;
            case 2:
                EditCard();
                return;
            case 3:
                DeleteCard();
                return;
        }
    }
}

void AddCard()
{
    AnsiConsole.MarkupLine("Add a card");
    var question = AnsiConsole.Ask<string>("Question: ");
    var answer = AnsiConsole.Ask<string>("Answer: ");
    dbManager.AddCard(new FlashCards()
    {
        question = question,
        answer = answer,
        stackId = currentStackId
    });
}

void EditCard()
{
    AnsiConsole.MarkupLine("Edit a card");
    
    var id = AnsiConsole.Ask<string>("Id of the card:");
    
    var question = AnsiConsole.Ask<string>("Question: ");
    var answer = AnsiConsole.Ask<string>("Answer: ");
    dbManager.EditCard(new FlashCards()
    {
        id = int.Parse(id),
        question = question,
        answer = answer,
        stackId = currentStackId
    });
}

void DeleteCard()
{
    AnsiConsole.MarkupLine("Delete a card");
    var id = AnsiConsole.Ask<string>("Id of the card:");
    dbManager.DeleteCard(int.Parse(id));
}